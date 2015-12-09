using Aptk.Plugins.AzureMobileServices.Data;

namespace AptkAms.Test.Core.Model
{
    public class TodoItem : EntityData
    {
        public string Text { get; set; }
        public bool Complete { get; set; }
    }
}
