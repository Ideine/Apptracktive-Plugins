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

You can specify custom handlers.

One thing you can do with handler is automaticaly ask user to log in again when his token expired or if not yet logged in.


    /// <summary>
    /// DelegatingHandler to automaticaly log in user again if its auth token expired
    /// </summary>
    public class AzureForMobileIdentityHandler : DelegatingHandler
    {
        private readonly IAzureForMobilePluginConfiguration _configuration;
        private AzureForMobileAuthenticationProvider _provider;
        private IAzureForMobileCredentials _credentials;
        public IAzureForMobileService AzureForMobileService;

        public AzureForMobileIdentityHandler(IAzureForMobilePluginConfiguration configuration, AzureForMobileAuthenticationProvider defaultProvider)
        {
            _configuration = configuration;
            _provider = defaultProvider;
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
                    _provider = _credentials.Provider;

                    clonedRequest.Headers.Remove("X-ZUMO-AUTH");
                    // Set the authentication header
                    clonedRequest.Headers.Add("X-ZUMO-AUTH", _credentials.User.MobileServiceAuthenticationToken);

                    // Resend the request
                    response = await base.SendAsync(clonedRequest, cancellationToken);
                }

                if (response.StatusCode == HttpStatusCode.Unauthorized
                    && _provider != AzureForMobileAuthenticationProvider.None
                    && _provider != AzureForMobileAuthenticationProvider.LoginPassword)
                {
                    if (_credentials != null)
                        _provider = _credentials.Provider;

                    try
                    {
                        // Log in user again
                        var user = await AzureForMobileService.Identity.LoginAsync(_provider);

                        // Save the user if possible
                        if (_credentials == null) _credentials = new AzureForMobileCredentials(_provider, user);
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
4. If not yet logged in, it use a default identity provider to log in (I should change that part by an Action like onError so that we could show an identity provider picker view)
5. When logged in again, send the original request



Now you created your custom handler, you have to tell the plugin to use it thanks to each platform configuration.

Between var configuration = new AzureForMobilePluginConfiguration(...); and AzureForMobilePluginLoader.Init(...); add:

    var identityHandler = new AzureForMobileIdentityHandler(configuration, AzureForMobileAuthenticationProvider.Facebook);
    configuration.Handlers = new HttpMessageHandler[] { identityHandler };

Then, after AzureForMobilePluginLoader.Init(...); add:

    identityHandler.AzureForMobileService = AzureForMobilePluginLoader.Instance;
    
#### Caching credentials

More details to come...
