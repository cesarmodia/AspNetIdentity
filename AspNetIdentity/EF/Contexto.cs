using AspNetIdentity.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AspNetIdentity.EF
{
    public class ContextoIdentity : IdentityDbContext<Usuario>
    {
        public ContextoIdentity() : base("dbIdentity")
        { }
        static ContextoIdentity()
        {
            Database.SetInitializer<ContextoIdentity>(new InicializacionBBDDIdentity());
        }
        public static ContextoIdentity Crear()
        {
            return new ContextoIdentity();
        }
    }

    public class InicializacionBBDDIdentity : DropCreateDatabaseIfModelChanges<ContextoIdentity>
    {
        protected override void Seed(ContextoIdentity context)
        {
            ProcesarConfiguracionInicial();
            base.Seed(context);
        }
        private void ProcesarConfiguracionInicial()
        {
            // Pendiente la carga de datos inicial
        }
    }
}