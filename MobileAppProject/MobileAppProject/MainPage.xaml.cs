using System;
using System.Collections.Generic;
using System.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;  // for settings & Storage

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MobileAppProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            // want to react to the loading event for this page
            // need to override the default/base/...
            this.Loading += MainPage_Loading; // runs when the page loads (once)
        }

        // override events for navigating to and leaving the page
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // happens every time the user arrives at this page
            loadPivotPage();
            base.OnNavigatedTo(e);
        }

        private void loadPivotPage()
        {
            ApplicationDataContainer localSetting = ApplicationData.Current.LocalSettings;
            try
            {
                pvtPages.SelectedIndex = Convert.ToInt32(localSetting.Values["currentPage"]);
            }
            catch
            {
                pvtPages.SelectedIndex = 2;
            }

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        private void savePivotPage()
        {
            ApplicationDataContainer localSetting = ApplicationData.Current.LocalSettings;
            localSetting.Values["currentPage"] = pvtPages.SelectedIndex;
        }

        private void MainPage_Loading(FrameworkElement sender, object args)
        {
            // add this code to the loading procedure for the page
            // not for the aplication
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

            // retrieve the value from the localSettings
            // store it in a string - I know this already
            // for the chessboard, write this directly to _rows
            // call the checked event for that radio button (with that tag)
            string strSetting;
            try
            {
                strSetting = localSettings.Values["userChoice"].ToString();
                tblSetting.Text = strSetting;
            }
            catch (Exception ex)
            {
                tblSetting.Text = "Error: " + ex.HResult + ", " + ex.Message;
            }

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // save the value of the check button
            RadioButton current = (RadioButton)sender;

            // save the tag value as the setting
            // access the data container called LocalSettings

            ApplicationDataContainer localSettings
                = ApplicationData.Current.LocalSettings;

            // within the container, save the name value pair of interest
            //localSettings.Values["nameOfSetting"] = "valueOfSetting";
            localSettings.Values["userChoice"] = current.Tag.ToString();
            // over writes any existing value of userChoice

            // just save the highest score a user got
            // need to check that value already stored.
            int newHighScore = 101;
            try
            {
                int temp = Convert.ToInt32(localSettings.Values["highScore"]);
                if (temp < newHighScore)
                {
                    localSettings.Values["highScore"] = newHighScore.ToString();
                }
            }
            catch
            {
                // doesn't exist, just set the value
                localSettings.Values["highScore"] = newHighScore.ToString();
            }




        }

        private void pvtPages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            savePivotPage();
        }
    }
}
