using MotorTransportCompany_MVVP.Model.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.Services
{
    internal class GarageManagerService : IService<GarageManager>
    {
        private IDAO<GarageManager> _garageManagerDao = new GarageManagerDAO();
        public void Add(GarageManager entity)
        {
            _garageManagerDao.Add(entity);
        }

        public void Delete(int id)
        {
            _garageManagerDao.Delete(id);
        }

        public List<GarageManager> GetAll()
        {
            return _garageManagerDao.GetAll();
        }

        public GarageManager GetEntityByID(int id)
        {
            return _garageManagerDao.GetEntityById(id);
        }

        public void Update(GarageManager entity)
        {
            _garageManagerDao.Update(entity);
        }
    }
}
