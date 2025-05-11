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
    public partial class payment
    {
        public payment(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); payment_date = DateTime.Today; }
        public XPCollection<DevExpress.Persistent.BaseImpl.AuditDataItemPersistent> AuditTrail
        {
            get { return AuditedObjectWeakReference.GetAuditTrail(Session, this); }
        }
    }

}
