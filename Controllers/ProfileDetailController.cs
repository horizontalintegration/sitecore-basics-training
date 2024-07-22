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
  public class ProfileDetailController : Controller
  {
    public ActionResult MyProfile()
    {
      try
      {
        var rc = Sitecore.Mvc.Presentation.RenderingContext.CurrentOrNull;
        if (rc != null)
        {
          var dataSourceId = rc?.Rendering?.DataSource;
          if (!string.IsNullOrEmpty(dataSourceId))
          {
            Log.Info("Profile component has datasource assigned", this);
            Item profile = Sitecore.Context.Database.GetItem(dataSourceId);
            return View(profile);
          }
        }
      }
      catch (Exception ex)
      {
        Log.Error(ex.StackTrace, this);
      }
      return View();
    }
  }
}