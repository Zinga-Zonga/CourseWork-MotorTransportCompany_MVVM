using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.DAOs
{
    internal interface IViewDAO<T> where T : IEntity
    {
        List<T> GetAll();
        List<T> GetEntityById(int id);
    }
}
