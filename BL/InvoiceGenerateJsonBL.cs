using log4net;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UBL;
using static UBL.InvoiceJson;

namespace BL
{
    public class InvoiceGenerateJsonBL
    {
	private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static InvoiceJson.Root GenerateInvoiceJsonV3(string sDocEntry, string sObjType,  string sQueryCabecera, string sQueryDetalle, string sQueryLeyenda, string sQueryImpuestoCabecera, string sQueryGuia, string sQueryCuota, string sQueryDescuento,string sQueryDescuentoTotal,string sQueryDescuentoDetalle)
        {
            CultureInfo customCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = customCulture;
            Recordset oRecordSetCabecera = (Recordset)(dynamic)SapBL.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            Recordset oRecordSetLeyenda = (Recordset)(dynamic)SapBL.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            Recordset oRecordSetImpuestoCabecera = (Recordset)(dynamic)SapBL.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            Recordset oRecordSetImpuestoDetalle = (Recordset)(dynamic)SapBL.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            Recordset oRecordSetDetalles = (Recordset)(dynamic)SapBL.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            Recordset oRecordSetGuia = (Recordset)(dynamic)SapBL.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            Recordset oRecordSetCuota = (Recordset)(dynamic)SapBL.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            Recordset oRecordSetDescuento = (Recordset)(dynamic)SapBL.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            Recordset oRecordSetDescuentoTotal = (Recordset)(dynamic)SapBL.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            Recordset oRecordSetDescuentoDetalle = (Recordset)(dynamic)SapBL.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

            string sQueryExtractHeader = string.Empty;
            string sQueryExtractHeaderProperties = string.Empty;
            string sQueryExtractHeaderTaxes = string.Empty;
            string sQueryExtractDetails = string.Empty;
            string sQueryExtractGuia = string.Empty;
            string sQueryExtractCuota = string.Empty;
            string sQueryExtractDescuento = string.Empty;
            string sQueryExtractDescuentoTotal = string.Empty;
            string sQueryExtractDescuentoDetalle = string.Empty;
       
                if (SapBL.oCompany.DbServerType == BoDataServerTypes.dst_HANADB)
            {
                Log.Debug("-----------------------------");
                sQueryExtractHeader = $"CALL \"{sQueryCabecera}\" ('{sObjType}','{sDocEntry}');";
                Log.Debug("-----------------------------");
                sQueryExtractHeaderTaxes = $"CALL \"{sQueryImpuestoCabecera}\" ('{sObjType}','{sDocEntry}');";
                Log.Debug("-----------------------------");
                sQueryExtractHeaderProperties = $"CALL \"{sQueryLeyenda}\" ('{sObjType}','{sDocEntry}');";
                Log.Debug("-----------------------------");
                sQueryExtractDetails = $"CALL \"{sQueryDetalle}\" ('{sObjType}','{sDocEntry}');";
                Log.Debug("-----------------------------");
                sQueryExtractGuia = $"CALL \"{sQueryGuia}\" ('{sObjType}','{sDocEntry}');";
                Log.Debug("-----------------------------");
                sQueryExtractCuota = $"CALL \"{sQueryCuota}\" ('{sObjType}','{sDocEntry}');";
                Log.Debug("-----------------------------");
                sQueryExtractDescuento = $"CALL \"{sQueryDescuento}\" ('{sObjType}','{sDocEntry}');";
                Log.Debug("-----------------------------");
                sQueryExtractDescuentoTotal = $"CALL \"{sQueryDescuentoTotal}\" ('{sObjType}','{sDocEntry}');";
                Log.Debug("-----------------------------");
            }
            else
            {
                Log.Debug("-----------------------------");
                sQueryExtractHeader = $"EXEC \"{sQueryCabecera}\" '{sDocEntry}','{sObjType}'";
                Log.Debug("-----------------------------");
                sQueryExtractHeaderTaxes = $"EXEC \"{sQueryImpuestoCabecera}\" '{sDocEntry}','{sObjType}'";
                Log.Debug("-----------------------------");
                sQueryExtractHeaderProperties = $"EXEC \"{sQueryLeyenda}\" '{sDocEntry}','{sObjType}'";
                Log.Debug("-----------------------------");
                sQueryExtractDetails = $"EXEC \"{sQueryDetalle}\" '{sDocEntry}','{sObjType}'";
                Log.Debug("-----------------------------");
                sQueryExtractGuia = $"EXEC \"{sQueryGuia}\" '{sDocEntry}','{sObjType}'";
                Log.Debug("-----------------------------");
                sQueryExtractCuota = $"EXEC \"{sQueryCuota}\" '{sDocEntry}','{sObjType}'";
                Log.Debug("-----------------------------");
                sQueryExtractDescuento = $"EXEC \"{sQueryDescuento}\" '{sDocEntry}','{sObjType}'";
                Log.Debug("-----------------------------");
                sQueryExtractDescuentoTotal = $"EXEC \"{sQueryDescuentoTotal}\" '{sDocEntry}','{sObjType}'";
                Log.Debug("-----------------------------");
            }
            Log.Debug("-----------------------------");
            InvoiceJson.Root oInvoice = new InvoiceJson.Root();
            InvoiceJson.Invoice oFactura = new InvoiceJson.Invoice();
            Log.Debug("-----------------------------");
            try
            {
                Log.Debug("-----------------------------");
                oRecordSetCabecera.DoQuery(sQueryExtractHeader);
                Log.Debug("-----------------------------");
                Log.Debug(" Query :: " + sQueryExtractHeader);
                Log.Debug("-----------------------------");
                while (!oRecordSetCabecera.EoF)
                {
                    oInvoice._D = (dynamic)oRecordSetCabecera.Fields.Item("_D").Value;
                    oInvoice._A = (dynamic)oRecordSetCabecera.Fields.Item("_A").Value;
                    oInvoice._B = (dynamic)oRecordSetCabecera.Fields.Item("_B").Value;
                    oInvoice._E = (dynamic)oRecordSetCabecera.Fields.Item("_E").Value;
                    oRecordSetDescuentoTotal.DoQuery(sQueryExtractDescuentoTotal);
                    if (oRecordSetDescuentoTotal.RecordCount > 0)
                    { 
                        while (!oRecordSetDescuentoTotal.EoF)
                        {

                            Log.Debug("-----------------------------------");
                            List<InvoiceJson.UBLExtension2> listExtension2 = new List<InvoiceJson.UBLExtension2>();
                            Log.Debug("-----------------------------------");
                            List<InvoiceJson.ExtensionContent> lstExtensionContent = new List<InvoiceJson.ExtensionContent>();
                            Log.Debug("-----------------------------------");
                            List<InvoiceJson.TotalDiscount> lstTotalDiscount = new List<InvoiceJson.TotalDiscount>();
                            Log.Debug("-----------------------------------");
                            lstTotalDiscount.Add(new InvoiceJson.TotalDiscount
                            {
                                _ = Convert.ToString((dynamic)oRecordSetDescuentoTotal.Fields.Item("Invoice.UBLExtensions.UBLExtension.ExtensionContent.TotalDiscount._").Value)
                            });
                            Log.Debug("-----------------------------------");
                            lstExtensionContent.Add(new InvoiceJson.ExtensionContent
                            {
                                TotalDiscount = lstTotalDiscount
                            });
                            Log.Debug("-----------------------------------");
                            listExtension2.Add(new InvoiceJson.UBLExtension2
                            {
                                ExtensionContent = lstExtensionContent
                            });
                            Log.Debug("-----------------------------------");
                            oFactura.UBLExtensions.Add(new InvoiceJson.UBLExtension1
                            {
                                UBLExtension = listExtension2
                            });
                            Log.Debug("-----------------------------------");
                            oRecordSetDescuentoTotal.MoveNext();
                        }
                       
                    }
                    Log.Debug("-----------------------------------");
                    InvoiceJson.UBLVersionID uBLVersionID = new InvoiceJson.UBLVersionID();
                    Log.Debug("-----------------------------------");
                    uBLVersionID._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.UBLVersionID._").Value;
                    Log.Debug("-----------------------------------");
                    oFactura.UBLVersionID.Add(uBLVersionID);
                    Log.Debug("-----------------------------------");
                    InvoiceJson.CustomizationID customizationID = new InvoiceJson.CustomizationID();
                    Log.Debug("-----------------------------------");
                    customizationID._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.CustomizationID._").Value;
                    Log.Debug("-----------------------------------");
                    oFactura.CustomizationID.Add(customizationID);
                    Log.Debug("-----------------------------------");
                    InvoiceJson.ID iD = new InvoiceJson.ID();
                    Log.Debug("-----------------------------------");
                    iD._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.ID._").Value;
                    Log.Debug("-----------------------------------");
                    oFactura.ID.Add(iD);
                    Log.Debug("-----------------------------------");
                    InvoiceJson.IssueDate issueDate = new InvoiceJson.IssueDate();
                    Log.Debug("-----------------------------------");
                    issueDate._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.IssueDate._").Value;
                    Log.Debug("-----------------------------------");
                    oFactura.IssueDate.Add(issueDate);
                    Log.Debug("-----------------------------------");
                    InvoiceJson.DueDate DueDate = new InvoiceJson.DueDate();
                    Log.Debug("-----------------------------------");
                    DueDate._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.DueDate._").Value;
                    Log.Debug("-----------------------------------");
                    oFactura.DueDate.Add(DueDate);
                    Log.Debug("-----------------------------------");
                    InvoiceJson.IssueTime issueTime = new InvoiceJson.IssueTime();
                    Log.Debug("-----------------------------------");
                    issueTime._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.IssueTime._").Value;
                    Log.Debug("-----------------------------------");
                    if (!string.IsNullOrEmpty(issueTime._))
                    {
                        oFactura.IssueTime.Add(issueTime);
                    }
                    Log.Debug("-----------------------------------");
                    InvoiceJson.InvoiceTypeCode invoiceTypeCode = new InvoiceJson.InvoiceTypeCode();
                    Log.Debug("-----------------------------------");
                    invoiceTypeCode._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.InvoiceTypeCode._").Value;
                    Log.Debug("-----------------------------------");
                    invoiceTypeCode.listID = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.InvoiceTypeCode.listID").Value;
                    Log.Debug("-----------------------------------");
                    invoiceTypeCode.listAgencyName = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.InvoiceTypeCode.listAgencyName").Value;
                    Log.Debug("-----------------------------------");
                    invoiceTypeCode.listSchemeURI = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.InvoiceTypeCode.listSchemeURI").Value;
                    Log.Debug("-----------------------------------");
                    invoiceTypeCode.name = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.InvoiceTypeCode.name").Value;
                    Log.Debug("-----------------------------------");
                    invoiceTypeCode.listName = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.InvoiceTypeCode.listName").Value;
                    Log.Debug("-----------------------------------");
                    invoiceTypeCode.listURI = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.InvoiceTypeCode.listURI").Value;
                    Log.Debug("-----------------------------------");
                    oFactura.InvoiceTypeCode.Add(invoiceTypeCode);
                    Log.Debug("-----------------------------------");
                    oRecordSetLeyenda.DoQuery(sQueryExtractHeaderProperties);
                    while (!oRecordSetLeyenda.EoF)
                    {
                        Log.Debug("-----------------------------------");
                        InvoiceJson.Note oNote = new InvoiceJson.Note();
                        Log.Debug("-----------------------------------");
                        string TextContent = Convert.ToString((dynamic)oRecordSetLeyenda.Fields.Item("Invoice.Note._").Value);
                        Log.Debug("-----------------------------------");
                        if (!string.IsNullOrEmpty(TextContent))
                        {
                            Log.Debug("-----------------------------------");
                            oNote._ = (dynamic)oRecordSetLeyenda.Fields.Item("Invoice.Note._").Value;
                            Log.Debug("-----------------------------------");
                        }
                        Log.Debug("-----------------------------------");
                        string LanguageLocalIdentifier = Convert.ToString((dynamic)oRecordSetLeyenda.Fields.Item("Invoice.Note.languageLocaleID").Value);
                        Log.Debug("-----------------------------------");
                        if (!string.IsNullOrEmpty(LanguageLocalIdentifier))
                        {
                            Log.Debug("-----------------------------------");
                            oNote.languageLocaleID = (dynamic)oRecordSetLeyenda.Fields.Item("Invoice.Note.languageLocaleID").Value;
                            Log.Debug("-----------------------------------");
                        }
                        //string LanguageIdentifier = Convert.ToString((dynamic)oRecordSetLeyenda.Fields.Item("Invoice.Note.LanguageIdentifier").Value);
                        //Log.Debug("-----------------------------------");
                        //if (!string.IsNullOrEmpty(LanguageIdentifier))
                        //{
                        //    Log.Debug("-----------------------------------");
                        //    oNote.langu = Convert.ToString((dynamic)oRecordSetLeyenda.Fields.Item("Invoice.Note.LanguageIdentifier").Value);
                        //    Log.Debug("-----------------------------------");
                        //}
                        string languageID = Convert.ToString((dynamic)oRecordSetLeyenda.Fields.Item("Invoice.Note.languageID").Value);
                        Log.Debug("-----------------------------------");
                        if (!string.IsNullOrEmpty(languageID))
                        {
                            Log.Debug("-----------------------------------");
                            oNote.languageID = Convert.ToString((dynamic)oRecordSetLeyenda.Fields.Item("Invoice.Note.languageID").Value);
                            Log.Debug("-----------------------------------");
                        }
                        oFactura.Note.Add(oNote);
                        Log.Debug("-----------------------------------");
                        oRecordSetLeyenda.MoveNext();
                        Log.Debug("-----------------------------------");
                    }
                    Log.Debug("-----------------------------------");
                    string sComentario = Convert.ToString((dynamic)oRecordSetCabecera.Fields.Item("Invoice.Note._").Value);
                    Log.Debug("-----------------------------------");
                    if (!string.IsNullOrEmpty(sComentario))
                    {
                        Log.Debug("-----------------------------------");
                        oFactura.Note.Add(new InvoiceJson.Note
                        {
                            _ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.Note._").Value
                        });
                        Log.Debug("-----------------------------------");
                    }
                    Log.Debug("-----------------------------------");
                    oRecordSetGuia.DoQuery(sQueryExtractGuia);
                    while (!oRecordSetGuia.EoF)
                    {
                        Log.Debug("-----------------------------------");
                        InvoiceJson.DespatchDocumentReference despatchDocumentReference = new InvoiceJson.DespatchDocumentReference();
                        InvoiceJson.DocumentTypeCode documentTypeCode = new InvoiceJson.DocumentTypeCode();
                        InvoiceJson.ID iD4 = new InvoiceJson.ID();
                        Log.Debug("-----------------------------------");
                        iD4._ = (dynamic)oRecordSetGuia.Fields.Item("Invoice.DespatchDocumentReference.ID._").Value;
                        Log.Debug("-----------------------------------");
                        documentTypeCode._ = (dynamic)oRecordSetGuia.Fields.Item("Invoice.DespatchDocumentReference.DocumentTypeCode._").Value;
                        Log.Debug("-----------------------------------");
                        documentTypeCode.listAgencyName = (dynamic)oRecordSetGuia.Fields.Item("Invoice.DespatchDocumentReference.DocumentTypeCode.listAgencyName").Value;
                        Log.Debug("-----------------------------------");
                        documentTypeCode.listName = (dynamic)oRecordSetGuia.Fields.Item("Invoice.DespatchDocumentReference.DocumentTypeCode.listName").Value;
                        Log.Debug("-----------------------------------");
                        documentTypeCode.listURI = (dynamic)oRecordSetGuia.Fields.Item("Invoice.DespatchDocumentReference.DocumentTypeCode.listSchemeURI").Value;
                        Log.Debug("-----------------------------------");
                        Log.Debug("-----------------------------------");
                        despatchDocumentReference.DocumentTypeCode.Add(documentTypeCode);
                        despatchDocumentReference.ID.Add(iD4);
                        oFactura.DespatchDocumentReference.Add(despatchDocumentReference);
                        oRecordSetGuia.MoveNext();
                        Log.Debug("-----------------------------------");
                    }
                    Log.Debug("-----------------------------------");
                    InvoiceJson.DocumentCurrencyCode documentCurrencyCode = new InvoiceJson.DocumentCurrencyCode();
                    Log.Debug("-----------------------------------");
                    documentCurrencyCode._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.DocumentCurrencyCode._").Value;
                    Log.Debug("-----------------------------------");
                    documentCurrencyCode.listID = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.DocumentCurrencyCode.listID").Value;
                    Log.Debug("-----------------------------------");
                    documentCurrencyCode.listName = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.DocumentCurrencyCode.listName").Value;
                    Log.Debug("-----------------------------------");
                    documentCurrencyCode.listAgencyName = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.DocumentCurrencyCode.listAgencyName").Value;
                    Log.Debug("-----------------------------------");
                    oFactura.DocumentCurrencyCode.Add(documentCurrencyCode);
                    Log.Debug("-----------------------------------");
                    InvoiceJson.LineCountNumeric lineCountNumeric = new InvoiceJson.LineCountNumeric();
                    Log.Debug("-----------------------------------");
                    lineCountNumeric._ = Convert.ToInt32((dynamic)oRecordSetCabecera.Fields.Item("Invoice.LineCountNumeric._").Value);
                    Log.Debug("-----------------------------------");
                    oFactura.LineCountNumeric.Add(lineCountNumeric);
                    Log.Debug("-----------------------------------");
                    InvoiceJson.ID orderId = new InvoiceJson.ID();
                    Log.Debug("-----------------------------------");
                    orderId._ = Convert.ToString((dynamic)oRecordSetCabecera.Fields.Item("Invoice.OrderReference.ID._").Value);
                    Log.Debug("-----------------------------------");
                    List<InvoiceJson.ID> lstOrderId = new List<InvoiceJson.ID>();
                    Log.Debug("-----------------------------------");
                    lstOrderId.Add(orderId);
                    Log.Debug("-----------------------------------");
                    if (!string.IsNullOrEmpty(orderId.TextContent) && !orderId.TextContent.Equals("0"))
                    {
                        oFactura.OrderReference.Add(new InvoiceJson.OrderReference
                        {
                            ID = lstOrderId
                        });
                    }
                    else
                    {
                        oFactura.OrderReference = null;
                    }
                    Log.Debug("-----------------------------------");
                    InvoiceJson.Signature signature = new InvoiceJson.Signature();
                    InvoiceJson.SignatoryParty signatoryParty = new InvoiceJson.SignatoryParty();
                    InvoiceJson.PartyIdentification partyIdentification = new InvoiceJson.PartyIdentification();
                    InvoiceJson.PartyName partyName = new InvoiceJson.PartyName();
                    InvoiceJson.Name name = new InvoiceJson.Name();
                    InvoiceJson.ID iD3 = new InvoiceJson.ID();
                    InvoiceJson.ID iD2 = new InvoiceJson.ID();
                    InvoiceJson.DigitalSignatureAttachment digitalSignatureAttachment = new InvoiceJson.DigitalSignatureAttachment();
                    InvoiceJson.ExternalReference externalReference = new InvoiceJson.ExternalReference();
                    InvoiceJson.URI uRI = new InvoiceJson.URI();


                    Log.Debug("-----------------------------------");
                    iD2._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.Signature.ID._").Value;
                    Log.Debug("-----------------------------------");
                    iD3._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.Signature.SignatoryParty.PartyIdentification.ID._").Value;
                    iD3.schemeID = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.Signature.SignatoryParty.PartyIdentification.ID.schemeID").Value;
                    iD3.SchemeName = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.Signature.SignatoryParty.PartyIdentification.ID.schemeName").Value; 
                    iD3.schemeAgencyName = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.Signature.SignatoryParty.PartyIdentification.ID.schemeAgencyName").Value;
                    iD3.schemeURI = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.Signature.SignatoryParty.PartyIdentification.ID.schemeURI").Value;


                    Log.Debug("-----------------------------------");
                    name._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.Signature.SignatoryParty.PartyName.Name._").Value;
                    Log.Debug("-----------------------------------");
                    uRI._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.Signature.DigitalSignatureAttachment.ExternalReference.URI._").Value;
                    Log.Debug("-----------------------------------");
                    externalReference.URI.Add(uRI);
                    digitalSignatureAttachment.ExternalReference.Add(externalReference);
                    partyName.Name.Add(name);
                    partyIdentification.ID.Add(iD3);
                    signatoryParty.PartyIdentification.Add(partyIdentification);
                    signatoryParty.PartyName.Add(partyName);
                    signature.ID.Add(iD2);
                    signature.SignatoryParty.Add(signatoryParty);
                    signature.DigitalSignatureAttachment.Add(digitalSignatureAttachment);
                    oFactura.Signature.Add(signature);
                    Log.Debug("-----------------------------------");
                    if ((!string.IsNullOrEmpty((dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingSupplierParty.Party.PartyIdentification.ID._").Value)))
                    {
                        Log.Debug("-----------------------------------");
                        InvoiceJson.AccountingSupplierParty accountingSupplierParty = new InvoiceJson.AccountingSupplierParty();
                        InvoiceJson.Party party2 = new InvoiceJson.Party();
                        InvoiceJson.PartyIdentification partyIdentification3 = new InvoiceJson.PartyIdentification();
                        InvoiceJson.ID iD8 = new InvoiceJson.ID();
                        InvoiceJson.PartyName partyName3 = new InvoiceJson.PartyName();
                        InvoiceJson.Name name5 = new InvoiceJson.Name();
                        InvoiceJson.PartyLegalEntity partyLegalEntity2 = new InvoiceJson.PartyLegalEntity();
                        InvoiceJson.RegistrationAddress registrationAddress2 = new InvoiceJson.RegistrationAddress();
                        InvoiceJson.RegistrationName registrationName2 = new InvoiceJson.RegistrationName();
                        InvoiceJson.ID iD11 = new InvoiceJson.ID();
                        InvoiceJson.AddressLine addressLine2 = new InvoiceJson.AddressLine();
                        InvoiceJson.Line line2 = new InvoiceJson.Line();
                        InvoiceJson.AddressTypeCode addressTypeCode2 = new InvoiceJson.AddressTypeCode();
                        InvoiceJson.CityName cityName2 = new InvoiceJson.CityName();
                        InvoiceJson.CountrySubentity countrySubentity2 = new InvoiceJson.CountrySubentity();
                        InvoiceJson.District district2 = new InvoiceJson.District();
                        InvoiceJson.Country country2 = new InvoiceJson.Country();
                        InvoiceJson.IdentificationCode identificationCode2 = new InvoiceJson.IdentificationCode();
                        InvoiceJson.Contact contact2 = new InvoiceJson.Contact();
                        InvoiceJson.ElectronicMail electronicMail2 = new InvoiceJson.ElectronicMail();
                        InvoiceJson.CitySubdivision citySubdivision2 = new InvoiceJson.CitySubdivision();
                      //POSTAL ADDRESS
                        InvoiceJson.PostalAddress postalAddress = new InvoiceJson.PostalAddress();
                        InvoiceJson.ID iDPA8 = new InvoiceJson.ID();
                        InvoiceJson.StreetName streetName = new  InvoiceJson.StreetName();
                        InvoiceJson.CityName cityNamepa = new InvoiceJson.CityName();
                        InvoiceJson.CountrySubentity countrySubentitypa = new InvoiceJson.CountrySubentity();
                        InvoiceJson.District districtpa = new InvoiceJson.District();
                        InvoiceJson.Country countrypa = new InvoiceJson.Country();
                        InvoiceJson.IdentificationCode identificationCodepa = new InvoiceJson.IdentificationCode();

                        Log.Debug("-----------------------------------");
                        iD8._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingSupplierParty.Party.PartyIdentification.ID._").Value;
                        Log.Debug("-----------------------------------");
                        iD8.schemeID = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingSupplierParty.Party.PartyIdentification.ID.schemeID").Value;
                        Log.Debug("-----------------------------------");
                        iD8.SchemeName = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingSupplierParty.Party.PartyIdentification.ID.schemeName").Value;
                        Log.Debug("-----------------------------------");
                        iD8.schemeAgencyName = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingSupplierParty.Party.PartyIdentification.ID.schemeAgencyName").Value;
                        Log.Debug("-----------------------------------");
                        iD8.schemeURI = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingSupplierParty.Party.PartyIdentification.ID.schemeURI").Value;
                        Log.Debug("-----------------------------------");
                        name5._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingSupplierParty.Party.PartyName.Name._").Value;
                        Log.Debug("-----------------------------------");
                        line2._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingSupplierParty.Party.PartyLegalEntity.RegistrationAddress.AddressLine.Line._").Value;
                        Log.Debug("-----------------------------------");
                        addressTypeCode2._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingSupplierParty.Party.PartyLegalEntity.RegistrationAddress.AddressTypeCode._").Value;
                        Log.Debug("-----------------------------------");
                        addressTypeCode2.listAgencyName = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingSupplierParty.Party.PartyLegalEntity.RegistrationAddress.AddressTypeCode.listAgencyName").Value;
                        Log.Debug("-----------------------------------");
                        addressTypeCode2.listName = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingSupplierParty.Party.PartyLegalEntity.RegistrationAddress.AddressTypeCode.listName").Value;
                        Log.Debug("-----------------------------------");
                        cityName2._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingSupplierParty.Party.PartyLegalEntity.RegistrationAddress.CityName._").Value;
                        Log.Debug("-----------------------------------");
                        countrySubentity2._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingSupplierParty.Party.PartyLegalEntity.RegistrationAddress.CountrySubentity._").Value;
                        Log.Debug("-----------------------------------");
                        district2._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingSupplierParty.Party.PartyLegalEntity.RegistrationAddress.District._").Value;
                        Log.Debug("-----------------------------------");
                        identificationCode2._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingSupplierParty.Party.PartyLegalEntity.RegistrationAddress.Country.IdentificationCode._").Value;
                        Log.Debug("-----------------------------------");
                        identificationCode2.listAgencyName = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingSupplierParty.Party.PartyLegalEntity.RegistrationAddress.Country.IdentificationCode.listAgencyName").Value;
                        Log.Debug("-----------------------------------");
                        identificationCode2.listID = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingSupplierParty.Party.PartyLegalEntity.RegistrationAddress.Country.IdentificationCode.listID").Value;
                        Log.Debug("-----------------------------------");
                        identificationCode2.listName = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingSupplierParty.Party.PartyLegalEntity.RegistrationAddress.Country.IdentificationCode.listName").Value;
                        Log.Debug("-----------------------------------");
                        registrationName2._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingSupplierParty.Party.PartyLegalEntity.RegistrationName._").Value;
                        Log.Debug("-----------------------------------");
                        iD11.schemeAgencyName = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingSupplierParty.Party.PartyLegalEntity.RegistrationAddress.ID.schemeAgencyName").Value;
                        Log.Debug("-----------------------------------");
                        iD11.SchemeName = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingSupplierParty.Party.PartyLegalEntity.RegistrationAddress.ID.schemeName").Value;
                        Log.Debug("-----------------------------------");
                        electronicMail2._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingSupplierParty.Party.Contact.ElectronicMail._").Value;
                        Log.Debug("-----------------------------------");
                        iD11._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingSupplierParty.Party.PartyLegalEntity.RegistrationAddress.ID._").Value;
                        Log.Debug("-----------------------------------");
                        citySubdivision2._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingSupplierParty.Party.PartyLegalEntity.RegistrationAddress.CitySubdivisionName._").Value;

                        iDPA8._= (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingSupplierParty.Party.PartyLegalEntity.RegistrationAddress.ID._").Value;
                        streetName._= (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingSupplierParty.Party.PartyLegalEntity.RegistrationAddress.AddressLine.Line._").Value;
                        cityNamepa._= (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingSupplierParty.Party.PartyLegalEntity.RegistrationAddress.CityName._").Value;
                        countrySubentitypa._= (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingSupplierParty.Party.PartyLegalEntity.RegistrationAddress.CountrySubentity._").Value;
                        districtpa._= (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingSupplierParty.Party.PartyLegalEntity.RegistrationAddress.District._").Value;
                        identificationCodepa._= (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingSupplierParty.Party.PartyLegalEntity.RegistrationAddress.Country.IdentificationCode._").Value;




                        country2.IdentificationCode.Add(identificationCode2);
                        countrypa.IdentificationCode.Add(identificationCodepa);
                        postalAddress.District.Add(districtpa);
                        postalAddress.CountrySubentity.Add(countrySubentitypa);
                        postalAddress.CityName.Add(cityNamepa);
                        postalAddress.StreetName.Add(streetName);
                        postalAddress.ID.Add(iDPA8);
                        postalAddress.Country.Add(countrypa);
                        addressLine2.Line.Add(line2);
                        registrationAddress2.AddressLine.Add(addressLine2);
                        registrationAddress2.CitySubdivisionName.Add(citySubdivision2);
                        registrationAddress2.AddressTypeCode.Add(addressTypeCode2);
                        registrationAddress2.CityName.Add(cityName2);
                        registrationAddress2.Country.Add(country2);
                        registrationAddress2.CountrySubentity.Add(countrySubentity2);
                        registrationAddress2.District.Add(district2);
                        registrationAddress2.ID.Add(iD11);
                        partyLegalEntity2.RegistrationAddress.Add(registrationAddress2);
                        partyLegalEntity2.RegistrationName.Add(registrationName2);
                        partyName3.Name.Add(name5);
                        partyIdentification3.ID.Add(iD8);
                        party2.PartyIdentification.Add(partyIdentification3);
                        party2.PartyName.Add(partyName3);
                        party2.PartyLegalEntity.Add(partyLegalEntity2);
                        party2.PostalAddress.Add(postalAddress);
                        contact2.ElectronicMail.Add(electronicMail2);
                        party2.Contact.Add(contact2);
                        accountingSupplierParty.Party.Add(party2);
                        oFactura.AccountingSupplierParty.Add(accountingSupplierParty);
                        Log.Debug("-----------------------------------");
                    }
                    Log.Debug("-----------------------------------");
                    if ((!string.IsNullOrEmpty((dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingCustomerParty.Party.PartyIdentification.ID._").Value)))
                    {
                        Log.Debug("-----------------------------------");
                        InvoiceJson.AccountingCustomerParty accountingCustomerParty = new InvoiceJson.AccountingCustomerParty();
                        InvoiceJson.Party party = new InvoiceJson.Party();
                        InvoiceJson.PartyIdentification partyIdentification2 = new InvoiceJson.PartyIdentification();
                        InvoiceJson.ID iD7 = new InvoiceJson.ID();
                        InvoiceJson.PartyName partyName2 = new InvoiceJson.PartyName();
                        InvoiceJson.Name name4 = new InvoiceJson.Name();
                        InvoiceJson.PartyLegalEntity partyLegalEntity = new InvoiceJson.PartyLegalEntity();
                        InvoiceJson.RegistrationAddress registrationAddress = new InvoiceJson.RegistrationAddress();
                        InvoiceJson.RegistrationName registrationName = new InvoiceJson.RegistrationName();
                        InvoiceJson.ID iD10 = new InvoiceJson.ID();
                        InvoiceJson.AddressLine addressLine = new InvoiceJson.AddressLine();
                        InvoiceJson.Line line = new InvoiceJson.Line();
                        InvoiceJson.AddressTypeCode addressTypeCode = new InvoiceJson.AddressTypeCode();
                        InvoiceJson.CityName cityName = new InvoiceJson.CityName();
                        InvoiceJson.CountrySubentity countrySubentity = new InvoiceJson.CountrySubentity();
                        InvoiceJson.District district = new InvoiceJson.District();
                        InvoiceJson.Country country = new InvoiceJson.Country();
                        InvoiceJson.IdentificationCode identificationCode = new InvoiceJson.IdentificationCode();
                        InvoiceJson.Contact contact = new InvoiceJson.Contact();
                        InvoiceJson.ElectronicMail electronicMail = new InvoiceJson.ElectronicMail();
                        InvoiceJson.CitySubdivision citySubdivision = new InvoiceJson.CitySubdivision();
                        InvoiceJson.IdentificationCode identificationCodepa = new InvoiceJson.IdentificationCode();
                        //POSTAL ADDRESS
                        InvoiceJson.PostalAddress postalAddress = new InvoiceJson.PostalAddress();
                        InvoiceJson.ID iDPA8 = new InvoiceJson.ID();
                        InvoiceJson.StreetName streetName = new InvoiceJson.StreetName();
                        InvoiceJson.CityName cityNamepa = new InvoiceJson.CityName();
                        InvoiceJson.CountrySubentity countrySubentitypa = new InvoiceJson.CountrySubentity();
                        InvoiceJson.District districtpa = new InvoiceJson.District();
                        InvoiceJson.Country countrypa = new InvoiceJson.Country();
                   


                        Log.Debug("-----------------------------------");
                        iD7._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingCustomerParty.Party.PartyIdentification.ID._").Value;
                        Log.Debug("-----------------------------------");
                        iD7.schemeURI = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingCustomerParty.Party.PartyIdentification.ID.schemeURI").Value;
                        Log.Debug("-----------------------------------");
                        iD7.SchemeName = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingCustomerParty.Party.PartyIdentification.ID.schemeName").Value;
                        Log.Debug("-----------------------------------");
                        iD7.schemeID = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingCustomerParty.Party.PartyIdentification.ID.schemeID").Value;
                        Log.Debug("-----------------------------------");
                        iD7.schemeAgencyName = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingCustomerParty.Party.PartyIdentification.ID.schemeAgencyName").Value;
                        Log.Debug("-----------------------------------");
                        name4._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingCustomerParty.Party.PartyName.Name._").Value;
                        Log.Debug("-----------------------------------");
                        line._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationAddress.AddressLine.Line._").Value;

                        Log.Debug("-----------------------------------");
                        cityName._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationAddress.CityName._").Value;
                        Log.Debug("-----------------------------------");
                        countrySubentity._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationAddress.CountrySubentity._").Value;
                        Log.Debug("-----------------------------------");
                        district._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationAddress.District._").Value;
                        Log.Debug("-----------------------------------");
                        identificationCode._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationAddress.Country.IdentificationCode._").Value;
                        Log.Debug("-----------------------------------");
                        identificationCode.listAgencyName = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationAddress.Country.IdentificationCode.listAgencyName").Value;
                        Log.Debug("-----------------------------------");
                        identificationCode.listID = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationAddress.Country.IdentificationCode.listID").Value;
                        Log.Debug("-----------------------------------");
                        identificationCode.listName = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationAddress.Country.IdentificationCode.listName").Value;

                        Log.Debug("-----------------------------------");
                        registrationName._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationName._").Value;
                        Log.Debug("-----------------------------------");
                        iD10.schemeAgencyName = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationAddress.ID.schemeAgencyName").Value;
                        Log.Debug("-----------------------------------");
                        iD10.SchemeName = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationAddress.ID.schemeName").Value;
                        Log.Debug("-----------------------------------");
                        electronicMail._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingCustomerParty.Party.Contact.ElectronicMail._").Value;
                        Log.Debug("-----------------------------------");
                        iD10._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationAddress.ID._").Value;
                        Log.Debug("-----------------------------------");
                        citySubdivision._= (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationAddress.CitySubdivisionName._").Value;
                        Log.Debug("-----------------------------------");
                        iDPA8._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationAddress.ID._").Value;
                        streetName._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationAddress.AddressLine.Line._").Value;
                        cityNamepa._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationAddress.CityName._").Value;
                        countrySubentitypa._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationAddress.CountrySubentity._").Value;
                        districtpa._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationAddress.District._").Value;
                        identificationCodepa._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationAddress.Country.IdentificationCode._").Value;



                        country.IdentificationCode.Add(identificationCode);
                        countrypa.IdentificationCode.Add(identificationCodepa);
                        postalAddress.District.Add(districtpa);
                        postalAddress.CountrySubentity.Add(countrySubentitypa);
                        postalAddress.CityName.Add(cityNamepa);
                        postalAddress.StreetName.Add(streetName);
                        postalAddress.ID.Add(iDPA8);
                        postalAddress.Country.Add(countrypa);

                        addressLine.Line.Add(line);
                        registrationAddress.AddressLine.Add(addressLine);
                        registrationAddress.CitySubdivisionName.Add(citySubdivision);
                        registrationAddress.AddressTypeCode.Add(addressTypeCode);
                        registrationAddress.CityName.Add(cityName);
                        registrationAddress.Country.Add(country);
                        registrationAddress.CountrySubentity.Add(countrySubentity);
                        registrationAddress.District.Add(district);
                        registrationAddress.ID.Add(iD10);
                        partyLegalEntity.RegistrationAddress.Add(registrationAddress);
                        partyLegalEntity.RegistrationName.Add(registrationName);
                        partyName2.Name.Add(name4);
                        partyIdentification2.ID.Add(iD7);
                        party.PartyIdentification.Add(partyIdentification2);
                        party.PartyName.Add(partyName2);
                        party.PostalAddress.Add(postalAddress);
                        party.PartyLegalEntity.Add(partyLegalEntity);
                        contact.ElectronicMail.Add(electronicMail);
                        party.Contact.Add(contact);
                        accountingCustomerParty.Party.Add(party);
                        oFactura.AccountingCustomerParty.Add(accountingCustomerParty);
                        Log.Debug("-----------------------------------");
                    }
                    //if ((!string.IsNullOrEmpty((dynamic)oRecordSetCabecera.Fields.Item("Invoice.PaymentMeans.ID.IdentifierContent").Value)))
                    //{
                    //    string sCodigo2 = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.PaymentMeans.ID.IdentifierContent").Value;
                    //    if (sCodigo2.Equals("Detraccion"))
                    //    {
                    //        Log.Debug("-----------------------------------");
                    //        InvoiceJson.PaymentMean paymentMean = new InvoiceJson.PaymentMean();
                    //        InvoiceJson.ID iD14 = new InvoiceJson.ID();
                    //        InvoiceJson.PaymentMeansCode paymentMeansCode = new InvoiceJson.PaymentMeansCode();
                    //        InvoiceJson.PayeeFinancialAccount payeeFinancialAccount = new InvoiceJson.PayeeFinancialAccount();
                    //        InvoiceJson.ID iD15 = new InvoiceJson.ID();
                    //        Log.Debug("-----------------------------------");
                    //        iD15.IdentifierContent = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.PayeeFinancialAccount.ID.IdentifierContent").Value;
                    //        Log.Debug("-----------------------------------");
                    //        payeeFinancialAccount.ID.Add(iD15);
                    //        Log.Debug("-----------------------------------");
                    //        paymentMeansCode._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.PaymentMeans.PaymentMeansCode.CodeContent").Value;
                    //        Log.Debug("-----------------------------------");
                    //        paymentMeansCode.listAgencyName = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.PaymentMeans.PaymentMeansCode.CodeListAgencyNameText").Value;
                    //        Log.Debug("-----------------------------------");
                    //        paymentMeansCode.listName = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.PaymentMeans.PaymentMeansCode.CodeListNameText").Value;
                    //        Log.Debug("-----------------------------------");
                    //        paymentMeansCode.listURI = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.PaymentMeans.PaymentMeansCode.CodeListUniformResourceIdentifier").Value;
                    //        Log.Debug("-----------------------------------");
                    //        iD14.IdentifierContent = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.PaymentMeans.ID.IdentifierContent").Value;
                    //        Log.Debug("-----------------------------------");
                    //        paymentMean.ID.Add(iD14);
                    //        Log.Debug("-----------------------------------");
                    //        paymentMean.PaymentMeansCode.Add(paymentMeansCode);
                    //        Log.Debug("-----------------------------------");
                    //        paymentMean.PayeeFinancialAccount.Add(payeeFinancialAccount);
                    //        Log.Debug("-----------------------------------");
                    //        InvoiceJson.PaymentMean oPaymentMeans = new InvoiceJson.PaymentMean();
                    //        Log.Debug("-----------------------------------");
                    //        oPaymentMeans = paymentMean;
                    //        Log.Debug("-----------------------------------");
                    //        List<InvoiceJson.PaymentMean> lstPaymentMeans = new List<InvoiceJson.PaymentMean>();
                    //        Log.Debug("-----------------------------------");
                    //        lstPaymentMeans.Add(oPaymentMeans);
                    //        Log.Debug("-----------------------------------");
                    //        oFactura.PaymentMeans = lstPaymentMeans;
                    //        Log.Debug("-----------------------------------");
                    //        Log.Debug("-----------------------------------");
                    //        InvoiceJson.PaymentTerm paymentTerm3 = new InvoiceJson.PaymentTerm();
                    //        InvoiceJson.PaymentMeansID paymentMeansID2 = new InvoiceJson.PaymentMeansID();
                    //        InvoiceJson.ID iD18 = new InvoiceJson.ID();
                    //        InvoiceJson.Note note2 = new InvoiceJson.Note();
                    //        InvoiceJson.PaymentPercent paymentPercent = new InvoiceJson.PaymentPercent();
                    //        InvoiceJson.Amount amount4 = new InvoiceJson.Amount();
                    //        Log.Debug("-----------------------------------");
                    //        amount4._ = Convert.ToString((dynamic)oRecordSetCabecera.Fields.Item("Invoice.PaymentTerms.Amount.AmountContent").Value);
                    //        Log.Debug("-----------------------------------");
                    //        amount4.currencyID = Convert.ToString((dynamic)oRecordSetCabecera.Fields.Item("Invoice.PaymentTerms.Amount.AmountCurrencyIdentifier").Value);
                    //        Log.Debug("-----------------------------------");
                    //        iD18.IdentifierContent = "Detraccion";
                    //        Log.Debug("-----------------------------------");
                    //        paymentMeansID2._ = Convert.ToString((dynamic)oRecordSetCabecera.Fields.Item("Invoice.PaymentTerms.ID.IdentifierContent").Value);
                    //        Log.Debug("-----------------------------------");
                    //        paymentMeansID2.schemeName = Convert.ToString((dynamic)oRecordSetCabecera.Fields.Item("Invoice.PaymentTerms.ID.IdentificationSchemeNameText").Value);
                    //        Log.Debug("-----------------------------------");
                    //        paymentMeansID2.schemeAgencyName = Convert.ToString((dynamic)oRecordSetCabecera.Fields.Item("Invoice.PaymentTerms.ID.IdentificationSchemeAgencyNameText").Value);
                    //        Log.Debug("-----------------------------------");
                    //        paymentMeansID2.schemeURI = Convert.ToString((dynamic)oRecordSetCabecera.Fields.Item("Invoice.PaymentTerms.ID.IdentificationSchemeUniformResourceIdentifier").Value);
                    //        Log.Debug("-----------------------------------");
                    //        List<InvoiceJson.PaymentMeansID> paymentMeansIDs = new List<InvoiceJson.PaymentMeansID>();
                    //        Log.Debug("-----------------------------------");
                    //        paymentMeansIDs.Add(paymentMeansID2);
                    //        Log.Debug("-----------------------------------");
                    //        note2._ = Convert.ToString((dynamic)oRecordSetCabecera.Fields.Item("Invoice.PaymentTerms.Note.TextContent").Value);
                    //        Log.Debug("-----------------------------------");
                    //        paymentPercent._ = Convert.ToString((dynamic)oRecordSetCabecera.Fields.Item("Invoice.PaymentTerms.PaymentPercent.NumericContent").Value);
                    //        Log.Debug("-----------------------------------");
                    //        paymentTerm3.ID.Add(iD18);
                    //        paymentTerm3.Note.Add(note2);
                    //        paymentTerm3.PaymentPercent.Add(paymentPercent);
                    //        if (!string.IsNullOrEmpty(amount4.currencyID))
                    //        {
                    //            paymentTerm3.Amount.Add(amount4);
                    //        }
                    //        else
                    //        {
                    //            paymentTerm3.Amount = null;
                    //        }
                    //        paymentTerm3.PaymentMeansID = paymentMeansIDs;
                    //        oFactura.PaymentTerms.Add(paymentTerm3);
                    //        Log.Debug("-----------------------------------");
                    //    }
                    //}
                    oRecordSetDescuento.DoQuery(sQueryExtractDescuento);
                    if (oRecordSetDescuento.RecordCount>0)
                    {
                      
                    while (!oRecordSetDescuento.EoF)
                    {
                        InvoiceJson.AllowanceCharge allowanceCharge = new InvoiceJson.AllowanceCharge();
                        InvoiceJson.ChargeIndicator chargeIndicator = new InvoiceJson.ChargeIndicator();
                        InvoiceJson.AllowanceChargeReasonCode allowanceChargeReasonCode = new InvoiceJson.AllowanceChargeReasonCode();
                        InvoiceJson.Amount amount = new InvoiceJson.Amount();
                        InvoiceJson.MultiplierFactorNumeric multiplierFactorNumeric = new InvoiceJson.MultiplierFactorNumeric();
                        InvoiceJson.BaseAmount baseAmount = new InvoiceJson.BaseAmount();
                        InvoiceJson.PerUnitAmount perUnitAmount = new InvoiceJson.PerUnitAmount();
                        Log.Debug("-----------------------------------");
                        chargeIndicator._ = Convert.ToString((dynamic)oRecordSetDescuento.Fields.Item("Invoice.AllowanceCharge.ChargeIndicator._").Value);
                        Log.Debug("-----------------------------------");
                        allowanceChargeReasonCode._ = (dynamic)oRecordSetDescuento.Fields.Item("Invoice.AllowanceCharge.AllowanceChargeReasonCode._").Value;
                        Log.Debug("-----------------------------------");
                        allowanceChargeReasonCode.listAgencyName = (dynamic)oRecordSetDescuento.Fields.Item("Invoice.AllowanceCharge.AllowanceChargeReasonCode.listAgencyName").Value;
                        Log.Debug("-----------------------------------");
                        allowanceChargeReasonCode.listName = (dynamic)oRecordSetDescuento.Fields.Item("Invoice.AllowanceCharge.AllowanceChargeReasonCode.listName").Value;
                        Log.Debug("-----------------------------------");
                        allowanceChargeReasonCode.listSchemeURI = (dynamic)oRecordSetDescuento.Fields.Item("Invoice.AllowanceCharge.AllowanceChargeReasonCode.listSchemeURI").Value;
                        Log.Debug("-----------------------------------");
                        amount._ = Convert.ToString((dynamic)oRecordSetDescuento.Fields.Item("Invoice.AllowanceCharge.Amount._").Value);
                        Log.Debug("-----------------------------------");
                        amount.currencyID = (dynamic)oRecordSetDescuento.Fields.Item("Invoice.AllowanceCharge.Amount.currencyID").Value;
                        Log.Debug("-----------------------------------");
                        multiplierFactorNumeric._ = (dynamic)oRecordSetDescuento.Fields.Item("Invoice.AllowanceCharge.MultiplierFactorNumeric._").Value;
                        Log.Debug("-----------------------------------");
                        baseAmount._ = Convert.ToString((dynamic)oRecordSetDescuento.Fields.Item("Invoice.AllowanceCharge.BaseAmount._").Value);
                        Log.Debug("-----------------------------------");
                        baseAmount.currencyID = Convert.ToString((dynamic)oRecordSetDescuento.Fields.Item("Invoice.AllowanceCharge.BaseAmount.currencyID").Value);
                        Log.Debug("-----------------------------------");
                        perUnitAmount._ = Convert.ToString((dynamic)oRecordSetDescuento.Fields.Item("Invoice.AllowanceCharge.PerUnitAmount._").Value);
                        Log.Debug("-----------------------------------");
                        perUnitAmount.currencyID = Convert.ToString((dynamic)oRecordSetDescuento.Fields.Item("Invoice.AllowanceCharge.PerUnitAmount.currencyID").Value);
                        Log.Debug("-----------------------------------");
                        allowanceCharge.ChargeIndicator.Add(chargeIndicator);
                        allowanceCharge.Amount.Add(amount);
                        allowanceCharge.AllowanceChargeReasonCode.Add(allowanceChargeReasonCode);
                        if (!string.IsNullOrEmpty(multiplierFactorNumeric._))
                        {
                            allowanceCharge.MultiplierFactorNumeric.Add(multiplierFactorNumeric);
                        }
                        else
                        {
                            allowanceCharge.MultiplierFactorNumeric = null;
                        }
                        if (!string.IsNullOrEmpty(baseAmount._) && !baseAmount._.Equals("0"))
                        {
                            allowanceCharge.BaseAmount.Add(baseAmount);
                        }
                        else
                        {
                            allowanceCharge.BaseAmount = null;
                        }
                        if (!string.IsNullOrEmpty(perUnitAmount.currencyID) && !perUnitAmount._.Equals("0"))
                        {
                            allowanceCharge.PerUnitAmount.Add(perUnitAmount);
                        }
                        else
                        {
                            allowanceCharge.PerUnitAmount = null;
                        }
                        oFactura.AllowanceCharge.Add(allowanceCharge);
                        Log.Debug("-----------------------------------");
                        oRecordSetDescuento.MoveNext();
                    }
                }
                    //if ((!string.IsNullOrEmpty(Convert.ToString((dynamic)oRecordSetCabecera.Fields.Item("Invoice.AdditionalDocumentReference.ID").Value))))
                    //{
                    //    Log.Debug("-----------------------------------");
                    //    InvoiceJson.AdditionalDocumentReference additionalDocumentReference = new InvoiceJson.AdditionalDocumentReference();
                    //    additionalDocumentReference.ID.Add(new InvoiceJson.ID
                    //    {
                    //        IdentifierContent = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.AdditionalDocumentReference.ID").Value
                    //    });
                    //    Log.Debug("-----------------------------------");
                    //    additionalDocumentReference.DocumentTypeCode.Add(new InvoiceJson.DocumentTypeCode
                    //    {
                    //        CodeContent = "02",
                    //        CodeListSchemeUniformResourceIdentifier = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo12",
                    //        CodeListAgencyNameText = "PE:SUNAT",
                    //        CodeListNameText = "Documento Relacionado"
                    //    });
                    //    Log.Debug("-----------------------------------");
                    //    additionalDocumentReference.DocumentStatusCode.Add(new InvoiceJson.DocumentStatusCode
                    //    {
                    //        CodeContent = "01",
                    //        CodeListAgencyNameText = "PE:SUNAT",
                    //        CodeListNameText = "Anticipo"
                    //    });
                    //    Log.Debug("-----------------------------------");
                    //    List<InvoiceJson.PartyIdentification> lstParty = new List<InvoiceJson.PartyIdentification>();
                    //    Log.Debug("-----------------------------------");
                    //    List<InvoiceJson.ID> lstID = new List<InvoiceJson.ID>();
                    //    Log.Debug("-----------------------------------");
                    //    lstID.Add(new InvoiceJson.ID
                    //    {
                    //        IdentifierContent = Convert.ToString((dynamic)oRecordSetCabecera.Fields.Item("Invoice.Signature.SignatoryParty.PartyIdentification.ID.TextContent").Value),
                    //        schemeID = "6",
                    //        SchemeName = "Documento de Identidad",
                    //        schemeAgencyName = "PE:SUNAT",
                    //        schemeURI = "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06"
                    //    });
                    //    Log.Debug("-----------------------------------");
                    //    lstParty.Add(new InvoiceJson.PartyIdentification
                    //    {
                    //        ID = lstID
                    //    });
                    //    Log.Debug("-----------------------------------");
                    //    additionalDocumentReference.IssuerParty.Add(new InvoiceJson.IssuerParty
                    //    {
                    //        PartyIdentification = lstParty
                    //    });
                    //    Log.Debug("-----------------------------------");
                    //    oFactura.AdditionalDocumentReference.Add(additionalDocumentReference);
                    //    Log.Debug("-----------------------------------");
                    //    List<InvoiceJson.ID> listID2 = new List<InvoiceJson.ID>();
                    //    Log.Debug("-----------------------------------");
                    //    List<InvoiceJson.PaidAmount> listPaidAmount = new List<InvoiceJson.PaidAmount>();
                    //    Log.Debug("-----------------------------------");
                    //    List<InvoiceJson.PaidDate> listPaidDate = new List<InvoiceJson.PaidDate>();
                    //    listPaidDate.Add(new InvoiceJson.PaidDate
                    //    {
                    //        _ = Convert.ToString((dynamic)oRecordSetCabecera.Fields.Item("Invoice.PrepaidPayment.PaidDate._").Value)
                    //    });
                    //    listPaidAmount.Add(new InvoiceJson.PaidAmount
                    //    {
                    //        _ = Convert.ToString((dynamic)oRecordSetCabecera.Fields.Item("Invoice.PrepaidPayment.PaidAmount._").Value),
                    //        currencyID = Convert.ToString((dynamic)oRecordSetCabecera.Fields.Item("Invoice.PrepaidPayment.PaidAmount.currencyID").Value)
                    //    });
                    //    Log.Debug("-----------------------------------");
                    //    listID2.Add(new InvoiceJson.ID
                    //    {
                    //        IdentifierContent = "01",
                    //        SchemeName = "Anticipo",
                    //        schemeAgencyName = "PE:SUNAT"
                    //    });
                    //    Log.Debug("-----------------------------------");
                    //    oFactura.PrepaidPayment.Add(new InvoiceJson.PrepaidPayment
                    //    {
                    //        ID = listID2,
                    //        PaidAmount = listPaidAmount,
                    //        PaidDate = listPaidDate
                    //    });
                    //    Log.Debug("-----------------------------------");
                    //}
                    //else
                    //{
                    //    Log.Debug("-----------------------------------");
                    //    oFactura.AdditionalDocumentReference = null;
                    //    Log.Debug("-----------------------------------");
                    //    oFactura.PrepaidPayment = null;
                    //}
                    Log.Debug("-----------------------------------");
                    oRecordSetImpuestoCabecera.DoQuery(sQueryExtractHeaderTaxes);
                    if (oRecordSetImpuestoCabecera.RecordCount > 0)
                    {
                        List<InvoiceJson.TaxSubtotal> taxSubtotals = new List<InvoiceJson.TaxSubtotal>();
                        Log.Debug("-----------------------------------");
                        while (!oRecordSetImpuestoCabecera.EoF)
                        {
                            Log.Debug("-----------------------------------");
                            InvoiceJson.TaxSubtotal taxSubtotal2 = new InvoiceJson.TaxSubtotal();
                            InvoiceJson.TaxableAmount taxableAmount2 = new InvoiceJson.TaxableAmount();
                            InvoiceJson.TaxAmount taxAmount2 = new InvoiceJson.TaxAmount();
                            InvoiceJson.TaxCategory taxCategory2 = new InvoiceJson.TaxCategory();
                            InvoiceJson.TaxScheme taxScheme2 = new InvoiceJson.TaxScheme();
                            InvoiceJson.ID iD6 = new InvoiceJson.ID();
                            InvoiceJson.Name name3 = new InvoiceJson.Name();
                            InvoiceJson.TaxTypeCode taxTypeCode2 = new InvoiceJson.TaxTypeCode();
                            Log.Debug("-----------------------------------");
                            iD6.SchemeName = (dynamic)oRecordSetImpuestoCabecera.Fields.Item("Invoice.TaxTotal.TaxSubtotal.TaxCategory.TaxScheme.ID.schemeName").Value;
                            Log.Debug("-----------------------------------");
                            iD6._ = (dynamic)oRecordSetImpuestoCabecera.Fields.Item("Invoice.TaxTotal.TaxSubtotal.TaxCategory.TaxScheme.ID._").Value;
                            Log.Debug("-----------------------------------");
                            iD6.schemeURI = (dynamic)oRecordSetImpuestoCabecera.Fields.Item("Invoice.TaxTotal.TaxSubtotal.TaxCategory.TaxScheme.ID.schemeURI").Value;
                            Log.Debug("-----------------------------------");
                            iD6.schemeAgencyName = (dynamic)oRecordSetImpuestoCabecera.Fields.Item("Invoice.TaxTotal.TaxSubtotal.TaxCategory.TaxScheme.ID.schemeAgencyName").Value;
                            Log.Debug("-----------------------------------");
                            taxAmount2.currencyID = (dynamic)oRecordSetImpuestoCabecera.Fields.Item("Invoice.TaxTotal.TaxSubtotal.TaxAmount.currencyID").Value;
                            Log.Debug("-----------------------------------");
                            taxAmount2._ = (dynamic)oRecordSetImpuestoCabecera.Fields.Item("Invoice.TaxTotal.TaxSubtotal.TaxAmount._").Value;
                            Log.Debug("-----------------------------------");
                            taxableAmount2._ = (dynamic)oRecordSetImpuestoCabecera.Fields.Item("Invoice.TaxTotal.TaxSubtotal.TaxableAmount._").Value;
                            Log.Debug("-----------------------------------");
                            taxableAmount2.currencyID = (dynamic)oRecordSetImpuestoCabecera.Fields.Item("Invoice.TaxTotal.TaxSubtotal.TaxableAmount.currencyID").Value;
                            Log.Debug("-----------------------------------");
                            name3._ = (dynamic)oRecordSetImpuestoCabecera.Fields.Item("Invoice.TaxTotal.TaxSubtotal.TaxCategory.TaxScheme.Name._").Value;
                            Log.Debug("-----------------------------------");
                            taxTypeCode2._ = (dynamic)oRecordSetImpuestoCabecera.Fields.Item("Invoice.TaxTotal.TaxSubtotal.TaxCategory.TaxScheme.TaxTypeCode._").Value;
                            Log.Debug("-----------------------------------");
                            taxScheme2.ID.Add(iD6);
                            taxScheme2.Name.Add(name3);
                            taxScheme2.TaxTypeCode.Add(taxTypeCode2);
                            taxCategory2.TaxScheme.Add(taxScheme2);
                            taxSubtotal2.TaxableAmount.Add(taxableAmount2);
                            taxSubtotal2.TaxAmount.Add(taxAmount2);
                            taxSubtotal2.TaxCategory.Add(taxCategory2);
                            taxSubtotals.Add(taxSubtotal2);
                            Log.Debug("-----------------------------------");
                            oRecordSetImpuestoCabecera.MoveNext();
                        }
                        Log.Debug("-----------------------------------");
                        InvoiceJson.TaxTotal oTaxTotal = new InvoiceJson.TaxTotal();
                        InvoiceJson.TaxAmount taxAmount3 = new InvoiceJson.TaxAmount();
                        Log.Debug("-----------------------------------");
                        taxAmount3._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.TaxTotal.TaxAmount._").Value.ToString();
                        Log.Debug("-----------------------------------");
                        taxAmount3.currencyID = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.TaxTotal.TaxAmount.currencyID").Value;
                        Log.Debug("-----------------------------------");
                        oTaxTotal.TaxAmount.Add(taxAmount3);
                        oTaxTotal.TaxSubtotal = taxSubtotals;
                        oFactura.TaxTotal.Add(oTaxTotal);
                        Log.Debug("-----------------------------------");
                    }






                    InvoiceJson.LegalMonetaryTotal legalMonetaryTotal = new InvoiceJson.LegalMonetaryTotal();
                    InvoiceJson.LineExtensionAmount lineExtensionAmount = new InvoiceJson.LineExtensionAmount();
                    InvoiceJson.TaxInclusiveAmount taxInclusiveAmount = new InvoiceJson.TaxInclusiveAmount();
                    InvoiceJson.PayableAmount payableAmount = new InvoiceJson.PayableAmount();
                    InvoiceJson.AllowanceTotalAmount allowanceTotalAmount = new InvoiceJson.AllowanceTotalAmount();
                    InvoiceJson.PrepaidAmount prepaidAmount = new InvoiceJson.PrepaidAmount();   
                    Log.Debug("-----------------------------------");
                    lineExtensionAmount._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.LegalMonetaryTotal.LineExtensionAmount._").Value;
                    Log.Debug("-----------------------------------");
                    lineExtensionAmount.currencyID = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.LegalMonetaryTotal.LineExtensionAmount.currencyID").Value;
                    Log.Debug("-----------------------------------");
                    taxInclusiveAmount._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.LegalMonetaryTotal.TaxInclusiveAmount._").Value;
                    Log.Debug("-----------------------------------");
                    taxInclusiveAmount.currencyID = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.LegalMonetaryTotal.TaxInclusiveAmount.currencyID").Value;
                    Log.Debug("-----------------------------------");
                    payableAmount._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.LegalMonetaryTotal.PayableAmount._").Value;
                    Log.Debug("-----------------------------------");
                    payableAmount.currencyID = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.LegalMonetaryTotal.TaxInclusiveAmount.currencyID").Value;
                    Log.Debug("-----------------------------------");
                    allowanceTotalAmount._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.LegalMonetaryTotal.AllowanceTotalAmount._").Value;
                    Log.Debug("-----------------------------------");
                    allowanceTotalAmount.currencyID = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.LegalMonetaryTotal.AllowanceTotalAmount.currencyID").Value;
                    Log.Debug("-----------------------------------");
                    prepaidAmount._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.LegalMonetaryTotal.PrepaidAmount._").Value;
                    Log.Debug("-----------------------------------");
                    prepaidAmount.currencyID = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.LegalMonetaryTotal.PrepaidAmount.currencyID").Value;
                    Log.Debug("-----------------------------------");
                    if ((!string.IsNullOrEmpty(Convert.ToString((dynamic)oRecordSetCabecera.Fields.Item("Invoice.LegalMonetaryTotal.PrepaidAmount.currencyID").Value))))
                    {
                        Log.Debug("-----------------------------------");
                        legalMonetaryTotal.PrepaidAmount.Add(prepaidAmount);
                       
                        Log.Debug("-----------------------------------");
                    }
                    else
                    {
                        legalMonetaryTotal.PrepaidAmount = null;
                    }
                    legalMonetaryTotal.LineExtensionAmount.Add(lineExtensionAmount);
                    legalMonetaryTotal.TaxInclusiveAmount.Add(taxInclusiveAmount);
                    legalMonetaryTotal.PayableAmount.Add(payableAmount);
                    if ((!string.IsNullOrEmpty((dynamic)oRecordSetCabecera.Fields.Item("Invoice.LegalMonetaryTotal.AllowanceTotalAmount.currencyID").Value)))
                    {
                        legalMonetaryTotal.AllowanceTotalAmount.Add(allowanceTotalAmount);
                    }
                    else
                    {
                        legalMonetaryTotal.AllowanceTotalAmount = null;
                    }
                    oFactura.LegalMonetaryTotal.Add(legalMonetaryTotal);
                    Log.Debug("-----------------------------------");
                    //if ((!string.IsNullOrEmpty((dynamic)oRecordSetCabecera.Fields.Item("Invoice.PaymentTerms.ID.IdentifierContent").Value)))
                    //{
                    //    Log.Debug("-----------------------------------");
                    //    string sCodigo = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.PaymentTerms.ID.IdentifierContent").Value;
                    //    Log.Debug("-----------------------------------");
                    //    if (sCodigo.Equals("Percepcion"))
                    //    {
                    //        InvoiceJson.PaymentTerm paymentTerm2 = new InvoiceJson.PaymentTerm();
                    //        InvoiceJson.ID iD17 = new InvoiceJson.ID();
                    //        InvoiceJson.Amount amount3 = new InvoiceJson.Amount();
                    //        Log.Debug("-----------------------------------");
                    //        iD17.IdentifierContent = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.PaymentTerms.ID.IdentifierContent").Value;
                    //        Log.Debug("-----------------------------------");
                    //        amount3._ = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.PaymentTerms.Amount.AmountContent").Value;
                    //        Log.Debug("-----------------------------------");
                    //        amount3.currencyID = (dynamic)oRecordSetCabecera.Fields.Item("Invoice.PaymentTerms.Amount.AmountCurrencyIdentifier").Value;
                    //        Log.Debug("-----------------------------------");
                    //        paymentTerm2.ID.Add(iD17);
                    //        if (!string.IsNullOrEmpty(amount3.currencyID))
                    //        {
                    //            paymentTerm2.Amount.Add(amount3);
                    //        }
                    //        else
                    //        {
                    //            paymentTerm2.Amount = null;
                    //        }
                    //        oFactura.PaymentTerms.Add(paymentTerm2);
                    //    }
                    //}
                    Log.Debug(sQueryExtractCuota ?? "");
                    oRecordSetCuota.DoQuery(sQueryExtractCuota);
                    while (!oRecordSetCuota.EoF)
                    {
                        Log.Debug("-----------------------------------");
                        InvoiceJson.PaymentTerm paymentTerm = new InvoiceJson.PaymentTerm();
                        Log.Debug("-----------------------------------");
                        InvoiceJson.ID iD16 = new InvoiceJson.ID();
                        Log.Debug("-----------------------------------");
                        InvoiceJson.Amount amount2 = new InvoiceJson.Amount();
                        Log.Debug("-----------------------------------");
                        InvoiceJson.PaymentMeansID paymentMeansID = new InvoiceJson.PaymentMeansID();
                        Log.Debug("-----------------------------------");
                        paymentMeansID._ = Convert.ToString((dynamic)oRecordSetCuota.Fields.Item("Invoice.PaymentTerms.PaymentMeansID._").Value);
                        Log.Debug("-----------------------------------");
                        iD16._ = Convert.ToString((dynamic)oRecordSetCuota.Fields.Item("InvoicePaymentTerms.ID._").Value);
                        Log.Debug("-----------------------------------");
                        amount2._ = Convert.ToString((dynamic)oRecordSetCuota.Fields.Item("Invoice.PaymentTerms.Amount._").Value);
                        Log.Debug("-----------------------------------");
                        amount2.currencyID = Convert.ToString((dynamic)oRecordSetCuota.Fields.Item("Invoice.PaymentTerms.Amount.currencyID").Value);
                        Log.Debug("-----------------------------------");
                        paymentTerm.ID.Add(iD16);
                        if (!string.IsNullOrEmpty(amount2.currencyID))
                        {
                            paymentTerm.Amount.Add(amount2);
                        }
                        paymentTerm.PaymentMeansID.Add(paymentMeansID);
                        if ((!string.IsNullOrEmpty(Convert.ToString((dynamic)oRecordSetCuota.Fields.Item("Invoice.PaymentTerms.PaymentDueDate._").Value))))
                        {
                            paymentTerm.PaymentDueDate.Add(new InvoiceJson.PaymentDueDate
                            {
                                _ = (dynamic)oRecordSetCuota.Fields.Item("Invoice.PaymentTerms.PaymentDueDate._").Value
                            });
                        }
                        oFactura.PaymentTerms.Add(paymentTerm);
                        oRecordSetCuota.MoveNext();
                    }
                    oRecordSetDetalles.DoQuery(sQueryExtractDetails);
                    while (!oRecordSetDetalles.EoF)
                    {
                        List<InvoiceJson.TaxSubtotal> taxDetailSubtotals = new List<InvoiceJson.TaxSubtotal>();
                        Log.Debug("-----------------------------------");
                        InvoiceJson.InvoiceLine invoiceLine = new InvoiceJson.InvoiceLine();
                        InvoiceJson.ID iD5 = new InvoiceJson.ID();
                        InvoiceJson.InvoicedQuantity invoicedQuantity = new InvoiceJson.InvoicedQuantity();
                        InvoiceJson.LineExtensionAmount lineExtensionAmount2 = new InvoiceJson.LineExtensionAmount();
                        InvoiceJson.PricingReference pricingReference = new InvoiceJson.PricingReference();
                        InvoiceJson.AlternativeConditionPrice alternativeConditionPrice = new InvoiceJson.AlternativeConditionPrice();
                        InvoiceJson.PriceAmount priceAmount = new InvoiceJson.PriceAmount();
                        InvoiceJson.PriceTypeCode priceTypeCode = new InvoiceJson.PriceTypeCode();
                        InvoiceJson.Item item = new InvoiceJson.Item();
                        InvoiceJson.Description description = new InvoiceJson.Description();
                        InvoiceJson.SellersItemIdentification sellersItemIdentification = new InvoiceJson.SellersItemIdentification();
                        InvoiceJson.ID iD9 = new InvoiceJson.ID();
                        InvoiceJson.Note note = new InvoiceJson.Note();
                        InvoiceJson.Price price = new InvoiceJson.Price();
                        InvoiceJson.PriceAmount priceAmount2 = new InvoiceJson.PriceAmount();
                        InvoiceJson.TaxTotal taxTotal = new InvoiceJson.TaxTotal();
                        InvoiceJson.BillingReference billingReference = new InvoiceJson.BillingReference();
                        InvoiceJson.BillingReferenceLine billingReferenceLine = new InvoiceJson.BillingReferenceLine();
                        InvoiceJson.ID iD13 = new InvoiceJson.ID();
                        //iD13._ = Convert.ToString((dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.LineExtensionAmount._").Value);
                        //iD13.schemeID = "AL";
                        //billingReferenceLine.ID.Add(iD13);
                        //billingReference.BillingReferenceLine.Add(billingReferenceLine);
                        Log.Debug("-----------------------------------");
                        iD5._ = Convert.ToString((dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.ID._").Value);
                        Log.Debug("-----------------------------------");
                        invoicedQuantity._ = Convert.ToString((dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.InvoicedQuantity._").Value);
                        Log.Debug("-----------------------------------");
                        invoicedQuantity.unitCode = (dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.InvoicedQuantity.unitCode").Value;
                        Log.Debug("-----------------------------------");
                        invoicedQuantity.unitCodeListD = (dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.InvoicedQuantity.unitCodeListD").Value;
                        Log.Debug("-----------------------------------");
                        invoicedQuantity.unitCodeListAgencyName = (dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.InvoicedQuantity.unitCodeListAgencyName").Value;
                        Log.Debug("-----------------------------------");
                        lineExtensionAmount2._ = Convert.ToString((dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.LineExtensionAmount._").Value);
                        Log.Debug("-----------------------------------");
                        lineExtensionAmount2.currencyID = Convert.ToString((dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.LineExtensionAmount.currencyID").Value);
                        Log.Debug("-----------------------------------");
                        priceAmount._ = Convert.ToString((dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.PricingReference.AlternativeConditionPrice.PriceAmount._").Value);
                        Log.Debug("-----------------------------------");
                        priceAmount.currencyID = Convert.ToString((dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.PricingReference.AlternativeConditionPrice.PriceAmount.currencyID").Value);
                        Log.Debug("-----------------------------------");
                        priceTypeCode._ = (dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.PricingReference.AlternativeConditionPrice.PriceTypeCode._").Value;
                        Log.Debug("-----------------------------------");
                        priceTypeCode.listName = (dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.PricingReference.AlternativeConditionPrice.PriceTypeCode.listName").Value;
                        Log.Debug("-----------------------------------");
                        priceTypeCode.listAgencyName = (dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.PricingReference.AlternativeConditionPrice.PriceTypeCode.listAgencyName").Value;
                        Log.Debug("-----------------------------------");
                        priceTypeCode.listURI = (dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.PricingReference.AlternativeConditionPrice.PriceTypeCode.listURI").Value;
                        Log.Debug("-----------------------------------");
                        description._ = (dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.Item.Description._").Value;
                        Log.Debug("-----------------------------------");
                        iD9._ = (dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.Item.SellersItemIdentification.ID._").Value;
                        Log.Debug("-----------------------------------");
                        note._ = Convert.ToString((dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.Note._").Value);
                        Log.Debug("-----------------------------------");
                        priceAmount2.currencyID = (dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.Price.PriceAmount.currencyID").Value;
                        Log.Debug("-----------------------------------");
                        priceAmount2._ = (dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.Price.PriceAmount._").Value;
                        Log.Debug("-----------------------------------");
                        Log.Debug("-----------------------------------");
                        InvoiceJson.TaxSubtotal taxSubtotal = new InvoiceJson.TaxSubtotal();
                        InvoiceJson.TaxableAmount taxableAmount = new InvoiceJson.TaxableAmount();
                        InvoiceJson.TaxAmount taxAmount = new InvoiceJson.TaxAmount();
                        InvoiceJson.TaxCategory taxCategory = new InvoiceJson.TaxCategory();
                        InvoiceJson.TaxScheme taxScheme = new InvoiceJson.TaxScheme();
                        InvoiceJson.ID iD12 = new InvoiceJson.ID();
                        InvoiceJson.Name name2 = new InvoiceJson.Name();
                        InvoiceJson.TaxTypeCode taxTypeCode = new InvoiceJson.TaxTypeCode();
                        InvoiceJson.TaxAmount taxAmount4 = new InvoiceJson.TaxAmount();
                        InvoiceJson.Percent percent = new InvoiceJson.Percent();
                        InvoiceJson.TaxExemptionReasonCode taxExemptionReasonCode = new InvoiceJson.TaxExemptionReasonCode();
                        InvoiceJson.AllowanceCharge2 detailAllowanceCharge = new InvoiceJson.AllowanceCharge2();
                        //    string sDescuento = Convert.ToString(Convert.ToString((dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.AllowanceCharge.ChargeIndicator.IndicatorContent").Value));
                        if (SapBL.oCompany.DbServerType == BoDataServerTypes.dst_HANADB)
                        {
                            sQueryExtractDescuentoDetalle = $"CALL \"{sQueryDescuentoDetalle}\" ('{sObjType}','{sDocEntry}','{iD9._}');";
                            Log.Debug("-----------------------------");
                        }
                        else
                        {
                            sQueryExtractDescuentoDetalle = $"EXEC \"{sQueryDescuentoDetalle}\" '{sDocEntry}','{sObjType}','{iD9._}'";
                            Log.Debug("-----------------------------");
                        }
                        oRecordSetDescuentoDetalle.DoQuery(sQueryExtractDescuentoDetalle);
                        if (oRecordSetDescuentoDetalle.RecordCount >0)
                        { 
                        while (!oRecordSetDescuentoDetalle.EoF)
                        {
                                string sDescuento = Convert.ToString(Convert.ToString((dynamic)oRecordSetDescuentoDetalle.Fields.Item("Invoice.InvoiceLine.Allowancecharge.ChargeIndicator._").Value));

                                if (!string.IsNullOrEmpty(sDescuento))
                                {
                                    Log.Debug("-----------------------------------");
                                    detailAllowanceCharge.ChargeIndicator.Add(new InvoiceJson.ChargeIndicator
                                    {
                                        _ = Convert.ToString((dynamic)oRecordSetDescuentoDetalle.Fields.Item("Invoice.InvoiceLine.Allowancecharge.ChargeIndicator._").Value)
                                    });
                                    Log.Debug("-----------------------------------");
                                    detailAllowanceCharge.AllowanceChargeReasonCode.Add(new InvoiceJson.AllowanceChargeReasonCode
                                    {
                                        _ = Convert.ToString((dynamic)oRecordSetDescuentoDetalle.Fields.Item("Invoice.InvoiceLine.Allowancecharge.AllowanceChargeReasonCode._").Value),
                                        listAgencyName = Convert.ToString((dynamic)oRecordSetDescuentoDetalle.Fields.Item("Invoice.InvoiceLine.Allowancecharge.AllowanceChargeReasonCode.listAgencyName").Value),
                                        listName = Convert.ToString((dynamic)oRecordSetDescuentoDetalle.Fields.Item("Invoice.InvoiceLine.Allowancecharge.AllowanceChargeReasonCode.listName").Value),
                                        listSchemeURI = Convert.ToString((dynamic)oRecordSetDescuentoDetalle.Fields.Item("Invoice.InvoiceLine.Allowancecharge.AllowanceChargeReasonCode.listSchemeURI").Value)
                                    });
                                    Log.Debug("-----------------------------------");
                                  
                                    
                                    detailAllowanceCharge.MultiplierFactorNumeric.Add(new InvoiceJson.MultiplierFactorNumeric
                                    {
                                        _ = Convert.ToString((dynamic)oRecordSetDescuentoDetalle.Fields.Item("Invoice.InvoiceLine.Allowancecharge.MultiplierFactorNumeric._").Value),
                                        });
                                    Log.Debug("-----------------------------------");
                                    detailAllowanceCharge.Amount.Add(new InvoiceJson.Amount
                                    {
                                        _ = Convert.ToString((dynamic)oRecordSetDescuentoDetalle.Fields.Item("Invoice.InvoiceLine.Allowancecharge.Amount._").Value),
                                        currencyID = Convert.ToString((dynamic)oRecordSetDescuentoDetalle.Fields.Item("Invoice.InvoiceLine.Allowancecharge.Amount.currencyID").Value)
                                    });
                                  
                                    Log.Debug("-----------------------------------");
                                    detailAllowanceCharge.BaseAmount.Add(new InvoiceJson.BaseAmount
                                    {
                                        _ = Convert.ToString((dynamic)oRecordSetDescuentoDetalle.Fields.Item("Invoice.InvoiceLine.Allowancecharge.BaseAmount._").Value),
                                        currencyID = Convert.ToString((dynamic)oRecordSetDescuentoDetalle.Fields.Item("Invoice.InvoiceLine.Allowancecharge.BaseAmount.currencyID").Value)
                                    });
                                    Log.Debug("-----------------------------------");
                                    invoiceLine.AllowanceCharge.Add(detailAllowanceCharge);
                                    Log.Debug("-----------------------------------");
                                }
                                oRecordSetDescuentoDetalle.MoveNext();
                            }
                   
                        }
                          
                        Log.Debug("-----------------------------------");
                        iD12.SchemeName = (dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.TaxTotal.TaxSubtotal.TaxCategory.TaxScheme.ID.schemeName").Value;
                        Log.Debug("-----------------------------------");
                        iD12._ = (dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.TaxTotal.TaxSubtotal.TaxCategory.TaxScheme.ID._").Value;
                        Log.Debug("-----------------------------------");
                        iD12.schemeURI = (dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.TaxTotal.TaxSubtotal.TaxCategory.TaxScheme.ID.schemeURI").Value;
                        Log.Debug("-----------------------------------");
                        iD12.schemeAgencyName = (dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.TaxTotal.TaxSubtotal.TaxCategory.TaxScheme.ID.schemeAgencyName").Value;
                        Log.Debug("-----------------------------------");
                        taxAmount.currencyID = (dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.TaxTotal.TaxSubtotal.TaxAmount.currencyID").Value;
                        Log.Debug("-----------------------------------");
                        taxAmount._ = (dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.TaxTotal.TaxSubtotal.TaxAmount._").Value;
                        Log.Debug("-----------------------------------");
                        taxableAmount._ = (dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.TaxTotal.TaxSubtotal.TaxableAmount._").Value;
                        Log.Debug("-----------------------------------");
                        taxableAmount.currencyID = (dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.TaxTotal.TaxSubtotal.TaxableAmount.currencyID").Value;
                        Log.Debug("-----------------------------------");
                        name2._ = (dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.TaxTotal.TaxSubtotal.TaxCategory.TaxScheme.Name._").Value;
                        Log.Debug("-----------------------------------");
                        taxTypeCode._ = (dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.axTotal.TaxSubtotal.TaxCategory.TaxScheme.TaxTypeCode._").Value;
                        Log.Debug("-----------------------------------");
                        taxAmount4._ = (dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.TaxTotal.TaxAmount._").Value;
                        Log.Debug("-----------------------------------");
                        taxAmount4.currencyID = (dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.TaxTotal.TaxAmount.currencyID").Value;
                        Log.Debug("-----------------------------------");
                        percent._ = Convert.ToDouble((dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.TaxTotal.TaxSubtotal.TaxCategory.Percent._").Value);
                        Log.Debug("-----------------------------------");
                        taxExemptionReasonCode.listAgencyName = (dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.TaxTotal.TaxSubtotal.TaxCategory.TaxExemptionReasonCode.listAgencyName").Value;
                        Log.Debug("-----------------------------------");
                        taxExemptionReasonCode._ = (dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.TaxTotal.TaxSubtotal.TaxCategory.TaxExemptionReasonCode._").Value;
                        Log.Debug("-----------------------------------");
                        taxExemptionReasonCode.listName = (dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.TaxTotal.TaxSubtotal.TaxCategory.TaxExemptionReasonCode.listName").Value;
                        Log.Debug("-----------------------------------");
                        taxExemptionReasonCode.listURI = (dynamic)oRecordSetDetalles.Fields.Item("Invoice.InvoiceLine.TaxTotal.TaxSubtotal.TaxCategory.TaxExemptionReasonCode.listURI").Value;
                        Log.Debug("-----------------------------------");
                        taxScheme.ID.Add(iD12);
                        taxScheme.Name.Add(name2);
                        taxScheme.TaxTypeCode.Add(taxTypeCode);
                        taxCategory.TaxScheme.Add(taxScheme);
                        taxCategory.Percent.Add(percent);
                        taxCategory.TaxExemptionReasonCode.Add(taxExemptionReasonCode);
                        taxSubtotal.TaxableAmount.Add(taxableAmount);
                        taxSubtotal.TaxAmount.Add(taxAmount);
                        taxSubtotal.TaxCategory.Add(taxCategory);
                        taxDetailSubtotals.Add(taxSubtotal);
                        Log.Debug("-----------------------------------");
                        taxTotal.TaxAmount.Add(taxAmount4);
                        taxTotal.TaxSubtotal = taxDetailSubtotals;
                        price.PriceAmount.Add(priceAmount2);
                        sellersItemIdentification.ID.Add(iD9);
                        item.Description.Add(description);
                        item.SellersItemIdentification.Add(sellersItemIdentification);
                        alternativeConditionPrice.PriceAmount.Add(priceAmount);
                        alternativeConditionPrice.PriceTypeCode.Add(priceTypeCode);
                        pricingReference.AlternativeConditionPrice.Add(alternativeConditionPrice);
                   //     invoiceLine.BillingReference.Add(billingReference);
                        invoiceLine.ID.Add(iD5);
                        invoiceLine.InvoicedQuantity.Add(invoicedQuantity);
                        invoiceLine.Item.Add(item);
                        invoiceLine.LineExtensionAmount.Add(lineExtensionAmount2);
                        invoiceLine.Note.Add(note);
                        invoiceLine.Price.Add(price);
                        invoiceLine.PricingReference.Add(pricingReference);
                        invoiceLine.TaxTotal.Add(taxTotal);
                        oFactura.InvoiceLine.Add(invoiceLine);
                        oRecordSetDetalles.MoveNext();
                    }
                    Log.Debug("-----------------------------------");
                    oRecordSetCabecera.MoveNext();
                    Log.Debug("-----------------------------------");
                }
                oInvoice.Invoice.Add(oFactura);
                return oInvoice;
            }
            catch (Exception ex)
            {
                Log.Debug($"Se presentó el siguiente ERROR :: {ex.Message}");
                return null;
            }
        }
    }
}
