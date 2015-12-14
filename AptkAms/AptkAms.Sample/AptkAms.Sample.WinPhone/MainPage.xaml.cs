using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using AptkAms.Sample.WinPhone.Resources;

namespace AptkAms.Sample.WinPhone
{
    public partial class MainPage
    {
        // Constructeur
        public MainPage()
        {
            InitializeComponent();

            this.LoadApplication(new Core.App());
        }
    }
}