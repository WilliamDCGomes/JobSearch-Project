using JobSearch.App.Services;
using JobSearch.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobSearch.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private UserService _service;
        public Login()
        {
            InitializeComponent();
            _service = new UserService();
        }

        private void GoRegister(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Register());
        }

        private async void GoStart(object sender, EventArgs e)
        {
            string email = Email.Text;
            string password = Password.Text;

            User user = await _service.GetUser(email, password);
            if (user == null)
            {
                await DisplayAlert("Erro!", "Nenhum usuário encontrado!", "OK");
            }
            else
            {
                App.Current.Properties.Add("User", JsonConvert.SerializeObject(user));
                App.Current.MainPage = new NavigationPage(new Start());
            }
        }
    }
}