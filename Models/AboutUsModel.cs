using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SC_Playground.Models
{
    public class AboutUsModel
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public AboutUsModel(Item item)
        {
            if(item != null)
            {
                Title = item["Title"];
                Description = item["Description"];
            }
        }
    }
}
