using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model
{
    internal interface IRepository<T>
    {
        T GetById(int id);
        List<T> GetAll();
        void Add(T entity);
        void Remove(int id);
        List<T> GetList();
    }
}
