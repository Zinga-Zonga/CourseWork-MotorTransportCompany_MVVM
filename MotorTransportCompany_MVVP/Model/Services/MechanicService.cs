using MotorTransportCompany_MVVP.Model.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.Services
{
    internal class MechanicService : IService<Mechanic>
    {
        private IDAO<Mechanic> _mechanicDao = new MechanicsDAO();
        public void Add(Mechanic entity)
        {
            _mechanicDao.Add(entity);
        }

        public void Delete(int id)
        {
            _mechanicDao.Delete(id);
        }

        public List<Mechanic> GetAll()
        {
            return _mechanicDao.GetAll();
        }

        public Mechanic GetEntityByID(int id)
        {
            return _mechanicDao.GetEntityById(id);
        }

        public void Update(Mechanic entity)
        {
            _mechanicDao.Update(entity);
        }
    }
}
