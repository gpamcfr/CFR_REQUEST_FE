using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UBL
{
    public class AplicationResponseType
    {
        [XmlRoot(ElementName = "CanonicalizationMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public class CanonicalizationMethod
        {
            [XmlAttribute(AttributeName = "Algorithm")]
            public string Algorithm { get; set; }
        }

        [XmlRoot(ElementName = "SignatureMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public class SignatureMethod
        {
            [XmlAttribute(AttributeName = "Algorithm")]
            public string Algorithm { get; set; }
        }

        [XmlRoot(ElementName = "Transform", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public class Transform
        {
            [XmlAttribute(AttributeName = "Algorithm")]
            public string Algorithm { get; set; }
        }

        [XmlRoot(ElementName = "Transforms", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public class Transforms
        {
            [XmlElement(ElementName = "Transform", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
            public Transform Transform { get; set; }
        }

        [XmlRoot(ElementName = "DigestMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public class DigestMethod
        {
            [XmlAttribute(AttributeName = "Algorithm")]
            public string Algorithm { get; set; }
        }

        [XmlRoot(ElementName = "Reference", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public class Reference
        {
            [XmlElement(ElementName = "Transforms", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
            public Transforms Transforms { get; set; }

            [XmlElement(ElementName = "DigestMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
            public DigestMethod DigestMethod { get; set; }

            [XmlElement(ElementName = "DigestValue", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
            public string DigestValue { get; set; }

            [XmlAttribute(AttributeName = "URI")]
            public string URI { get; set; }
        }

        [XmlRoot(ElementName = "SignedInfo", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public class SignedInfo
        {
            [XmlElement(ElementName = "CanonicalizationMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
            public CanonicalizationMethod CanonicalizationMethod { get; set; }

            [XmlElement(ElementName = "SignatureMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
            public SignatureMethod SignatureMethod { get; set; }

            [XmlElement(ElementName = "Reference", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
            public Reference Reference { get; set; }
        }

        [XmlRoot(ElementName = "X509IssuerSerial", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public class X509IssuerSerial
        {
            [XmlElement(ElementName = "X509IssuerName", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
            public string X509IssuerName { get; set; }

            [XmlElement(ElementName = "X509SerialNumber", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
            public string X509SerialNumber { get; set; }
        }

        [XmlRoot(ElementName = "X509Data", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public class X509Data
        {
            [XmlElement(ElementName = "X509Certificate", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
            public string X509Certificate { get; set; }

            [XmlElement(ElementName = "X509IssuerSerial", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
            public X509IssuerSerial X509IssuerSerial { get; set; }
        }

        [XmlRoot(ElementName = "KeyInfo", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public class KeyInfo
        {
            [XmlElement(ElementName = "X509Data", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
            public X509Data X509Data { get; set; }
        }

        [XmlRoot(ElementName = "Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public class Signature
        {
            [XmlElement(ElementName = "SignedInfo", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
            public SignedInfo SignedInfo { get; set; }

            [XmlElement(ElementName = "SignatureValue", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
            public string SignatureValue { get; set; }

            [XmlElement(ElementName = "KeyInfo", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
            public KeyInfo KeyInfo { get; set; }

            [XmlAttribute(AttributeName = "ds", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Ds { get; set; }
        }

        [XmlRoot(ElementName = "ExtensionContent", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")]
        public class ExtensionContent
        {
            [XmlElement(ElementName = "Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
            public Signature Signature { get; set; }
        }

        [XmlRoot(ElementName = "UBLExtension", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")]
        public class UBLExtension
        {
            [XmlElement(ElementName = "ExtensionContent", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")]
            public ExtensionContent ExtensionContent { get; set; }
        }

        [XmlRoot(ElementName = "UBLExtensions", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")]
        public class UBLExtensions
        {
            [XmlElement(ElementName = "UBLExtension", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")]
            public UBLExtension UBLExtension { get; set; }
        }

        [XmlRoot(ElementName = "CompanyID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public class CompanyID
        {
            [XmlAttribute(AttributeName = "schemeAgencyName")]
            public string SchemeAgencyName { get; set; }

            [XmlAttribute(AttributeName = "schemeID")]
            public string SchemeID { get; set; }

            [XmlAttribute(AttributeName = "schemeURI")]
            public string SchemeURI { get; set; }

            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "PartyLegalEntity", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public class PartyLegalEntity
        {
            [XmlElement(ElementName = "CompanyID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
            public CompanyID CompanyID { get; set; }

            public PartyLegalEntity()
            {
                CompanyID = new CompanyID();
            }
        }

        [XmlRoot(ElementName = "SenderParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public class SenderParty
        {
            [XmlElement(ElementName = "PartyLegalEntity", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
            public PartyLegalEntity PartyLegalEntity { get; set; }

            public SenderParty()
            {
                PartyLegalEntity = new PartyLegalEntity();
            }
        }

        [XmlRoot(ElementName = "ReceiverParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public class ReceiverParty
        {
            [XmlElement(ElementName = "PartyLegalEntity", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
            public PartyLegalEntity PartyLegalEntity { get; set; }

            public ReceiverParty()
            {
                PartyLegalEntity = new PartyLegalEntity();
            }
        }

        [XmlRoot(ElementName = "ResponseCode", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public class ResponseCode
        {
            [XmlAttribute(AttributeName = "listAgencyName")]
            public string ListAgencyName { get; set; }

            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "StatusReasonCode", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public class StatusReasonCode
        {
            [XmlAttribute(AttributeName = "listURI")]
            public string ListURI { get; set; }

            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "Status", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public class Status
        {
            [XmlElement(ElementName = "StatusReasonCode", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
            public StatusReasonCode StatusReasonCode { get; set; }

            [XmlElement(ElementName = "StatusReason", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
            public string StatusReason { get; set; }

            public Status()
            {
                StatusReasonCode = new StatusReasonCode();
            }
        }

        [XmlRoot(ElementName = "Response", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public class Response
        {
            [XmlElement(ElementName = "ReferenceID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
            public string ReferenceID { get; set; }

            [XmlElement(ElementName = "ResponseCode", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
            public ResponseCode ResponseCode { get; set; }

            [XmlElement(ElementName = "Description", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
            public string Description { get; set; }

            [XmlElement(ElementName = "Status", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
            public List<Status> Status { get; set; }

            public Response()
            {
                ResponseCode = new ResponseCode();
                Status = new List<Status>();
            }
        }

        [XmlRoot(ElementName = "ExternalReference", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public class ExternalReference
        {
            [XmlElement(ElementName = "DocumentHash", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
            public string DocumentHash { get; set; }
        }

        [XmlRoot(ElementName = "Attachment", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public class Attachment
        {
            [XmlElement(ElementName = "ExternalReference", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
            public ExternalReference ExternalReference { get; set; }

            public Attachment()
            {
                ExternalReference = new ExternalReference();
            }
        }

        [XmlRoot(ElementName = "DocumentReference", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public class DocumentReference
        {
            [XmlElement(ElementName = "ID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
            public string ID { get; set; }

            [XmlElement(ElementName = "IssueDate", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
            public string IssueDate { get; set; }

            [XmlElement(ElementName = "DocumentTypeCode", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
            public string DocumentTypeCode { get; set; }

            [XmlElement(ElementName = "Attachment", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
            public Attachment Attachment { get; set; }

            public DocumentReference()
            {
                Attachment = new Attachment();
            }
        }

        [XmlRoot(ElementName = "IssuerParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public class IssuerParty
        {
            [XmlElement(ElementName = "PartyLegalEntity", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
            public PartyLegalEntity PartyLegalEntity { get; set; }

            public IssuerParty()
            {
                PartyLegalEntity = new PartyLegalEntity();
            }
        }

        [XmlRoot(ElementName = "RecipientParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public class RecipientParty
        {
            [XmlElement(ElementName = "PartyLegalEntity", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
            public PartyLegalEntity PartyLegalEntity { get; set; }

            public RecipientParty()
            {
                PartyLegalEntity = new PartyLegalEntity();
            }
        }

        [XmlRoot(ElementName = "DocumentResponse", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public class DocumentResponse
        {
            [XmlElement(ElementName = "Response", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
            public Response Response { get; set; }

            [XmlElement(ElementName = "DocumentReference", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
            public DocumentReference DocumentReference { get; set; }

            [XmlElement(ElementName = "IssuerParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
            public IssuerParty IssuerParty { get; set; }

            [XmlElement(ElementName = "RecipientParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
            public RecipientParty RecipientParty { get; set; }

            public DocumentResponse()
            {
                Response = new Response();
                DocumentReference = new DocumentReference();
                IssuerParty = new IssuerParty();
                RecipientParty = new RecipientParty();
            }
        }

        [XmlRoot(ElementName = "ApplicationResponse", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:ApplicationResponse-2")]
        public class ApplicationResponse
        {
            [XmlElement(ElementName = "UBLExtensions", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")]
            public UBLExtensions UBLExtensions { get; set; }

            [XmlElement(ElementName = "UBLVersionID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
            public string UBLVersionID { get; set; }

            [XmlElement(ElementName = "CustomizationID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
            public string CustomizationID { get; set; }

            [XmlElement(ElementName = "ID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
            public string ID { get; set; }

            [XmlElement(ElementName = "IssueDate", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
            public string IssueDate { get; set; }

            [XmlElement(ElementName = "IssueTime", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
            public string IssueTime { get; set; }

            [XmlElement(ElementName = "ResponseDate", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
            public string ResponseDate { get; set; }

            [XmlElement(ElementName = "ResponseTime", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
            public string ResponseTime { get; set; }

            [XmlElement(ElementName = "SenderParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
            public SenderParty SenderParty { get; set; }

            [XmlElement(ElementName = "ReceiverParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
            public ReceiverParty ReceiverParty { get; set; }

            [XmlElement(ElementName = "DocumentResponse", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
            public DocumentResponse DocumentResponse { get; set; }

            [XmlAttribute(AttributeName = "ns4", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Ns4 { get; set; }

            [XmlAttribute(AttributeName = "xmlns")]
            public string Xmlns { get; set; }

            [XmlAttribute(AttributeName = "ns2", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Ns2 { get; set; }

            [XmlAttribute(AttributeName = "ns3", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Ns3 { get; set; }

            public ApplicationResponse()
            {
                UBLExtensions = new UBLExtensions();
                SenderParty = new SenderParty();
                ReceiverParty = new ReceiverParty();
                DocumentResponse = new DocumentResponse();
            }
        }
    }
}
