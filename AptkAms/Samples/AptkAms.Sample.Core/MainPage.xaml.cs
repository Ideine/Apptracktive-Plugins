using Aptk.Plugins.AzureMobileServices;
using AptkAms.Sample.Core.Model;
using Xamarin.Forms;

namespace AptkAms.Sample.Core
{
    public partial class MainPage : ContentPage
    {
        private readonly IAptkAmsService _aptkAmsService ;

        public MainPage()
        {
            InitializeComponent();

            _aptkAmsService = AptkPluginLoader.Instance;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            ToDoItems.ItemsSource = await _aptkAmsService.Data.RemoteTable<TodoItem>().ToListAsync();
        }
    }
}
