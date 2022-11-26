using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorTransportCompany_MVVP.Model.Data
{
    internal class Repository<T> : IRepository<T> where T : IEntity
    {
        public List<T> _repository = new List<T>();

        public void Add(T entity)
        {
            //_repository.Add(entity);
            // AddToDB
        }

        public List<T> GetAll()
        {

            //string path = typeof(T).Name.ToString();

            //if (!File.Exists(Application.persistentDataPath + $"/{path}.json"))
            //{
            //    return null;
            //}

            //using (var f = File.OpenText(Application.persistentDataPath + $"/{path}.json"))
            //{
            //    Debug.Log("Repository");
            //    Debug.Log(Application.persistentDataPath);
            //    var json = f.ReadToEnd();
            //    _repository = JsonConvert.DeserializeObject<List<T>>(json,
            //        new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            //    return _repository;

            //}
            return _repository;
        }
        public List<T> GetList()
        {
            return _repository;
        }
        public T GetById(int id)
        {
            return _repository.FirstOrDefault(entity => entity.Id == id);
        }


        public void Remove(int id)
        {
            var entit = _repository.FirstOrDefault(entity => entity.Id == id);

            if (entit != null)
            {
                _repository.Remove(entit);
            }
        }
    }
}
