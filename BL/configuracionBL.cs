using DAL;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace BL
{
    public class configuracionBL
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static configuracionDAL.Configuracion cargarConfiguracion()
        {
            string FilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\Config.xml";
            configuracionDAL.Configuracion oConfiguracion = new configuracionDAL.Configuracion();
            try
            {
                if (!File.Exists(FilePath))
                {
                    log.Debug($"Se presentó el siguiente ERROR : No existe el archivo de configuración {FilePath}");
                    return null;
                }
                oConfiguracion = DeserializeXMLFileToObject<configuracionDAL.Configuracion>(FilePath);
            }
            catch (Exception ex)
            {
                log.Debug($"Se presentó el siguiente ERROR : {ex.Message}");
            }
            log.Info("Se cargó correctamente la configuración. ");
            return oConfiguracion;
        }

        public static void guardarConfiguracion(configuracionDAL.Configuracion oConfiguracion)
        {
            try
            {
                string FilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\Config.xml";
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(oConfiguracion.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, oConfiguracion);
                    stream.Position = 0L;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(FilePath);
                    stream.Close();
                }
                log.Debug("Se guardó correctamente la configuración.");
            }
            catch (Exception ex)
            {
                log.Debug($"Se presentó el siguiente ERROR : {ex.Message}");
            }
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
