using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ProyectoClinica2.Filters
{
    public class PacienteCustomAuthorization : AuthorizeAttribute
    {
        private readonly string _pacienteId;
        public PacienteCustomAuthorization(string pacienteId)
        {
            _pacienteId = pacienteId;

        }
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var principalContext = actionContext.RequestContext.Principal;
            var userId = principalContext.Identity.GetUserId();
            return userId == _pacienteId;
        }
    }
}