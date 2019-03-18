using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portafolios.Areas.Admin.Filters;

namespace Portafolios.Areas.Admin.Controllers
{
    [Autenticado]
    public class TestimoniosController : Controller
    {
        // GET: Admin/Testimonios
        public ActionResult Index()
        {
            return View();
        }
    }
}