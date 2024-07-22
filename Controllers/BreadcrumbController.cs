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

      var CurrentItem = Sitecore.Context.Item;
      var model = new BreadcrumbModel();

      var ancestors = CurrentItem.Axes.GetAncestors();

      List<BrItem> items = new List<BrItem>();

      foreach (var item in ancestors)
      {
        items.Add(new BrItem()
        {
          Name = item.Name,
          Url = LinkManager.GetItemUrl(item),
        });
      }

      items.Add(new BrItem()
      {
        Name = CurrentItem.Name,
        Url = LinkManager.GetItemUrl(CurrentItem),
      });


      model.links = items;

      return View(model);
    }


  }
}