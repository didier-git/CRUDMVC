using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUDMVC.Context;
using CRUDMVC.Models;

namespace CRUDMVC.Controllers
{
    public class ComprasController : Controller
    {
        private readonly DataContext _context;

        public ComprasController(DataContext context)
        {
            _context = context;
        }

        // GET: Compras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Compras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,FechaDeCompra,Monto,LugarDeCompra")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(compra);
        }



        // GET: Compras
        public async Task<IActionResult> Index()
        {
            var lugares = await _context.Compras.Select(c => c.LugarDeCompra).Distinct().ToListAsync();
            ViewBag.Lugares = lugares;

              return await _context.Compras.ToListAsync() != null ? 
                          View(await _context.Compras.ToListAsync()) :
                          Problem("Entity set 'DataContext.Compras'  is null.");
        }

        public async Task<JsonResult> IndexJ()
        {
            //var lugares = await _context.Compras.Select(c => c.LugarDeCompra).Distinct().ToListAsync();
            //ViewBag.Lugares = lugares;
            var compras = await _context.Compras.ToListAsync();
            return Json(compras); 
                 
        }

        public async Task<JsonResult> ComprasPorFiltro(string fecha, string lugar)
        {
            List<Compra> compras = new();
            int fechaD;
            if ((fecha != "" && fecha != null) && lugar != "Sin lugar")
            {
                fechaD = DateTime.Parse(fecha).Month;
                compras = await _context.Compras.Where(c => c.FechaDeCompra.Month == fechaD && c.LugarDeCompra == lugar).ToListAsync();

            }else if((fecha != "" && fecha != null) && lugar == "Sin lugar")
            {
                fechaD = DateTime.Parse(fecha).Month;
                compras = await _context.Compras.Where(c=> c.FechaDeCompra.Month == fechaD).ToListAsync();
            }
            else if (lugar !="Sin lugar" && (fecha == ""|| fecha == null))
            {
                compras = await _context.Compras.Where(c=> c.LugarDeCompra==lugar).ToListAsync();
            }
            else
            {
                compras = await _context.Compras.ToListAsync();
            }
            
            return Json(compras);
        }


        public async Task<JsonResult> CompraPorFecha(string fechaRequest)
        {
            var fecha = DateTime.Parse(fechaRequest).Month;
            var compras = await _context.Compras.Where(c=>c.FechaDeCompra.Month== fecha).ToListAsync();

            return Json(compras);
        }

        public async Task<JsonResult> ComprasPorLugar(string lugar)
        {
            var compras = await _context.Compras.Where(c=> c.LugarDeCompra == lugar).ToListAsync();

            return Json(compras);
        }

        public async Task<JsonResult> ComprasPorLugarFecha(string fecha, string lugar, string? todos)
        {
            var fechaD = DateTime.Parse(fecha).Month;
            var compras = await _context.Compras.Where(c => c.FechaDeCompra.Month == fechaD && c.LugarDeCompra == lugar).ToListAsync();
            
            return Json(compras);
        }
        
        // GET: Compras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

      

        // GET: Compras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras.FindAsync(id);
            if (compra == null)
            {
                return NotFound();
            }
            return View(compra);
        }

        // POST: Compras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,FechaDeCompra,Monto,LugarDeCompra")] Compra compra)
        {
            if (id != compra.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompraExists(compra.Id))
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
            return View(compra);
        }

        // GET: Compras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Compras == null)
            {
                return Problem("Entity set 'DataContext.Compras'  is null.");
            }
            var compra = await _context.Compras.FindAsync(id);
            if (compra != null)
            {
                _context.Compras.Remove(compra);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompraExists(int id)
        {
          return (_context.Compras?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
