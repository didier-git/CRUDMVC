using CRUDMVC.Context;
using CRUDMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDMVC.Controllers
{
    public class UsuarioController :Controller
    {
        public readonly DataContext _context;

        public UsuarioController( DataContext context)
        {
            _context = context;
        }

        public IActionResult Index(Usuario usuario)
        {
            
            
            return View(usuario);

        }

        public IActionResult Registro()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro([Bind("Id,UsuarioName,password")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
               await _context.Usuarios.AddAsync(usuario);
               await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(InicioSesion));
        }
        public IActionResult InicioSesion()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InicioSesion([Bind("UsuarioName,password")] Usuario usuario)
        {
            var user =  _context.Usuarios.Where(u => u.UsuarioName == usuario.UsuarioName && u.password == usuario.password).FirstOrDefault();
            if(user != null)
            {
                return RedirectToAction("Index","Home");
            }
            else
            {
                return NotFound();
            }
        }

    }
}
