﻿using Microsoft.AspNetCore.Mvc;

namespace SmartVisitServer.Web.ViewComponents.AdminLayout
{
    public class _Head : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
