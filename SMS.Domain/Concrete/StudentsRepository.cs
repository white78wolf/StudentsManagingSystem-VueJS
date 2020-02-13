using SMS.Domain.Abstract;
using SMS.Domain.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SMS.Domain.Concrete
{
    public class StudentsRepository: IRepository<Student>//IStudentsRepository
    {
        //IEnumerable<T> GetAll();
        //T Get(int id);
        //IEnumerable<T> Find(Func<T, bool> predicate);
        //void Create(T item);
        //void Update(T item);
        //void Delete(T item);

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

        //StudentsContext _context;

        //public StudentsRepository(StudentsContext context)
        //{
        //    _context = context;
        //}

        //public IEnumerable<Student> Students => _context.Students;

        //public void SaveStudent(Student student)
        //{
        //    if (student.Id == 0)
        //        _context.Students.Add(student);
        //    else
        //    {
        //        Student dbEntry = _context.Students.Find(student.Id);
        //        if (dbEntry != null)
        //        {
        //            dbEntry.UniqId = student.UniqId;
        //            dbEntry.Name = student.Name;
        //            dbEntry.MiddleName = student.MiddleName;
        //            dbEntry.LastName = student.LastName;
        //            dbEntry.Gender = student.Gender;
        //        }
        //    }
        //    _context.SaveChanges();
        //}

        //public Student DeleteStudent(Student student)
        //{            
        //    if (student != null)
        //    {
        //        _context.Students.Remove(student);
        //        _context.SaveChanges();
        //    }
        //    return student;
        //}        
    }
}
