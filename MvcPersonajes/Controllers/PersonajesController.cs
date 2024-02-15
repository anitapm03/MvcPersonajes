using Microsoft.AspNetCore.Mvc;
using MvcPersonajes.Models;

namespace MvcPersonajes.Controllers
{
    public class PersonajesController : Controller
    {

        private IRepository repo;
        public PersonajesController(IRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Personaje> personajes = this.repo.GetPersonajes();
            return View(personajes);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Personaje personaje)
        {
            this.repo.InsertarPersonaje(personaje.IdPersonaje,
                personaje.Nombre, personaje.Imagen);
            return RedirectToAction("Index");
        }

        public IActionResult Eliminar(int idpersonaje)
        {
            this.repo.EliminarPersonaje(idpersonaje);
            return RedirectToAction("Index");
        }

        public IActionResult Modificar(int idpersonaje)
        {
            Personaje personaje = this.repo.FindPersonaje(idpersonaje);
            return View(personaje);
        }

        [HttpPost]
        public IActionResult Modificar(Personaje personaje)
        {
            //Personaje personaje = this.repo.FindPersonaje(id);
            this.repo.ModificarPersonaje(personaje.IdPersonaje,
                personaje.Nombre, personaje.Imagen);
            return RedirectToAction("Index");
        }

        public IActionResult Detalle(int idpersonaje)
        {
            Personaje personaje = this.repo.FindPersonaje(idpersonaje);
            return View(personaje);
        }
    }
}
