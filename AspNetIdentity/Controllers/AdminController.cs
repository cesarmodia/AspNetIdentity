using AspNetIdentity.EF;
using AspNetIdentity.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetIdentity.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            IEnumerable<Usuario> usuarios = ObtenerGestionUsuarios().Users;
            return View(usuarios);
        }

        private GestionUsuarios ObtenerGestionUsuarios()
        {
            return HttpContext.GetOwinContext().GetUserManager<GestionUsuarios>();
        }
    }
}