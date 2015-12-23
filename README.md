# Plugins
Xamarin &amp; Windows plugins

## AzureForMobile

AzureForMobile plugin let you work with most of Azure usefull services for mobile development such as Azure Mobile Services/Azure Mobile Apps, Push notification, File storage or SignalR.

The main purpose is to access any API functionality by a single line of code from anywhere in the project (PCL or not), like:

    var openItems = await _azureForMobileService.Data.RemoteTable<TodoItem>().Where(t => !t.Complete).ToListAsync();

Or:

    await _azureForMobileService.Identity.LoginAsync(AzureForMobileAuthenticationProvider.Facebook);

Open the [project folder](https://github.com/Apptracktive/Plugins/tree/master/AzureForMobile) for more details and read blog posts on [Apptracktive](http://www.apptracktive.com)
