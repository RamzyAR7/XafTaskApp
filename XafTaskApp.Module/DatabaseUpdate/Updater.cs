using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using XafTaskApp.Module.BusinessObjects;
using Microsoft.Extensions.DependencyInjection;
using DevExpress.Persistent.Base;

namespace XafTaskApp.Module.DatabaseUpdate
{
    public class Updater : ModuleUpdater
    {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }

        public override void UpdateDatabaseAfterUpdateSchema()
        {
            base.UpdateDatabaseAfterUpdateSchema();

            // Existing user/role setup (unchanged)
            var defaultRole = CreateDefaultRole();
            var adminRole = CreateAdminRole();
            ObjectSpace.CommitChanges();

            UserManager userManager = ObjectSpace.ServiceProvider.GetRequiredService<UserManager>();
            if (userManager.FindUserByName<ApplicationUser>(ObjectSpace, "User") == null)
            {
                string EmptyPassword = "";
                _ = userManager.CreateUser<ApplicationUser>(ObjectSpace, "User", EmptyPassword, (user) =>
                {
                    user.Roles.Add(defaultRole);
                });
            }
            if (userManager.FindUserByName<ApplicationUser>(ObjectSpace, "Admin") == null)
            {
                string EmptyPassword = "";
                _ = userManager.CreateUser<ApplicationUser>(ObjectSpace, "Admin", EmptyPassword, (user) =>
                {
                    user.Roles.Add(adminRole);
                });
            }
            ObjectSpace.CommitChanges();

            // Add Test Data
            // 1. Create Customers
            customer customer1 = ObjectSpace.FirstOrDefault<customer>(c => c.customer_name == "John Doe");
            if (customer1 == null)
            {
                customer1 = ObjectSpace.CreateObject<customer>();
                customer1.customer_name = "John Doe";
                customer1.customer_address = "123 Main St, City";
                customer1.customer_mobile = "1234567890";
            }

            customer customer2 = ObjectSpace.FirstOrDefault<customer>(c => c.customer_name == "Jane Smith");
            if (customer2 == null)
            {
                customer2 = ObjectSpace.CreateObject<customer>();
                customer2.customer_name = "Jane Smith";
                customer2.customer_address = "456 Oak Ave, Town";
                customer2.customer_mobile = "0987654321";
            }

            customer customer3 = ObjectSpace.FirstOrDefault<customer>(c => c.customer_name == "Alice Johnson");
            if (customer3 == null)
            {
                customer3 = ObjectSpace.CreateObject<customer>();
                customer3.customer_name = "Alice Johnson";
                customer3.customer_address = "789 Pine Rd, Village";
                customer3.customer_mobile = "5551234567";
            }

            customer customer4 = ObjectSpace.FirstOrDefault<customer>(c => c.customer_name == "Bob Wilson");
            if (customer4 == null)
            {
                customer4 = ObjectSpace.CreateObject<customer>();
                customer4.customer_name = "Bob Wilson";
                customer4.customer_address = "101 Maple Dr, Suburb";
                customer4.customer_mobile = "4449876543";
            }

            customer customer5 = ObjectSpace.FirstOrDefault<customer>(c => c.customer_name == "Emma Brown");
            if (customer5 == null)
            {
                customer5 = ObjectSpace.CreateObject<customer>();
                customer5.customer_name = "Emma Brown";
                customer5.customer_address = "202 Cedar Ln, City";
                customer5.customer_mobile = "3334567890";
            }

            // 2. Create Products
            product product1 = ObjectSpace.FirstOrDefault<product>(p => p.product_name == "Laptop");
            if (product1 == null)
            {
                product1 = ObjectSpace.CreateObject<product>();
                product1.product_name = "Laptop";
                product1.product_desc = "High-performance laptop";
                product1.product_price = 1000;
                product1.sales_price = 950;
                product1.sales_price2 = 900;
                product1.sales_price3 = 850;
            }

            product product2 = ObjectSpace.FirstOrDefault<product>(p => p.product_name == "Mouse");
            if (product2 == null)
            {
                product2 = ObjectSpace.CreateObject<product>();
                product2.product_name = "Mouse";
                product2.product_desc = "Wireless mouse";
                product2.product_price = 50;
                product2.sales_price = 45;
                product2.sales_price2 = 40;
                product2.sales_price3 = 35;
            }

            product product3 = ObjectSpace.FirstOrDefault<product>(p => p.product_name == "Keyboard");
            if (product3 == null)
            {
                product3 = ObjectSpace.CreateObject<product>();
                product3.product_name = "Keyboard";
                product3.product_desc = "Mechanical keyboard";
                product3.product_price = 80;
                product3.sales_price = 75;
                product3.sales_price2 = 70;
                product3.sales_price3 = 65;
            }

            product product4 = ObjectSpace.FirstOrDefault<product>(p => p.product_name == "Monitor");
            if (product4 == null)
            {
                product4 = ObjectSpace.CreateObject<product>();
                product4.product_name = "Monitor";
                product4.product_desc = "27-inch LED monitor";
                product4.product_price = 300;
                product4.sales_price = 290;
                product4.sales_price2 = 280;
                product4.sales_price3 = 270;
            }

            product product5 = ObjectSpace.FirstOrDefault<product>(p => p.product_name == "Headphones");
            if (product5 == null)
            {
                product5 = ObjectSpace.CreateObject<product>();
                product5.product_name = "Headphones";
                product5.product_desc = "Noise-canceling headphones";
                product5.product_price = 150;
                product5.sales_price = 140;
                product5.sales_price2 = 130;
                product5.sales_price3 = 120;
            }

            // 3. Create Invoices
            invoice invoice1 = ObjectSpace.FirstOrDefault<invoice>(i => i.invoice_desc == "Invoice for John Doe");
            if (invoice1 == null)
            {
                invoice1 = ObjectSpace.CreateObject<invoice>();
                invoice1.customer_id = customer1; // John Doe
                invoice1.invoice_desc = "Invoice for John Doe";
                // invoice_date is set to today in the constructor
            }

            invoice invoice2 = ObjectSpace.FirstOrDefault<invoice>(i => i.invoice_desc == "Invoice for Jane Smith");
            if (invoice2 == null)
            {
                invoice2 = ObjectSpace.CreateObject<invoice>();
                invoice2.customer_id = customer2; // Jane Smith
                invoice2.invoice_desc = "Invoice for Jane Smith";
            }

            invoice invoice3 = ObjectSpace.FirstOrDefault<invoice>(i => i.invoice_desc == "Invoice for Alice Johnson");
            if (invoice3 == null)
            {
                invoice3 = ObjectSpace.CreateObject<invoice>();
                invoice3.customer_id = customer3; // Alice Johnson
                invoice3.invoice_desc = "Invoice for Alice Johnson";
            }

            // 4. Create Invoice Details
            // Invoice 1 (John Doe)
            invoice_detail detail1 = ObjectSpace.FirstOrDefault<invoice_detail>(d => d.invoice_id == invoice1 && d.product_id == product1);
            if (detail1 == null)
            {
                detail1 = ObjectSpace.CreateObject<invoice_detail>();
                detail1.invoice_id = invoice1;
                detail1.product_id = product1; // Laptop
                detail1.qun = 2;
                detail1.unit_price = product1.sales_price;
                detail1.total = detail1.qun * detail1.unit_price;
            }

            invoice_detail detail2 = ObjectSpace.FirstOrDefault<invoice_detail>(d => d.invoice_id == invoice1 && d.product_id == product2);
            if (detail2 == null)
            {
                detail2 = ObjectSpace.CreateObject<invoice_detail>();
                detail2.invoice_id = invoice1;
                detail2.product_id = product2; // Mouse
                detail2.qun = 5;
                detail2.unit_price = product2.sales_price;
                detail2.total = detail2.qun * detail2.unit_price;
            }

            invoice_detail detail3 = ObjectSpace.FirstOrDefault<invoice_detail>(d => d.invoice_id == invoice1 && d.product_id == product3);
            if (detail3 == null)
            {
                detail3 = ObjectSpace.CreateObject<invoice_detail>();
                detail3.invoice_id = invoice1;
                detail3.product_id = product3; // Keyboard
                detail3.qun = 1;
                detail3.unit_price = product3.sales_price;
                detail3.total = detail3.qun * detail3.unit_price;
            }

            // Invoice 2 (Jane Smith)
            invoice_detail detail4 = ObjectSpace.FirstOrDefault<invoice_detail>(d => d.invoice_id == invoice2 && d.product_id == product4);
            if (detail4 == null)
            {
                detail4 = ObjectSpace.CreateObject<invoice_detail>();
                detail4.invoice_id = invoice2;
                detail4.product_id = product4; // Monitor
                detail4.qun = 1;
                detail4.unit_price = product4.sales_price;
                detail4.total = detail4.qun * detail4.unit_price;
            }

            invoice_detail detail5 = ObjectSpace.FirstOrDefault<invoice_detail>(d => d.invoice_id == invoice2 && d.product_id == product5);
            if (detail5 == null)
            {
                detail5 = ObjectSpace.CreateObject<invoice_detail>();
                detail5.invoice_id = invoice2;
                detail5.product_id = product5; // Headphones
                detail5.qun = 2;
                detail5.unit_price = product5.sales_price;
                detail5.total = detail5.qun * detail5.unit_price;
            }

            // Invoice 3 (Alice Johnson)
            invoice_detail detail6 = ObjectSpace.FirstOrDefault<invoice_detail>(d => d.invoice_id == invoice3 && d.product_id == product1);
            if (detail6 == null)
            {
                detail6 = ObjectSpace.CreateObject<invoice_detail>();
                detail6.invoice_id = invoice3;
                detail6.product_id = product1; // Laptop
                detail6.qun = 1;
                detail6.unit_price = product1.sales_price2; // Using sales_price2
                detail6.total = detail6.qun * detail6.unit_price;
            }

            invoice_detail detail7 = ObjectSpace.FirstOrDefault<invoice_detail>(d => d.invoice_id == invoice3 && d.product_id == product2);
            if (detail7 == null)
            {
                detail7 = ObjectSpace.CreateObject<invoice_detail>();
                detail7.invoice_id = invoice3;
                detail7.product_id = product2; // Mouse
                detail7.qun = 3;
                detail7.unit_price = product2.sales_price3; // Using sales_price3
                detail7.total = detail7.qun * detail7.unit_price;
            }

            // 5. Create Payments
            payment payment1 = ObjectSpace.FirstOrDefault<payment>(p => p.invoice_id == invoice1);
            if (payment1 == null)
            {
                payment1 = ObjectSpace.CreateObject<payment>();
                payment1.customer_id = customer1; // John Doe
                payment1.invoice_id = invoice1;
            }

            payment payment2 = ObjectSpace.FirstOrDefault<payment>(p => p.invoice_id == invoice2);
            if (payment2 == null)
            {
                payment2 = ObjectSpace.CreateObject<payment>();
                payment2.customer_id = customer2; // Jane Smith
                payment2.invoice_id = invoice2;
            }

            payment payment3 = ObjectSpace.FirstOrDefault<payment>(p => p.invoice_id == invoice3);
            if (payment3 == null)
            {
                payment3 = ObjectSpace.CreateObject<payment>();
                payment3.customer_id = customer3; // Alice Johnson
                payment3.invoice_id = invoice3;
            }

            ObjectSpace.CommitChanges(); // Persist all initial data
        }

