using BL;
using DAL;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UBL;
using static UBL.InvoiceJson;

namespace Request_FE
{
    public partial class Service1 : ServiceBase
    {
        private Timer myTimer;

        private configuracionDAL.Configuracion oConfiguracion;

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

    

        private EventLog eventLog1;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                stlapso.Start();
            }
            catch (Exception ex)
            {
                Log.Error($"Se encontró el siguiente ERROR : {ex.Message} ");
              }
            
        }

        protected override void OnStop()
        {
           
            try
            {
                stlapso.Stop();
            }
            catch (Exception ex)
            {
                Log.Error($"Se encontró el siguiente ERROR : {ex.Message} ");
            }
        }

        private void stlapso_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            XmlConfigurator.Configure(new FileInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\log4net.config"));
            try
            {
                oConfiguracion = configuracionBL.cargarConfiguracion();
                Log.Debug("------------------------------------------------");
                if (oConfiguracion != null)
                {
                    Log.Debug("------------------------------------------------");
                    if (oConfiguracion.Conexion.Count > 0)
                    {
                        Log.Debug("------------------------------------------------");
                        for (int index = 0; index < oConfiguracion.Conexion.Count; index++)
                        {
                            if (!SapBL.conexion(oConfiguracion.Conexion[index].Sap.Servidor, oConfiguracion.Conexion[index].Sap.Tiposervidor, oConfiguracion.Conexion[index].Sap.Userdb, oConfiguracion.Conexion[index].Sap.Clavedb, oConfiguracion.Conexion[index].Sap.Usersap, oConfiguracion.Conexion[index].Sap.Clavesap, oConfiguracion.Conexion[index].Sap.Db, oConfiguracion.Conexion[index].Sap.Servidor+":30000"))
                            {
                                continue;
                            }
                            string sQuery = string.Empty;
                            sQuery = (oConfiguracion.Conexion[index].Sap.Tiposervidor.Contains("MSSQL") ? (" EXEC " + oConfiguracion.Conexion[index].Sap.Enviar) : (" CALL " + oConfiguracion.Conexion[index].Sap.Enviar));
                            List<TransaccionDAL> lstTransaccion = ProcessBL.generarPendiente(sQuery);
                            Log.Debug($"Se encontró un total de {lstTransaccion.Count} transacciones por procesar");
                            string strAccesoToken = string.Empty;
                            if (lstTransaccion != null && lstTransaccion.Count > 0)
                            {
                                Log.Debug("---------------------------------------------------------------------------");
                                Log.Debug("URL BASE : " + oConfiguracion.Conexion[index].Efact.Url.Base);
                                Log.Debug("USUARIO : " + oConfiguracion.Conexion[index].Efact.Usuario);
                                Log.Debug("CLAVE : " + oConfiguracion.Conexion[index].Efact.Clave);
                                Log.Debug("TOKEN : " + oConfiguracion.Conexion[index].Efact.Url.Token);
                                Log.Debug("---------------------------------------------------------------------------");
                                strAccesoToken = "ss";// RestServiceBL.getToken(oConfiguracion.Conexion[index].Efact.Url.Base, oConfiguracion.Conexion[index].Efact.Usuario, oConfiguracion.Conexion[index].Efact.Clave, oConfiguracion.Conexion[index].Efact.Url.Token);
                                if (!string.IsNullOrEmpty(strAccesoToken))
                                {
                                    foreach (TransaccionDAL transaccion in lstTransaccion)
                                    {
                                        string sJson = string.Empty;
                                        string sBajaId = string.Empty;
                                        Log.Debug("Generando Json : " + transaccion.sTipoDocumento + "-" + transaccion.sSerieDocumento + "-" + transaccion.sCorrelativoDocumento);
                                        if (transaccion.sAnulacion.Equals("C"))
                                        {
                                            //Log.Debug("---------------------------------------------------------------------------");
                                            //configuracionDAL.Script script = oConfiguracion.Conexion[index].Extraccion.Script.Where((configuracionDAL.Script o) => o.Tipodocumento.Equals("BAJA")).FirstOrDefault();
                                            //Log.Debug("---------------------------------------------------------------------------");
                                            //if (script != null)
                                            //{
                                            //    Log.Debug("---------------------------------------------------------------------------");
                                            //    VoidedDocumentJson.Rootobject voidedDocuments = VoidedDocumentsJsonBL.GenerateVoidedDocuments(transaccion.sDocEntry, transaccion.sObjType, script.Cabecera);
                                            //    Log.Debug("---------------------------------------------------------------------------");
                                            //    sJson = UtilyBL.ObjectToJsonString(voidedDocuments);
                                            //    Log.Debug("---------------------------------------------------------------------------");
                                            //    sBajaId = voidedDocuments.VoidedDocuments[0].ID[0].IdentifierContent;
                                            //    Log.Debug("---------------------------------------------------------------------------");
                                            //}
                                        }
                                        else
                                        {
                                            Log.Debug("---------------------------------------------------------------------------");
                                            string sTipoDocumento = transaccion.sTipoDocumento;
                                            Log.Debug("---------------------------------------------------------------------------");
                                            switch (sTipoDocumento)
                                            {
                                                case "01":
                                                    {
                                                        configuracionDAL.Script scriptFactura = oConfiguracion.Conexion[index].Extraccion.Script.Where((configuracionDAL.Script o) => o.Tipodocumento.Equals("01")).FirstOrDefault();
                                                        if (scriptFactura != null)
                                                        {
                                                            Log.Debug("---------------------------------------------------------------------------");
                                                            InvoiceJson.Root facturaJson = InvoiceGenerateJsonBL.GenerateInvoiceJsonV3(transaccion.sDocEntry, transaccion.sObjType,  scriptFactura.Cabecera, scriptFactura.Detalle, scriptFactura.Leyenda, scriptFactura.Impuestocabecera, scriptFactura.DocumentoRelacionado, scriptFactura.Cuota, scriptFactura.Descuento,scriptFactura.DescuentoTotal,scriptFactura.DescuentoDetalle);
                                                            Log.Debug("---------------------------------------------------------------------------");
                                                            facturaJson = CleanJsonBL.cleanInvoice(facturaJson);
                                                            Log.Debug("--------------------------------------------------------------");
                                                            sJson = UtilyBL.ObjectToJsonString(facturaJson);
                                                            Log.Debug("---------------------------------------------------------------------------");
                                                        }
                                                        break;
                                                    }
                                                case "03":
                                                    {
                                                        configuracionDAL.Script scriptFactura = oConfiguracion.Conexion[index].Extraccion.Script.Where((configuracionDAL.Script o) => o.Tipodocumento.Equals("01")).FirstOrDefault();
                                                        if (scriptFactura != null)
                                                        {
                                                            Log.Debug("---------------------------------------------------------------------------");
                                                            InvoiceJson.Root facturaJson = InvoiceGenerateJsonBL.GenerateInvoiceJsonV3(transaccion.sDocEntry, transaccion.sObjType, scriptFactura.Cabecera, scriptFactura.Detalle, scriptFactura.Leyenda, scriptFactura.Impuestocabecera, scriptFactura.DocumentoRelacionado, scriptFactura.Cuota, scriptFactura.Descuento, scriptFactura.DescuentoTotal, scriptFactura.DescuentoDetalle);
                                                            Log.Debug("---------------------------------------------------------------------------");
                                                            facturaJson = CleanJsonBL.cleanInvoice(facturaJson);
                                                            Log.Debug("--------------------------------------------------------------");
                                                            sJson = UtilyBL.ObjectToJsonString(facturaJson);
                                                            Log.Debug("---------------------------------------------------------------------------");
                                                        }
                                                        break;
                                                    }
                                                case "07":
                                                    {
                                                        //configuracionDAL.Script scriptNC = oConfiguracion.Conexion[index].Extraccion.Script.Where((configuracionDAL.Script o) => o.Tipodocumento.Equals("07")).FirstOrDefault();
                                                        //if (scriptNC != null)
                                                        //{
                                                        //    Log.Debug("---------------------------------------------------------------------------");
                                                        //    CreditNoteJson.Root creditNoteJson = CreditNoteGenerateJsonBL.GenerateCreditNoteJson(transaccion.sDocEntry, transaccion.sObjType, scriptNC.Cabecera, scriptNC.Detalle, scriptNC.Leyenda, scriptNC.Impuestocabecera, scriptNC.Cuota);
                                                        //    Log.Debug("---------------------------------------------------------------------------");
                                                        //    sJson = UtilyBL.ObjectToJsonString(creditNoteJson);
                                                        //    Log.Debug("---------------------------------------------------------------------------");
                                                        //}
                                                        break;
                                                    }
                                                case "08":
                                                    {
                                                        //configuracionDAL.Script scriptND = oConfiguracion.Conexion[index].Extraccion.Script.Where((configuracionDAL.Script o) => o.Tipodocumento.Equals("08")).FirstOrDefault();
                                                        //if (scriptND != null)
                                                        //{
                                                        //    Log.Debug("---------------------------------------------------------------------------");
                                                        //    DebitNoteJson.Root debitNoteJson = DebitNoteGenerateJsonBL.GenerateDebitNoteJson(transaccion.sDocEntry, transaccion.sObjType, scriptND.Cabecera, scriptND.Detalle, scriptND.Leyenda, scriptND.Impuestocabecera);
                                                        //    Log.Debug("---------------------------------------------------------------------------");
                                                        //    sJson = UtilyBL.ObjectToJsonString(debitNoteJson);
                                                        //    Log.Debug("---------------------------------------------------------------------------");
                                                        //}
                                                        break;
                                                    }
                                                case "09":
                                                    {
                                                        //configuracionDAL.Script scriptGRE = oConfiguracion.Conexion[index].Extraccion.Script.Where((configuracionDAL.Script o) => o.Tipodocumento.Equals("09")).FirstOrDefault();
                                                        //if (scriptGRE != null)
                                                        //{
                                                        //    Log.Debug("---------------------------------------------------------------------------");
                                                        //    GuiaRemisionJson.Root guiaRemisionJson = GuiaRemisionJsonBL.GenerateDespatchAdviceJson(transaccion.sDocEntry, transaccion.sObjType, scriptGRE.Guiaremision, scriptGRE.DetalleGuiaremision);
                                                        //    Log.Debug("---------------------------------------------------------------------------");
                                                        //    sJson = UtilyBL.ObjectToJsonString(guiaRemisionJson);
                                                        //    Log.Debug("---------------------------------------------------------------------------");
                                                        //}
                                                        break;
                                                    }
                                            }
                                        }
                                        if (!string.IsNullOrEmpty(sJson))
                                        {
                                            string sRuta = string.Empty;
                                            sRuta = ((!transaccion.sAnulacion.Equals("N")) ? (oConfiguracion.Conexion[index].Efact.Adjunto + "\\" + oConfiguracion.Conexion[index].Control.Ruc + "\\" + oConfiguracion.Conexion[index].Control.Ruc + "-" + sBajaId + ".json") : (oConfiguracion.Conexion[index].Efact.Adjunto + "\\" + oConfiguracion.Conexion[index].Control.Ruc + "\\" + oConfiguracion.Conexion[index].Control.Ruc + "-" + transaccion.sTipoDocumento + "-" + transaccion.sSerieDocumento + "-" + transaccion.sCorrelativoDocumento + ".json"));
                                            File.WriteAllText(sRuta, sJson);
                                          //  SapBL.AgregarAnexo(Convert.ToInt32(transaccion.sDocEntry), Convert.ToInt32(transaccion.sObjType), sRuta);
                                       //     RestServiceBL.sendDocumentoJson(strAccesoToken, sJson, oConfiguracion.Conexion[index].Efact.Url.Base, oConfiguracion.Conexion[index].Efact.Url.Enviodocumento, sRuta, transaccion.sObjType, transaccion.sDocEntry, transaccion.sTipoDocumento, oConfiguracion.Conexion[index].Sap.Campousuario.Estadosunat, oConfiguracion.Conexion[index].Sap.Campousuario.Respuestaefact, oConfiguracion.Conexion[index].Sap.Campousuario.Numeroticket);
                                        }
                                    }
                                }
                            }
                            SapBL.desconectar();
                        }
                        Log.Debug("------------------------------------------------");
                    }
                }
                else
                {
                    Log.Debug("------------------------------------------------");
                    Log.Debug("No se encontró el archivo de configuración.");
                    Log.Debug("------------------------------------------------");
                }
            }
            catch (Exception ex)
            {
                Log.Debug($"Se encontró el siguiente ERROR : {ex.Message} ");
                Log.Error($"Se encontró el siguiente ERROR : {ex.Message} ");
              //  eventLog1.WriteEntry("Se presentó el siguiente ERROR al procesar el Servicio VAS REQ - FACTURACION ELECTRONICA EFACT :: " + ex.Message, EventLogEntryType.Error, 0);
            }
        }
    }
}
