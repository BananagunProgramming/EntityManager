
using System;
using System.Collections.Generic;

namespace EntityManager.Abstract
{
    public interface IServiceQueryBase
    {
        T GetEntity<T>(Guid id) where T : class;
        IEnumerable<T> GetAllEntities<T>() where T : class;
    }
}
