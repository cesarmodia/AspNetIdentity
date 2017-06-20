using AspNetIdentity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetIdentity.EF
{
    public class GestionUsuarios : UserManager<Usuario>
    {
        public GestionUsuarios(IUserStore<Usuario> AlmacenUsuarios) : base(AlmacenUsuarios)
        {
        }

        public static GestionUsuarios Crear(IdentityFactoryOptions<GestionUsuarios> opciones, IOwinContext contexto)
        {
            ContextoIdentity baseDatos = contexto.Get<ContextoIdentity>();
            GestionUsuarios gestionUsuarios = new GestionUsuarios(new UserStore<Usuario>(baseDatos));

            return gestionUsuarios;
        }
    }
}