using DAL;
using log4net;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static DAL.configuracionDAL;

namespace BL
{
    public class ProcessBL
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static List<TransaccionDAL> generarPendiente(string sQuery)
        {
            List<TransaccionDAL> lstPendiente = new List<TransaccionDAL>();
            Recordset oRecordSetPendientes = (Recordset)(dynamic)SapBL.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            try
            {
                oRecordSetPendientes.DoQuery(sQuery);
                int iContador = 0;
                while (!oRecordSetPendientes.EoF)
                {
                    TransaccionDAL oTransaccion = new TransaccionDAL();
                    Log.Debug("-----------------------------");
                    oTransaccion.sCorrelativoDocumento = Convert.ToString((dynamic)oRecordSetPendientes.Fields.Item("sCorrelativoDocumento").Value);
                    Log.Debug("-----------------------------");
                    oTransaccion.sDocEntry = Convert.ToString((dynamic)oRecordSetPendientes.Fields.Item("sDocEntry").Value);
                    Log.Debug("-----------------------------");
                    oTransaccion.sDocNum = Convert.ToString((dynamic)oRecordSetPendientes.Fields.Item("sDocNum").Value);
                    Log.Debug("-----------------------------");
                    oTransaccion.sObjType = Convert.ToString((dynamic)oRecordSetPendientes.Fields.Item("sObjType").Value);
                    Log.Debug("-----------------------------");
                    oTransaccion.sSerieDocumento = Convert.ToString((dynamic)oRecordSetPendientes.Fields.Item("sSerieDocumento").Value);
                    Log.Debug("-----------------------------");
                    oTransaccion.sTipoDocumento = Convert.ToString((dynamic)oRecordSetPendientes.Fields.Item("sTipoDocumento").Value);
                    Log.Debug("-----------------------------");
                    oTransaccion.sAnulacion = Convert.ToString((dynamic)oRecordSetPendientes.Fields.Item("sAnulacion").Value);
                    Log.Debug("-----------------------------");
                    lstPendiente.Add(oTransaccion);
                    iContador++;
                    Log.Debug($"Se está procesando la transaccion envío # {iContador}");
                    oRecordSetPendientes.MoveNext();
                }
                return lstPendiente;
            }
            catch (Exception ex)
            {
                Log.Debug($"Se presentó el siguiente ERROR :: {ex.Message}");
                return null;
            }
        }

        public static List<TransaccionDAL> generarConsulta(string sQuery)
        {
            List<TransaccionDAL> lstConsulta = new List<TransaccionDAL>();
            Recordset oRecordSetConsulta = (Recordset)(dynamic)SapBL.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            try
            {
                oRecordSetConsulta.DoQuery(sQuery);
                int iContador = 0;
                while (!oRecordSetConsulta.EoF)
                {
                    TransaccionDAL oTransaccion = new TransaccionDAL();
                    Log.Debug("-----------------------------");
                    oTransaccion.sCorrelativoDocumento = Convert.ToString((dynamic)oRecordSetConsulta.Fields.Item("sCorrelativoDocumento").Value);
                    Log.Debug("-----------------------------");
                    oTransaccion.sDocEntry = Convert.ToString((dynamic)oRecordSetConsulta.Fields.Item("sDocEntry").Value);
                    Log.Debug("-----------------------------");
                    oTransaccion.sDocNum = Convert.ToString((dynamic)oRecordSetConsulta.Fields.Item("sDocNum").Value);
                    Log.Debug("-----------------------------");
                    oTransaccion.sObjType = Convert.ToString((dynamic)oRecordSetConsulta.Fields.Item("sObjType").Value);
                    Log.Debug("-----------------------------");
                    oTransaccion.sSerieDocumento = Convert.ToString((dynamic)oRecordSetConsulta.Fields.Item("sSerieDocumento").Value);
                    Log.Debug("-----------------------------");
                    oTransaccion.sTipoDocumento = Convert.ToString((dynamic)oRecordSetConsulta.Fields.Item("sTipoDocumento").Value);
                    Log.Debug("-----------------------------");
                    oTransaccion.sNumeroTicket = Convert.ToString((dynamic)oRecordSetConsulta.Fields.Item("sNumeroTicket").Value);
                    Log.Debug("-----------------------------");
                    lstConsulta.Add(oTransaccion);
                    iContador++;
                    Log.Debug($"Se está procesando la transaccion consulta # {iContador}");
                    oRecordSetConsulta.MoveNext();
                }
                return lstConsulta;
            }
            catch (Exception ex)
            {
                Log.Debug($"Se presentó el siguiente ERROR :: {ex.Message}");
                return null;
            }
        }
    }
}
