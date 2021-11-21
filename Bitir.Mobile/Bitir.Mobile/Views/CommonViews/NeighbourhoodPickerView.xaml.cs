using Bitir.Mobile.ViewModels;
using Module.Shared.Entities.AuthModuleEntities;
using Syncfusion.XForms.ComboBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bitir.Mobile.Views.CommonViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NeighbourhoodPickerView : ContentView
    {
        public NeighbourhoodPickerView()
        {
            InitializeComponent();
        }
    }
}