﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pastelaria.Web.Controllers
{
    [Authorize]
    public class SobreController : Controller
    {
        // GET: Sobre
        public ActionResult Index()
        {
            return View();
        }
    }
}