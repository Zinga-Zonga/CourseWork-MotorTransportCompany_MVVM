using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.ViewModels
{
    internal class DriversWindowViewModel
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string FCS { get; set; }

        public string BirthdayDate { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public int PassportNumber { get; set; }
        public int LicenseNumber { get; set; }
        public string Categories { get; set; }

        private class ComboCheckDriversCategories
        {

        }
    }
}
