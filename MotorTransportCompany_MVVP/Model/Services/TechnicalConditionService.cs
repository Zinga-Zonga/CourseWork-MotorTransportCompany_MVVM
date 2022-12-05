using MotorTransportCompany_MVVP.Model.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.Services
{
    internal class TechnicalConditionService : IService<TechnicalCondition>
    {
        private IDAO<TechnicalCondition> _technicalConditionDao = new TechnicalConditionsDAO();
        public void Add(TechnicalCondition entity)
        {
            _technicalConditionDao.Add(entity);
        }

        public void Delete(int id)
        {
            _technicalConditionDao.Delete(id);
        }

        public List<TechnicalCondition> GetAll()
        {
            return _technicalConditionDao.GetAll();
        }

        public TechnicalCondition GetEntityByID(int id)
        {
            return _technicalConditionDao.GetEntityById(id);
        }

        public void Update(TechnicalCondition entity)
        {
            _technicalConditionDao.Update(entity);
        }
    }
}
