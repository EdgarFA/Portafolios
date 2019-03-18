using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helper;
using Model;
using Portafolios.Areas.Admin.Filters;

namespace Portafolios.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private Usuario usuerio = new Usuario();

        [NoLogin]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout() 
        {
            // Eliminar la sesion actual
            SessionHelper.DestroyUserSession();
            return Redirect("~/");
        }

        public JsonResult Acceder(string Email, string Password)
        {
            var rm = usuerio.Acceder(Email, Password);

            if (rm.response)
            {
                rm.href = Url.Content("~/admin/usuario");
            }

            return Json(rm);
        }
    }
}