﻿using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace XafTaskApp.Module.BusinessObjects
{

    public partial class sysdiagrams
    {
        public sysdiagrams(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
