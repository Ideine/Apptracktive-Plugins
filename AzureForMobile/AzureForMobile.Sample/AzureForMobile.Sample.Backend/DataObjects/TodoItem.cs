using Microsoft.WindowsAzure.Mobile.Service;

namespace AptkAms.Sample.Backend.DataObjects
{
    public class TodoItem : EntityData
    {
        public string Text { get; set; }

        public bool Complete { get; set; }
    }
}