﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using DevExpress.Persistent.Validation;
namespace XafTaskApp.Module.BusinessObjects
{
    public partial class customer : XPLiteObject
    {
        int fcustomer_id;
        [System.ComponentModel.DisplayName("Customer ID")]
        [Key(true)]
        public int customer_id
        {
            get { return fcustomer_id; }
            set { SetPropertyValue<int>(nameof(customer_id), ref fcustomer_id, value); }
        }
        string fcustomer_name;
        [System.ComponentModel.DisplayName("Customer Name")]
        [Size(150)]
        [RuleRequiredField("CustomerNameRequired", DefaultContexts.Save, "Customer Name is required.")] // Required field
        public string customer_name
        {
            get { return fcustomer_name; }
            set { SetPropertyValue<string>(nameof(customer_name), ref fcustomer_name, value); }
        }
        string fcustomer_address;
        [System.ComponentModel.DisplayName("Customer Address")]
        [Size(500)]
        public string customer_address
        {
            get { return fcustomer_address; }
            set { SetPropertyValue<string>(nameof(customer_address), ref fcustomer_address, value); }
        }
        string fcustomer_mobile;
        [System.ComponentModel.DisplayName("Customer Mobile")]
        [Size(50)]
        [RuleRegularExpression("CustomerMobileFormat", DefaultContexts.Save, @"^\d{10}$", CustomMessageTemplate = "Mobile number must be 10 digits.")] // Example: 10-digit mobile number
        public string customer_mobile
        {
            get { return fcustomer_mobile; }
            set { SetPropertyValue<string>(nameof(customer_mobile), ref fcustomer_mobile, value); }
        }
        [Association(@"invoiceReferencescustomer")]
        public XPCollection<invoice> invoices { get { return GetCollection<invoice>(nameof(invoices)); } }
        [Association(@"paymentReferencescustomer")]
        public XPCollection<payment> payments { get { return GetCollection<payment>(nameof(payments)); } }

    }

}
