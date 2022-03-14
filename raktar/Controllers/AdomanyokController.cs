using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using raktar.Data;
using raktar.Models;

namespace raktar.Controllers
{
    public class AdomanyokController : Controller
    {
        private readonly ApplicationDbContext _context;

        //public string ElnevKeres { get; private set; }
        //public string KategoriaKeres { get; private set; }

        public AdomanyokController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Adomanyok
        public async Task<IActionResult> Index(string ElnevKeres, string KategoriaKeres)
        {
            Kereses ujKereses = new Kereses();
            var adomanyok = _context.Adomany.Select(x => x);

            if (!string.IsNullOrEmpty(ElnevKeres))
            {
                ujKereses.ElnevKeres = ElnevKeres;
                adomanyok = adomanyok.Where(x => x.Elnevezes.Contains(ElnevKeres));
            }
            if (!string.IsNullOrEmpty(KategoriaKeres))
            {
                ujKereses.KategoriaKeres = KategoriaKeres;
                adomanyok = adomanyok.Where(x => x.Kategoria.Equals(KategoriaKeres));
            }

            ujKereses.KategoriaLista = new SelectList(await _context.Adomany.Select(x => x.Kategoria).Distinct().ToListAsync());
            ujKereses.AdomanyLista = await adomanyok.ToListAsync();
            return View(ujKereses);
        }

        // GET: Adomanyok/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adomany = await _context.Adomany.FirstOrDefaultAsync(m => m.Id == id);
            
            if (adomany == null)
            {
                return NotFound();
            }

            return View(adomany);
        }

        // GET: Adomanyok/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Adomanyok/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Elnevezes,Kategoria,CsomEgyseg,Darab")] Adomany adomany)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adomany);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adomany);
        }

        // GET: Adomanyok/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adomany = await _context.Adomany.FindAsync(id);
            if (adomany == null)
            {
                return NotFound();
            }
            return View(adomany);
        }

        // POST: Adomanyok/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Elnevezes,Kategoria,CsomEgyseg,Darab")] Adomany adomany)
        {
            if (id != adomany.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adomany);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdomanyExists(adomany.Id))
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
            return View(adomany);
        }

        // GET: Adomanyok/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adomany = await _context.Adomany
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adomany == null)
            {
                return NotFound();
            }

            return View(adomany);
        }

        // POST: Adomanyok/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adomany = await _context.Adomany.FindAsync(id);
            _context.Adomany.Remove(adomany);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdomanyExists(int id)
        {
            return _context.Adomany.Any(e => e.Id == id);
        }
    }
}
