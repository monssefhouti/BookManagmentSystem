using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookManagment.Models.Interfaces
{
    public interface IBookRepository<T>
    {
        IList<T> AllItems();
        T Find(int id);
        void AddItem(T entity);
        void DeleteItem(int id);
        void UpdateItem(T entity);
        void UpdateItemById(int id,T entity);
    }
}
