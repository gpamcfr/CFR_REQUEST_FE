using log4net;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class SapBL
    {
        public static Company oCompany = (Company)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("632F4591-AA62-4219-8FB6-22BCF5F60100")));

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static bool conexion(string servidor, string tiposervidor, string bdusuario, string bdclave, string sapusuario, string sapclave, string bd, string licenceserver)
        {
            if (!oCompany.Connected)
            {
                oCompany.Server = servidor;
                oCompany.SLDServer = servidor+":40000";
               oCompany.language = BoSuppLangs.ln_Spanish;
                switch (tiposervidor)
                {
                    case "MSSQL2008":
                        oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2008;
                        break;
                    case "MSSQL2012":
                        oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2012;
                        break;
                    case "MSSQL2014":
                        oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2014;
                        break;
                    case "MSSQL2016":
                        oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2016;
                        break;
                    case "MSSQL2019":
                        oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2019;
                        break;
                    case "HANADB":
                        oCompany.DbServerType = BoDataServerTypes.dst_HANADB;
                        break;
                }
                oCompany.UseTrusted = false;
                oCompany.DbUserName = bdusuario;
                oCompany.DbPassword = bdclave;
                oCompany.CompanyDB = bd;
                oCompany.UserName = sapusuario;
                oCompany.Password = sapclave;
                if (!string.IsNullOrEmpty(licenceserver))
                {
                    oCompany.LicenseServer = licenceserver;
                }
                int result = -1;
                try
                {
                    result = oCompany.Connect();
                }
                catch (Exception ex)
                {
                    log.Debug($"Se encontró el siguiente ERROR : {ex.Message} ");
                    return false;
                }
                if (result != 0)
                {
                    string error = oCompany.GetLastErrorCode() + " :: " + oCompany.GetLastErrorDescription();
                    log.Debug($"Se encontró el siguiente ERROR : {error}");
                    return false;
                }
                log.Debug($"Se procedió a conectar a : {oCompany.CompanyName}");
                return true;
            }
            log.Debug("Ya se encuentra conectado.");
            return true;
        }

        public static void desconectar()
        {
            try
            {
                if (oCompany.Connected)
                {
                    oCompany.Disconnect();
                }
            }
            catch (Exception ex)
            {
                log.Debug($"Se encontró el siguiente ERROR : {ex.Message} ");
            }
        }

        public static bool actualizarEstadoSAP(string estado, int docentry, int objtype, string tipoDocumneto, string mensaje, string estadoFacturacion, string respuestaSunat, string actualizacionNativa)
        {
            if (actualizacionNativa.Equals("Y"))
            {
                Documents oDoc = null;
                Payments oPay = null;
                StockTransfer oTrans = null;
                int iRetVal = -1;
                string sErrMsg = string.Empty;
                try
                {
                    switch (objtype)
                    {
                        case 46:
                            oPay = (Payments)(dynamic)oCompany.GetBusinessObject((BoObjectTypes)objtype);
                            if (oPay.GetByKey(docentry))
                            {
                                oPay.UserFields.Fields.Item(estadoFacturacion).Value = estado;
                                oPay.UserFields.Fields.Item(respuestaSunat).Value = mensaje;
                                if (oPay.Update() != 0)
                                {
                                    string error3 = oCompany.GetLastErrorCode() + " :: " + oCompany.GetLastErrorDescription();
                                    log.Debug($"Se encontró el siguiente ERROR : {error3}");
                                    return false;
                                }
                                return true;
                            }
                            return false;
                        default:
                            oDoc = (Documents)(dynamic)oCompany.GetBusinessObject((BoObjectTypes)objtype);
                            if (oDoc.GetByKey(docentry))
                            {
                                oDoc.UserFields.Fields.Item(estadoFacturacion).Value = estado;
                                oDoc.UserFields.Fields.Item(respuestaSunat).Value = mensaje;
                                if (oDoc.Update() != 0)
                                {
                                    string error2 = oCompany.GetLastErrorCode() + " :: " + oCompany.GetLastErrorDescription();
                                    log.Debug($"Se encontró el siguiente ERROR : {error2}");
                                    return false;
                                }
                                return true;
                            }
                            return false;
                        case 67:
                            oTrans = (dynamic)oCompany.GetBusinessObject(BoObjectTypes.oStockTransfer);
                            if (oTrans.GetByKey(docentry))
                            {
                                oTrans.UserFields.Fields.Item(estadoFacturacion).Value = estado;
                                oTrans.UserFields.Fields.Item(respuestaSunat).Value = mensaje;
                                if (oTrans.Update() != 0)
                                {
                                    string error = oCompany.GetLastErrorCode() + " :: " + oCompany.GetLastErrorDescription();
                                    log.Debug($"Se encontró el siguiente ERROR : {error}");
                                    return false;
                                }
                                return true;
                            }
                            return false;
                    }
                }
                catch (Exception ex2)
                {
                    log.Debug($"Se encontró el siguiente ERROR : {ex2.Message} ");
                    return false;
                }
                finally
                {
                    switch (objtype)
                    {
                        case 46:
                            Marshal.ReleaseComObject(oPay);
                            oPay = null;
                            break;
                        default:
                            Marshal.ReleaseComObject(oDoc);
                            oDoc = null;
                            break;
                        case 67:
                            Marshal.ReleaseComObject(oTrans);
                            oTrans = null;
                            break;
                    }
                    GC.Collect();
                }
            }
            try
            {
                mensaje = mensaje.Replace("'", " ");
                string sQueryUpdate = string.Empty;
                switch (objtype)
                {
                    case 46:
                        sQueryUpdate = $"UPDATE OVPM SET {estadoFacturacion}='{estado}',{respuestaSunat}='{mensaje}' WHERE \"DocEntry\"='{Convert.ToInt32(docentry)}'";
                        break;
                    case 13:
                        sQueryUpdate = $"UPDATE OINV SET {estadoFacturacion}='{estado}',{respuestaSunat}='{mensaje}' WHERE \"DocEntry\"='{Convert.ToInt32(docentry)}'";
                        break;
                    case 14:
                        sQueryUpdate = $"UPDATE ORIN SET {estadoFacturacion}='{estado}',{respuestaSunat}='{mensaje}' WHERE \"DocEntry\"='{Convert.ToInt32(docentry)}'";
                        break;
                    case 15:
                        sQueryUpdate = $"UPDATE ODLN SET {estadoFacturacion}='{estado}',{respuestaSunat}='{mensaje}' WHERE \"DocEntry\"='{Convert.ToInt32(docentry)}'";
                        break;
                    case 60:
                        sQueryUpdate = $"UPDATE OIGE SET {estadoFacturacion}='{estado}',{respuestaSunat}='{mensaje}' WHERE \"DocEntry\"='{Convert.ToInt32(docentry)}'";
                        break;
                    case 16:
                        sQueryUpdate = $"UPDATE ORDN SET {estadoFacturacion}='{estado}',{respuestaSunat}='{mensaje}' WHERE \"DocEntry\"='{Convert.ToInt32(docentry)}'";
                        break;
                    case 20:
                        sQueryUpdate = $"UPDATE OPDN SET {estadoFacturacion}='{estado}',{respuestaSunat}='{mensaje}' WHERE \"DocEntry\"='{Convert.ToInt32(docentry)}'";
                        break;
                    case 203:
                        sQueryUpdate = $"UPDATE ODPI SET {estadoFacturacion}='{estado}',{respuestaSunat}='{mensaje}' WHERE \"DocEntry\"='{Convert.ToInt32(docentry)}'";
                        break;
                }
                Recordset oRecordSet = (Recordset)(dynamic)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                log.Debug($"Se procedió a ejecutar el siguiente SP :: {sQueryUpdate}");
                oRecordSet.DoQuery(sQueryUpdate);
                return true;
            }
            catch (Exception ex)
            {
                log.Debug($"Se encontró el siguiente ERROR : {ex.Message} ");
                return false;
            }
        }

        public static bool actualizarEstadoSAPRespuestSUNAT(int docentry, int objtype, string actualizacionNativa, string sCampoEstado, string sValorEstado, string sCampoRespuestaCDR, string sValorRespuestaCDR)
        {
            if (actualizacionNativa.Equals("Y"))
            {
                Documents oDoc = null;
                Payments oPay = null;
                StockTransfer oTrans = null;
                int iRetVal = -1;
                string sErrMsg = string.Empty;
                try
                {
                    switch (objtype)
                    {
                        case 46:
                            oPay = (Payments)(dynamic)oCompany.GetBusinessObject((BoObjectTypes)objtype);
                            if (oPay.GetByKey(docentry))
                            {
                                if (oPay.Update() != 0)
                                {
                                    string error3 = oCompany.GetLastErrorCode() + " :: " + oCompany.GetLastErrorDescription();
                                    log.Debug($"Se encontró el siguiente ERROR : {error3}");
                                    return false;
                                }
                                return true;
                            }
                            return false;
                        default:
                            oDoc = (Documents)(dynamic)oCompany.GetBusinessObject((BoObjectTypes)objtype);
                            if (oDoc.GetByKey(docentry))
                            {
                                oDoc.UserFields.Fields.Item(sCampoEstado).Value = sValorEstado;
                                oDoc.UserFields.Fields.Item(sCampoRespuestaCDR).Value = sValorRespuestaCDR;
                                if (oDoc.Update() != 0)
                                {
                                    string error2 = oCompany.GetLastErrorCode() + " :: " + oCompany.GetLastErrorDescription();
                                    log.Debug($"Se encontró el siguiente ERROR : {error2}");
                                    return false;
                                }
                                return true;
                            }
                            return false;
                        case 67:
                            oTrans = (dynamic)oCompany.GetBusinessObject(BoObjectTypes.oStockTransfer);
                            if (oTrans.GetByKey(docentry))
                            {
                                if (oTrans.Update() != 0)
                                {
                                    string error = oCompany.GetLastErrorCode() + " :: " + oCompany.GetLastErrorDescription();
                                    log.Debug($"Se encontró el siguiente ERROR : {error}");
                                    return false;
                                }
                                return true;
                            }
                            return false;
                    }
                }
                catch (Exception ex2)
                {
                    log.Debug($"Se encontró el siguiente ERROR : {ex2.Message} ");
                    return false;
                }
                finally
                {
                    switch (objtype)
                    {
                        case 46:
                            Marshal.ReleaseComObject(oPay);
                            oPay = null;
                            break;
                        default:
                            Marshal.ReleaseComObject(oDoc);
                            oDoc = null;
                            break;
                        case 67:
                            Marshal.ReleaseComObject(oTrans);
                            oTrans = null;
                            break;
                    }
                    GC.Collect();
                }
            }
            try
            {
                sValorRespuestaCDR = sValorRespuestaCDR.Replace("'", "");
                string sQueryUpdate = string.Empty;
                switch (objtype)
                {
                    case 46:
                        sQueryUpdate = $"UPDATE OVPM SET {sCampoEstado}='{sValorEstado}',{sCampoRespuestaCDR}='{sValorRespuestaCDR}' WHERE \"DocEntry\"='{Convert.ToInt32(docentry)}'";
                        break;
                    case 13:
                        sQueryUpdate = $"UPDATE OINV SET {sCampoEstado}='{sValorEstado}',{sCampoRespuestaCDR}='{sValorRespuestaCDR}' WHERE \"DocEntry\"='{Convert.ToInt32(docentry)}'";
                        break;
                    case 14:
                        sQueryUpdate = $"UPDATE ORIN SET {sCampoEstado}='{sValorEstado}',{sCampoRespuestaCDR}='{sValorRespuestaCDR}' WHERE \"DocEntry\"='{Convert.ToInt32(docentry)}'";
                        break;
                    case 15:
                        sQueryUpdate = $"UPDATE ODLN SET {sCampoEstado}='{sValorEstado}',{sCampoRespuestaCDR}='{sValorRespuestaCDR}' WHERE \"DocEntry\"='{Convert.ToInt32(docentry)}'";
                        break;
                    case 60:
                        sQueryUpdate = $"UPDATE OIGE SET {sCampoEstado}='{sValorEstado}',{sCampoRespuestaCDR}='{sValorRespuestaCDR}' WHERE \"DocEntry\"='{Convert.ToInt32(docentry)}'";
                        break;
                    case 16:
                        sQueryUpdate = $"UPDATE ORDN SET {sCampoEstado}='{sValorEstado}',{sCampoRespuestaCDR}='{sValorRespuestaCDR}' WHERE \"DocEntry\"='{Convert.ToInt32(docentry)}'";
                        break;
                    case 20:
                        sQueryUpdate = $"UPDATE OPDN SET {sCampoEstado}='{sValorEstado}',{sCampoRespuestaCDR}='{sValorRespuestaCDR}' WHERE \"DocEntry\"='{Convert.ToInt32(docentry)}'";
                        break;
                    case 203:
                        sQueryUpdate = $"UPDATE ODPI SET {sCampoEstado}='{sValorEstado}',{sCampoRespuestaCDR}='{sValorRespuestaCDR}' WHERE \"DocEntry\"='{Convert.ToInt32(docentry)}'";
                        break;
                }
                Recordset oRecordSet = (Recordset)(dynamic)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                log.Debug($"Se procedió a ejecutar el siguiente SP :: {sQueryUpdate}");
                oRecordSet.DoQuery(sQueryUpdate);
                return true;
            }
            catch (Exception ex)
            {
                log.Debug($"Se encontró el siguiente ERROR : {ex.Message} ");
                return false;
            }
        }

        public static bool actualizarEstadoSAP_V2(string estado, int docentry, int objtype, string tipoDocumneto, string mensaje, string estadoFacturacion, string respuestaSunat, string actualizacionNativa, string campoTicket, string sTicket)
        {
            if (actualizacionNativa.Equals("Y"))
            {
                Documents oDoc = null;
                Payments oPay = null;
                StockTransfer oTrans = null;
                int iRetVal = -1;
                string sErrMsg = string.Empty;
                try
                {
                    switch (objtype)
                    {
                        case 15:
                            oDoc = (Documents)(dynamic)oCompany.GetBusinessObject((BoObjectTypes)objtype);
                            if (oDoc.GetByKey(docentry))
                            {
                                oDoc.UserFields.Fields.Item(estadoFacturacion).Value = estado;
                                oDoc.UserFields.Fields.Item(respuestaSunat).Value = mensaje;
                                oDoc.UserFields.Fields.Item(campoTicket).Value = sTicket;
                                if (oDoc.Update() != 0)
                                {
                                    string error4 = oCompany.GetLastErrorCode() + " :: " + oCompany.GetLastErrorDescription();
                                    log.Debug($"Se encontró el siguiente ERROR : {error4}");
                                    return false;
                                }
                                return true;
                            }
                            return false;
                        case 46:
                            oPay = (Payments)(dynamic)oCompany.GetBusinessObject((BoObjectTypes)objtype);
                            if (oPay.GetByKey(docentry))
                            {
                                oPay.UserFields.Fields.Item(estadoFacturacion).Value = estado;
                                oPay.UserFields.Fields.Item(respuestaSunat).Value = mensaje;
                                if (oPay.Update() != 0)
                                {
                                    string error3 = oCompany.GetLastErrorCode() + " :: " + oCompany.GetLastErrorDescription();
                                    log.Debug($"Se encontró el siguiente ERROR : {error3}");
                                    return false;
                                }
                                return true;
                            }
                            return false;
                        default:
                            oDoc = (Documents)(dynamic)oCompany.GetBusinessObject((BoObjectTypes)objtype);
                            if (oDoc.GetByKey(docentry))
                            {
                                oDoc.UserFields.Fields.Item(estadoFacturacion).Value = estado;
                                oDoc.UserFields.Fields.Item(respuestaSunat).Value = mensaje;
                                oDoc.UserFields.Fields.Item(campoTicket).Value = sTicket;
                                if (oDoc.Update() != 0)
                                {
                                    string error2 = oCompany.GetLastErrorCode() + " :: " + oCompany.GetLastErrorDescription();
                                    log.Debug($"Se encontró el siguiente ERROR : {error2}");
                                    return false;
                                }
                                return true;
                            }
                            return false;
                        case 67:
                            oTrans = (dynamic)oCompany.GetBusinessObject(BoObjectTypes.oStockTransfer);
                            if (oTrans.GetByKey(docentry))
                            {
                                oTrans.UserFields.Fields.Item(estadoFacturacion).Value = estado;
                                oTrans.UserFields.Fields.Item(respuestaSunat).Value = mensaje;
                                if (oTrans.Update() != 0)
                                {
                                    string error = oCompany.GetLastErrorCode() + " :: " + oCompany.GetLastErrorDescription();
                                    log.Debug($"Se encontró el siguiente ERROR : {error}");
                                    return false;
                                }
                                return true;
                            }
                            return false;
                    }
                }
                catch (Exception ex2)
                {
                    log.Debug($"Se encontró el siguiente ERROR : {ex2.Message} ");
                    return false;
                }
                finally
                {
                    switch (objtype)
                    {
                        case 46:
                            Marshal.ReleaseComObject(oPay);
                            oPay = null;
                            break;
                        default:
                            Marshal.ReleaseComObject(oDoc);
                            oDoc = null;
                            break;
                        case 67:
                            Marshal.ReleaseComObject(oTrans);
                            oTrans = null;
                            break;
                    }
                    GC.Collect();
                }
            }
            try
            {
                mensaje = mensaje.Replace("'", " ");
                string sQueryUpdate = string.Empty;
                switch (objtype)
                {
                    case 67:
                        sQueryUpdate = string.Format("UPDATE OWTR SET {0}='{1}',{2}='{3}',{5}='{6}' WHERE \"DocEntry\"='{4}'", estadoFacturacion, estado, respuestaSunat, mensaje, Convert.ToInt32(docentry), campoTicket, sTicket);
                        break;
                    case 46:
                        sQueryUpdate = string.Format("UPDATE OVPM SET {0}='{1}',{2}='{3}',{5}='{6}' WHERE \"DocEntry\"='{4}'", estadoFacturacion, estado, respuestaSunat, mensaje, Convert.ToInt32(docentry), campoTicket, sTicket);
                        break;
                    case 13:
                        sQueryUpdate = string.Format("UPDATE OINV SET {0}='{1}',{2}='{3}',{5}='{6}' WHERE \"DocEntry\"='{4}'", estadoFacturacion, estado, respuestaSunat, mensaje, Convert.ToInt32(docentry), campoTicket, sTicket);
                        break;
                    case 14:
                        sQueryUpdate = string.Format("UPDATE ORIN SET {0}='{1}',{2}='{3}',{5}='{6}' WHERE \"DocEntry\"='{4}'", estadoFacturacion, estado, respuestaSunat, mensaje, Convert.ToInt32(docentry), campoTicket, sTicket);
                        break;
                    case 15:
                        sQueryUpdate = string.Format("UPDATE ODLN SET {0}='{1}',{2}='{3}',{5}='{6}' WHERE \"DocEntry\"='{4}'", estadoFacturacion, estado, respuestaSunat, mensaje, Convert.ToInt32(docentry), campoTicket, sTicket);
                        break;
                    case 60:
                        sQueryUpdate = string.Format("UPDATE OIGE SET {0}='{1}',{2}='{3}',{5}='{6}' WHERE \"DocEntry\"='{4}'", estadoFacturacion, estado, respuestaSunat, mensaje, Convert.ToInt32(docentry), campoTicket, sTicket);
                        break;
                    case 16:
                        sQueryUpdate = string.Format("UPDATE ORDN SET {0}='{1}',{2}='{3}',{5}='{6}' WHERE \"DocEntry\"='{4}'", estadoFacturacion, estado, respuestaSunat, mensaje, Convert.ToInt32(docentry), campoTicket, sTicket);
                        break;
                    case 20:
                        sQueryUpdate = string.Format("UPDATE OPDN SET {0}='{1}',{2}='{3}',{5}='{6}' WHERE \"DocEntry\"='{4}'", estadoFacturacion, estado, respuestaSunat, mensaje, Convert.ToInt32(docentry), campoTicket, sTicket);
                        break;
                    case 203:
                        sQueryUpdate = string.Format("UPDATE ODPI SET {0}='{1}',{2}='{3}',{5}='{6}' WHERE \"DocEntry\"='{4}'", estadoFacturacion, estado, respuestaSunat, mensaje, Convert.ToInt32(docentry), campoTicket, sTicket);
                        break;
                }
                Recordset oRecordSet = (Recordset)(dynamic)oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                log.Debug($"Se procedió a ejecutar el siguiente SP :: {sQueryUpdate}");
                oRecordSet.DoQuery(sQueryUpdate);
                return true;
            }
            catch (Exception ex)
            {
                log.Debug($"Se encontró el siguiente ERROR : {ex.Message} ");
                return false;
            }
        }

        public static bool AgregarAnexo(int docentry, int objtype, string fileName)
        {
            try
            {
                log.Debug("--------------------------------------------------------------");
                Documents oDoc = null;
                Payments oPay = null;
                StockTransfer oTrans = null;
                switch (objtype)
                {
                    case 46:
                        {
                            log.Debug("--------------------------------------------------------------");
                            oPay = (Payments)(dynamic)oCompany.GetBusinessObject((BoObjectTypes)objtype);
                            log.Debug("--------------------------------------------------------------");
                            Attachments2 oAtt = (dynamic)oCompany.GetBusinessObject(BoObjectTypes.oAttachments2);
                            log.Debug("--------------------------------------------------------------");
                            if (!oDoc.GetByKey(docentry))
                            {
                                break;
                            }
                            log.Debug("--------------------------------------------------------------");
                            if (!oAtt.GetByKey(oDoc.AttachmentEntry))
                            {
                                log.Debug("--------------------------------------------------------------");
                                oAtt.Lines.Add();
                                log.Debug("--------------------------------------------------------------");
                                oAtt.Lines.FileName = Path.GetFileNameWithoutExtension(fileName);
                                log.Debug("--------------------------------------------------------------");
                                oAtt.Lines.FileExtension = Path.GetExtension(fileName).Substring(1);
                                log.Debug("--------------------------------------------------------------");
                                oAtt.Lines.SourcePath = Path.GetDirectoryName(fileName);
                                log.Debug("--------------------------------------------------------------");
                                oAtt.Lines.Override = BoYesNoEnum.tYES;
                                log.Debug("--------------------------------------------------------------");
                                int iResponse2 = oAtt.Add();
                                log.Debug("--------------------------------------------------------------");
                                if (iResponse2 != 0)
                                {
                                    string error7 = oCompany.GetLastErrorCode() + " :: " + oCompany.GetLastErrorDescription();
                                    log.Debug($"Se encontró el siguiente ERROR : {error7}");
                                    return false;
                                }
                                log.Debug("--------------------------------------------------------------");
                                int AttEntry4 = int.Parse(oCompany.GetNewObjectKey());
                                log.Debug("--------------------------------------------------------------");
                                oDoc.AttachmentEntry = AttEntry4;
                                log.Debug("--------------------------------------------------------------");
                                if (oDoc.Update() != 0)
                                {
                                    string error10 = oCompany.GetLastErrorCode() + " :: " + oCompany.GetLastErrorDescription();
                                    log.Debug($"Se encontró el siguiente ERROR : {error10}");
                                }
                                else
                                {
                                    log.Debug("Se agrego correctamente los anexos");
                                }
                                break;
                            }
                            log.Debug("--------------------------------------------------------------");
                            bool encontroAnexo2 = false;
                            log.Debug("--------------------------------------------------------------");
                            for (int j = 0; j < oAtt.Lines.Count; j++)
                            {
                                log.Debug("--------------------------------------------------------------");
                                oAtt.Lines.SetCurrentLine(j);
                                log.Debug("--------------------------------------------------------------");
                                if (oAtt.Lines.FileName.Equals(Path.GetFileNameWithoutExtension(fileName)))
                                {
                                    log.Debug("--------------------------------------------------------------");
                                    oAtt.Lines.FileName = Path.GetFileNameWithoutExtension(fileName);
                                    log.Debug("--------------------------------------------------------------");
                                    oAtt.Lines.FileExtension = Path.GetExtension(fileName).Substring(1);
                                    log.Debug("--------------------------------------------------------------");
                                    oAtt.Lines.SourcePath = Path.GetDirectoryName(fileName);
                                    log.Debug("--------------------------------------------------------------");
                                    oAtt.Lines.Override = BoYesNoEnum.tYES;
                                    log.Debug("--------------------------------------------------------------");
                                    int iResponse6 = oAtt.Update();
                                    log.Debug("--------------------------------------------------------------");
                                    if (iResponse6 != 0)
                                    {
                                        string error18 = oCompany.GetLastErrorCode() + " :: " + oCompany.GetLastErrorDescription();
                                        log.Debug($"Se encontró el siguiente ERROR : {error18}");
                                        return false;
                                    }
                                    log.Debug("--------------------------------------------------------------");
                                    int AttEntry9 = int.Parse(oCompany.GetNewObjectKey());
                                    log.Debug("--------------------------------------------------------------");
                                    oDoc.AttachmentEntry = AttEntry9;
                                    log.Debug("--------------------------------------------------------------");
                                    if (oDoc.Update() != 0)
                                    {
                                        string error17 = oCompany.GetLastErrorCode() + " :: " + oCompany.GetLastErrorDescription();
                                        log.Debug($"Se encontró el siguiente ERROR : {error17}");
                                    }
                                    else
                                    {
                                        log.Debug("Se agrego correctamente los anexos");
                                    }
                                    encontroAnexo2 = true;
                                }
                            }
                            if (!encontroAnexo2)
                            {
                                log.Debug("--------------------------------------------------------------");
                                oAtt.Lines.Add();
                                log.Debug("--------------------------------------------------------------");
                                oAtt.Lines.FileName = Path.GetFileNameWithoutExtension(fileName);
                                log.Debug("--------------------------------------------------------------");
                                oAtt.Lines.FileExtension = Path.GetExtension(fileName).Substring(1);
                                log.Debug("--------------------------------------------------------------");
                                oAtt.Lines.SourcePath = Path.GetDirectoryName(fileName);
                                log.Debug("--------------------------------------------------------------");
                                oAtt.Lines.Override = BoYesNoEnum.tYES;
                                log.Debug("--------------------------------------------------------------");
                                if (oAtt.Update() != 0)
                                {
                                    string error13 = oCompany.GetLastErrorCode() + " :: " + oCompany.GetLastErrorDescription();
                                    log.Debug($"Se encontró el siguiente ERROR : {error13}");
                                    return false;
                                }
                                log.Debug("--------------------------------------------------------------");
                                int AttEntry7 = int.Parse(oCompany.GetNewObjectKey());
                                log.Debug("--------------------------------------------------------------");
                                oDoc.AttachmentEntry = AttEntry7;
                                log.Debug("--------------------------------------------------------------");
                                if (oDoc.Update() != 0)
                                {
                                    string error16 = oCompany.GetLastErrorCode() + " :: " + oCompany.GetLastErrorDescription();
                                    log.Debug($"Se encontró el siguiente ERROR : {error16}");
                                }
                                else
                                {
                                    log.Debug("Se agrego correctamente los anexos");
                                }
                                log.Debug("Se agrego correctamente los anexos");
                            }
                            break;
                        }
                    default:
                        {
                            log.Debug("--------------------------------------------------------------");
                            oDoc = (Documents)(dynamic)oCompany.GetBusinessObject((BoObjectTypes)objtype);
                            log.Debug("--------------------------------------------------------------");
                            Attachments2 oAtt3 = (dynamic)oCompany.GetBusinessObject(BoObjectTypes.oAttachments2);
                            log.Debug("--------------------------------------------------------------");
                            if (!oDoc.GetByKey(docentry))
                            {
                                break;
                            }
                            log.Debug("--------------------------------------------------------------");
                            if (!oAtt3.GetByKey(oDoc.AttachmentEntry))
                            {
                                log.Debug("--------------------------------------------------------------");
                                oAtt3.Lines.Add();
                                log.Debug("--------------------------------------------------------------");
                                oAtt3.Lines.FileName = Path.GetFileNameWithoutExtension(fileName);
                                log.Debug("--------------------------------------------------------------");
                                oAtt3.Lines.FileExtension = Path.GetExtension(fileName).Substring(1);
                                log.Debug("--------------------------------------------------------------");
                                oAtt3.Lines.SourcePath = Path.GetDirectoryName(fileName);
                                log.Debug("--------------------------------------------------------------");
                                oAtt3.Lines.Override = BoYesNoEnum.tYES;
                                log.Debug("--------------------------------------------------------------");
                                int iResponse5 = oAtt3.Add();
                                log.Debug("--------------------------------------------------------------");
                                if (iResponse5 != 0)
                                {
                                    string error15 = oCompany.GetLastErrorCode() + " :: " + oCompany.GetLastErrorDescription();
                                    log.Debug($"Se encontró el siguiente ERROR : {error15}");
                                    return false;
                                }
                                log.Debug("--------------------------------------------------------------");
                                int AttEntry8 = int.Parse(oCompany.GetNewObjectKey());
                                log.Debug("--------------------------------------------------------------");
                                oDoc.AttachmentEntry = AttEntry8;
                                log.Debug("--------------------------------------------------------------");
                                if (oDoc.Update() != 0)
                                {
                                    string error14 = oCompany.GetLastErrorCode() + " :: " + oCompany.GetLastErrorDescription();
                                    log.Debug($"Se encontró el siguiente ERROR : {error14}");
                                }
                                else
                                {
                                    log.Debug("Se agrego correctamente los anexos");
                                }
                                break;
                            }
                            log.Debug("--------------------------------------------------------------");
                            bool encontroAnexo3 = false;
                            log.Debug("--------------------------------------------------------------");
                            for (int k = 0; k < oAtt3.Lines.Count; k++)
                            {
                                log.Debug("--------------------------------------------------------------");
                                oAtt3.Lines.SetCurrentLine(k);
                                log.Debug("--------------------------------------------------------------");
                                if (oAtt3.Lines.FileName.Equals(Path.GetFileNameWithoutExtension(fileName)))
                                {
                                    log.Debug("--------------------------------------------------------------");
                                    oAtt3.Lines.FileName = Path.GetFileNameWithoutExtension(fileName);
                                    log.Debug("--------------------------------------------------------------");
                                    oAtt3.Lines.FileExtension = Path.GetExtension(fileName).Substring(1);
                                    log.Debug("--------------------------------------------------------------");
                                    oAtt3.Lines.SourcePath = Path.GetDirectoryName(fileName);
                                    log.Debug("--------------------------------------------------------------");
                                    oAtt3.Lines.Override = BoYesNoEnum.tYES;
                                    log.Debug("--------------------------------------------------------------");
                                    int iResponse4 = oAtt3.Update();
                                    log.Debug("--------------------------------------------------------------");
                                    if (iResponse4 != 0)
                                    {
                                        string error9 = oCompany.GetLastErrorCode() + " :: " + oCompany.GetLastErrorDescription();
                                        log.Debug($"Se encontró el siguiente ERROR : {error9}");
                                        return false;
                                    }
                                    log.Debug("--------------------------------------------------------------");
                                    int AttEntry5 = int.Parse(oCompany.GetNewObjectKey());
                                    log.Debug("--------------------------------------------------------------");
                                    oDoc.AttachmentEntry = AttEntry5;
                                    log.Debug("--------------------------------------------------------------");
                                    if (oDoc.Update() != 0)
                                    {
                                        string error8 = oCompany.GetLastErrorCode() + " :: " + oCompany.GetLastErrorDescription();
                                        log.Debug($"Se encontró el siguiente ERROR : {error8}");
                                    }
                                    else
                                    {
                                        log.Debug("Se agrego correctamente los anexos");
                                    }
                                    encontroAnexo3 = true;
                                }
                            }
                            if (!encontroAnexo3)
                            {
                                log.Debug("--------------------------------------------------------------");
                                oAtt3.Lines.Add();
                                log.Debug("--------------------------------------------------------------");
                                oAtt3.Lines.FileName = Path.GetFileNameWithoutExtension(fileName);
                                log.Debug("--------------------------------------------------------------");
                                oAtt3.Lines.FileExtension = Path.GetExtension(fileName).Substring(1);
                                log.Debug("--------------------------------------------------------------");
                                oAtt3.Lines.SourcePath = Path.GetDirectoryName(fileName);
                                log.Debug("--------------------------------------------------------------");
                                oAtt3.Lines.Override = BoYesNoEnum.tYES;
                                log.Debug("--------------------------------------------------------------");
                                if (oAtt3.Update() != 0)
                                {
                                    string error12 = oCompany.GetLastErrorCode() + " :: " + oCompany.GetLastErrorDescription();
                                    log.Debug($"Se encontró el siguiente ERROR : {error12}");
                                    return false;
                                }
                                log.Debug("--------------------------------------------------------------");
                                int AttEntry6 = int.Parse(oCompany.GetNewObjectKey());
                                log.Debug("--------------------------------------------------------------");
                                oDoc.AttachmentEntry = AttEntry6;
                                log.Debug("--------------------------------------------------------------");
                                if (oDoc.Update() != 0)
                                {
                                    string error11 = oCompany.GetLastErrorCode() + " :: " + oCompany.GetLastErrorDescription();
                                    log.Debug($"Se encontró el siguiente ERROR : {error11}");
                                }
                                else
                                {
                                    log.Debug("Se agrego correctamente los anexos");
                                }
                                log.Debug("Se agrego correctamente los anexos");
                            }
                            break;
                        }
                    case 67:
                        {
                            log.Debug("--------------------------------------------------------------");
                            oTrans = (StockTransfer)(dynamic)oCompany.GetBusinessObject((BoObjectTypes)objtype);
                            log.Debug("--------------------------------------------------------------");
                            Attachments2 oAtt2 = (dynamic)oCompany.GetBusinessObject(BoObjectTypes.oAttachments2);
                            log.Debug("--------------------------------------------------------------");
                            if (!oDoc.GetByKey(docentry))
                            {
                                break;
                            }
                            log.Debug("--------------------------------------------------------------");
                            if (!oAtt2.GetByKey(oDoc.AttachmentEntry))
                            {
                                log.Debug("--------------------------------------------------------------");
                                oAtt2.Lines.Add();
                                log.Debug("--------------------------------------------------------------");
                                oAtt2.Lines.FileName = Path.GetFileNameWithoutExtension(fileName);
                                log.Debug("--------------------------------------------------------------");
                                oAtt2.Lines.FileExtension = Path.GetExtension(fileName).Substring(1);
                                log.Debug("--------------------------------------------------------------");
                                oAtt2.Lines.SourcePath = Path.GetDirectoryName(fileName);
                                log.Debug("--------------------------------------------------------------");
                                oAtt2.Lines.Override = BoYesNoEnum.tYES;
                                log.Debug("--------------------------------------------------------------");
                                int iResponse3 = oAtt2.Add();
                                log.Debug("--------------------------------------------------------------");
                                if (iResponse3 != 0)
                                {
                                    string error6 = oCompany.GetLastErrorCode() + " :: " + oCompany.GetLastErrorDescription();
                                    log.Debug($"Se encontró el siguiente ERROR : {error6}");
                                    return false;
                                }
                                log.Debug("--------------------------------------------------------------");
                                int AttEntry3 = int.Parse(oCompany.GetNewObjectKey());
                                log.Debug("--------------------------------------------------------------");
                                oDoc.AttachmentEntry = AttEntry3;
                                log.Debug("--------------------------------------------------------------");
                                if (oDoc.Update() != 0)
                                {
                                    string error5 = oCompany.GetLastErrorCode() + " :: " + oCompany.GetLastErrorDescription();
                                    log.Debug($"Se encontró el siguiente ERROR : {error5}");
                                }
                                else
                                {
                                    log.Debug("Se agrego correctamente los anexos");
                                }
                                break;
                            }
                            log.Debug("--------------------------------------------------------------");
                            bool encontroAnexo = false;
                            log.Debug("--------------------------------------------------------------");
                            for (int i = 0; i < oAtt2.Lines.Count; i++)
                            {
                                log.Debug("--------------------------------------------------------------");
                                oAtt2.Lines.SetCurrentLine(i);
                                log.Debug("--------------------------------------------------------------");
                                if (oAtt2.Lines.FileName.Equals(Path.GetFileNameWithoutExtension(fileName)))
                                {
                                    log.Debug("--------------------------------------------------------------");
                                    oAtt2.Lines.FileName = Path.GetFileNameWithoutExtension(fileName);
                                    log.Debug("--------------------------------------------------------------");
                                    oAtt2.Lines.FileExtension = Path.GetExtension(fileName).Substring(1);
                                    log.Debug("--------------------------------------------------------------");
                                    oAtt2.Lines.SourcePath = Path.GetDirectoryName(fileName);
                                    log.Debug("--------------------------------------------------------------");
                                    oAtt2.Lines.Override = BoYesNoEnum.tYES;
                                    log.Debug("--------------------------------------------------------------");
                                    int iResponse = oAtt2.Update();
                                    log.Debug("--------------------------------------------------------------");
                                    if (iResponse != 0)
                                    {
                                        string error2 = oCompany.GetLastErrorCode() + " :: " + oCompany.GetLastErrorDescription();
                                        log.Debug($"Se encontró el siguiente ERROR : {error2}");
                                        return false;
                                    }
                                    log.Debug("--------------------------------------------------------------");
                                    int AttEntry = int.Parse(oCompany.GetNewObjectKey());
                                    log.Debug("--------------------------------------------------------------");
                                    oDoc.AttachmentEntry = AttEntry;
                                    log.Debug("--------------------------------------------------------------");
                                    if (oDoc.Update() != 0)
                                    {
                                        string error = oCompany.GetLastErrorCode() + " :: " + oCompany.GetLastErrorDescription();
                                        log.Debug($"Se encontró el siguiente ERROR : {error}");
                                    }
                                    else
                                    {
                                        log.Debug("Se agrego correctamente los anexos");
                                    }
                                    encontroAnexo = true;
                                }
                            }
                            if (!encontroAnexo)
                            {
                                log.Debug("--------------------------------------------------------------");
                                oAtt2.Lines.Add();
                                log.Debug("--------------------------------------------------------------");
                                oAtt2.Lines.FileName = Path.GetFileNameWithoutExtension(fileName);
                                log.Debug("--------------------------------------------------------------");
                                oAtt2.Lines.FileExtension = Path.GetExtension(fileName).Substring(1);
                                log.Debug("--------------------------------------------------------------");
                                oAtt2.Lines.SourcePath = Path.GetDirectoryName(fileName);
                                log.Debug("--------------------------------------------------------------");
                                oAtt2.Lines.Override = BoYesNoEnum.tYES;
                                log.Debug("--------------------------------------------------------------");
                                if (oAtt2.Update() != 0)
                                {
                                    string error4 = oCompany.GetLastErrorCode() + " :: " + oCompany.GetLastErrorDescription();
                                    log.Debug($"Se encontró el siguiente ERROR : {error4}");
                                    return false;
                                }
                                log.Debug("--------------------------------------------------------------");
                                int AttEntry2 = int.Parse(oCompany.GetNewObjectKey());
                                log.Debug("--------------------------------------------------------------");
                                oDoc.AttachmentEntry = AttEntry2;
                                log.Debug("--------------------------------------------------------------");
                                if (oDoc.Update() != 0)
                                {
                                    string error3 = oCompany.GetLastErrorCode() + " :: " + oCompany.GetLastErrorDescription();
                                    log.Debug($"Se encontró el siguiente ERROR : {error3}");
                                }
                                else
                                {
                                    log.Debug("Se agrego correctamente los anexos");
                                }
                                log.Debug("Se agrego correctamente los anexos");
                            }
                            break;
                        }
                }
                return true;
            }
            catch (Exception ex)
            {
                log.Debug($"Se encontró el siguiente ERROR : {ex.Message} ");
                return false;
            }
        }
    }
}
