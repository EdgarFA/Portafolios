using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace Portafolios.Controllers
{
    public class DefaultController : Controller
    {
        private TablaDato tabladato = new TablaDato();
        // GET: Default
        public string Index()
        {
            return "Hola";
        }
    }
}