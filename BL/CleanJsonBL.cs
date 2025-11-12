using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UBL;

namespace BL
{
    public class CleanJsonBL
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static InvoiceJson.Root cleanInvoice(InvoiceJson.Root oInvoice)
        {
            try
            {
                foreach (var line in oInvoice.Invoice[0].UBLExtensions)
                {
                    foreach (var line2 in line.UBLExtension)
                    {
                        foreach (var line3 in line2.ExtensionContent)
                        {
                            foreach (var line4 in line3.TotalDiscount)
                            {
                                if (line4._ != null && line4._.Length==0)
                                {
                                    oInvoice.Invoice[0].UBLExtensions = null;
                                }

                            }
                        }
                    }
                }


                Log.Debug("--------------------------------------------------------------");
                if (oInvoice.Invoice[0].IssueTime != null && oInvoice.Invoice[0].IssueTime.Count > 0)
                {
                   
                }
                else
                {
                    Log.Debug("--------------------------------------------------------------");
                    oInvoice.Invoice[0].IssueTime = null;
                    Log.Debug("--------------------------------------------------------------");
                }

                Log.Debug("--------------------------------------------------------------");
                if (oInvoice.Invoice[0].DespatchDocumentReference != null && oInvoice.Invoice[0].DespatchDocumentReference.Count > 0)
                {

                }
                else
                {
                    Log.Debug("--------------------------------------------------------------");
                    oInvoice.Invoice[0].DespatchDocumentReference = null;
                    Log.Debug("--------------------------------------------------------------");
                }
                Log.Debug("--------------------------------------------------------------");
                if (oInvoice.Invoice[0].AdditionalDocumentReference != null && oInvoice.Invoice[0].AdditionalDocumentReference.Count > 0)
                {

                }
                else
                {
                    Log.Debug("--------------------------------------------------------------");
                    oInvoice.Invoice[0].AdditionalDocumentReference = null;
                    Log.Debug("--------------------------------------------------------------");
                }
                Log.Debug("--------------------------------------------------------------");
                for (int i = 0; i < oInvoice.Invoice[0].AccountingCustomerParty.Count; i++)
                {
                    Log.Debug("--------------------------------------------------------------");
                    for (int j = 0; j < oInvoice.Invoice[0].AccountingCustomerParty[i].Party.Count; j++)
                    {
                        Log.Debug("--------------------------------------------------------------");
                        if (string.IsNullOrEmpty(oInvoice.Invoice[0].AccountingCustomerParty[i].Party[j].PartyLegalEntity[0].RegistrationAddress[0].AddressTypeCode[0]._))
                        {
                            Log.Debug("--------------------------------------------------------------");
                            oInvoice.Invoice[0].AccountingCustomerParty[i].Party[0].PartyLegalEntity[0].RegistrationAddress[0].AddressTypeCode = null;
                            Log.Debug("--------------------------------------------------------------");

                        }

                    }
                        

                }
                Log.Debug("--------------------------------------------------------------");
                if (oInvoice.Invoice[0].PrepaidPayment != null && oInvoice.Invoice[0].PrepaidPayment.Count > 0)
                {

                }
                else
                {
                    Log.Debug("--------------------------------------------------------------");
                    oInvoice.Invoice[0].PrepaidPayment = null;
                    Log.Debug("--------------------------------------------------------------");
                }



                foreach (var line in oInvoice.Invoice[0].PaymentTerms)
                {
                    if (line.Note != null && line.Note.Count == 0)
                    {
                        line.Note = null;

                    }

                    if (line.PaymentPercent != null && line.PaymentPercent.Count == 0)
                    {
                        line.PaymentPercent = null;

                    }
                    if (line.Amount != null && line.Amount.Count == 0)
                    {
                        line.Amount = null;

                    }
                    if (line.PaymentDueDate != null && line.PaymentDueDate.Count == 0)
                    {
                        line.PaymentDueDate = null;

                    }
                }


                //var paymentTerm = oInvoice.Invoice[0].PaymentTerms[0];

                //if (paymentTerm.Note != null && paymentTerm.Note.Count == 0)
                //    paymentTerm.Note = null;

                //if (paymentTerm.PaymentPercent != null && paymentTerm.PaymentPercent.Count == 0)
                //    paymentTerm.PaymentPercent = null;

                //if (paymentTerm.Amount != null && paymentTerm.Amount.Count == 0)
                //    paymentTerm.Amount = null;

                //if (paymentTerm.PaymentDueDate != null && paymentTerm.PaymentDueDate.Count == 0)
                //    paymentTerm.PaymentDueDate = null;

           
                foreach (var line in oInvoice.Invoice[0].TaxTotal[0].TaxSubtotal)

                {
                    foreach  (var line2 in line.TaxCategory)
                    {
                        if (line2.Percent != null && line2.Percent.Count == 0)
                        {
                            line2.Percent = null;
                        }
                        if (line2.TaxExemptionReasonCode != null && line2.TaxExemptionReasonCode.Count == 0)
                        {
                            line2.TaxExemptionReasonCode = null;
                        }
                    }
                 

                }

                foreach (var line in oInvoice.Invoice)

                {


                    if (line.AllowanceCharge != null && line.AllowanceCharge.Count == 0)
                    {
                        line.AllowanceCharge = null;
                    }



                }


                //var taxCategory = oInvoice.Invoice[0]
                //                .TaxTotal[0]
                //                .TaxSubtotal[0]
                //                .TaxCategory[0];

                //if (taxCategory.Percent != null && taxCategory.Percent.Count == 0)
                //    taxCategory.Percent = null;

                //if (taxCategory.TaxExemptionReasonCode != null && taxCategory.TaxExemptionReasonCode.Count == 0)
                //    taxCategory.TaxExemptionReasonCode = null;





                foreach (var line in oInvoice.Invoice[0].InvoiceLine)
                {
                    if (line.BillingReference != null && line.BillingReference.Count == 0)
                    {
                        line.BillingReference = null;
                    }
                }
                foreach (var line in oInvoice.Invoice[0].InvoiceLine)
                {


                    if (line.AllowanceCharge != null && line.AllowanceCharge.Count == 0)
                    {
                        line.AllowanceCharge = null;
                    }else
                    {
                        foreach (var line2 in line.AllowanceCharge)
                        {
                            if (line2.MultiplierFactorNumeric.Count==0 && line2.MultiplierFactorNumeric!=null)
                            {

                            }
                            else
                            {
                                foreach (var line3 in line2.MultiplierFactorNumeric)
                                {
                                        if (line3._!=null && line3._.Length==0)
                                    {
                                        line2.MultiplierFactorNumeric = null;
                                    }
                                }
                               
                            }    
                        
                           
                        }
                        
                    }

                }

                //for (int i = 0; i < oInvoice.Invoice[0].PaymentTerms.Count; i++)
                //{
                Log.Debug("--------------------------------------------------------------");
                    //for (int j = 0; j < oInvoice.Invoice[0].PaymentTerms[i].Note.Count; j++)
                    //{
                    Log.Debug("--------------------------------------------------------------");
                    //if (string.IsNullOrEmpty(oInvoice.Invoice[0].PaymentTerms[0].Note[0]._))
                    //{
                    //    Log.Debug("--------------------------------------------------------------");
                    //    oInvoice.Invoice[0].PaymentTerms[0].Note = null;
                    //    Log.Debug("--------------------------------------------------------------");

                    //}

                    //if (string.IsNullOrEmpty(oInvoice.Invoice[0].PaymentTerms[0].PaymentPercent[0]._))
                    //{
                    //    Log.Debug("--------------------------------------------------------------");
                    //    oInvoice.Invoice[0].PaymentTerms[0].PaymentPercent = null;
                    //    Log.Debug("--------------------------------------------------------------");

                    //}
                    //if (string.IsNullOrEmpty(oInvoice.Invoice[0].PaymentTerms[0].Amount[0]._))
                    //{
                    //    Log.Debug("--------------------------------------------------------------");
                    //    oInvoice.Invoice[0].PaymentTerms[0].Amount = null;
                    //    Log.Debug("--------------------------------------------------------------");

                    //}
                    //if (string.IsNullOrEmpty(oInvoice.Invoice[0].PaymentTerms[0].PaymentDueDate[0]._))
                    //{
                    //    Log.Debug("--------------------------------------------------------------");
                    //    oInvoice.Invoice[0].PaymentTerms[0].PaymentDueDate = null;
                    //    Log.Debug("--------------------------------------------------------------");

                    //}

                    //}


                //}

                Log.Debug("--------------------------------------------------------------");
                Log.Debug("--------------------------------------------------------------");
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
