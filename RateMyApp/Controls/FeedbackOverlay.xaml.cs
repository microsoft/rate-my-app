/**
 * Copyright (c) 2013-2014 Microsoft Mobile. All rights reserved.
 *
 * Nokia, Nokia Connecting People, Nokia Developer, and HERE are trademarks
 * and/or registered trademarks of Nokia Corporation. Other product and company
 * names mentioned herein may be trademarks or trade names of their respective
 * owners.
 *
 * See the license text file delivered with this project for more information.
 */

using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows;
using RateMyApp.Helpers;

#if SILVERLIGHT
using Microsoft.Phone.Controls;
using Microsoft.Phone.Info;
using Microsoft.Phone.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Visibility = System.Windows.Visibility;
using DoubleAnimation = System.Windows.Media.Animation.DoubleAnimation;
#else
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.ApplicationModel.Email;
using System.IO;
using Windows.Storage;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.ApplicationModel.Resources;
using DoubleAnimation = Windows.UI.Xaml.Media.Animation.DoubleAnimation;
#endif

using RateMyApp.Resources;


namespace RateMyApp.Controls
{
    /// <summary>
    /// The FeedbackOverlay is a user control which can be placed on the 
    /// first page in the app. The control must be the last element inside
    /// the layout grid and span all rows and columns so it is not obscured.
    /// </summary>
    public partial class FeedbackOverlay : UserControl
    {
        public static readonly DependencyProperty VisibilityForDesignProperty =
            DependencyProperty.Register("VisibilityForDesign", typeof(Visibility), typeof(FeedbackOverlay), new PropertyMetadata(Visibility.Collapsed, null));

        public static void SetVisibilityForDesign(FeedbackOverlay element, Visibility value)
        {
            element.SetValue(VisibilityForDesignProperty, value);
        }

        public static Visibility GetVisibilityForDesign(FeedbackOverlay element)
        {
            return (Visibility)element.GetValue(VisibilityForDesignProperty);
        }

