using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using XafTaskApp.Module.BusinessObjects;
using System;

namespace XafTaskApp.Module.Controllers
{
    public class PaymentViewController : ObjectViewController<DetailView, payment>
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            var invoiceProperty = View.FindItem(nameof(payment.invoice_id)) as PropertyEditor;
            if (invoiceProperty != null)
            {
                invoiceProperty.ControlValueChanged += InvoiceProperty_ControlValueChanged;
            }
        }

        private void InvoiceProperty_ControlValueChanged(object sender, EventArgs e)
        {
            var obj = View.CurrentObject as payment;
            if (obj != null && obj.invoice_id != null)
            {
                // Calculate total from invoice details
                double total = 0;
                foreach (var detail in obj.invoice_id.invoice_details)
                {
                    total += detail.total;
                }
                obj.payment_amount = total;
                View.ObjectSpace.SetModified(obj);
            }
        }

        protected override void OnDeactivated()
        {
            var invoiceProperty = View.FindItem(nameof(payment.invoice_id)) as PropertyEditor;
            if (invoiceProperty != null)
            {
                invoiceProperty.ControlValueChanged -= InvoiceProperty_ControlValueChanged;
            }
            base.OnDeactivated();
        }
    }
} 