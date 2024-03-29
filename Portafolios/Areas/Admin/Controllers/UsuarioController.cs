﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helper;
using Model;
using Portafolios.Areas.Admin.Filters;

namespace Portafolios.Areas.Admin.Controllers
{
    [Autenticado]
    public class UsuarioController : Controller
    {
        private Usuario usuario = new Usuario();
        private TablaDato dato = new TablaDato();

        public ActionResult Index()
        {
            ViewBag.Paises = dato.Listar("pais");
            return View(usuario.Obtener(SessionHelper.GetUser()));
        }

        public JsonResult Guardar(Usuario model, HttpPostedFileBase Foto)
        {
            var rm = new ResponseModel();

            ModelState.Remove("password");
            if (ModelState.IsValid)
            {
                rm = model.Guardar(Foto);
            }

            return Json(rm);
        }
    }
}