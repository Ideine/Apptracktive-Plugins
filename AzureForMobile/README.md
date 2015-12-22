# AzureForMobile

AzureForMobile plugin let you work with most of Azure usefull services for mobile development such as Azure Mobile Services/Azure Mobile Apps, Push notification, File storage or SignalR.

The main purpose is to access any API functionality by a single line of code from anywhere in the project (PCL or not), like:

    var openItems = await _azureForMobileService.Data.RemoteTable<TodoItem>().Where(t => !t.Complete).ToListAsync();

Or:

    await _azureForMobileService.Identity.LoginAsync(AzureForMobileAuthenticationProvider.Facebook);

## Setup

Just install the AzureForMobile package from nuget and then follow the ToDo-AzureForMobile instructions.

## MVVM

After plugin installed and configured, you'd better register an instance of it to then resolve it.

#### MVVMCross

    Mvx.RegisterSingleton(AzureForMobilePluginLoader.Instance);

#### FreshMVVM

    FreshIOC.Container.Register<IAzureForMobileService>(AzureForMobilePluginLoader.Instance);
