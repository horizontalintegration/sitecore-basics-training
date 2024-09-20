using Sitecore.Data.Events;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Events;
using System;

namespace SC_Playground.Events
{
    public class ItemEvents
    {
        public ItemEvents()
        {

        }

        public void OnItemSaved(object sender, EventArgs args)
        {
            var item = Event.ExtractParameter(args, 0) as Item;

            Log.Info(string.Format("Following item is saved ID {0} : Item Name {1} ", item.ID, item.Name), this);
        }

        public void OnItemCreated(object sender, EventArgs args)
        {
            ItemCreatedEventArgs item = (ItemCreatedEventArgs)Event.ExtractParameter(args, 0);

            Log.Info(string.Format("Following item is created ID {0} : Item Name {1} ", item.Item.ID, item.Item.Name), this);
        }
    }
}