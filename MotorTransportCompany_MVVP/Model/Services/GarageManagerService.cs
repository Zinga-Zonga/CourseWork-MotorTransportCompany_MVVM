using MotorTransportCompany_MVVP.Model.DAOs;
using MotorTransportCompany_MVVP.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.Services
{
    internal class GarageManagerService
    {
        private IDAO<GarageManager> _garageManagerDao = new GarageManagerDAO();

        private IViewDAO<GarageManagerSqlView> _garageManagerViewDao = new GarageManagerViewDAO();

        public void Add(GarageManager entity)
        {
            _garageManagerDao.Add(entity);
        }

        public void Delete(int id)
        {
            _garageManagerDao.Delete(id);
        }

        public List<GarageManagerSqlView> GetAll()
        {
            return _garageManagerViewDao.GetAll();
        }

        public List<GarageManagerSqlView> GetEntityByID(int id)
        {
            return _garageManagerViewDao.GetEntityById(id);
        }

        public void Update(GarageManager entity)
        {
            _garageManagerDao.Update(entity);
        }
    }
}
