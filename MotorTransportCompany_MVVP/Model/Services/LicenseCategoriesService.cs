using MotorTransportCompany_MVVP.Model.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.Services
{
    internal class LicenseCategoriesService : IService<LicenseCategory>
    {
        private IDAO<LicenseCategory> _licenseCategoriesDao = new LicenseCategoriesDAO();
        public void Add(LicenseCategory entity)
        {
            _licenseCategoriesDao.Add(entity);
        }

        public void Delete(int id)
        {
            _licenseCategoriesDao.Delete(id);
        }

        public List<LicenseCategory> GetAll()
        {
            return _licenseCategoriesDao.GetAll();
        }

        public LicenseCategory GetEntityByID(int id)
        {
            return _licenseCategoriesDao.GetEntityById(id);
        }

        public void Update(LicenseCategory entity)
        {
            _licenseCategoriesDao.Update(entity);
        }
    }
}
