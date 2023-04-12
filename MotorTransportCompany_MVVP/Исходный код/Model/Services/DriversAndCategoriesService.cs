using MotorTransportCompany_MVVP.Model.DAOs;
using MotorTransportCompany_MVVP.Model.Domain;
using MotorTransportCompany_MVVP.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.Services
{
    internal class DriversAndCategoriesService
    {
        private IDAO<DriversAndCategories> _driversAndCategoriesDao = new DriversCategoriesDAO();
        private IViewDAO<DriversAndCategoriesSqlView> _driversAndCategoriesViewDao = new DriversCategoriesSqlViewDAO();
        public void Add(DriversAndCategories entity)
        {
            _driversAndCategoriesDao.Add(entity);
        }
        public List<DriversAndCategories> GetTable()
        {
            return _driversAndCategoriesDao.GetAll();
        }
        public void Delete(int id)
        {
            _driversAndCategoriesDao.Delete(id);
        }

        public List<DriversAndCategoriesSqlView> GetAll()
        {
            return _driversAndCategoriesViewDao.GetAll();
        }

        public List<DriversAndCategoriesSqlView> GetEntityByID(int id)
        {
            return _driversAndCategoriesViewDao.GetEntityById(id);
        }

        public void Update(DriversAndCategories entity)
        {
            _driversAndCategoriesDao.Update(entity);
        }
    }
}
