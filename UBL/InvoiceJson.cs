using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBL
{
    public class InvoiceJson
    {
        public class UBLVersionID
        {
            public string _ { get; set; }
        }

        public class PaymentPercent
        {
            public string _ { get; set; }
        }

        public class CustomizationID
        {
            public string _ { get; set; }
        }

        public class PaymentMeansCode
        {
            public string _ { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string listName { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string listAgencyName { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string listURI { get; set; }
        }

        public class PayeeFinancialAccount
        {
            public List<ID> ID { get; set; }

            public PayeeFinancialAccount()
            {
                ID = new List<ID>();
            }
        }

        public class ID
        {
            public string _ { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string IdentifierContent { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string schemeID { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string SchemeName { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string schemeAgencyName { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string schemeURI { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string TextContent { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string IdentificationSchemeIdentifier { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string IdentificationSchemeNameText { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string IdentificationSchemeAgencyNameText { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string IdentificationSchemeUniformResourceIdentifier { get; set; }
        }

        public class IssueDate
        {
            public string _ { get; set; }
        }

        public class DueDate
        {
            public string _ { get; set; }
        }

        public class IssueTime
        {
            public string _ { get; set; }
        }

        public class InvoiceTypeCode
        {
            public string _ { get; set; }

            public string listName { get; set; }

            public string listSchemeURI { get; set; }

            public string listID { get; set; }

            public string name { get; set; }

            public string listURI { get; set; }

            public string listAgencyName { get; set; }
        }

        public class Note
        {
            public string _ { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string languageLocaleID { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string languageID { get; set; }
        }

        public class DocumentCurrencyCode
        {
            public string _ { get; set; }

            public string listID { get; set; }

            public string listName { get; set; }

            public string listAgencyName { get; set; }
        }

        public class LineCountNumeric
        {
            public int _ { get; set; }
        }

        public class PartyIdentification
        {
            public List<ID> ID { get; set; }

            public PartyIdentification()
            {
                ID = new List<ID>();
            }
        }

        public class Name
        {
            public string _ { get; set; }
        }

        public class PartyName
        {
            public List<Name> Name { get; set; }

            public PartyName()
            {
                Name = new List<Name>();
            }
        }

        public class SignatoryParty
        {
            public List<PartyIdentification> PartyIdentification { get; set; }

            public List<PartyName> PartyName { get; set; }

            public SignatoryParty()
            {
                PartyIdentification = new List<PartyIdentification>();
                PartyName = new List<PartyName>();
            }
        }

        public class URI
        {
            public string _ { get; set; }
        }

        public class ExternalReference
        {
            public List<URI> URI { get; set; }

            public ExternalReference()
            {
                URI = new List<URI>();
            }
        }

        public class DigitalSignatureAttachment
        {
            public List<ExternalReference> ExternalReference { get; set; }

            public DigitalSignatureAttachment()
            {
                ExternalReference = new List<ExternalReference>();
            }
        }

        public class Signature
        {
            public List<ID> ID { get; set; }

            public List<SignatoryParty> SignatoryParty { get; set; }

            public List<DigitalSignatureAttachment> DigitalSignatureAttachment { get; set; }

            public Signature()
            {
                ID = new List<ID>();
                SignatoryParty = new List<SignatoryParty>();
                DigitalSignatureAttachment = new List<DigitalSignatureAttachment>();
            }
        }

        public class RegistrationName
        {
            public string _ { get; set; }
        }

        public class AddressTypeCode
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string _ { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string listAgencyName { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string listName { get; set; }
        }

        public class CityName
        {
            public string _ { get; set; }
        }

        public class CountrySubentity
        {
            public string _ { get; set; }
        }

        public class District
        {
            public string _ { get; set; }
        }
        public class CitySubdivision
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string _ { get; set; }
        }
        public class Line
        {
            public string _ { get; set; }
        }

        public class AddressLine
        {
            public List<Line> Line { get; set; }

            public AddressLine()
            {
                Line = new List<Line>();
            }
        }

        public class IdentificationCode
        {
            public string _ { get; set; }

            public string listID { get; set; }

            public string listAgencyName { get; set; }

            public string listName { get; set; }
        }

        public class Country
        {
            public List<IdentificationCode> IdentificationCode { get; set; }

            public Country()
            {
                IdentificationCode = new List<IdentificationCode>();
            }
        }
        public class StreetName
        {
            public string _ { get; set; }
        }
        public class PostalAddress
        {
            public List<ID> ID { get; set; }
          
            public List<StreetName> StreetName { get; set; }
            public List<CityName> CityName { get; set; }

            public List<CountrySubentity> CountrySubentity { get; set; }

            public List<District> District { get; set; }

      

            public List<Country> Country { get; set; }

            public PostalAddress()
            {
                ID = new List<ID>();
                StreetName = new List<StreetName>();
                CityName = new List<CityName>();
                CountrySubentity = new List<CountrySubentity>();
                District = new List<District>();               
                Country = new List<Country>();
            }
        }
            public class RegistrationAddress
        {
            public List<ID> ID { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<AddressTypeCode> AddressTypeCode { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<CitySubdivision> CitySubdivisionName { get; set; }
            public List<CityName> CityName { get; set; }

            public List<CountrySubentity> CountrySubentity { get; set; }

            public List<District> District { get; set; }

            public List<AddressLine> AddressLine { get; set; }

            public List<Country> Country { get; set; }

            public RegistrationAddress()
            {
                ID = new List<ID>();
                AddressTypeCode = new List<AddressTypeCode>();
                CitySubdivisionName = new List<CitySubdivision>();  
                CityName = new List<CityName>();
                CountrySubentity = new List<CountrySubentity>();
                District = new List<District>();
                AddressLine = new List<AddressLine>();
                Country = new List<Country>();
            }
        }

        public class PartyLegalEntity
        {
            public List<RegistrationName> RegistrationName { get; set; }

            public List<RegistrationAddress> RegistrationAddress { get; set; }

            public PartyLegalEntity()
            {
                RegistrationName = new List<RegistrationName>();
                RegistrationAddress = new List<RegistrationAddress>();
            }
        }

        public class Party
        {
            public List<PartyIdentification> PartyIdentification { get; set; }

            public List<PartyName> PartyName { get; set; }

            public List<PostalAddress> PostalAddress { get; set; }
            public List<PartyLegalEntity> PartyLegalEntity { get; set; }

            public List<Contact> Contact { get; set; }

            public Party()
            {
                PartyIdentification = new List<PartyIdentification>();
                PartyName = new List<PartyName>();
                PostalAddress= new List<PostalAddress>();
                PartyLegalEntity = new List<PartyLegalEntity>();
                Contact = new List<Contact>();
            }
        }

        public class AccountingSupplierParty
        {
            public List<Party> Party { get; set; }

            public AccountingSupplierParty()
            {
                Party = new List<Party>();
            }
        }

        public class ElectronicMail
        {
            public string _ { get; set; }
        }

        public class Contact
        {
            public List<ElectronicMail> ElectronicMail { get; set; }

            public Contact()
            {
                ElectronicMail = new List<ElectronicMail>();
            }
        }

        public class AccountingCustomerParty
        {
            public List<Party> Party { get; set; }

            public AccountingCustomerParty()
            {
                Party = new List<Party>();
            }
        }

        public class DocumentStatusCode
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string CodeContent { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string CodeListAgencyNameText { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string CodeListNameText { get; set; }

            public string _ { get; set; }

            public string listName { get; set; }

            public string listAgencyName { get; set; }
        }

        public class IssuerParty
        {
            public List<PartyIdentification> PartyIdentification { get; set; }
        }

        public class AdditionalDocumentReference
        {
            public List<ID> ID { get; set; }

            public List<DocumentTypeCode> DocumentTypeCode { get; set; }

            public List<DocumentStatusCode> DocumentStatusCode { get; set; }

            public List<IssuerParty> IssuerParty { get; set; }

            public AdditionalDocumentReference()
            {
                ID = new List<ID>();
                DocumentTypeCode = new List<DocumentTypeCode>();
                DocumentStatusCode = new List<DocumentStatusCode>();
                IssuerParty = new List<IssuerParty>();
            }
        }

        public class PaymentMeansID
        {
            public string _ { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string schemeName { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string schemeAgencyName { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string schemeURI { get; set; }
           
        }

        public class PaymentDueDate
        {
            public string _ { get; set; }
        }

        public class DocumentTypeCode
        {
            public string _ { get; set; }

            public string listName { get; set; }

            public string listAgencyName { get; set; }

            public string listURI { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string CodeContent { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string CodeListSchemeUniformResourceIdentifier { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string CodeListAgencyNameText { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string CodeListNameText { get; set; }
        }

        public class DespatchDocumentReference
        {
            public List<ID> ID { get; set; }

            public List<DocumentTypeCode> DocumentTypeCode { get; set; }

            public DespatchDocumentReference()
            {
                ID = new List<ID>();
                DocumentTypeCode = new List<DocumentTypeCode>();
            }
        }

        public class Amount
        {
            public string _ { get; set; }

            public string currencyID { get; set; }
        }

        public class PaymentTerm
        {
          
            public List<ID> ID { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<PaymentMeansID> PaymentMeansID { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<Note> Note { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<PaymentPercent> PaymentPercent { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<Amount> Amount { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<PaymentDueDate> PaymentDueDate { get; set; }

            public PaymentTerm()
            {
                ID = new List<ID>();
                PaymentMeansID = new List<PaymentMeansID>();
                Note = new List<Note>();
                PaymentPercent = new List<PaymentPercent>();
                Amount = new List<Amount>();
                PaymentDueDate = new List<PaymentDueDate>();
            }
        }

        public class ChargeIndicator
        {
            public string _ { get; set; }
        }

        public class AllowanceChargeReasonCode
        {
            public string _ { get; set; }

            public string listAgencyName { get; set; }

            public string listName { get; set; }

            public string listSchemeURI { get; set; }
        }

        public class MultiplierFactorNumeric
        {
            public string _ { get; set; }
        }

        public class BaseAmount
        {
            public string _ { get; set; }

            public string currencyID { get; set; }
        }

        public class PerUnitAmount
        {
            public string _ { get; set; }

            public string currencyID { get; set; }
        }

        public class AllowanceCharge
        {
            public List<ChargeIndicator> ChargeIndicator { get; set; }

            public List<AllowanceChargeReasonCode> AllowanceChargeReasonCode { get; set; }

            public List<MultiplierFactorNumeric> MultiplierFactorNumeric { get; set; }

            public List<Amount> Amount { get; set; }

            public List<BaseAmount> BaseAmount { get; set; }

            public List<PerUnitAmount> PerUnitAmount { get; set; }

            public AllowanceCharge()
            {
                ChargeIndicator = new List<ChargeIndicator>();
                AllowanceChargeReasonCode = new List<AllowanceChargeReasonCode>();
                MultiplierFactorNumeric = new List<MultiplierFactorNumeric>();
                Amount = new List<Amount>();
                BaseAmount = new List<BaseAmount>();
                PerUnitAmount = new List<PerUnitAmount>();
            }
        }

        public class AllowanceCharge2
        {
            public List<ChargeIndicator> ChargeIndicator { get; set; }

            public List<AllowanceChargeReasonCode> AllowanceChargeReasonCode { get; set; }
            public List<MultiplierFactorNumeric> MultiplierFactorNumeric { get; set; }
            public List<Amount> Amount { get; set; }
            public List<BaseAmount> BaseAmount { get; set; }
            public AllowanceCharge2()
            {
                ChargeIndicator = new List<ChargeIndicator>();
                AllowanceChargeReasonCode = new List<AllowanceChargeReasonCode>();
                MultiplierFactorNumeric = new List<MultiplierFactorNumeric>();
                Amount = new List<Amount>();
                BaseAmount = new List<BaseAmount>();
            }
        }

        public class TaxAmount
        {
            public string _ { get; set; }

            public string currencyID { get; set; }
        }

        public class TaxableAmount
        {
            public string _ { get; set; }

            public string currencyID { get; set; }
        }

        public class TaxTypeCode
        {
            public string _ { get; set; }
        }

        public class TaxScheme
        {
            public List<ID> ID { get; set; }

            public List<Name> Name { get; set; }

            public List<TaxTypeCode> TaxTypeCode { get; set; }

            public TaxScheme()
            {
                ID = new List<ID>();
                Name = new List<Name>();
                TaxTypeCode = new List<TaxTypeCode>();
            }
        }

        public class TaxCategory
        {
            public List<Percent> Percent { get; set; }

            public List<TaxExemptionReasonCode> TaxExemptionReasonCode { get; set; }

            public List<TaxScheme> TaxScheme { get; set; }

            public TaxCategory()
            {
                TaxScheme = new List<TaxScheme>();
                Percent = new List<Percent>();
                TaxExemptionReasonCode = new List<TaxExemptionReasonCode>();
            }
        }

        public class TaxSubtotal
        {
            public List<TaxableAmount> TaxableAmount { get; set; }

            public List<TaxAmount> TaxAmount { get; set; }

            public List<TaxCategory> TaxCategory { get; set; }

            public TaxSubtotal()
            {
                TaxableAmount = new List<TaxableAmount>();
                TaxAmount = new List<TaxAmount>();
                TaxCategory = new List<TaxCategory>();
            }
        }

        public class TaxTotal
        {
            public List<TaxAmount> TaxAmount { get; set; }

            public List<TaxSubtotal> TaxSubtotal { get; set; }

            public TaxTotal()
            {
                TaxAmount = new List<TaxAmount>();
                TaxSubtotal = new List<TaxSubtotal>();
            }
        }

        public class LineExtensionAmount
        {
            public string _ { get; set; }

            public string currencyID { get; set; }
        }

        public class TaxInclusiveAmount
        {
            public string _ { get; set; }

            public string currencyID { get; set; }
        }

        public class PayableAmount
        {
            public string _ { get; set; }

            public string currencyID { get; set; }
        }

        public class AllowanceTotalAmount
        {
            public string _ { get; set; }

            public string currencyID { get; set; }
        }

        public class PrepaidAmount
        {
            public string _ { get; set; }

            public string currencyID { get; set; }
        }

        public class LegalMonetaryTotal
        {
            public List<LineExtensionAmount> LineExtensionAmount { get; set; }

            public List<TaxInclusiveAmount> TaxInclusiveAmount { get; set; }

            public List<PrepaidAmount> PrepaidAmount { get; set; }

            public List<AllowanceTotalAmount> AllowanceTotalAmount { get; set; }

            public List<PayableAmount> PayableAmount { get; set; }

            public LegalMonetaryTotal()
            {
                LineExtensionAmount = new List<LineExtensionAmount>();
                TaxInclusiveAmount = new List<TaxInclusiveAmount>();
                PrepaidAmount = new List<PrepaidAmount>();
                AllowanceTotalAmount = new List<AllowanceTotalAmount>();
                PayableAmount = new List<PayableAmount>();
            }
        }

        public class InvoicedQuantity
        {
            public string _ { get; set; }

            public string unitCode { get; set; }

            public string unitCodeListD { get; set; }

            public string unitCodeListAgencyName { get; set; }
        }

        public class PriceAmount
        {
            public string _ { get; set; }

            public string currencyID { get; set; }
        }

        public class PriceTypeCode
        {
            public string _ { get; set; }

            public string listName { get; set; }

            public string listAgencyName { get; set; }

            public string listURI { get; set; }
        }

        public class AlternativeConditionPrice
        {
            public List<PriceAmount> PriceAmount { get; set; }

            public List<PriceTypeCode> PriceTypeCode { get; set; }

            public AlternativeConditionPrice()
            {
                PriceAmount = new List<PriceAmount>();
                PriceTypeCode = new List<PriceTypeCode>();
            }
        }

        public class BillingReferenceLine
        {
            public List<ID> ID { get; set; }

            public BillingReferenceLine()
            {
                ID = new List<ID>();
            }
        }

        public class BillingReference
        {
            public List<BillingReferenceLine> BillingReferenceLine { get; set; }

            public BillingReference()
            {
                BillingReferenceLine = new List<BillingReferenceLine>();
            }
        }

        public class PricingReference
        {
            public List<AlternativeConditionPrice> AlternativeConditionPrice { get; set; }

            public PricingReference()
            {
                AlternativeConditionPrice = new List<AlternativeConditionPrice>();
            }
        }

        public class Percent
        {
            public double _ { get; set; }
        }

        public class TaxExemptionReasonCode
        {
            public string _ { get; set; }

            public string listAgencyName { get; set; }

            public string listName { get; set; }

            public string listURI { get; set; }
        }

        public class Description
        {
            public string _ { get; set; }
        }

        public class SellersItemIdentification
        {
            public List<ID> ID { get; set; }

            public SellersItemIdentification()
            {
                ID = new List<ID>();
            }
        }

        public class Item
        {
            public List<Description> Description { get; set; }

            public List<SellersItemIdentification> SellersItemIdentification { get; set; }

            public Item()
            {
                Description = new List<Description>();
                SellersItemIdentification = new List<SellersItemIdentification>();
            }
        }

        public class Price
        {
            public List<PriceAmount> PriceAmount { get; set; }

            public Price()
            {
                PriceAmount = new List<PriceAmount>();
            }
        }

        public class InvoiceLine
        {
            public List<ID> ID { get; set; }

            public List<Note> Note { get; set; }

            public List<InvoicedQuantity> InvoicedQuantity { get; set; }

            public List<LineExtensionAmount> LineExtensionAmount { get; set; }

            public List<BillingReference> BillingReference { get; set; }

            public List<PricingReference> PricingReference { get; set; }

            public List<AllowanceCharge2> AllowanceCharge { get; set; }

            public List<TaxTotal> TaxTotal { get; set; }

            public List<Item> Item { get; set; }

            public List<Price> Price { get; set; }

            public InvoiceLine()
            {
                ID = new List<ID>();
                Note = new List<Note>();
                InvoicedQuantity = new List<InvoicedQuantity>();
                LineExtensionAmount = new List<LineExtensionAmount>();
                BillingReference = new List<BillingReference>();
                PricingReference = new List<PricingReference>();
                AllowanceCharge = new List<AllowanceCharge2>();
                TaxTotal = new List<TaxTotal>();
                Item = new List<Item>();
                Price = new List<Price>();
            }
        }

        public class PaymentMean
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<ID> ID { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<PaymentMeansCode> PaymentMeansCode { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<PayeeFinancialAccount> PayeeFinancialAccount { get; set; }

            public PaymentMean()
            {
                ID = new List<ID>();
                PaymentMeansCode = new List<PaymentMeansCode>();
                PayeeFinancialAccount = new List<PayeeFinancialAccount>();
            }
        }

        public class OrderReference
        {
            public List<ID> ID { get; set; }

            public OrderReference()
            {
                ID = new List<ID>();
            }
        }

        public class PaidAmount
        {
            public string _ { get; set; }

            public string currencyID { get; set; }
        }

        public class PaidDate
        {
            public string _ { get; set; }
        }

        public class PrepaidPayment
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<ID> ID { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<PaidAmount> PaidAmount { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<PaidDate> PaidDate { get; set; }
        }

        public class ExtensionContent
        {
            public List<TotalDiscount> TotalDiscount { get; set; }
        }

        public class TotalDiscount
        {
            public string _ { get; set; }
        }

        public class UBLExtension1
        {
            public List<UBLExtension2> UBLExtension { get; set; }

            public UBLExtension1()
            {
                UBLExtension = new List<UBLExtension2>();
            }
        }

        public class UBLExtension2
        {
            public List<ExtensionContent> ExtensionContent { get; set; }

            public UBLExtension2()
            {
                ExtensionContent = new List<ExtensionContent>();
            }
        }

        public class Invoice
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<UBLExtension1> UBLExtensions { get; set; }

            public List<UBLVersionID> UBLVersionID { get; set; }

            public List<CustomizationID> CustomizationID { get; set; }

            public List<ID> ID { get; set; }

            public List<IssueDate> IssueDate { get; set; }

            public List<DueDate> DueDate { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<IssueTime> IssueTime { get; set; }

            public List<InvoiceTypeCode> InvoiceTypeCode { get; set; }

            public List<Note> Note { get; set; }

            public List<DocumentCurrencyCode> DocumentCurrencyCode { get; set; }

            public List<LineCountNumeric> LineCountNumeric { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<OrderReference> OrderReference { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<DespatchDocumentReference> DespatchDocumentReference { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<AdditionalDocumentReference> AdditionalDocumentReference { get; set; }

            public List<Signature> Signature { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<AccountingSupplierParty> AccountingSupplierParty { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<AccountingCustomerParty> AccountingCustomerParty { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<PrepaidPayment> PrepaidPayment { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<PaymentMean> PaymentMeans { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<PaymentTerm> PaymentTerms { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public List<AllowanceCharge> AllowanceCharge { get; set; }

            public List<TaxTotal> TaxTotal { get; set; }

            public List<LegalMonetaryTotal> LegalMonetaryTotal { get; set; }

            public List<InvoiceLine> InvoiceLine { get; set; }

            public Invoice()
            {
                UBLExtensions = new List<UBLExtension1>();
                UBLVersionID = new List<UBLVersionID>();
                CustomizationID = new List<CustomizationID>();
                ID = new List<ID>();
                IssueDate = new List<IssueDate>();
                DueDate = new List<DueDate>();
                IssueTime = new List<IssueTime>();
                InvoiceTypeCode = new List<InvoiceTypeCode>();
                Note = new List<Note>();
                DocumentCurrencyCode = new List<DocumentCurrencyCode>();
                LineCountNumeric = new List<LineCountNumeric>();
                OrderReference = new List<OrderReference>();
                DespatchDocumentReference = new List<DespatchDocumentReference>();
                AdditionalDocumentReference = new List<AdditionalDocumentReference>();
                Signature = new List<Signature>();
                AccountingSupplierParty = new List<AccountingSupplierParty>();
                AccountingCustomerParty = new List<AccountingCustomerParty>();
                PaymentTerms = new List<PaymentTerm>();
                PrepaidPayment = new List<PrepaidPayment>();
                AllowanceCharge = new List<AllowanceCharge>();
                TaxTotal = new List<TaxTotal>();
                LegalMonetaryTotal = new List<LegalMonetaryTotal>();
                InvoiceLine = new List<InvoiceLine>();
            }
        }

        public class Root
        {
            public string _D { get; set; }

            public string _A { get; set; }

            public string _B { get; set; }

            public string _E { get; set; }

            public List<Invoice> Invoice { get; set; }

            public Root()
            {
                Invoice = new List<Invoice>();
            }
        }
    }
}
