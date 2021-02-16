using IntroDonetCore.DAL;
using IntroDonetCore.Services.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntroDonetCore.Services.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly IntroContext _context;

        public Repository(IntroContext context)
        {
            _context = context;
        }

        private void Save() => _context.SaveChanges();

        public void Add(T entity)
        {
            _context.Add(entity);
            Save();
        }

        public int Count(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate).Count();
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
            Save();
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public IEnumerable<T> GetByFiler(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate).ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            Save();
        }


    }

}
