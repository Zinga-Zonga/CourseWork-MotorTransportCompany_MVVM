using MotorTransportCompany_MVVP.Model.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.Services
{
    internal class TransportSpecificationService : IService<TransportSpecification>
    {
        private IDAO<TransportSpecification> _transportSpecificationDao = new TransportSpecificationsDAO();
        public void Add(TransportSpecification entity)
        {
            _transportSpecificationDao.Add(entity);
        }

        public void Delete(int id)
        {
            _transportSpecificationDao.Delete(id);
        }

        public List<TransportSpecification> GetAll()
        {
            return _transportSpecificationDao.GetAll();
        }

        public TransportSpecification GetEntityByID(int id)
        {
            return _transportSpecificationDao.GetEntityById(id);
        }

        public void Update(TransportSpecification entity)
        {
            _transportSpecificationDao.Update(entity);
        }
    }
}
