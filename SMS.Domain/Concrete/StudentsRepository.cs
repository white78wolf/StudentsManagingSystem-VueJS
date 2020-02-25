using SMS.Domain.Abstract;
using SMS.Domain.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SMS.Domain.Concrete
{
    public class StudentsRepository: IRepository<Student>
    {
        private StudentsContext db;

        public StudentsRepository(StudentsContext context)
        {
            db = context;
        }

        public IEnumerable<Student> GetAll()
        {
            return db.Students;
        }

        public Student Get(int id)
        {
            return db.Students.Find(id);
        }

        public IEnumerable<Student> Find(Func<Student, bool> predicate)
        {
            return db.Students.Where(predicate).ToList();
        }

        public void Create(Student student)
        {
            db.Add(student);
        }

        public void Update(Student student)
        {
            db.Entry(student).State = EntityState.Modified;
        }        

        public void Delete(Student student)
        {
            db.Remove(student);
        }       
    }
}