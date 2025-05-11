using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace XafTaskApp.Module.BusinessObjects
{
    public static class ConnectionHelper
    {
        static Type[] persistentTypes = new Type[] {
            typeof(customer),
            typeof(invoice),
            typeof(invoice_detail),
            typeof(payment),
            typeof(product),
            typeof(sysdiagrams)
        };
        public static Type[] GetPersistentTypes()
        {
            Type[] copy = new Type[persistentTypes.Length];
            Array.Copy(persistentTypes, copy, persistentTypes.Length);
            return copy;
        }
        static Type[] nonPersistentTypes = new Type[] {
            typeof(sp_helpdiagramdefinitionResult),
            typeof(sp_helpdiagramsResult)
        };
        public static Type[] GetNonPersistentTypes()
        {
            Type[] copy = new Type[nonPersistentTypes.Length];
            Array.Copy(nonPersistentTypes, copy, nonPersistentTypes.Length);
            return copy;
        }
        public const string ConnectionStringName = ".xaf_test";
        public static void Connect(IConfiguration configuration, DevExpress.Xpo.DB.AutoCreateOption autoCreateOption, bool threadSafe = false)
        {
            if (threadSafe)
            {
                var provider = GetConnectionProvider(configuration, autoCreateOption);
                var dictionary = new DevExpress.Xpo.Metadata.ReflectionDictionary();
                dictionary.GetDataStoreSchema(persistentTypes);
                dictionary.CollectClassInfos(nonPersistentTypes);
                XpoDefault.DataLayer = new ThreadSafeDataLayer(dictionary, provider);
            }
            else
            {
                XpoDefault.DataLayer = XpoDefault.GetDataLayer(configuration.GetConnectionString(ConnectionStringName), autoCreateOption);
            }
            XpoDefault.Session = null;
        }
        public static DevExpress.Xpo.DB.IDataStore GetConnectionProvider(IConfiguration configuration, DevExpress.Xpo.DB.AutoCreateOption autoCreateOption)
        {
            return XpoDefault.GetConnectionProvider(configuration.GetConnectionString(ConnectionStringName), autoCreateOption);
        }
        public static DevExpress.Xpo.DB.IDataStore GetConnectionProvider(IConfiguration configuration, DevExpress.Xpo.DB.AutoCreateOption autoCreateOption, out IDisposable[] objectsToDisposeOnDisconnect)
        {
            return XpoDefault.GetConnectionProvider(configuration.GetConnectionString(ConnectionStringName), autoCreateOption, out objectsToDisposeOnDisconnect);
        }
        public static IDataLayer GetDataLayer(IConfiguration configuration, DevExpress.Xpo.DB.AutoCreateOption autoCreateOption)
        {
            return XpoDefault.GetDataLayer(configuration.GetConnectionString(ConnectionStringName), autoCreateOption);
        }
    }
}
