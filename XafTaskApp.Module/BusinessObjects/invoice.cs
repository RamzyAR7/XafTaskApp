using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
namespace XafTaskApp.Module.BusinessObjects
{

    [DefaultClassOptions]
    public partial class invoice
    {
        public invoice(Session session) : base(session)
        {
            invoice_date = DateTime.Today; // Set initial invoice_date
        }
        public override void AfterConstruction() { base.AfterConstruction(); }
        public XPCollection<DevExpress.Persistent.BaseImpl.AuditDataItemPersistent> AuditTrail
        {
            get { return AuditedObjectWeakReference.GetAuditTrail(Session, this); }
        }
    }

}
