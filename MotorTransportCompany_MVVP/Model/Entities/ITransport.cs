using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model
{
    internal interface ITransport
    {
        TransportSpecifications Model { get; set; }
        string Number { get; set; }
        string Department { get; set; }
        TechnicalCondition TechnicalCondition { get; set; }

    }
}
