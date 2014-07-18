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

#if SILVERLIGHT
using System.IO.IsolatedStorage;
#endif

namespace RateMyApp.Helpers
{
    /// <summary>
    /// This helper class can be used to create, store, retrieve and
    /// remove settings in IsolatedStorageSettings.
    /// </summary>
    public class StorageHelper
    {
        /// <summary>
        /// Stores the given key value pair.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="overwrite">If true, will overwrite the existing value.</param>
        /// <returns>True if success, false otherwise.</returns>
        public static bool StoreSetting(string key, object value, bool overwrite)
        {
#if SILVERLIGHT
            if (overwrite || !IsolatedStorageSettings.ApplicationSettings.Contains(key))
            {
                IsolatedStorageSettings.ApplicationSettings[key] = value;
                return true;
            }
#else
            if (overwrite || !Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey(key))
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values[key] = value;
                return true;
            }
#endif
            return false;
        }

        /// <summary>
        /// Retrieves a setting matching the given key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns>The value for the key or a default value if key is not found.</returns>
        public static T GetSetting<T>(string key)
        {
#if SILVERLIGHT		
            if (IsolatedStorageSettings.ApplicationSettings.Contains(key))
            {
                return (T)IsolatedStorageSettings.ApplicationSettings[key];
            }
#else
            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey(key))
            {
                return (T)Windows.Storage.ApplicationData.Current.LocalSettings.Values[key];
            }
#endif
            return default(T);
        }

        /// <summary>
        /// Retrieves a setting matching the given key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns>The value for the key or the default value if key is not found.</returns>
        public static T GetSetting<T>(string key, T defaultVal)
        {
#if SILVERLIGHT	
            if (IsolatedStorageSettings.ApplicationSettings.Contains(key))
            {
                return (T)IsolatedStorageSettings.ApplicationSettings[key];
            }
#else
            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey(key))
            {
                return (T)Windows.Storage.ApplicationData.Current.LocalSettings.Values[key];
            }
#endif
            return defaultVal;
        }

        /// <summary>
        /// Removes a setting from IsolatedStorageSettings.
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveSetting(string key)
        {
#if SILVERLIGHT		
            IsolatedStorageSettings.ApplicationSettings.Remove(key);
#else
			Windows.Storage.ApplicationData.Current.LocalSettings.Values.Remove(key);
#endif
        }

        public static void FlushToFile()
        {
#if SILVERLIGHT
            IsolatedStorageSettings.ApplicationSettings.Save();
#else
#warning No Save() on WinRT :(
            //Windows.Storage.ApplicationData.Current.LocalSettings
#endif
        }

    }
}
