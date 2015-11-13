using System.Threading.Tasks;
using Aptk.Plugins.AzureMobileServices;
using Aptk.Plugins.AzureMobileServices.Abstractions;
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

            _aptkAmsService = Loader.Instance;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            ToDoItems.ItemsSource = await _aptkAmsService.Data.RemoteTable<TodoItem>().ToListAsync();
        }
    }
}