        // Existing methods (unchanged)
        public override void UpdateDatabaseBeforeUpdateSchema()
        {
            base.UpdateDatabaseBeforeUpdateSchema();
        }

        private PermissionPolicyRole CreateAdminRole()
        {
            PermissionPolicyRole adminRole = ObjectSpace.FirstOrDefault<PermissionPolicyRole>(r => r.Name == "Administrators");
            if (adminRole == null)
            {
                adminRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                adminRole.Name = "Administrators";
                adminRole.IsAdministrative = true;
            }
            return adminRole;
        }

        private PermissionPolicyRole CreateDefaultRole()
        {
            PermissionPolicyRole defaultRole = ObjectSpace.FirstOrDefault<PermissionPolicyRole>(role => role.Name == "Default");
            if (defaultRole == null)
            {
                defaultRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                defaultRole.Name = "Default";
                defaultRole.AddObjectPermissionFromLambda<ApplicationUser>(SecurityOperations.Read, cm => cm.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
                defaultRole.AddMemberPermissionFromLambda<ApplicationUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", cm => cm.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
                defaultRole.AddMemberPermissionFromLambda<ApplicationUser>(SecurityOperations.Write, "StoredPassword", cm => cm.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Deny);
                defaultRole.AddObjectPermission<ModelDifference>(SecurityOperations.ReadWriteAccess, "UserId = ToStr(CurrentUserId())", SecurityPermissionState.Allow);
                defaultRole.AddObjectPermission<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, "Owner.UserId = ToStr(CurrentUserId())", SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.Create, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.Create, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<AuditDataItemPersistent>(SecurityOperations.Read, SecurityPermissionState.Deny);
                defaultRole.AddObjectPermissionFromLambda<AuditDataItemPersistent>(SecurityOperations.Read, a => a.UserId == CurrentUserIdOperator.CurrentUserId().ToString(), SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<AuditedObjectWeakReference>(SecurityOperations.Read, SecurityPermissionState.Allow);
            }
            return defaultRole;
        }
    }
}
