using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SC_Playground.Models;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using Sitecore.Resources.Media;


namespace SC_Playground.Controllers
{
    public class AboutUsController : Controller 
    {
        public ActionResult Index() 
        {
            var database = Sitecore.Context.Database;
            Item dataSource = database.GetItem("{89A801E4-3D24-403A-924D-442EB186D84D}");

            if (dataSource == null)
            {
                dataSource = Sitecore.Context.Item;
            }
            var item = RenderingContext.Current.Rendering.Item;
            var model = new AboutUsModel(dataSource);
            return View("~/Views/AboutUs/AboutUs.cshtml", model); 
        }


    }
}