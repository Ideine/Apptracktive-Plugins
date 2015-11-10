using Xamarin.Forms;

namespace AptkAms.Sample.WinPhone8
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            Forms.Init();
            this.LoadApplication(new Core.App());
        }
    }
}