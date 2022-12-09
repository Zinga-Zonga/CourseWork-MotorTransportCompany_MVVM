using MotorTransportCompany_MVVP.Model.DAOs;
using MotorTransportCompany_MVVP.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.Services
{
    internal class MechanicService
    {
        private IDAO<Mechanic> _mechanicDao = new MechanicsDAO();
        private IViewDAO<MechanicSqlView> _mechanicViewDao = new MechanicsViewDAO();
        public void Add(Mechanic entity)
        {
            _mechanicDao.Add(entity);
        }

        public void Delete(int id)
        {
            _mechanicDao.Delete(id);
        }

        public List<MechanicSqlView> GetAll()
        {
            return _mechanicViewDao.GetAll();
        }

        public List<MechanicSqlView> GetEntityByID(int id)
        {
            return _mechanicViewDao.GetEntityById(id);
        }

        public void Update(Mechanic entity)
        {
            _mechanicDao.Update(entity);
        }
    }
}
