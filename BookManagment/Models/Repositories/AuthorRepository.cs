using BookManagment.Models.Entities;
using BookManagment.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookManagment.Models.Repositories
{

    public class AuthorRepository : IBookRepository<Authors>
    {
        DataContext db;
        public AuthorRepository(DataContext db)
        {
            this.db = db;
        }
        public void AddItem(Authors entity)
        {
            db.Authors.Add(entity);
            db.SaveChanges();
        }

        public IList<Authors> AllItems()
        {
            return db.Authors.ToList();
        }

        public void DeleteItem(int id)
        {
            var find = Find(id);
            db.Authors.Remove(find);
            db.SaveChanges();
        }

        public Authors Find(int id)
        {
            var find = db.Authors.SingleOrDefault(x => x.Id == id);
            return find;
        }

        public void UpdateItem(Authors authors)
        {
            db.Attach(authors);
            db.Entry(authors).State = EntityState.Modified;
        }

        public void UpdateItemById(int id, Authors entity)
        {
            db.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;

        }
    }
}
