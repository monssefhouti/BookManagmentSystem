using BookManagment.Models.Entities;
using BookManagment.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookManagment.Models.Repositories
{
    public class BooksRepository : IBookRepository<Books>
    {
        DataContext db;
        public BooksRepository(DataContext db)
        {
            this.db = db;
        }
        public void AddItem(Books book)
        {
            db.Add(book);
            db.SaveChanges();
        }

        public IList<Books> AllItems()
        {
            return db.Books.ToList();
        }

        public void DeleteItem(int id)
        {
            var find = Find(id);
            db.Remove(find);
            db.SaveChanges();
        }

        public Books Find(int id)
        {
            var Find = db.Books.SingleOrDefault(book => book.Id == id);
            return Find;
        }

        public void UpdateItem(Books book)
        {
            db.Attach(book);
            db.Entry(book).State = EntityState.Modified;
        }

        public void UpdateItemById(int id, Books entity)
        {
            db.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
        }
    }
}
