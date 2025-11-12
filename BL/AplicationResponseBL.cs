using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UBL;

namespace BL
{
    public class AplicationResponseBL
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static AplicationResponseType.ApplicationResponse cargarCDR(string FilePath)
        {
            AplicationResponseType.ApplicationResponse cdr = new AplicationResponseType.ApplicationResponse();
            try
            {
                if (!File.Exists(FilePath))
                {
                    log.Debug($"Se presentó el siguiente ERROR : No existe el archivo de configuración {FilePath}");
                    return null;
                }
                cdr = DeserializeXMLFileToObject<AplicationResponseType.ApplicationResponse>(FilePath);
            }
            catch (Exception ex)
            {
                log.Debug($"Se presentó el siguiente ERROR : {ex.Message}");
            }
            return cdr;
        }

        private static T DeserializeXMLFileToObject<T>(string XmlFilename)
        {
            T returnObject = default(T);
            if (string.IsNullOrEmpty(XmlFilename))
            {
                return default(T);
            }
            try
            {
                StreamReader xmlStream = new StreamReader(XmlFilename);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                returnObject = (T)serializer.Deserialize(xmlStream);
                xmlStream.Close();
                log.Debug($"Se cargó correctamente cargado de la ruta : {XmlFilename}");
            }
            catch (Exception ex)
            {
                log.Debug($"Se presentó el siguiente ERROR : {ex.Message}");
            }
            return returnObject;
        }
    }
}
