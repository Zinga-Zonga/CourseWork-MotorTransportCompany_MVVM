using MotorTransportCompany_MVVP.Model.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.Services
{
    internal class DriverService : IService<Driver>
    {
        private IDAO<Driver> _driversDao = new DriversDAO();
        public void Add(Driver entity)
        {
            _driversDao.Add(entity);
        }

        public void Delete(int id)
        {
            _driversDao.Delete(id);
        }

        public List<Driver> GetAll()
        {
            return _driversDao.GetAll();
        }

        public Driver GetEntityByID(int id)
        {
            return _driversDao.GetEntityById(id);
        }

        public void Update(Driver entity)
        {
            _driversDao.Update(entity);
        }
    }
}
