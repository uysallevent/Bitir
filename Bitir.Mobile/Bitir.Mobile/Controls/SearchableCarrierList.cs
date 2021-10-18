using Bitir.Mobile.Models;
using Module.Shared.Entities.ProductModuleEntities;
using SalesModule.Dtos;
using Xamarin.Forms.Internals;

namespace Bitir.Mobile.Controls
{
    [Preserve(AllMembers = true)]

    public class SearchableCarrierList : SearchableListView
    {
        #region Method
        public override bool FilterContacts(object obj)
        {
            if (base.FilterContacts(obj))
            {
                var taskInfo = obj as StoreCarrier;

                if (taskInfo == null || string.IsNullOrEmpty(taskInfo.Plate) || taskInfo.Capacity == null)
                {
                    return false;
                }

                return taskInfo.Plate.ToLowerInvariant().Contains(this.SearchText.ToLowerInvariant())
                       || taskInfo.Capacity.ToString().ToLowerInvariant().Contains(this.SearchText.ToLowerInvariant());
            }

            return false;
        }
        #endregion
    }
}
