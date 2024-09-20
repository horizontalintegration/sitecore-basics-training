using Sitecore.Diagnostics;
using Sitecore.Publishing.Pipelines.PublishItem;

namespace SC_Playground.Pipelines.Publishing
{
    public class CustomPublishItemLogProcessor : PublishItemProcessor
    {
        public override void Process(PublishItemContext context)
        {
            if (context == null || context.Aborted)
                return;

            Log.Info($"Current Item that is being published: {context.ItemId.ToString()}", this);
        }
    }
}
