using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Publishing;
using Sitecore.Tasks;
using System;

namespace SC_Playground.Schedulers
{
    public class CreateAndPublishItem
    {
        public void Execute(Item[] items, CommandItem commandItem, ScheduleItem scheduleItem)
        {
            try
            {
                if (items.Length > 0)
                {
                    Log.Info("PipelineProcessorsExample.Schedulers.CreateAndPublishItem Scheduler Started", this);
                    Database dbMaster = Factory.GetDatabase("master");
                    Database dbWeb = Factory.GetDatabase("web");
                    Item homeItemNode = items[1];
                    if (homeItemNode != null)
                    {
                        Item _newItem = homeItemNode.Add("Item" + new Random().Next(), new TemplateID(items[0].ID));
                        if (_newItem != null)
                        {
                            _newItem.Editing.BeginEdit();
                            _newItem.Fields["Title"].Value = "Testing Item 1";
                            _newItem.Editing.EndEdit();

                            Log.Info(string.Format("New Item Created ID {0} : New Item Name {1} ", _newItem.ID, _newItem.Name), this);
                            PublishOptions po = new PublishOptions(dbMaster, dbWeb, PublishMode.SingleItem, Sitecore.Context.Language, DateTime.Now)
                            {
                                RootItem = _newItem,
                                Deep = false
                            };

                            (new Publisher(po)).Publish();
                        }
                    }
                    Log.Info("PipelineProcessorsExample.Schedulers.CreateAndPublishItem Scheduler Ended", this);
                }

                #region Another way how to use the items fields
                Log.Info("PipelineProcessorsExample.Schedulers.CreateAndPublishItem Scheduler Started", this);
                if (string.IsNullOrEmpty(scheduleItem["item"]) && scheduleItem["items"].Split('|').Length == 0)
                    return;

                string templateId = scheduleItem["items"].Split('|')[0];
                string homeId = scheduleItem["items"].Split('|')[1];
                string itemName = scheduleItem["items"].Split('|')[2];

                Database masterDB = Factory.GetDatabase("master");
                Database webDB = Factory.GetDatabase("web");
                Item homeItem = masterDB.GetItem(homeId);
                if (homeItem != null)
                {
                    Item _newItem = homeItem.Add(itemName, new TemplateID(new ID(templateId)));
                    if (_newItem != null)
                    {
                        _newItem.Editing.BeginEdit();
                        _newItem.Fields["Title"].Value = "Testing Item 1";
                        _newItem.Editing.EndEdit();

                        Log.Info(string.Format("New Item Created ID {0} : New Item Name {1} ", _newItem.ID, _newItem.Name), this);
                        PublishOptions po = new PublishOptions(masterDB, webDB, PublishMode.SingleItem, Sitecore.Context.Language, DateTime.Now)
                        {
                            RootItem = _newItem,
                            Deep = false
                        };

                        (new Publisher(po)).Publish();
                    }
                }
                Log.Info("PipelineProcessorsExample.Schedulers.CreateAndPublishItem Scheduler Ended", this);
                #endregion
            }
            catch (Exception e)
            {
                Log.Error("Scheduler Exception:" + e.InnerException.Message, this);
            }
        }
    }
}