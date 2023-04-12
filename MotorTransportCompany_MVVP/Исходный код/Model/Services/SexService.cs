using MotorTransportCompany_MVVP.Model.DAOs;
using MotorTransportCompany_MVVP.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.Services
{
    internal class SexService : IService<SexType>
    {
        private IDAO<SexType> _sexTypeDao = new SexDAO();
        public void Add(SexType entity)
        {
            _sexTypeDao.Add(entity);
        }

        public void Delete(int id)
        {
            _sexTypeDao.Delete(id);
        }

        public List<SexType> GetAll()
        {
            return _sexTypeDao.GetAll();
        }

        public SexType GetEntityByID(int id)
        {
            return _sexTypeDao.GetEntityById(id);
        }

        public void Update(SexType entity)
        {
            _sexTypeDao.Update(entity);
        }
    }
}
