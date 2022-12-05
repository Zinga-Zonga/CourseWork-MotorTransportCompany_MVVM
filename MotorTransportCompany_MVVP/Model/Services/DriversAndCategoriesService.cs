using MotorTransportCompany_MVVP.Model.DAOs;
using MotorTransportCompany_MVVP.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.Services
{
    internal class DriversAndCategoriesService : IService<DriversAndCategories>
    {
        private IDAO<DriversAndCategories> _driversAndCategoriesDao = new DriversCategoriesDAO();
        public void Add(DriversAndCategories entity)
        {
            _driversAndCategoriesDao.Add(entity);
        }

        public void Delete(int id)
        {
            _driversAndCategoriesDao.Delete(id);
        }

        public List<DriversAndCategories> GetAll()
        {
            return _driversAndCategoriesDao.GetAll();
        }

        public DriversAndCategories GetEntityByID(int id)
        {
            return _driversAndCategoriesDao.GetEntityById(id);
        }

        public void Update(DriversAndCategories entity)
        {
            _driversAndCategoriesDao.Update(entity);
        }
    }
}
