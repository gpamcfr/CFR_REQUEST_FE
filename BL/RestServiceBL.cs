using log4net;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static DAL.configuracionDAL;

namespace BL
{
    public class RestServiceBL
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static string getToken(string sURL, string sUserName, string sPassword, string lastUrl)
        {
            Log.Debug("-----------------------------------------");
            string access_token = "";
            try
            {
                Log.Debug("-----------------------------------------");
                string clientId = "client";
                Log.Debug("-----------------------------------------");
                string clientSecret = "secret";
                string credentials = $"{clientId}:{clientSecret}";
                Log.Debug("-----------------------------------------");
                RestClient restClient = new RestClient(sURL);
                Log.Debug("-----------------------------------------");
                RestRequest restRequest = new RestRequest(lastUrl);
                Log.Debug("-----------------------------------------");
                restRequest.RequestFormat = DataFormat.Json;
                Log.Debug("-----------------------------------------");
                restRequest.Method = Method.POST;
                Log.Debug("-----------------------------------------");
                restRequest.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials)));
                Log.Debug("-----------------------------------------");
                restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                Log.Debug("-----------------------------------------");
                restRequest.AddHeader("Accept", "application/json");
                Log.Debug("-----------------------------------------");
                restRequest.AddParameter("grant_type", "password");
                Log.Debug("-----------------------------------------");
                restRequest.AddParameter("username", sUserName);
                Log.Debug("-----------------------------------------");
                restRequest.AddParameter("password", sPassword);
                Log.Debug("-----------------------------------------");
                IRestResponse response = restClient.Execute(restRequest);
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    Log.Debug("-------------------------------------------------------");
                    dynamic objError = new ExpandoObject();
                    objError = JsonConvert.DeserializeObject(response.Content);
                    access_token = "";
                    object error = objError.invalid_grant;
                    object error_description = objError.error_description;
                    Log.Debug("-------------------------------------------------------");
                }
                if (response.StatusCode == HttpStatusCode.Accepted || response.StatusCode == HttpStatusCode.OK)
                {
                    Log.Debug("-------------------------------------------------------");
                    dynamic objRpta = new ExpandoObject();
                    objRpta = JsonConvert.DeserializeObject(response.Content);
                    access_token = objRpta.access_token;
                    string token_type = objRpta.token_type;
                    int expires_in = objRpta.expires_in;
                    Log.Debug("-------------------------------------------------------");
                }
            }
            catch (Exception ex)
            {
                Log.Debug($"Se procedió a encontrar el siguiente ERROR :: {ex.Message}");
            }
            return access_token;
        }

        public static void sendDocumentoJson(string strAccesoToken, string objectString, string strURL, string LastUrl, string sFileName, string sObjType, string sDocEntry, string sTipoDocumento, string sCampoEstado, string sCampoRespuesta, string sCampoTicket)
        {
            try
            {
                string strEnvioDocTicket = "";
                string strTicket = "";
                string code = "";
                RestClient restClient = new RestClient(strURL);
                RestRequest restRequest = new RestRequest(LastUrl);
                restRequest.RequestFormat = DataFormat.Json;
                restRequest.Method = Method.POST;
                restRequest.AddHeader("Authorization", "Bearer " + strAccesoToken);
                restRequest.AddHeader("Content-Type", "multipart/form-data");
                restRequest.AddHeader("Accept", "application/json");
                restRequest.AddFile("file", sFileName);
                IRestResponse response = restClient.Execute(restRequest);
                dynamic objRptaTicket = new ExpandoObject();
                objRptaTicket = JsonConvert.DeserializeObject(response.Content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    code = objRptaTicket.code;
                    strTicket = objRptaTicket.description;
                    SapBL.actualizarEstadoSAP_V2("1", int.Parse(sDocEntry), int.Parse(sObjType), sTipoDocumento, response.Content, sCampoEstado, sCampoRespuesta, "N", sCampoTicket, strTicket);
                }
                else
                {
                    string strEnvioDocCode = objRptaTicket.code;
                    strEnvioDocTicket = objRptaTicket.description;
                    SapBL.actualizarEstadoSAP("5", int.Parse(sDocEntry), int.Parse(sObjType), sTipoDocumento, response.Content, sCampoEstado, sCampoRespuesta, "N");
                }
            }
            catch (Exception ex)
            {
                Log.Debug($"Se procedió a encontrar el siguiente ERROR :: {ex.Message}");
            }
        }

        public static byte[] getPDF(string strAccesoToken, string baseURL, string strURL, string LastUrl, string sObjType, string sDocEntry, string numeroTicket)
        {
            try
            {
                RestClient restClient = new RestClient(baseURL);
                string URL_COMPLETA = $"{strURL}{LastUrl}{numeroTicket}";
                Log.Debug($"URL :: {URL_COMPLETA}");
                RestRequest restRequest = new RestRequest(URL_COMPLETA);
                restRequest.RequestFormat = DataFormat.Json;
                restRequest.Method = Method.GET;
                restRequest.AddHeader("Authorization", "Bearer " + strAccesoToken);
                restRequest.AddHeader("Content-Type", "multipart/form-data");
                restRequest.AddHeader("Accept", "application/json");
                IRestResponse response = restClient.Execute(restRequest);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return restClient.DownloadData(restRequest);
                }
                return null;
            }
            catch (Exception ex)
            {
                Log.Debug($"Se procedió a encontrar el siguiente ERROR :: {ex.Message}");
                return null;
            }
        }

        public static byte[] getCDR(string strAccesoToken, string baseURL, string strURL, string LastUrl, string sObjType, string sDocEntry, string numeroTicket, string sCampoEstado, string sCampoRespuesta)
        {
            try
            {
                RestClient restClient = new RestClient(baseURL);
                string URL_COMPLETA = $"{strURL}{LastUrl}{numeroTicket}";
                Log.Debug($"URL :: {URL_COMPLETA}");
                RestRequest restRequest = new RestRequest(URL_COMPLETA);
                restRequest.RequestFormat = DataFormat.Json;
                restRequest.Method = Method.GET;
                restRequest.AddHeader("Authorization", "Bearer " + strAccesoToken);
                restRequest.AddHeader("Content-Type", "multipart/form-data");
                restRequest.AddHeader("Accept", "application/json");
                IRestResponse response = restClient.Execute(restRequest);
                Log.Debug("-----------------------------------------------------------");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Log.Debug("-----------------------------------------------------------");
                    Log.Debug(response.Content);
                    byte[] r = restClient.DownloadData(restRequest);
                    Log.Debug("-----------------------------------------------------------");
                    return r;
                }
                Log.Debug("-----------------------------------------------------------");
                string strEnvioDocTicket = "";
                dynamic objRptaTicket = new ExpandoObject();
                objRptaTicket = JsonConvert.DeserializeObject(response.Content);
                string strEnvioDocCode = objRptaTicket.code;
                strEnvioDocTicket = objRptaTicket.description;
                Log.Debug("-----------------------------------------------------------");
                SapBL.actualizarEstadoSAP("5", int.Parse(sDocEntry), int.Parse(sObjType), "", response.Content, sCampoEstado, sCampoRespuesta, "N");
                Log.Debug("-----------------------------------------------------------");
                return null;
            }
            catch (Exception ex)
            {
                Log.Debug($"Se procedió a encontrar el siguiente ERROR :: {ex.Message}");
                return null;
            }
        }

        public static byte[] getXML(string strAccesoToken, string baseURL, string strURL, string LastUrl, string sObjType, string sDocEntry, string numeroTicket)
        {
            try
            {
                RestClient restClient = new RestClient(baseURL);
                string URL_COMPLETA = $"{strURL}{LastUrl}{numeroTicket}";
                Log.Debug($"URL :: {URL_COMPLETA}");
                RestRequest restRequest = new RestRequest(URL_COMPLETA);
                restRequest.RequestFormat = DataFormat.Json;
                restRequest.Method = Method.GET;
                restRequest.AddHeader("Authorization", "Bearer " + strAccesoToken);
                restRequest.AddHeader("Content-Type", "multipart/form-data");
                restRequest.AddHeader("Accept", "application/json");
                IRestResponse response = restClient.Execute(restRequest);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return restClient.DownloadData(restRequest);
                }
                return null;
            }
            catch (Exception ex)
            {
                Log.Debug($"Se procedió a encontrar el siguiente ERROR :: {ex.Message}");
                return null;
            }
        }
    }
}
