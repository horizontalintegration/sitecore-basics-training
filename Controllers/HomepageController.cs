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
    public class HomepageController : Controller 
    {
        public ActionResult Homepage() 
        { 
            var database = Sitecore.Context.Database;
            Item dataSource = database.GetItem(RenderingContext.Current.Rendering.DataSource);

            if (dataSource == null)
            {
                dataSource = Sitecore.Context.Item;
            }
            var model = GetEventPageModel(dataSource);
            return View(model); 
        }

        private static HomepageModel GetEventPageModel(Item dataSource)
        {

            var model = new HomepageModel();
            model.PromoHeading = dataSource.Fields["PromoHeading"].Value;
            model.PromoDesc = dataSource.Fields["PromoDesc"].Value;
            ImageField imageField = dataSource.Fields["PromoImage"];
            model.PromoImage = MediaManager.GetMediaUrl(imageField.MediaItem);

            return model;
        }


    }
}