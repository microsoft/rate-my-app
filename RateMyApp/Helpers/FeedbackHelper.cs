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
using System;
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
    public class FeedbackHelper
    {
        // Constants
        private const string LaunchCountKey = "RATE_MY_APP_LAUNCH_COUNT";
        private const string ReviewedKey = "RATE_MY_APP_REVIEWED";
        private const string LastLaunchDateKey = "RATE_MY_APP_LAST_LAUNCH_DATE";

        // Members
        public static readonly FeedbackHelper Default = new FeedbackHelper();
        private int _launchCount = 0;
        private bool _reviewed = false;
        private DateTime _lastLaunchDate = new DateTime();

        public FeedbackState State
        {
            get;
            set;
        }

        public int FirstCount
        {
            get;
            set;
        }

        public int SecondCount
        {
            get;
            set;
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
            _reviewed = true;
            StoreState();
        }

        /// <summary>
        /// Reset review and feedback launch counter and review state.
        /// </summary>
        public void Reset()
        {
            _launchCount = 0;
            _reviewed = false;
            _lastLaunchDate = DateTime.Now;
            StoreState();
        }

        /// <summary>
        /// Loads last state from storage and works out the new state.
        /// </summary>
        private void LoadState()
        {
            try
            {
                _launchCount = StorageHelper.GetSetting<int>(LaunchCountKey);
                _reviewed = StorageHelper.GetSetting<bool>(ReviewedKey);
                _lastLaunchDate = StorageHelper.GetSetting<DateTime>(LastLaunchDateKey);

                if (!_reviewed)
                {
                    if (!CountDays || _lastLaunchDate.Date < DateTime.Now.Date)
                    {
                        _launchCount++;
                        _lastLaunchDate = DateTime.Now;
                    }

                    if (_launchCount == FirstCount)
                    {
                        State = FeedbackState.FirstReview;
                    }
                    else if (_launchCount == SecondCount)
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
                StorageHelper.StoreSetting(LaunchCountKey, _launchCount, true);
                StorageHelper.StoreSetting(ReviewedKey, _reviewed, true);
                StorageHelper.StoreSetting(LastLaunchDateKey, _lastLaunchDate, true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("FeedbackHelper.StoreState - Failed to store state, Exception: {0}", ex.ToString()));
            }
        }
    }
}
