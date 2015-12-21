using Aptk.Plugins.AzureForMobile.Data;

namespace AzureForMobile.Test.Core.Model
{
    public class TodoItem : EntityData
    {
        public string Text { get; set; }
        public bool Complete { get; set; }
    }
}
