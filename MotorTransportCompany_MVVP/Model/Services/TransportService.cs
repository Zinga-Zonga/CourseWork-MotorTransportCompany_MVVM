using MotorTransportCompany_MVVP.Model.DAOs;
using MotorTransportCompany_MVVP.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.Services
{
    internal class TransportService
    {
        private IDAO<Transport> _transportDao = new TransportDAO();
        private TransportSqlViewDAO _transportSqlViewDao = new TransportSqlViewDAO();
        public void Add(Transport entity)
        {
            _transportDao.Add(entity);
        }

        public void Delete(int id)
        {
            _transportDao.Delete(id);
        }

        public List<TransportSqlView> GetAll()
        {
            return _transportSqlViewDao.GetAll();
        }

        public List<TransportSqlView> GetEntityByID(int id)
        {
            return _transportSqlViewDao.GetEntityById(id);
        }

        public void Update(Transport entity)
        {
            _transportDao.Update(entity);
        }
    }
}
