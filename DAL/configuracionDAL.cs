using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DAL
{
    public class configuracionDAL
    {
        [XmlRoot(ElementName = "control")]
        public class Control
        {
            [XmlElement(ElementName = "url")]
            public string Url { get; set; }

            [XmlElement(ElementName = "hora")]
            public string Hora { get; set; }

            [XmlElement(ElementName = "ruc")]
            public string Ruc { get; set; }
        }

        [XmlRoot(ElementName = "campousuario")]
        public class Campousuario
        {
            [XmlElement(ElementName = "estadosunat")]
            public string Estadosunat { get; set; }

            [XmlElement(ElementName = "respuestasunat")]
            public string Respuestasunat { get; set; }

            [XmlElement(ElementName = "respuestaefact")]
            public string Respuestaefact { get; set; }

            [XmlElement(ElementName = "numeroticket")]
            public string Numeroticket { get; set; }
        }

        [XmlRoot(ElementName = "sap")]
        public class Sap
        {
            [XmlElement(ElementName = "servidor")]
            public string Servidor { get; set; }

            [XmlElement(ElementName = "tiposervidor")]
            public string Tiposervidor { get; set; }

            [XmlElement(ElementName = "usersap")]
            public string Usersap { get; set; }

            [XmlElement(ElementName = "clavesap")]
            public string Clavesap { get; set; }

            [XmlElement(ElementName = "userdb")]
            public string Userdb { get; set; }

            [XmlElement(ElementName = "clavedb")]
            public string Clavedb { get; set; }

            [XmlElement(ElementName = "db")]
            public string Db { get; set; }

            [XmlElement(ElementName = "enviar")]
            public string Enviar { get; set; }

            [XmlElement(ElementName = "consultar")]
            public string Consultar { get; set; }

            [XmlElement(ElementName = "campousuario")]
            public Campousuario Campousuario { get; set; }

            public Sap()
            {
                Campousuario = new Campousuario();
            }
        }

        [XmlRoot(ElementName = "url")]
        public class Url
        {
            [XmlElement(ElementName = "base")]
            public string Base { get; set; }

            [XmlElement(ElementName = "token")]
            public string Token { get; set; }

            [XmlElement(ElementName = "enviodocumento")]
            public string Enviodocumento { get; set; }

            [XmlElement(ElementName = "consultadocumento")]
            public string Consultadocumento { get; set; }

            [XmlElement(ElementName = "consultadocumentocdr")]
            public string ConsultadocumentoCDR { get; set; }

            [XmlElement(ElementName = "consultadocumentopdf")]
            public string ConsultadocumentoPDF { get; set; }

            [XmlElement(ElementName = "consultadocumentoxml")]
            public string ConsultadocumentoXML { get; set; }
        }

        [XmlRoot(ElementName = "efact")]
        public class Efact
        {
            [XmlElement(ElementName = "usuario")]
            public string Usuario { get; set; }

            [XmlElement(ElementName = "clave")]
            public string Clave { get; set; }

            [XmlElement(ElementName = "adjunto")]
            public string Adjunto { get; set; }

            [XmlElement(ElementName = "url")]
            public Url Url { get; set; }

            public Efact()
            {
                Url = new Url();
            }
        }

        [XmlRoot(ElementName = "script")]
        public class Script
        {
            [XmlElement(ElementName = "tipodocumento")]
            public string Tipodocumento { get; set; }

            [XmlElement(ElementName = "cuota")]
            public string Cuota { get; set; }

            [XmlElement(ElementName = "cabecera")]
            public string Cabecera { get; set; }

            [XmlElement(ElementName = "detalle")]
            public string Detalle { get; set; }

            [XmlElement(ElementName = "descuento")]
            public string Descuento { get; set; }
            [XmlElement(ElementName = "descuentototal")]
            public string DescuentoTotal { get; set; }
            [XmlElement(ElementName = "descuentodetalle")]
            public string DescuentoDetalle { get; set; }

            [XmlElement(ElementName = "leyenda")]
            public string Leyenda { get; set; }

            [XmlElement(ElementName = "impuestocabecera")]
            public string Impuestocabecera { get; set; }

            [XmlElement(ElementName = "documentorelacionado")]
            public string DocumentoRelacionado { get; set; }

            [XmlElement(ElementName = "guiaremision")]
            public string Guiaremision { get; set; }

            [XmlElement(ElementName = "detalleguiaremision")]
            public string DetalleGuiaremision { get; set; }
        }

        [XmlRoot(ElementName = "extraccion")]
        public class Extraccion
        {
            [XmlElement(ElementName = "script")]
            public List<Script> Script { get; set; }

            public Extraccion()
            {
                Script = new List<Script>();
            }
        }

        [XmlRoot(ElementName = "conexion")]
        public class Conexion
        {
            [XmlElement(ElementName = "control")]
            public Control Control { get; set; }

            [XmlElement(ElementName = "nombre")]
            public string Nombre { get; set; }

            [XmlElement(ElementName = "sap")]
            public Sap Sap { get; set; }

            [XmlElement(ElementName = "efact")]
            public Efact Efact { get; set; }

            [XmlElement(ElementName = "extraccion")]
            public Extraccion Extraccion { get; set; }

            public Conexion()
            {
                Extraccion = new Extraccion();
                Efact = new Efact();
                Sap = new Sap();
                Control = new Control();
            }
        }

        [XmlRoot(ElementName = "configuracion")]
        public class Configuracion
        {
            [XmlElement(ElementName = "conexion")]
            public List<Conexion> Conexion { get; set; }

            public Configuracion()
            {
                Conexion = new List<Conexion>();
            }
        }
    }
}
