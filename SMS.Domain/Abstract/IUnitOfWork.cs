using SMS.Domain.Entities;
using System;

namespace SMS.Domain.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Student> Students { get; }

        void Save();
    }
}
