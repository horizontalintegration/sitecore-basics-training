using SC_Playground.Models;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using Sitecore.Resources.Media;
using System.Web.Mvc;


namespace SC_Playground.Controllers
{
    //Promo Component
    //Using Controller Rendering (Dynamic Binding) = PromoDetailController & ~/Views/PromoDetail/GetPromo.cshtml
    //Using View Rendering (Static Binding) = ~/Views/Promo Component/Promo.cshtml

    public class PromoDetailController : Controller 
    {
        public ActionResult GetPromo() 
        { 
            var database = Sitecore.Context.Database;
            Item dataSource = database.GetItem(RenderingContext.Current.Rendering.DataSource);

            if (dataSource == null)
            {
                dataSource = Sitecore.Context.Item;
            }
            var model = GetPromoModel(dataSource);
            return View(model); 
        }

        private static PromoModel GetPromoModel(Item dataSource)
        {
            ImageField imageField = dataSource.Fields["PromoImage"];
            var model = new PromoModel
            {
                PromoHeading = dataSource.Fields["PromoHeading"].Value,
                PromoDesc = dataSource.Fields["PromoDesc"].Value,
                PromoImage = MediaManager.GetMediaUrl(imageField.MediaItem)
            };
            return model;
        }


    }
}