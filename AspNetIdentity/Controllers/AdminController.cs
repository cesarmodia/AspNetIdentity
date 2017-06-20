using AspNetIdentity.EF;
using AspNetIdentity.Models;
using AspNetIdentity.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpGet]
        public ActionResult Create()
        {
            return View(new CreateViewModel());
        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                string telefono = string.Empty;
                if (!string.IsNullOrEmpty(vm.Telefono))
                {
                    telefono = vm.Telefono;
                }

                Usuario usuario = new Usuario
                {
                    UserName = vm.Nombre,
                    Email = vm.Email,
                    PhoneNumber = vm.Telefono
                };

                GestionUsuarios gestionUsuarios = ObtenerGestionUsuarios();
                IdentityResult resultado = await gestionUsuarios.CreateAsync(usuario, vm.Clave);
                if (resultado.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    registrarErrores(resultado);
                }
            }
            return View(vm);
        }
        private void registrarErrores(IdentityResult resultado)
        {
            if (resultado != null)
            {
                foreach (string detalleError in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, detalleError);
                }
            }
        }
        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            GestionUsuarios gestionUsuarios = ObtenerGestionUsuarios();
            Usuario usuario = await gestionUsuarios.FindByIdAsync(id);

            if (usuario != null)
            {
                IdentityResult resultado = await gestionUsuarios.DeleteAsync(usuario);
                if (resultado.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error", resultado.Errors);
                }
            }
            else
            {
                return View("Error", new string[] { "Usuario no encontrado" });
            }
        }
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            GestionUsuarios gestionUsuarios = ObtenerGestionUsuarios();

            Usuario usuario = await gestionUsuarios.FindByIdAsync(id);
            if (usuario != null)
            {
                return View(usuario);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public async Task<ActionResult> Edit(string id, string Email, string PhoneNumber, string Password)
        {
            GestionUsuarios gestionUsuarios = ObtenerGestionUsuarios();
            
            Usuario usuario = await gestionUsuarios.FindByIdAsync(id);

            if (usuario != null)
            {
                usuario.Email = Email;
                usuario.PhoneNumber = PhoneNumber;

                if (!string.IsNullOrEmpty(Password))
                {
                    IdentityResult validacionClave = await gestionUsuarios.PasswordValidator.ValidateAsync(Password);
                    if (validacionClave.Succeeded)
                    {
                        usuario.PasswordHash = gestionUsuarios.PasswordHasher.HashPassword(Password);
                        IdentityResult resultado = await gestionUsuarios.UpdateAsync(usuario);
                        if (resultado.Succeeded)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            registrarErrores(resultado);
                        }
                    }
                    else
                    {
                        registrarErrores(validacionClave);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "La clave no puede estar vacía");
                }
            }
            else
            {
                ModelState.AddModelError("", "Usuario no encontrado");
            }
            return View(usuario);
        }
    }
}