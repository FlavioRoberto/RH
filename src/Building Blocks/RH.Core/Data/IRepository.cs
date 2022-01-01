using System;

namespace RH.Core.DomainObjects.Data
{
    public interface IRepository<T> : IDisposable where T : IAgregateRoot
    {

    }  
}
