
using System;

namespace EntityManager.Abstract
{
    public interface IServiceCommandBase
    {
        //T GetEntity<T>(Guid id) where T : class;
        //IEnumerable<T> GetAllEntities<T>() where T : class;
        void UpdateEntity<T>(T model) where T : class;
        void DeleteEntity<T>(Guid id) where T : class;
        void CreateEntity<T>(T input) where T : class;
    }
}
