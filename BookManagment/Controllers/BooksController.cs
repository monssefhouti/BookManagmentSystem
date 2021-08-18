using BookManagment.Models.Entities;
using BookManagment.Models.Interfaces;
using BookManagment.Models.ViewModels;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookManagment.Controllers
{
   [Microsoft.AspNetCore.Authorization.Authorize]
    public class BooksController : Controller
    {
        private readonly IBookRepository<Books> books;
        private readonly IBookRepository<Authors> aut;
        [Obsolete]
        private IHostingEnvironment hosting;
        private readonly DataContext db;

        [Obsolete]
        public BooksController(IBookRepository<Books> books,IBookRepository<Authors> aut, IHostingEnvironment hosting,DataContext db)
        {
            this.books = books;
            this.aut = aut;
            this.hosting = hosting;
            this.db = db;
        }
        [Route("Books/Index")]
        // GET: Books
        public IActionResult Index()
        {
            var BookL = books.AllItems();
            return View(BookL);
        }

        // GET: Books/Details/5
        public IActionResult Details(int id)
        {
            var find = books.Find(id);
            if (find == null)
            {
                return NotFound();
            }

            return View(find);
        }
        #region Create
        // GET: Books/Create
        public IActionResult Create()
        {
            var model = new EntitiesViewModel
            {
                AuthList = FillAuthList()
            };
            return View(model);
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EntitiesViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.File!=null)
                {
                    string uploads = Path.Combine(hosting.WebRootPath, "Upload");
                    string fullpath = Path.Combine(uploads, model.File.FileName);
                    model.File.CopyTo(new FileStream(fullpath, FileMode.Create,FileAccess.Write));
                }
                if(model.AuthorID == -1)
                {
                    ViewBag.Message = "Please, Select an Author.";
                    return View(FillInfo());
                }
                var authors = aut.Find(model.AuthorID);
                  Books b = new Books
                    {
                        BookName = model.BookName,
                        Description = model.Description,
                        ImageUrl = model.File.FileName,
                        PageNumber = model.PageNumber,
                        ReleaseDate = model.ReleaseDate,
                        Author = authors

                    };
                    books.AddItem(b);
                    db.SaveChanges();
               
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Please Fill All Fields");
            return View(model);
        }
        #endregion
        #region Edit
        // GET: Books/Edit/5
        public IActionResult Edit(int id)
        {
         
            var Find = books.Find(id);
            EntitiesViewModel EntityVM = new()
            {
                Id = Find.Id,
                BookName = Find.BookName,
                Description = Find.Description,
                PageNumber = Find.PageNumber,
                ImageUrl = Find.ImageUrl,
                AuthList = aut.AllItems().ToList(),
                ReleaseDate= Find.ReleaseDate
            };
            return View(EntityVM);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public IActionResult Edit(int id, EntitiesViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.File != null)
                    {
                        string uploads = Path.Combine(hosting.WebRootPath,"Upload");
                        string fullpath = Path.Combine(uploads, model.File.FileName);
                        model.File.CopyTo(new FileStream(fullpath, FileMode.Create));
                    }
                    var authors = aut.Find(model.AuthorID);
                    Books b = new Books()
                    {
                        Id = model.Id,
                        BookName = model.BookName,
                        Description = model.Description,
                        ImageUrl = model.File.FileName,
                        PageNumber = model.PageNumber,
                        ReleaseDate = model.ReleaseDate,
                        Author = authors

                    };
                    books.UpdateItem(b);
                    db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BooksExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }
            return View(model);
        }
        #endregion
        #region Delete
        // GET: Books/Delete/5
        public IActionResult Delete(int id)
        {

            var Find = books.Find(id);
            if (Find == null)
            {
                return NotFound();
            }

            return View(Find);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            books.DeleteItem(id);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool BooksExists(int id)
        {
            return books.AllItems().Any(e => e.Id == id);
        }
        #endregion

        public List<Authors> FillAuthList()
        {
            var Aut = aut.AllItems().ToList();
            Aut.Insert(0, new Authors { Id = -1, Name = "Select An Author" });
            return Aut;
        }
        EntitiesViewModel FillInfo()
        {
            var vmodel = new EntitiesViewModel
            {
                AuthList = FillAuthList()
            };
            return vmodel;
        }
    }
}
