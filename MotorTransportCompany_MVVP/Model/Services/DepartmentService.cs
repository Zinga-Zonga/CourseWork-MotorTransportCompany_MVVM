using MotorTransportCompany_MVVP.Model.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.Services
{
    internal class DepartmentService : IDAO<Department>
    {
        private IDAO<Department> _departmentDao = new DepartmentDAO();
        public void Add(Department entity)
        {
            _departmentDao.Add(entity);
        }

        public void Delete(int id)
        {
            _departmentDao.Delete(id);
        }

        public List<Department> GetAll()
        {
            return _departmentDao.GetAll();
        }

        public Department GetEntityById(int id)
        {
            return _departmentDao.GetEntityById(id);
        }

        public void Update(Department entity)
        {
            _departmentDao.Update(entity);
        }
    }
}
