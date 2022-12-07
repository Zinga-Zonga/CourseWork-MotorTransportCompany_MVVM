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
    internal class TransportDistributionService
    {

    
        private IDAO<TransportDistribution> _transportDistributionDao = new TransportDistributionDAO();
        private IViewDAO<TransportDistributionSqlView> _transportDistributionSqlViewDao = new TransportDistributionSqlViewDAO();
        public void Add(TransportDistribution entity)
        {
            _transportDistributionDao.Add(entity);
        }

        public void Delete(int id)
        {
            _transportDistributionDao.Delete(id);
        }

        public List<TransportDistributionSqlView> GetAll()
        {
            return _transportDistributionSqlViewDao.GetAll();
        }

        public TransportDistributionSqlView GetEntityByID(int id)
        {
            return _transportDistributionSqlViewDao.GetEntityById(id);
        }

        public void Update(TransportDistribution entity)
        {
            _transportDistributionDao.Update(entity);
        }
    }
}
