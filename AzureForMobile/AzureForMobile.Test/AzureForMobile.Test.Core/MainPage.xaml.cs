using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aptk.Plugins.AzureForMobile;
using Aptk.Plugins.AzureForMobile.Identity;
using Aptk.Plugins.AzureForMobile.LocalStore;
using AzureForMobile.Test.Core.Model;
using Xamarin.Forms;

namespace AzureForMobile.Test.Core
{
    public partial class MainPage : ContentPage
    {
        private readonly IAzureForMobileService _azureForMobileService;

        public MainPage()
        {
            InitializeComponent();

            _azureForMobileService = AzureForMobilePluginLoader.Instance;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await _azureForMobileService.Data.LocalTable<TodoItem>().PullAsync<TodoItem>(new CancellationToken());
            ToDoItems.ItemsSource = await GetTodoItemsAsync();
        }

        async Task AddItem(TodoItem item)
        {
            await _azureForMobileService.Data.LocalTable<TodoItem>().InsertAsync(item);
            ToDoItems.ItemsSource = await GetTodoItemsAsync();
        }

        async Task CompleteItem(TodoItem item)
        {
            item.Complete = true;
            await _azureForMobileService.Data.LocalTable<TodoItem>().UpdateAsync(item);
            ToDoItems.ItemsSource = await GetTodoItemsAsync();
        }

        public async void OnAdd(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NewItemName.Text))
            {
                var todo = new TodoItem { Text = NewItemName.Text };
                await AddItem(todo);
                NewItemName.Text = "";
                NewItemName.Unfocus();
            }
        }

        public async void OnComplete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var todo = mi.CommandParameter as TodoItem;
            await CompleteItem(todo);
        }

        public async void OnSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var todo = e.SelectedItem as TodoItem;
            if (Device.OS != TargetPlatform.iOS && todo != null)
            {
                // Not iOS - the swipe-to-complete is discoverable there
                if (Device.OS == TargetPlatform.Android)
                {
                    await DisplayAlert(todo.Text, "Press-and-hold to complete task " + todo.Text, "Got it!");
                }
                else {
                    // Windows, not all platforms support the Context Actions yet
                    if (await DisplayAlert("Complete?", "Do you wish to complete " + todo.Text + "?", "Complete", "Cancel"))
                    {
                        await CompleteItem(todo);
                    }
                }
            }
            // prevents background getting highlighted
            ToDoItems.SelectedItem = null;
        }

        public async void OnRefresh(object sender, EventArgs e)
        {
            var list = (ListView)sender;
            var success = false;
            try
            {
                ToDoItems.ItemsSource = await GetTodoItemsAsync();
                success = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
                // requires C# 6
                await DisplayAlert ("Refresh Error", "Couldn't refresh data ("+ex.Message+")", "OK");
            }
            list.EndRefresh();
            if (!success)
                await DisplayAlert("Refresh Error", "Couldn't refresh data", "OK");
        }

        public async void OnSync(object sender, EventArgs e)
        {
            await _azureForMobileService.Data.PushAsync();
        }

        async Task<List<TodoItem>> GetTodoItemsAsync()
        {
            return await _azureForMobileService.Data.LocalTable<TodoItem>().Where(i => !i.Complete).ToListAsync();
        }

        public void OnLog(object sender, EventArgs e)
        {
            if (!_azureForMobileService.Identity.EnsureLoggedIn())
            {
                _azureForMobileService.Identity.LoginAsync(AzureForMobileAuthenticationProvider.Facebook);
            }
        }
    }
}
