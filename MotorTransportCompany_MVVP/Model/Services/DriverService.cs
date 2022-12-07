using MotorTransportCompany_MVVP.Model.DAOs;
using MotorTransportCompany_MVVP.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.Services
{
    internal class DriverService
    {
        private IDAO<Driver> _driversDao = new DriversDAO();
        private IViewDAO<DriverSqlView> _driversSqlViewDao = new DriversSqlViewDAO();
        public void Add(Driver entity)
        {
            _driversDao.Add(entity);
        }

        public void Delete(int id)
        {
            _driversDao.Delete(id);
        }

        public List<DriverSqlView> GetAll()
        {
            return _driversSqlViewDao.GetAll();
        }

        public DriverSqlView GetEntityByID(int id)
        {
            return _driversSqlViewDao.GetEntityById(id);
        }

        public void Update(Driver entity)
        {
            _driversDao.Update(entity);
        }
    }
}
