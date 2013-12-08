/**
 * Copyright (c) 2013 Nokia Corporation. All rights reserved.
 *
 * Nokia, Nokia Connecting People, Nokia Developer, and HERE are trademarks
 * and/or registered trademarks of Nokia Corporation. Other product and company
 * names mentioned herein may be trademarks or trade names of their respective
 * owners.
 *
 * See the license text file delivered with this project for more information.
 */

using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace RateMyApp.Helpers
{
    public enum FeedbackState
    {
        Inactive = 0,
        Active,
        FirstReview,
        SecondReview,
        Feedback
    }

    /// <summary>
    /// This helper class controls the behaviour of the FeedbackOverlay control.
    /// When the app has been launched FirstCount times the initial prompt is shown.
    /// If the user reviews no more prompts are shown. When the app has been
    /// launched SecondCount times and not been reviewed, the prompt is shown.
    /// </summary>
    public class FeedbackHelper : INotifyPropertyChanged
    {
        // Constants
        private const string LaunchCountKey = "RATE_MY_APP_LAUNCH_COUNT";
        private const string ReviewedKey = "RATE_MY_APP_REVIEWED";
        private const string LastLaunchDateKey = "RATE_MY_APP_LAST_LAUNCH_DATE";

        // Members
        private int firstCount;
        private int secondCount;
        private FeedbackState state;
        private int launchCount = 0;
        public event PropertyChangedEventHandler PropertyChanged;
        public static readonly FeedbackHelper Default = new FeedbackHelper();
        private bool reviewed = false;
        private DateTime lastLaunchDate = new DateTime();

        public DateTime LastLaunchDate
        {
            get { return lastLaunchDate; }
            set
            {
                lastLaunchDate = value;
                OnPropertyChanged("LastLaunchDate");
            }
        }

        public bool IsReviewed
        {
            get { return reviewed; }
            set
            {
                reviewed = value;
                OnPropertyChanged("IsReviewed");
            }
        }

        public FeedbackState State
        {
            get { return state; }
            set
            {
                state = value;
                OnPropertyChanged("State");
            }
        }

        public int LaunchCount
        {
            get { return launchCount; }
            set
            {
                launchCount = value;
                OnPropertyChanged("LaunchCount");
            }
        }

        public int FirstCount
        {
            get { return firstCount; }
            set
            {
                firstCount = value;
                OnPropertyChanged("FirstCount");
            }
        }

        public int SecondCount
        {
            get { return secondCount; }
            set
            {
                secondCount = value;
                OnPropertyChanged("SecondCount");
            }
        }

        public bool CountDays
        {
            get;
            set;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        private FeedbackHelper()
        {
            State = FeedbackState.Active;
        }

        /// <summary>
        /// Called when FeedbackLayout control is instantiated, which is
        /// supposed to happen when application's main page is instantiated.
        /// </summary>
        public void Launching()
        {
            var license = new Microsoft.Phone.Marketplace.LicenseInformation();

            // Only load state if app is not trial, app is not activated after
            // being tombstoned, and state has not been loaded before.
            if (!license.IsTrial() && 
                PhoneApplicationService.Current.StartupMode == StartupMode.Launch && 
                State == FeedbackState.Active)
            {
                LoadState();
            }

            // Uncomment for testing
            // State = FeedbackState.FirstReview;
            // State = FeedbackState.SecondReview;
        }

        /// <summary>
        /// Call when user has reviewed.
        /// </summary>
        public void Reviewed()
        {
            IsReviewed = true;
            StoreState();
        }

        /// <summary>
        /// Reset review and feedback launch counter and review state.
        /// </summary>
        public void Reset()
        {
            LaunchCount = 0;
            IsReviewed = false;
            LastLaunchDate = DateTime.Now;
            StoreState();
        }

        /// <summary>
        /// Loads last state from storage and works out the new state.
        /// </summary>
        private void LoadState()
        {
            try
            {
                LaunchCount = StorageHelper.GetSetting<int>(LaunchCountKey);
                IsReviewed = StorageHelper.GetSetting<bool>(ReviewedKey);
                LastLaunchDate = StorageHelper.GetSetting<DateTime>(LastLaunchDateKey);

                if (!reviewed)
                {
                    if (!CountDays || lastLaunchDate.Date < DateTime.Now.Date)
                    {
                        LaunchCount++;
                        LastLaunchDate = DateTime.Now;
                    }

                    if (LaunchCount == FirstCount)
                    {
                        State = FeedbackState.FirstReview;
                    }
                    else if (LaunchCount == SecondCount)
                    {
                        State = FeedbackState.SecondReview;
                    }

                    StoreState();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("FeedbackHelper.LoadState - Failed to load state, Exception: {0}", ex.ToString()));
            }
        }

        /// <summary>
        /// Stores current state.
        /// </summary>
        private void StoreState()
        {
            try
            {
                StorageHelper.StoreSetting(LaunchCountKey, LaunchCount, true);
                StorageHelper.StoreSetting(ReviewedKey, reviewed, true);
                StorageHelper.StoreSetting(LastLaunchDateKey, lastLaunchDate, true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("FeedbackHelper.StoreState - Failed to store state, Exception: {0}", ex.ToString()));
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public void Review()
        {
            Reviewed();

            var marketplace = new MarketplaceReviewTask();
            marketplace.Show();
        }
    }
}
