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
    public partial class invoice_detail
    {
        public invoice_detail(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        public XPCollection<DevExpress.Persistent.BaseImpl.AuditDataItemPersistent> AuditTrail
        {
            get { return AuditedObjectWeakReference.GetAuditTrail(Session, this); }
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (propertyName == nameof(qun) || propertyName == nameof(unit_price))
            {
                total = qun * unit_price; // This automatically marks the object as modified in XPO
            }
        }
    }
}
