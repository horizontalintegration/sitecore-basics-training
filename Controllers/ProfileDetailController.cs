using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using System;
using System.Web.Mvc;


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