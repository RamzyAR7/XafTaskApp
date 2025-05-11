using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using XafTaskApp.Module.BusinessObjects;

public class InvoiceDetailViewController : ObjectViewController<ListView, invoice_detail>
{
    public InvoiceDetailViewController()
    {
        var changePriceAction = new SingleChoiceAction(this, "ChangePrice", DevExpress.Persistent.Base.PredefinedCategory.Edit)
        {
            Caption = "Change Price",
            ItemType = SingleChoiceActionItemType.ItemIsOperation
        };
        changePriceAction.Items.Add(new ChoiceActionItem("Option 1", 1));
        changePriceAction.Items.Add(new ChoiceActionItem("Option 2", 2));
        changePriceAction.Items.Add(new ChoiceActionItem("Option 3", 3));
        changePriceAction.Execute += ChangePriceAction_Execute;
    }

    private void ChangePriceAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
    {
        foreach (invoice_detail detail in View.SelectedObjects)
        {
            if (detail.product_id != null)
            {
                switch ((int)e.SelectedChoiceActionItem.Data)
                {
                    case 1:
                        detail.unit_price = detail.product_id.sales_price;
                        break;
                    case 2:
                        detail.unit_price = detail.product_id.sales_price2;
                        break;
                    case 3:
                        detail.unit_price = detail.product_id.sales_price3;
                        break;
                }
                ObjectSpace.SetModified(detail);
            }
        }
        ObjectSpace.CommitChanges();
    }
}

