using Sitecore.Abstractions;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Publishing;
using Sitecore.Publishing.Diagnostics;
using Sitecore.Publishing.Pipelines.Publish;
using Sitecore.Publishing.Pipelines.PublishItem;

namespace SC_Playground.Pipelines.Publishing
{
    public class CustomUpdateStatisticsPublishProcessor : PublishItemProcessor
    {
        public CustomUpdateStatisticsPublishProcessor()
        {
        }

        /// <summary>
        /// Processes the specified args.
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Process(PublishItemContext context)
        {
            Assert.ArgumentNotNull(context, "context");
            this.UpdateContextStatistics(context);
            this.UpdateJobStatistics(context);
            this.TraceInformation(context);

            // Custom logic that you want to add to the existing processor.
            Log.Info("Custom logic to be performed when this processor is called. ItemID : " + context.ItemId, this);
        }

        /// <summary>
        /// Traces the information.
        /// </summary>
        /// <param name="context">The context.</param>
        private void TraceInformation(PublishItemContext context)
        {
            string str;
            PublishItemResult result = context.Result;
            Item itemToPublish = context.PublishHelper.GetItemToPublish(context.ItemId);
            string str1 = (itemToPublish != null ? itemToPublish.Name : context.ItemName ?? "(null)");
            if (itemToPublish != null)
            {
                str = itemToPublish.Uri.ToString();
            }
            else
            {
                str = (context.ItemUri != null ? context.ItemUri.ToString() : "(null)");
            }
            string str2 = str;
            string str3 = (result != null ? result.Operation.ToString() : "(null)");
            string str4 = (result != null ? result.ChildAction.ToString() : "(null)");
            string str5 = (result == null || result.Explanation.Length <= 0 ? "(none)" : result.Explanation);
            PublishingLog.Info(string.Format("##Publish Item: Name={0}, Uri={1}, Operation={2}, ChildAction={3}, Explanation={4}", new object[] { str1, str2, str3, str4, str5 }), null);
        }

        /// <summary>
        /// Updates the publish context.
        /// </summary>
        /// <param name="context">The context.</param>
        private void UpdateContextStatistics(PublishItemContext context)
        {
            PublishItemResult result = context.Result;
            PublishContext publishContext = context.PublishContext;
            if (result == null || publishContext == null)
            {
                return;
            }
            PublishStatistics statistics = publishContext.Statistics;
            switch (result.Operation)
            {
                case PublishOperation.None:
                case PublishOperation.Skipped:
                    {
                        statistics.IncrementSkipped();
                        return;
                    }
                case PublishOperation.Created:
                    {
                        statistics.IncrementCreated();
                        return;
                    }
                case PublishOperation.Updated:
                    {
                        statistics.IncrementUpdated();
                        return;
                    }
                case PublishOperation.Deleted:
                    {
                        statistics.IncrementDeleted();
                        return;
                    }
                default:
                    {
                        return;
                    }
            }
        }

        /// <summary>
        /// Updates the job statistics.
        /// </summary>
        /// <param name="context">The context.</param>
        private void UpdateJobStatistics(PublishItemContext context)
        {
            Assert.ArgumentNotNull(context, "context");
            if (context.Result == null)
            {
                return;
            }
            BaseJob job = context.Job;
            if (job == null)
            {
                return;
            }
            job.Status.IncrementProcessed();
        }
    }
}