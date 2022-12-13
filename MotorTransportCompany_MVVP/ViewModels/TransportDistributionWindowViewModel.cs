using MotorTransportCompany_MVVP.Model.Services;
using MotorTransportCompany_MVVP.Util;
using MotorTransportCompany_MVVP.Util.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MotorTransportCompany_MVVP.ViewModels
{
    internal class TransportDistributionWindowViewModel : IDialogViewModel
    {
        public int Id { get; set; }
        public string Department { get; set; }
        public string FCS { get; set; }
        public string NumberAndModel { get; set; }
        public ICommand OkCommand { get; }
        public bool? DialogResult { get; set; }
        
        static TransportService _transportService = new TransportService();
        public List<string> NumbersAndModels { get; set; } = new List<string>();
        public List<string> FCSs { get; set; } = new List<string>();
        TransportDistributionWindowViewModel()
        {
            OkCommand = new RelayCommand(Ok, CanOk);
            FillNumbersAndModels();
        }
        private void FillNumbersAndModels()
        {
            foreach(var ts in _transportService.GetAll())
            {
                NumbersAndModels.Add($"{ts.Number}|{ts.Model}");
            }
        }
        private void FillFCSs()
        {
            foreach(var )
        }
        public void Ok()
        {
            DialogResult = true;
        }
        public bool CanOk()
        {
            return true;
        }

    }
}
