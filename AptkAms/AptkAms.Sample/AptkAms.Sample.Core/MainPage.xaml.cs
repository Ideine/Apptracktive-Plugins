using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aptk.Plugins.AzureMobileServices;
using AptkAms.Sample.Core.Model;
using Xamarin.Forms;

namespace AptkAms.Sample.Core
{
    public partial class MainPage : ContentPage
    {
        private readonly IAptkAmsService _aptkAmsService;

        public MainPage()
        {
            InitializeComponent();

            _aptkAmsService = AptkAmsPluginLoader.Instance;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //await _aptkAmsService.Data.LocalTable<TodoItem>().PullAsync<TodoItem>(new CancellationToken());
            ToDoItems.ItemsSource = await GetTodoItemsAsync();
        }

        async Task AddItem(TodoItem item)
        {
            await _aptkAmsService.Data.RemoteTable<TodoItem>().InsertAsync(item);
            ToDoItems.ItemsSource = await GetTodoItemsAsync();
        }

        async Task CompleteItem(TodoItem item)
        {
            item.Complete = true;
            await _aptkAmsService.Data.RemoteTable<TodoItem>().UpdateAsync(item);
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
                //await DisplayAlert ("Refresh Error", "Couldn't refresh data ("+ex.Message+")", "OK");
            }
            list.EndRefresh();
            if (!success)
                await DisplayAlert("Refresh Error", "Couldn't refresh data", "OK");
        }

        public async void OnSync(object sender, EventArgs e)
        {
            //await _aptkAmsService.Data.PushAsync();
            ToDoItems.ItemsSource = await GetTodoItemsAsync();
        }

        async Task<List<TodoItem>> GetTodoItemsAsync()
        {
            var test = _aptkAmsService.Data.RemoteTable<TodoItem>();
            var r =  await _aptkAmsService.Data.RemoteTable<TodoItem>().Where(i => !i.Complete).ToListAsync();
            return r;
        }
    }
}
