using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace RateMyXamlAppWP8
{
    public partial class AboutPage : PhoneApplicationPage
    {
        public AboutPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string AppTitle
        {
            get { return "Application name: "+(from manifest in System.Xml.Linq.XElement.Load("WMAppManifest.xml").Descendants("App") select manifest).SingleOrDefault().Attribute("Title").Value; }
        }

        public string AppVersion
        {
            get { return "Version: " + (from manifest in System.Xml.Linq.XElement.Load("WMAppManifest.xml").Descendants("App") select manifest).SingleOrDefault().Attribute("Version").Value; }
        }

        public string AppAuthor
        {
            get { return "Developed by: " + (from manifest in System.Xml.Linq.XElement.Load("WMAppManifest.xml").Descendants("App") select manifest).SingleOrDefault().Attribute("Author").Value; }
        }

        public string AppPublisher
        {
            get { return "Published by: " + (from manifest in System.Xml.Linq.XElement.Load("WMAppManifest.xml").Descendants("App") select manifest).SingleOrDefault().Attribute("Publisher").Value; }
        }


        private void RateAppButton_Click(object sender, RoutedEventArgs e)
        {
            RateMyApp.Helpers.FeedbackHelper.Default.Review();
        }

    }
}