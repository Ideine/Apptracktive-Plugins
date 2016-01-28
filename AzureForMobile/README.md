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
From there, you can do what you used to with standard MobileServiceTable and manage online Azure data (please refer to Azure Mobile Services documentation or see samples), like this:

    var openItems = await _azureForMobileService.Data.RemoteTable<TodoItem>().Where(t => !t.Complete).ToListAsync();

#### Identity

Identity offers methods to manage the login process.
For example, it allows you to ask user for social login directly from a PCL.

Authenticate user with social identity provider like this (ex Facebook):

    await _azureForMobileService.Identity.LoginAsync(AzureForMobileAuthenticationProvider.Facebook);
    
You can also register a new user with custom authentication like this:

    await _azureForMobileService.Identity.RegisterAsync<YOUR_REGISTRATION_REQUEST_CLASS, YOUR_REGISTRATION_RESULT_CLASS>("NAME_OF_YOUR_REGISTRATION_CONTROLER", YOUR_REGISTRATION_REQUEST_CLASS instance);

And then log in user like this:

    await _azureForMobileService.Identity.LoginAsync("NAME_OF_YOUR_LOGIN_CONTROLER", "USER_LOGIN", "USER_PASSWORD");
    
But if you need to, you might want to log in specifying your own request and result class like we do with registration.
    
Please see the backend sample for more details about the custom controlers itself.

#### Api

Api is here to send custom requests to custom Azure controllers.

## Advanced

#### Handling token expiration

You can specify custom handlers.

One thing you can do with handler is automaticaly use cashed token to authenticate unauthorized request and then ask user to log in again if his token expired or if not yet logged in thanks to callback action.


    /// <summary>
    /// DelegatingHandler to automaticaly log in user again if its auth token expired
    /// </summary>
    public class AzureForMobileIdentityHandler : DelegatingHandler
    {
        private readonly IAzureForMobilePluginConfiguration _configuration;
        private readonly Action _onLoggedOut;
        private IAzureForMobileCredentials _credentials;
        public IAzureForMobileService AzureForMobileService;

        public AzureForMobileIdentityHandler(IAzureForMobilePluginConfiguration configuration, Action onLoggedOut = null)
        {
            _configuration = configuration;
            _onLoggedOut = onLoggedOut;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Resolve IMvxAmsService if not yet defined
                if (AzureForMobileService == null)
                {
                    throw new InvalidOperationException("Make sure to configure AzureForMobile plugin properly before using it.");
                }

                // Cloning the request
                var clonedRequest = await CloneRequest(request);

                // Load saved user if possible
                if (_configuration.CredentialsCacheService != null
                    && _configuration.CredentialsCacheService.TryLoadCredentials(out _credentials)
                    && (AzureForMobileService.Identity.CurrentUser == null
                    || (AzureForMobileService.Identity.CurrentUser.UserId != _credentials.User.UserId
                    && AzureForMobileService.Identity.CurrentUser.MobileServiceAuthenticationToken != _credentials.User.MobileServiceAuthenticationToken)))
                {
                    AzureForMobileService.Identity.CurrentUser = _credentials.User;

                    clonedRequest.Headers.Remove("X-ZUMO-AUTH");
                    // Set the authentication header
                    clonedRequest.Headers.Add("X-ZUMO-AUTH", _credentials.User.MobileServiceAuthenticationToken);

                    // Resend the request
                    response = await base.SendAsync(clonedRequest, cancellationToken);
                }

                if (response.StatusCode == HttpStatusCode.Unauthorized
                    && _credentials != null
                    && _credentials.Provider != AzureForMobileAuthenticationProvider.None
                    && _credentials.Provider != AzureForMobileAuthenticationProvider.Custom)
                {
                    try
                    {
                        // Login user again
                        var user = await AzureForMobileService.Identity.LoginAsync(_credentials.Provider);

                        // Save the user if possible
                        if (_credentials == null) _credentials = new AzureForMobileCredentials(_credentials.Provider, user);
                        _configuration.CredentialsCacheService?.SaveCredentials(_credentials);

                        clonedRequest.Headers.Remove("X-ZUMO-AUTH");
                        // Set the authentication header
                        clonedRequest.Headers.Add("X-ZUMO-AUTH", user.MobileServiceAuthenticationToken);

                        // Resend the request
                        response = await base.SendAsync(clonedRequest, cancellationToken);
                    }
                    catch (InvalidOperationException)
                    {
                        // user cancelled auth, so lets return the original response
                        return response;
                    }
                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _onLoggedOut?.Invoke();
                }
            }

            return response;
        }

        private async Task<HttpRequestMessage> CloneRequest(HttpRequestMessage request)
        {
            var result = new HttpRequestMessage(request.Method, request.RequestUri);
            foreach (var header in request.Headers)
            {
                result.Headers.Add(header.Key, header.Value);
            }

            if (request.Content != null && request.Content.Headers.ContentType != null)
            {
                var requestBody = await request.Content.ReadAsStringAsync();
                var mediaType = request.Content.Headers.ContentType.MediaType;
                result.Content = new StringContent(requestBody, Encoding.UTF8, mediaType);
                foreach (var header in request.Content.Headers)
                {
                    if (!header.Key.Equals("Content-Type", StringComparison.OrdinalIgnoreCase))
                    {
                        result.Content.Headers.Add(header.Key, header.Value);
                    }
                }
            }

            return result;
        }
    }


