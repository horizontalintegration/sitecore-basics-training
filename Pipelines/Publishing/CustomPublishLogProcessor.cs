using Sitecore.Diagnostics;
using Sitecore.Publishing.Pipelines.Publish;
using System.Linq;

namespace SC_Playground.Pipelines.Publishing
{
    public class CustomPublishLogProcessor : PublishProcessor
    {
        public override void Process(PublishContext context)
        {
            if (context == null || context.Aborted)
                return;

            Log.Info($"Publish pipeline completed", this);

            var addUpdateItems = context.ProcessedPublishingCandidates.Keys
                .Select(i => context.PublishOptions.TargetDatabase.GetItem(i.ItemId)).Where(j => j != null);

            foreach (var processedItem in addUpdateItems)
            {
                Log.Info($"Items that have been published : " + processedItem.ID, this);
            }
        }
    }
}
