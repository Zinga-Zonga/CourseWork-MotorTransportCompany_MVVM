using MotorTransportCompany_MVVP.Model.DAOs;
using MotorTransportCompany_MVVP.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.Services
{
    internal class TransportSpecificationService
    {
        private IDAO<TransportSpecification> _transportSpecificationDao = new TransportSpecificationsDAO();
        private IViewDAO<TransportSpecificationSqlView> _transportSpecificationViewDAO = new TransportSpecificationViewDAO();
        public void Add(TransportSpecification entity)
        {
            _transportSpecificationDao.Add(entity);
        }

        public void Delete(int id)
        {
            _transportSpecificationDao.Delete(id);
        }

        public List<TransportSpecificationSqlView> GetAll()
        {
            return _transportSpecificationViewDAO.GetAll();
        }

        public List<TransportSpecificationSqlView> GetEntityByID(int id)
        {
            return _transportSpecificationViewDAO.GetEntityById(id);
        }

        public void Update(TransportSpecification entity)
        {
            _transportSpecificationDao.Update(entity);
        }
    }
}
