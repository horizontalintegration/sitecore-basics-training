using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SC_Playground.Models
{
  public class BreadcrumbModel
  {
    public List<BrItem> links;
  }
  public class BrItem
  {
    public string Name { get; set; }
    public string Url { get; set; }
  }
}