This code example:

1. Send a request to Azure
2. Check server response and if unauthorized
3. Check last used identity provider and automaticaly ask your user to log in with it again
4. If not yet logged in, execute the callback action if defined (like showing an identity provider picker login view for example)
5. When logged in again, send the original request
6. If still unauthorized, throw



Now you created your custom handler, you have to tell the plugin to use it thanks to each platform configuration.

Between var configuration = new AzureForMobilePluginConfiguration(...); and AzureForMobilePluginLoader.Init(...); add:

    var identityHandler = new AzureForMobileIdentityHandler(configuration, [Optional] YOUR_ACTION_IF_LOGGED_OUT);
    configuration.Handlers = new HttpMessageHandler[] { identityHandler };

Then, after AzureForMobilePluginLoader.Init(...); add:

    identityHandler.AzureForMobileService = AzureForMobilePluginLoader.Instance;
    
#### Caching credentials

You can tell the plugin how to store credentials for further use.

To do so, implement a storage feature of your choice like I did in samples with Xamarin.Settings and then implement the IAzureForMobileCredentialCacheService to use it:

    /// <summary>
    /// This IAzureForMobileCredentialsCacheService implementation is a working example 
    /// requiring the installation of Xamarin Settings plugin.
    /// </summary>
    public class AzureForMobileCredentialCacheService : IAzureForMobileCredentialsCacheService
    {
        public bool TryLoadCredentials(out IAzureForMobileCredentials credentials)
        {
            credentials = !string.IsNullOrEmpty(Settings.AzureForMobileIdentityUserId)
                          && !string.IsNullOrEmpty(Settings.AzureForMobileIdentityAuthToken)
                          && Settings.AzureForMobileIdentityProvider != AzureForMobileAuthenticationProvider.None
                ? new AzureForMobileCredentials(Settings.AzureForMobileIdentityProvider, new MobileServiceUser(Settings.AzureForMobileIdentityUserId)
                    {
                        MobileServiceAuthenticationToken = Settings.AzureForMobileIdentityAuthToken
                    })
                : null;

            return credentials != null;
        }

        public void SaveCredentials(IAzureForMobileCredentials credentials)
        {
            if (credentials == null)
                return;

            Settings.AzureForMobileIdentityProvider = credentials.Provider;
            Settings.AzureForMobileIdentityUserId = credentials.User.UserId;
            Settings.AzureForMobileIdentityAuthToken = credentials.User.MobileServiceAuthenticationToken;
        }

        public void ClearCredentials()
        {
            Settings.AzureForMobileIdentityProvider = AzureForMobileAuthenticationProvider.None;
            Settings.AzureForMobileIdentityUserId = string.Empty;
            Settings.AzureForMobileIdentityAuthToken = string.Empty;
        }
    }

Note that you'll have to create each property UserId, AuthToken and IdentityProvider on the settings feature side.

Also, you have to set this credential cache service implementation to each platform configuration:

Between var configuration = new AzureForMobilePluginConfiguration(...); and AzureForMobilePluginLoader.Init(...); add:

    configuration.CredentialsCacheService = new AzureForMobileCredentialCacheService();
    
The plugin will deal with each methods by itself before login and after login and logout.

More details on samples.


# AzureForMobile.LocalStore

You can manage local data and sync by adding the AzureForMobile Plugin's LocalStore Extension from Nuget and then follow the ToDo-AzureForMobile.LocalStore instructions.

Then you'll get access to LocalTable< T >() extension from Data where T could be one of your Model class and use it as you used to with the standard MobileServiceSyncTable (please refer to Microsoft official documentation) like:

    var openItems = await _azureForMobileService.Data.LocalTable<TodoItem>().Where(t => !t.Complete).ToListAsync();

Or

    await _azureForMobile.Data.PushAsync();
    
## Setup

Add the AzureForMobile Plugin's LocalStore Extension from Nuget and configure it.

Basic configuration:

After AzureForMobilePluginLoader.Init(...); add on each platform:

#### Android

    AzureForMobileLocalStorePluginLoader.Init(new AzureForMobileLocalStorePluginConfiguration(AzureForMobilePluginLoader.Instance, System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal)));

#### iOS

    SQLitePCL.CurrentPlatform.Init();
    AzureForMobileLocalStorePluginLoader.Init(new AzureForMobileLocalStorePluginConfiguration(AzureForMobilePluginLoader.Instance, System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal)));

#### WindowsPhone & Windows

    AzureForMobileLocalStorePluginLoader.Init(new AzureForMobileLocalStorePluginConfiguration(AzureForMobilePluginLoader.Instance, Windows.Storage.ApplicationData.Current.LocalFolder.Path));
    
## MVVM

Nothing to register as it's an extension of the main plugin instance

## Advanced

1. You can change the database path
2. You can change the database file name (default azureformobile.db)
3. You can change the initialization timeout (default 30s)


# AzureForMobile.Notification

Work with Push notifications.

Available soon...


# AzureForMobile.File

Upload/Download user files to/from Azure storage.

Available soon...


# AzureForMobile.Live

Send/Receive realtime messages with SignalR.

Available soon...
