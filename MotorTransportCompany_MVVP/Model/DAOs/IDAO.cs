using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.DAOs
{
    internal interface IDAO<E> where E : IEntity
    {
        List<E> GetAll();
        E getEntityById(int id);
        void Delete(int id);
        void Add(E entity);
        void Update(E entity);


    }
}
