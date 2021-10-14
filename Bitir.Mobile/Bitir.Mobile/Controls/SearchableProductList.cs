using Bitir.Mobile.Models;
using Module.Shared.Entities.ProductModuleEntities;
using Xamarin.Forms.Internals;

namespace Bitir.Mobile.Controls
{
    [Preserve(AllMembers = true)]

    public class SearchableProductList : SearchableListView
    {
        #region Method
        public override bool FilterContacts(object obj)
        {
            if (base.FilterContacts(obj))
            {
                var taskInfo = obj as StoreProductViewModel;

                if (taskInfo == null || string.IsNullOrEmpty(taskInfo.Name) || string.IsNullOrEmpty(taskInfo.FullName))
                {
                    return false;
                }

                return taskInfo.FullName.ToLowerInvariant().Contains(this.SearchText.ToLowerInvariant())
                       || taskInfo.Name.ToLowerInvariant().Contains(this.SearchText.ToLowerInvariant());
            }

            return false;
        }
        #endregion
    }
}
