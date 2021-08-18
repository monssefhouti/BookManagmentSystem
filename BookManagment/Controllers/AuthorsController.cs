using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookManagment.Models.Entities;
using BookManagment.Models.ViewModels;
using BookManagment.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BookManagment.Controllers
{
    
    public class AuthorsController : Controller
    {
        private readonly IBookRepository<Authors> authorRep;

        public AuthorsController(IBookRepository<Authors> authorRep)
        {
            this.authorRep = authorRep;
        }
        
        // GET: Authors
        public IActionResult Index()
        {
            var AuthorList = authorRep.AllItems();
            return View(AuthorList);
        }

        // GET: Authors/Details/5
        public IActionResult Details(int id)
        {
            var authors = authorRep.Find(id);
            if (authors == null)
            {
                return NotFound();
            }

            return View(authors);
        }
        [Authorize]
        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create(Authors authors)
        {
            if (ModelState.IsValid)
            {
                authorRep.AddItem(authors);
                return RedirectToAction(nameof(Index));
            }
            return View(authors);
        }

        // GET: Authors/Edit/5
        public IActionResult Edit(int id)
        {

            var authors = authorRep.Find(id);
            if (authors == null)
            {
                return NotFound();
            }
            return View(authors);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,Authors authors)
        {
            if (id != authors.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    authorRep.UpdateItemById(id, authors);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorsExists(authors.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(authors);
        }

        // GET: Authors/Delete/5
        public IActionResult Delete(int id)
        {
           

            var authors = authorRep.Find(id);
            if (authors == null)
            {
                return NotFound();
            }
            
            return View(authors);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            authorRep.DeleteItem(id);
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorsExists(int id)
        {
            return authorRep.AllItems().Any(x => x.Id == id);
        }
    }
}