	public new System.Windows.Media.Brush Background
        {
            get { return (System.Windows.Media.Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Background.  This enables animation, styling, binding, etc...
        public new static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(System.Windows.Media.Brush), typeof(FeedbackOverlay), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        // Use this from XAML to control whether animation is on or off
        #region EnableAnimation Dependency Property

        public static readonly DependencyProperty EnableAnimationProperty =
            DependencyProperty.Register("EnableAnimation", typeof(bool), typeof(FeedbackOverlay), new PropertyMetadata(true, null));

        public static void SetEnableAnimation(FeedbackOverlay element, bool value)
        {
            element.SetValue(EnableAnimationProperty, value);
        }

        public static bool GetEnableAnimation(FeedbackOverlay element)
        {
            return (bool)element.GetValue(EnableAnimationProperty);
        }

        #endregion

        // Use this from XAML to control animation duration
        #region AnimationDuration Dependency Property

        public static readonly DependencyProperty AnimationDurationProperty =
          DependencyProperty.Register("AnimationDuration", typeof(TimeSpan), typeof(FeedbackOverlay), new PropertyMetadata(new TimeSpan(0, 0, 0, 0, 500), null));

        public static void SetAnimationDuration(FeedbackOverlay element, TimeSpan value)
        {
            element.SetValue(AnimationDurationProperty, value);
        }

        public static TimeSpan GetAnimationDuration(FeedbackOverlay element)
        {
            return (TimeSpan)element.GetValue(AnimationDurationProperty);
        }

        #endregion
 

        // Use this for MVVM binding IsVisible
        #region IsVisible Dependency Property

        public static readonly DependencyProperty IsVisibleProperty =
            DependencyProperty.Register("IsVisible", typeof(bool), typeof(FeedbackOverlay), new PropertyMetadata(false, null));

        public static void SetIsVisible(FeedbackOverlay element, bool value)
        {
            element.SetValue(IsVisibleProperty, value);
        }

        public static bool GetIsVisible(FeedbackOverlay element)
        {
            return (bool)element.GetValue(IsVisibleProperty);
        }

        #endregion

        // Use this for MVVM binding IsNotVisible
        #region IsNotVisible Dependency Property

        public static readonly DependencyProperty IsNotVisibleProperty =
            DependencyProperty.Register(
                "IsNotVisible", typeof(bool), typeof(FeedbackOverlay),
                new PropertyMetadata(true, null));

        public static void SetIsNotVisible(FeedbackOverlay element, bool value)
        {
            element.SetValue(IsNotVisibleProperty, value);
        }

        public static bool GetIsNotVisible(FeedbackOverlay element)
        {
            return (bool)element.GetValue(IsNotVisibleProperty);
        }

        #endregion

        // Use this from XAML to control rating title
        #region RatingTitle Dependency Property

        public static readonly DependencyProperty RatingTitleProperty =
            DependencyProperty.Register(
                "RatingTitle", typeof(string), typeof(FeedbackOverlay),
                new PropertyMetadata(AppResources.RatingTitle, null));

        public static void SetRatingTitle(FeedbackOverlay element, string value)
        {
            element.SetValue(RatingTitleProperty, value);
        }

        public static string GetRatingTitle(FeedbackOverlay element)
        {
            return (string)element.GetValue(RatingTitleProperty);
        }

        #endregion

        // Use this from XAML to control rating message 1
        #region RatingMessage1 Dependency Property

        public static readonly DependencyProperty RatingMessage1Property =
            DependencyProperty.Register(
                "RatingMessage1", typeof(string), typeof(FeedbackOverlay),
                new PropertyMetadata(AppResources.RatingMessage1, null));

        public static void SetRatingMessage1(FeedbackOverlay element, string value)
        {
            element.SetValue(RatingMessage1Property, value);
        }

        public static string GetRatingMessage1(FeedbackOverlay element)
        {
            return (string)element.GetValue(RatingMessage1Property);
        }

        #endregion

        // Use this from XAML to control rating message 2
        #region RatingMessage2 Dependency Property

        public static readonly DependencyProperty RatingMessage2Property =
            DependencyProperty.Register(
                "RatingMessage2", typeof(string), typeof(FeedbackOverlay),
                new PropertyMetadata(AppResources.RatingMessage2, null));

        public static void SetRatingMessage2(FeedbackOverlay element, string value)
        {
            element.SetValue(RatingMessage2Property, value);
        }

        public static string GetRatingMessage2(FeedbackOverlay element)
        {
            return (string)element.GetValue(RatingMessage2Property);
        }

        #endregion

        // Use this from XAML to control rating button yes 
        #region RatingYes Dependency Property

        public static readonly DependencyProperty RatingYesProperty =
            DependencyProperty.Register(
                "RatingYes", typeof(string), typeof(FeedbackOverlay),
                new PropertyMetadata(AppResources.RatingYes, null));

        public static void SetRatingYes(FeedbackOverlay element, string value)
        {
            element.SetValue(RatingYesProperty, value);
        }

        public static string GetRatingYes(FeedbackOverlay element)
        {
            return (string)element.GetValue(RatingYesProperty);
        }

        #endregion

        // Use this from XAML to control rating button no 
        #region RatingNo Dependency Property

        public static readonly DependencyProperty RatingNoProperty =
            DependencyProperty.Register(
                "RatingNo", typeof(string), typeof(FeedbackOverlay),
                new PropertyMetadata(AppResources.RatingNo, null));

        public static void SetRatingNo(FeedbackOverlay element, string value)
        {
            element.SetValue(RatingNoProperty, value);
        }

        public static string GetRatingNo(FeedbackOverlay element)
        {
            return (string)element.GetValue(RatingNoProperty);
        }

        #endregion

        // Use this from XAML to control feedback title
        #region FeedbackTitle Dependency Property

        public static readonly DependencyProperty FeedbackTitleProperty =
            DependencyProperty.Register(
                "FeedbackTitle", typeof(string), typeof(FeedbackOverlay),
                new PropertyMetadata(AppResources.FeedbackTitle, null));

        public static void SetFeedbackTitle(FeedbackOverlay element, string value)
        {
            element.SetValue(FeedbackTitleProperty, value);
        }

        public static string GetFeedbackTitle(FeedbackOverlay element)
        {
            return (string)element.GetValue(FeedbackTitleProperty);
        }

        #endregion

        // Use this from XAML to control feedback message1
        #region FeedbackMessage1 Dependency Property

        public static readonly DependencyProperty FeedbackMessage1Property =
            DependencyProperty.Register(
                "FeedbackMessage1", typeof(string), typeof(FeedbackOverlay),
                new PropertyMetadata(AppResources.FeedbackMessage1, null));

        public static void SetFeedbackMessage1(FeedbackOverlay element, string value)
        {
            element.SetValue(FeedbackMessage1Property, value);
        }

        public static string GetFeedbackMessage1(FeedbackOverlay element)
        {
            return (string)element.GetValue(FeedbackMessage1Property);
        }

        #endregion

        // Use this from XAML to control feedback button yes
        #region FeedbackYes Dependency Property

        public static readonly DependencyProperty FeedbackYesProperty =
            DependencyProperty.Register(
                "FeedbackYes", typeof(string), typeof(FeedbackOverlay), 
                new PropertyMetadata(AppResources.FeedbackYes, null));

        public static void SetFeedbackYes(FeedbackOverlay element, string value)
        {
            element.SetValue(FeedbackYesProperty, value);
        }

        public static string GetFeedbackYes(FeedbackOverlay element)
        {
            return (string)element.GetValue(FeedbackYesProperty);
        }

        #endregion

        // Use this from XAML to control feedback button no 
        #region FeedbackNo Dependency Property

        public static readonly DependencyProperty FeedbackNoProperty =
            DependencyProperty.Register(
                "FeedbackNo", typeof(string), typeof(FeedbackOverlay),
                new PropertyMetadata(AppResources.FeedbackNo, null));

        public static void SetFeedbackNo(FeedbackOverlay element, string value)
        {
            element.SetValue(FeedbackNoProperty, value);
        }

        public static string GetFeedbackNo(FeedbackOverlay element)
        {
            return (string)element.GetValue(FeedbackNoProperty);
        }

        #endregion

        // Use this from XAML to control feedback to
        #region FeedbackTo Dependency Property

        public static readonly DependencyProperty FeedbackToProperty =
            DependencyProperty.Register(
                "FeedbackTo", typeof(string), typeof(FeedbackOverlay),
                new PropertyMetadata(null, null));

        public static void SetFeedbackTo(FeedbackOverlay element, string value)
        {
            element.SetValue(FeedbackToProperty, value);
        }

        public static string GetFeedbackTo(FeedbackOverlay element)
        {
            return (string)element.GetValue(FeedbackToProperty);
        }

        #endregion

        // Use this from XAML to control feedback subject
        #region FeedbackSubject Dependency Property

        public static readonly DependencyProperty FeedbackSubjectProperty =
            DependencyProperty.Register(
                "FeedbackSubject", typeof(string), typeof(FeedbackOverlay),
                new PropertyMetadata(AppResources.FeedbackSubject, null));

        public static void SetFeedbackSubject(FeedbackOverlay element, string value)
        {
            element.SetValue(FeedbackSubjectProperty, value);
        }

        public static string GetFeedbackSubject(FeedbackOverlay element)
        {
            return (string)element.GetValue(FeedbackSubjectProperty);
        }

        #endregion

        // Use this from XAML to control feedback body 
        #region FeedbackBody Dependency Property

        public static readonly DependencyProperty FeedbackBodyProperty =
            DependencyProperty.Register(
                "FeedbackBody", typeof(string), typeof(FeedbackOverlay),
                new PropertyMetadata(AppResources.FeedbackBody, null));

        public static void SetFeedbackBody(FeedbackOverlay element, string value)
        {
            element.SetValue(FeedbackBodyProperty, value);
        }

        public static string GetFeedbackBody(FeedbackOverlay element)
        {
            return (string)element.GetValue(FeedbackBodyProperty);
        }

        #endregion

        // Use this from XAML to control company name 
        #region CompanyName Dependency Property

        public static readonly DependencyProperty CompanyNameProperty =
            DependencyProperty.Register(
                "CompanyName", typeof(string), typeof(FeedbackOverlay),
                new PropertyMetadata(null, null));

        public static void SetCompanyName(FeedbackOverlay element, string value)
        {
            element.SetValue(CompanyNameProperty, value);
        }

        public static string GetCompanyName(FeedbackOverlay element)
        {
            return (string)element.GetValue(CompanyNameProperty);
        }

        #endregion

        // Use this from XAML to control application name 
        #region ApplicationName Dependency Property

        public static readonly DependencyProperty ApplicationNameProperty =
            DependencyProperty.Register(
                "ApplicationName", typeof(string), typeof(FeedbackOverlay),
                new PropertyMetadata(null, null));

        public static void SetApplicationName(FeedbackOverlay element, string value)
        {
            element.SetValue(ApplicationNameProperty, value);
        }

        public static string GetApplicationName(FeedbackOverlay element)
        {
            return (string)element.GetValue(ApplicationNameProperty);
        }

        #endregion

        // Use this from XAML to control first count
        #region FirstCount Dependency Property

        public static readonly DependencyProperty FirstCountProperty =
            DependencyProperty.Register("FirstCount", typeof(int), typeof(FeedbackOverlay), new PropertyMetadata(5, null));

        public static void SetFirstCount(FeedbackOverlay element, int value)
        {
            element.SetValue(FirstCountProperty, value);
        }

        public static int GetFirstCount(FeedbackOverlay element)
        {
            return (int)element.GetValue(FirstCountProperty);
        }

        #endregion

        // Use this from XAML to control second count
        #region SecondCount Dependency Property

        public static readonly DependencyProperty SecondCountProperty =
            DependencyProperty.Register("SecondCount", typeof(int), typeof(FeedbackOverlay), new PropertyMetadata(10, null));

        public static void SetSecondCount(FeedbackOverlay element, int value)
        {
            element.SetValue(SecondCountProperty, value);
        }

        public static int GetSecondCount(FeedbackOverlay element)
        {
            return (int)element.GetValue(SecondCountProperty);
        }

        #endregion

        // Use this from XAML to control whether to count only one launch per day
        #region CountDays Dependency Property

        public static readonly DependencyProperty CountDaysProperty =
            DependencyProperty.Register("CountDays", typeof(bool), typeof(FeedbackOverlay), new PropertyMetadata(false, null));

        public static void SetCountDays(FeedbackOverlay element, bool value)
        {
            element.SetValue(CountDaysProperty, value);
        }

        public static bool GetCountDays(FeedbackOverlay element)
        {
            return (bool)element.GetValue(CountDaysProperty);
        }

        #endregion

        // Use this from XAML to control overriding culture
        #region LanguageOverride Dependency Property

        public static readonly DependencyProperty LanguageOverrideProperty =
            DependencyProperty.Register("LanguageOverride", typeof(string), typeof(FeedbackOverlay), new PropertyMetadata(null, null));

        public static void SetLanguageOverride(FeedbackOverlay element, string value)
        {
            element.SetValue(LanguageOverrideProperty, value);
        }

        public static string GetLanguageOverride(FeedbackOverlay element)
        {
            return (string)element.GetValue(LanguageOverrideProperty);
        }

        #endregion

        // Use this for detecting visibility change on code
        public event EventHandler VisibilityChanged = null;

#if SILVERLIGHT
        // PhoneApplicationFrame needed for detecting back presses
        private PhoneApplicationFrame _rootFrame = null;
#endif

        // Title of the review/feedback notification
        private string Title
        {
            set
            {
                if (title.Text != value)
                {
                    title.Text = value;
                }
            }
        }

        // Message of the review/feedback notification
        private string Message
        {
            set
            {
                if (message.Text != value)
                {
                    message.Text = value;
                }
            }
        }

        // Button text for not acting upon review/feedback notification
        private string NoText
        {
            set
            {
                if ((string)noButton.Content != value)
                {
                    noButton.Content = value;
                }
            }
        }

        // Button text for acting upon review/feedback notification
        private string YesText
        {
            set
            {
                if ((string)yesButton.Content != value)
                {
                    yesButton.Content = value;
                }
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public FeedbackOverlay()
        {
            InitializeComponent();

            yesButton.Click += yesButton_Click;
            noButton.Click += noButton_Click;
            Loaded += FeedbackOverlay_Loaded;
            hideContent.Completed += hideContent_Completed;
        }

        /// <summary>
        /// Reset review and feedback funtionality. Makes notifications active
        /// again, for example, after a major application update.
        /// </summary>
        public void Reset()
        {
            FeedbackHelper.Default.Reset();
        }

        /// <summary>
        /// Reset review and feedback funtionality. Makes notifications active
        /// again, for example, after a major application update.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FeedbackOverlay_Loaded(object sender, RoutedEventArgs e)
        {
            // FeedbackTo property is mandatory and must be defined in xaml.
            if (GetFeedbackTo(this) == null || GetFeedbackTo(this).Length <= 0)
            {
                throw new ArgumentNullException("FeedbackTo", "Mandatory property not defined in FeedbackOverlay.");
            }
			
            // ApplicationName property is mandatory and must be defined in xaml.
            if (GetApplicationName(this) == null || GetApplicationName(this).Length <= 0)
            {
                throw new ArgumentNullException("ApplicationName", "Mandatory property not defined in FeedbackOverlay.");
            }
			
            // CompanyName property is mandatory and must be defined in xaml.
            if (GetCompanyName(this) == null || GetCompanyName(this).Length <= 0)
            {
                throw new ArgumentNullException("CompanyName", "Mandatory property not defined in FeedbackOverlay.");
            }

            // Application language override.
            if (GetLanguageOverride(this) != null)
            {
                OverrideLanguage();
            }

            // Set up FeedbackHelper with properties.
            RateMyApp.Helpers.FeedbackHelper.Default.FirstCount = FeedbackOverlay.GetFirstCount(this);
            RateMyApp.Helpers.FeedbackHelper.Default.SecondCount = FeedbackOverlay.GetSecondCount(this);
            RateMyApp.Helpers.FeedbackHelper.Default.CountDays = FeedbackOverlay.GetCountDays(this);

            // Inform FeedbackHelper of the creation of this control.
            RateMyApp.Helpers.FeedbackHelper.Default.Launching();

            // This class needs to be aware of Back key presses.
            AttachBackKeyPressed();

            if (FeedbackOverlay.GetEnableAnimation(this))
            {
                LayoutRoot.Opacity = 0;
                xProjection.RotationX = 90;
            }

            // Check if review/feedback notification should be shown.
            if (FeedbackHelper.Default.State == FeedbackState.FirstReview)
            {
                SetVisibility(true);
                SetupFirstMessage();

                if (FeedbackOverlay.GetEnableAnimation(this))
                {
                    showContent.Begin();
                }
            }
            else if (FeedbackHelper.Default.State == FeedbackState.SecondReview)
            {
                SetVisibility(true);
                SetupSecondMessage();

                if (FeedbackOverlay.GetEnableAnimation(this))
                {
                    showContent.Begin();
                }
            }
            else
            {
                SetVisibility(false);
                FeedbackHelper.Default.State = FeedbackState.Inactive;
            }

            foreach (var doubleAnimation in showContent.Children.OfType<DoubleAnimation>())
                doubleAnimation.Duration = FeedbackOverlay.GetAnimationDuration(this);
 
            foreach (var doubleAnimation in hideContent.Children.OfType<DoubleAnimation>())
                doubleAnimation.Duration = FeedbackOverlay.GetAnimationDuration(this);
        }

        /// <summary>
        /// Detect back key presses.
        /// </summary>
        private void AttachBackKeyPressed()
        {
#if SILVERLIGHT		
            if (_rootFrame == null)
            {
                _rootFrame = Application.Current.RootVisual as PhoneApplicationFrame;
                _rootFrame.BackKeyPress += FeedbackOverlay_BackKeyPress;
            }
#else
			Windows.Phone.UI.Input.HardwareButtons.BackPressed += FeedbackOverlay_BackKeyPress;
#endif
        }

        /// <summary>
        /// Handle back key presses.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#if SILVERLIGHT
        private void FeedbackOverlay_BackKeyPress(object sender, CancelEventArgs e)
#else
        private void FeedbackOverlay_BackKeyPress(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
#endif
        {
            // If back is pressed whilst notification is open, close 
            // the notification and cancel back to stop app from exiting.
            if (Visibility == Visibility.Visible)
            {
                OnNoClick();
#if SILVERLIGHT
                e.Cancel = true;
#else
				e.Handled = true;
#endif				
            }
        }

        /// <summary>
        /// Set up first review message shown after FirstCount launches.
        /// </summary>
        private void SetupFirstMessage()
        {
            Title = string.Format(FeedbackOverlay.GetRatingTitle(this), GetApplicationName());
            Message = FeedbackOverlay.GetRatingMessage1(this);
            YesText = FeedbackOverlay.GetRatingYes(this);
            NoText = FeedbackOverlay.GetRatingNo(this);
        }

        /// <summary>
        /// Set up second review message shown after SecondCount launches.
        /// </summary>
        private void SetupSecondMessage()
        {
            Title = string.Format(FeedbackOverlay.GetRatingTitle(this), GetApplicationName());
            Message = FeedbackOverlay.GetRatingMessage2(this);
            YesText = FeedbackOverlay.GetRatingYes(this);
            NoText = FeedbackOverlay.GetRatingNo(this);
        }

        /// <summary>
        /// Set up feedback message shown after first review message.
        /// </summary>
        private void SetupFeedbackMessage()
        {
            Title = FeedbackOverlay.GetFeedbackTitle(this);
            Message = string.Format(FeedbackOverlay.GetFeedbackMessage1(this), GetApplicationName());
            YesText = FeedbackOverlay.GetFeedbackYes(this);
            NoText = FeedbackOverlay.GetFeedbackNo(this);
        }

        /// <summary>
        /// Called when no button is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void noButton_Click(object sender, RoutedEventArgs e)
        {
            OnNoClick();
        }

        /// <summary>
        /// Handle no button presses.
        /// </summary>
        private void OnNoClick()
        {
            if (FeedbackOverlay.GetEnableAnimation(this))
            {
                hideContent.Begin();
            }
            else
            {
                ShowFeedback();
            }
        }

        /// <summary>
        /// Called when notification gets hidden.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
#if SILVERLIGHT		
        private void hideContent_Completed(object sender, EventArgs e)
#else
        private void hideContent_Completed(object sender, object e)
#endif
        {
            ShowFeedback();
        }

        /// <summary>
        /// Show feedback message.
        /// </summary>
        private void ShowFeedback()
        {
            // Feedback message is shown only after first review message.
            if (FeedbackHelper.Default.State == FeedbackState.FirstReview)
            {
                this.SetupFeedbackMessage();
                FeedbackHelper.Default.State = FeedbackState.Feedback;

                if (FeedbackOverlay.GetEnableAnimation(this))
                {
                    this.showContent.Begin();
                }
            }
            else
            {
                this.SetVisibility(false);
                FeedbackHelper.Default.State = FeedbackState.Inactive;
            }
        }

        /// <summary>
        /// Called when yes button is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void yesButton_Click(object sender, RoutedEventArgs e)
        {
            this.SetVisibility(false);

            if (FeedbackHelper.Default.State == FeedbackState.FirstReview)
            {
                this.Review();
            }
            else if (FeedbackHelper.Default.State == FeedbackState.SecondReview)
            {
                this.Review();
            }
            else if (FeedbackHelper.Default.State == FeedbackState.Feedback)
            {
                this.Feedback();
            }
            FeedbackHelper.Default.State = FeedbackState.Inactive;
        }

        /// <summary>
        /// Launch market place review.
        /// </summary>
        private void Review()
        {
            FeedbackHelper.Default.Review();

            //var marketplace = new MarketplaceReviewTask();
            //marketplace.Show();
        }

        /// <summary>
        /// Launch feedback email.
        /// </summary>
#if SILVERLIGHT		
        private void Feedback()
#else
        private async void Feedback()
#endif
        {
            string version = string.Empty;

#if SILVERLIGHT
            var appManifestResourceInfo = Application.GetResourceStream(new Uri("WMAppManifest.xml", UriKind.Relative));

            using (var appManifestStream = appManifestResourceInfo.Stream)
            {
                using (var reader = XmlReader.Create(appManifestStream, new XmlReaderSettings { IgnoreWhitespace = true, IgnoreComments = true }))
                {
                    var doc = XDocument.Load(reader);
                    var app = doc.Descendants("App").FirstOrDefault();
                    if (app != null)
                    {
                        var versionAttribute = app.Attribute("Version");
                        if (versionAttribute != null)
                        {
                            version = versionAttribute.Value;
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(version))
            {
                // Application version
                var asm = System.Reflection.Assembly.GetExecutingAssembly();
                var parts = asm.FullName.Split(',');
                version = parts[1].Split('=')[1];
            }
#else
            var uri = new System.Uri("ms-appx:///AppxManifest.xml");
            StorageFile file = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(uri);
            using (var rastream = await file.OpenReadAsync())
            using (var appManifestStream = rastream.AsStreamForRead())
            {
                using (var reader = XmlReader.Create(appManifestStream, new XmlReaderSettings { IgnoreWhitespace = true, IgnoreComments = true }))
                {
                    var doc = XDocument.Load(reader);
                    var app = doc.Descendants("Identity").FirstOrDefault();
                    if (app != null)
                    {
                        var versionAttribute = app.Attribute("Version");
                        if (versionAttribute != null)
                        {
                            version = versionAttribute.Value;
                        }
                    }
                }
            }
#endif

            string company = GetCompanyName(this);
            if (company == null || company.Length <= 0)
            {
                company = "<Company>";
            }

#if SILVERLIGHT
            // Body text including hardware, firmware and software info
            string body = string.Format(FeedbackOverlay.GetFeedbackBody(this),
                 DeviceStatus.DeviceName,
                 DeviceStatus.DeviceManufacturer,
                 DeviceStatus.DeviceFirmwareVersion,
                 DeviceStatus.DeviceHardwareVersion,
                 version,
                 company);

            // Email task
            var email = new EmailComposeTask();
            email.To = FeedbackOverlay.GetFeedbackTo(this);
            email.Subject = string.Format(FeedbackOverlay.GetFeedbackSubject(this), GetApplicationName());
            email.Body = body;

            email.Show();
#else
            var easClientDeviceInformation = new EasClientDeviceInformation();

            // Body text including hardware, firmware and software info
            string body = string.Format(FeedbackOverlay.GetFeedbackBody(this),
                 easClientDeviceInformation.SystemProductName,
                 easClientDeviceInformation.SystemManufacturer,
                 easClientDeviceInformation.SystemFirmwareVersion,
                 easClientDeviceInformation.SystemHardwareVersion,
                 version,
                 company);

            // Send an Email with attachment
            EmailMessage email = new EmailMessage();
            email.To.Add(new EmailRecipient(FeedbackOverlay.GetFeedbackTo(this)));
            email.Subject = string.Format(FeedbackOverlay.GetFeedbackSubject(this), GetApplicationName());
            email.Body = body;
            await EmailManager.ShowComposeNewEmailAsync(email);
#endif			
        }

        /// <summary>
        /// Set review/feedback notification visibility.
        /// </summary>
        /// <param name="visible">True to set visible, otherwise False.</param>
        private void SetVisibility(bool visible)
        {
            bool wasVisible = FeedbackOverlay.GetIsVisible(this) == true;

            if (visible)
            {
                PreparePanoramaPivot(false);
                FeedbackOverlay.SetIsVisible(this, true);
                FeedbackOverlay.SetIsNotVisible(this, false);
                Visibility = Visibility.Visible;
            }
            else
            {
                PreparePanoramaPivot(true);
                FeedbackOverlay.SetIsVisible(this, false);
                FeedbackOverlay.SetIsNotVisible(this, true);
                Visibility = Visibility.Collapsed;
            }

            if (wasVisible != visible)
            {
                OnVisibilityChanged();
            }
        }

        /// <summary>
        /// Called when visibility changes.
        /// </summary>
        private void OnVisibilityChanged()
        {
            if (VisibilityChanged != null)
            {
                VisibilityChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Prepare underlaying Pivot and Panorama controls if such exist.
        /// 
        /// Pivot and Panorama capture touch gestures and remain active even
        /// when overlaid with a UserControl. When FeedbackOverlay is shown,
        /// touch events for pivot and panorama controls are disabled, and
        /// they are enabled again after FeedbackOverlay is hidden.
        /// </summary>
        /// <param name="hitTestVisible">True to set visible, otherwise False.</param>
        private void PreparePanoramaPivot(bool hitTestVisible)
        {
            DependencyObject o = VisualTreeHelper.GetParent(this);

            int children = VisualTreeHelper.GetChildrenCount(o);
            for (int i = 0; i < children; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(o, i);
                Type t = child.GetType();
                if (t.FullName == "Microsoft.Phone.Controls.Panorama" ||
                    t.FullName == "Microsoft.Phone.Controls.Pivot")
                {
                    UIElement elem = (UIElement)child;
                    elem.IsHitTestVisible = hitTestVisible;
                }
            }
        }

        /// <summary>
        /// Override default assembly dependent localization for the control
        /// with another culture supported by the application and the library.
        /// </summary>
        private void OverrideLanguage()
        {
#if SILVERLIGHT		
            CultureInfo originalCulture = Thread.CurrentThread.CurrentUICulture;
            CultureInfo newCulture = new CultureInfo(GetLanguageOverride(this));

            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;
#else
            CultureInfo originalCulture = CultureInfo.DefaultThreadCurrentUICulture;
            CultureInfo newCulture = new CultureInfo(GetLanguageOverride(this));

            CultureInfo.DefaultThreadCurrentCulture = newCulture;
            CultureInfo.DefaultThreadCurrentUICulture = newCulture;
#endif

            SetFeedbackBody(this, AppResources.FeedbackBody);
            SetFeedbackMessage1(this, string.Format(AppResources.FeedbackMessage1, GetApplicationName()));
            SetFeedbackNo(this, AppResources.FeedbackNo);
            SetFeedbackSubject(this, string.Format(AppResources.FeedbackSubject, GetApplicationName()));
            SetFeedbackTitle(this, AppResources.FeedbackTitle);
            SetFeedbackYes(this, AppResources.FeedbackYes);
            SetRatingMessage1(this, AppResources.RatingMessage1);
            SetRatingMessage2(this, AppResources.RatingMessage2);
            SetRatingNo(this, AppResources.RatingNo);
            SetRatingTitle(this, string.Format(AppResources.RatingTitle, GetApplicationName()));
            SetRatingYes(this, AppResources.RatingYes);

#if SILVERLIGHT
            Thread.CurrentThread.CurrentCulture = originalCulture;
            Thread.CurrentThread.CurrentUICulture = originalCulture;
#else
            CultureInfo.DefaultThreadCurrentCulture = originalCulture;
            CultureInfo.DefaultThreadCurrentUICulture = originalCulture;
#endif			
        }

        /// <summary>
        /// Get application name.
        /// </summary>
        /// <returns>Name of the application.</returns>
        private string GetApplicationName()
        {
            string appName = GetApplicationName(this);

            // If application name has not been defined by the application,
            // extract it from the Application class.
            if (appName == null || appName.Length <= 0)
            {
                appName = Application.Current.ToString();
                if (appName.EndsWith(".App"))
                {
                    appName = appName.Remove(appName.LastIndexOf(".App"));
                }
            }

            return appName;
        }
    }
}
