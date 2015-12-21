namespace AzureForMobile.Sample.WinPhone
{
    public partial class MainPage
    {
        // Constructeur
        public MainPage()
        {
            InitializeComponent();

            this.LoadApplication(new AzureForMobile.Sample.Core.App());
        }
    }
}