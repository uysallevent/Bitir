using Module.Shared.Entities.SalesModuleEntities;
using Xamarin.Forms.Internals;

namespace Bitir.Mobile.Controls
{
    [Preserve(AllMembers = true)]

    public class SearchableStoreOrderList : SearchableListView
    {
        #region Method
        public override bool FilterContacts(object obj)
        {
            if (base.FilterContacts(obj))
            {
                var taskInfo = obj as StoreOrdersView;

                if (taskInfo == null || string.IsNullOrEmpty(taskInfo.ProductName) || string.IsNullOrEmpty(taskInfo.CustomerName))
                {
                    return false;
                }

                return taskInfo.ProductName.ToLowerInvariant().Contains(this.SearchText.ToLowerInvariant())
                       || taskInfo.CustomerName.ToLowerInvariant().Contains(this.SearchText.ToLowerInvariant());
            }

            return false;
        }
        #endregion
    }
}
