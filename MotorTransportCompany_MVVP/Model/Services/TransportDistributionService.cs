using MotorTransportCompany_MVVP.Model.DAOs;
using MotorTransportCompany_MVVP.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.Services
{
    internal class TransportDistributionService : IService<TransportDistribution>
    {
        private IDAO<TransportDistribution> _transportDistributionDao = new TransportDistributionDAO();
        public void Add(TransportDistribution entity)
        {
            _transportDistributionDao.Add(entity);
        }

        public void Delete(int id)
        {
            _transportDistributionDao.Delete(id);
        }

        public List<TransportDistribution> GetAll()
        {
            return _transportDistributionDao.GetAll();
        }

        public TransportDistribution GetEntityByID(int id)
        {
            return _transportDistributionDao.GetEntityById(id);
        }

        public void Update(TransportDistribution entity)
        {
            _transportDistributionDao.Update(entity);
        }
    }
}
