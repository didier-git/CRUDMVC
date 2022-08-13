using CRUDMVC.Context;
using CRUDMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDMVC.Controllers
{
    public class UsuarioController : Controller
    {
        public readonly DataContext _context;

        public UsuarioController(DataContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([Bind("Id,UsuarioName,password")] Usuario usuario)
        {
            var user = _context.Usuarios.Where(u => u.UsuarioName == usuario.UsuarioName && u.password == usuario.password).FirstOrDefault();

            if (user != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Registro([Bind("Id,UsuarioName,password")] Usuario usuario)
        {
            var user = _context.Usuarios.Where(u => u.UsuarioName == usuario.UsuarioName).FirstOrDefault();
            if (user != null)
            {
                return View(user);
            }
            if (ModelState.IsValid)
            {
                _context.Usuarios.Add(usuario);

                return RedirectToAction("Index", "Usuario");

            }

            return View();  
           

        }
    }
}
