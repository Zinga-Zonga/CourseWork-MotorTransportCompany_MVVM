using MotorTransportCompany_MVVP.Model.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.Services
{
    internal class FuelTypeService : IService<FuelType>
    {
        private IDAO<FuelType> _fuelTypeDao = new FuelTypeDAO();
        public void Add(FuelType entity)
        {
            _fuelTypeDao.Add(entity);
        }

        public void Delete(int id)
        {
            _fuelTypeDao.Delete(id);
        }

        public List<FuelType> GetAll()
        {
            return _fuelTypeDao.GetAll();
        }

        public FuelType GetEntityByID(int id)
        {
            return _fuelTypeDao.GetEntityById(id);
        }

        public void Update(FuelType entity)
        {
            _fuelTypeDao.Update(entity);
        }
    }
}
