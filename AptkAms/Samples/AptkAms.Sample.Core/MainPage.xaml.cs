using System.Threading;
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

            _aptkAmsService = AptkAmsPluginLoader.Instance;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //await _aptkAmsService.Data.LocalTable<TodoItem>().PullAsync<TodoItem>(new CancellationToken());
            ToDoItems.ItemsSource = await _aptkAmsService.Data.RemoteTable<TodoItem>().ToListAsync();
        }
    }
}
