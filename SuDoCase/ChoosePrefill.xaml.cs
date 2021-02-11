using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SuDoCase
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChoosePrefill : ContentPage
    {
        private bool firstSet;
        public ChoosePrefill()
        {
            InitializeComponent();
            title.SetOnAppTheme(Label.TextColorProperty, Color.Black, Color.FromHex("#46b5d1"));
        }

        private async void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!firstSet)
            {
                firstSet = true;
                return;
            }
            var picker = (Picker)sender;
            if (picker.SelectedIndex == 0)
                return;
            var main = new MainPage(picker.SelectedIndex);
            await Navigation.PushAsync(main);
        }
    }
}