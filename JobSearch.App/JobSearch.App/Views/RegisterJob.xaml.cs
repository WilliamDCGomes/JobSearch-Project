using JobSearch.App.Models;
using JobSearch.App.Utility.Load;
using JobSearch.Domain.Models;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSearch.App.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobSearch.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterJob : ContentPage
    {
        private JobService _service;
        public RegisterJob()
        {
            InitializeComponent();
            _service = new JobService();
        }

        private void GoBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void Save(object sender, EventArgs e)
        {
            TxtMessages.Text = String.Empty;
            User user = JsonConvert.DeserializeObject<User>(App.Current.Properties["User"].ToString());
            double salaryI=0;
            double salaryF=0;
            if (TxtInitialSalary.Text != null)
            {
                salaryI = Double.Parse(TxtInitialSalary.Text.Replace(",", "."));
            }
            if(TxtFinalSalary.Text != null)
            {
                salaryF = Double.Parse(TxtFinalSalary.Text.Replace(",", "."));
            }
            Job job = new Job()
            {
                Company = TxtCompany.Text,
                JobTitle = TxtJobTitle.Text,
                CityState = TxtCityState.Text,
                InitialSalary = salaryI,
                FinalSalary = salaryF,
                ContractType = (RBCLR.IsChecked) ? "CLT" : "PJ",
                TecnologyTools = TxtTecnologyTools.Text,
                CompanyDescription = TxtCompanyDescription.Text,
                JobDescription = TxtJobDescription.Text,
                Benefits = TxtBenefits.Text,
                InterestedSendEmailTo = TxtInterestedSendEmailTo.Text,
                PublicationDate = DateTime.Now,
                UserId = user.Id
            };
            await Navigation.PushPopupAsync(new Loading());
            ResponseService<Job> responseService = await _service.AddJob(job);
            if (!responseService.IsSucess)
            {
                if (responseService.StatusCode == 400)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var dicKey in responseService.Errors)
                    {
                        foreach (var message in dicKey.Value)
                        {
                            sb.AppendLine(message);
                        }
                        TxtMessages.Text = sb.ToString();
                    }
                }
                else
                {
                    await DisplayAlert("Erro!", "Ops! JobSearch foi pras cucuias...", "OK");
                }
                await Navigation.PopAllPopupAsync();
            }
            else
            {
                await Navigation.PopAllPopupAsync();
                await DisplayAlert("Vaga cadastrada!", "Vaga cadastrada com sucesso", "OK");
                await Navigation.PopAsync();
            }
        }
    }
}