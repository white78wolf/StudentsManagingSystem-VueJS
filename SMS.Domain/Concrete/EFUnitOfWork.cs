using SMS.Domain.Abstract;
using SMS.Domain.Entities;
using System;

namespace SMS.Domain.Concrete
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private StudentsContext db;
        private StudentsRepository studentsRepository;

        public EFUnitOfWork(StudentsContext context)
        {
            db = context;
        }

        public IRepository<Student> Students
        {
            get
            {
                if (studentsRepository == null)
                    studentsRepository = new StudentsRepository(db);
                return studentsRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
