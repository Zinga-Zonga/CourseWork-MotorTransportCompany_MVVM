using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.Services
{
    internal interface IService<T>
    {
        List<T> GetAll();
        T GetEntityByID(int id);
        void Delete(int id);
        void Add(T entity);
        void Update(T entity);
    }
}
