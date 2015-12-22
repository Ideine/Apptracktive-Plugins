# AzureForMobile

AzureForMobile plugin let you work with most of Azure usefull services for mobile development such as Azure Mobile Services/Azure Mobile Apps, Push notification, File storage or SignalR.

The main purpose is to access any API functionality by a single line of code from anywhere in the project (PCL or not), like:

    var openItems = await _azureForMobileService.Data.RemoteTable<TodoItem>().Where(t => !t.Complete).ToListAsync();

Or:

    await _azureForMobileService.Identity.LoginAsync(AzureForMobileAuthenticationProvider.Facebook);

## Setup

Just install the AzureForMobile package from nuget and then follow the ToDo-AzureForMobile instructions.

Basic configuration:

1. Update/Create your Model classes so that they all inherit from EntityData abstract class 
or ITableData interface if there's another parent class yet.

2. Configure and initialize the plugin on each platform:

#### Android

Add these lines into the OnCreate of the first launching activity (ex MainActivity or SplashScreen) and complete it:

    var configuration = new AzureForMobilePluginConfiguration("YOUR URL", "YOUR KEY", typeof("ONE OF YOUR MODEL CLASS").GetTypeInfo().Assembly);
    AzureForMobilePluginLoader.Init(configuration, ApplicationContext);

#### iOS

Add these lines into the AppDelegate FinishedLaunching method and complete it:

    var configuration = new AzureForMobilePluginConfiguration("YOUR URL", "YOUR KEY", typeof("ONE OF YOUR MODEL CLASS").GetTypeInfo().Assembly);
    AzureForMobilePluginLoader.Init(configuration, app);
    
#### WindowsPhone and Windows (any version)

    var configuration = new AzureForMobilePluginConfiguration("YOUR URL", "YOUR KEY", typeof("ONE OF YOUR MODEL CLASS").GetTypeInfo().Assembly);
    AzureForMobilePluginLoader.Init(configuration);

## MVVM

After plugin installed and configured, you'd better register an instance of it to then resolve it when needed or use dependency injection.
Here are some examples:

#### MVVMCross

    Mvx.RegisterSingleton(AzureForMobilePluginLoader.Instance);

#### FreshMVVM

    FreshIOC.Container.Register<IAzureForMobileService>(AzureForMobilePluginLoader.Instance);

#### MvvmLight

    SimpleIoc.Default.Register<IAzureForMobileService>(AzureForMobilePluginLoader.Instance);
    
## Usage

An instance of the plugin give you access by default to:

#### Data

Data give you access by default to RemoteTable< T >() where T could be one of your Model class.
From there, you can do what you used to with standard MobileServiceTable and manage online Azure data (please refer to Azure Mobile Services documentation or see samples).

#### Identity

Identity offers methods to manage the login process.
For example, it allows you to ask user for social login directly from a PCL.
More details to come...

#### Api

Api is here to send custom requests to custom Azure controllers.

## Advanced

#### Handling token expiration

More details to come...

#### Caching credentials

More details to come...
