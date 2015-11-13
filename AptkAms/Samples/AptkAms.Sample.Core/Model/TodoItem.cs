using Aptk.Plugins.AzureMobileServices.Abstractions.Data;

namespace AptkAms.Sample.Core.Model
{
    public class TodoItem : EntityData
    {
        public string Text { get; set; }
        public bool Complete { get; set; }
    }
}
