using MotorTransportCompany_MVVP.Model.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.Services
{
    internal class TransportService : IService<Transport>
    {
        private IDAO<Transport> _transportDao = new TransportDAO();
        public void Add(Transport entity)
        {
            _transportDao.Add(entity);
        }

        public void Delete(int id)
        {
            _transportDao.Delete(id);
        }

        public List<Transport> GetAll()
        {
            return _transportDao.GetAll();
        }

        public Transport GetEntityByID(int id)
        {
            return _transportDao.GetEntityById(id);
        }

        public void Update(Transport entity)
        {
            _transportDao.Update(entity);
        }
    }
}
