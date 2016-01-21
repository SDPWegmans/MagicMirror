using MagicMirror.Services;
using MagicMirror.Services.Responses;
using System;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using MagicMirror.OS;
using Windows.Storage;

namespace MagicMirror
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ServiceManager weatherServiceManager { get; set; }
        const int NUMBER_OF_FORECASTS = 3;

        /// <summary>
        /// 
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
            //EnterFullScreen();
            //Load content on to the UI
            DisplayTime();
            DisplayWeatherContent();
            DisplayQOTDContent();
            DisplayStickyNote();
            DisplayGreeting();
            //TurnMonitor();
        }

        private void DisplayGreeting()
        {
            var currentHour = DateTime.Now.Hour;
            string greeting = string.Empty;

            if (currentHour < 12)
            {
                greeting = "Good morning";
            }
            else if (currentHour >= 12 && currentHour < 17)
            {
                greeting = "Good afternoon";
            }
            else if (currentHour >= 17)
            {
                greeting = "Good evening";
            }
            //TODO: Profile switching variable for lobberella
            GreetingTextBlock.Text = string.Format("{0} Lobberella!",greeting);
        }

        /// <summary>
        /// Sticky notes when they exist!
        /// </summary>
        private void DisplayStickyNote()
        {
            //TODO:REsources file.  Put images and other config items somewhere global
            string stickyImgName = "StickyNote.png";
            StickyNoteImage.Source =  new BitmapImage(new Uri(WeatherImage.BaseUri, string.Format("Assets/{0}", stickyImgName)));
            StickyNoteImage.Visibility = Visibility.Visible;
        }

        #region Monitor 
        private void TurnMonitor()
        {
            MagicMirror.OS.Monitor.IssueCommand(MONITOR_STATE.LowPower);

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick_Monitor; ;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Start();

        }

        private void DispatcherTimer_Tick_Monitor(object sender, object e)
        {
            MagicMirror.OS.Monitor.IssueCommand(MONITOR_STATE.On);
        }
        #endregion

        #region Time Display
        /// <summary>
        /// 
        /// </summary>
        private void DisplayTime()
        {
            UpdateTime();
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 1, 0);
            dispatcherTimer.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateTime()
        {
            TimeTextBlock.Text = DateTime.Now.ToString("hh:mm tt");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DispatcherTimer_Tick(object sender, object e)
        {
            UpdateTime();
        }
        #endregion

        /// <summary>
        /// Displays Quote of the Day content on the screen
        /// </summary>
        private async void DisplayQOTDContent()
        {
            //TODO: Caching the call for a day because we are limited to how often we can call the api.

            ServiceManager QOTDServiceManager =
                new ServiceManager(new Uri("http://quotes.rest/qod.json?category=inspire"));

            string quote = string.Empty; string author = string.Empty;
            var QOTDResponse = await QOTDServiceManager.CallService<QOTDResponse>();
            if (QOTDResponse == null)
            {
                quote = "Love your husband!";
                author = "Lobberman";
            }
            else
            {
                quote = QOTDResponse.contents.quotes[0].quote;
                author = QOTDResponse.contents.quotes[0].author;
            }

            QOTDtextBlock.Text = string.Format(" \"{0}\" - {1}", quote, author);
        }

        /// <summary>
        /// Displays weather content on the screen
        /// </summary>
        private async void DisplayWeatherContent()
        {
            //TODO: Display next 3 days of weather with smaller icons
            weatherServiceManager =
                new ServiceManager(new Uri("https://api.forecast.io/forecast/bfca1ae07ffdf0931899e6d17c7b2875/43.1117230,-77.4087580"));

            var weatherResponse = await weatherServiceManager.CallService<WeatherServiceResponse>();
            var currentWeather = weatherResponse.currently;
            if (weatherResponse == null)
            {
                currentWeather.summary = "Love";
                currentWeather.temperature = 69;
                currentWeather.icon = "lobster";
            }

            WeatherImage.Source = new BitmapImage(new Uri(WeatherImage.BaseUri, string.Format("Assets/WeatherImages/{0}.png", currentWeather.icon))); //Weather images courtesy http://d3stroy.deviantart.com/art/Weezle-Weather-Icons-187306753
            CurrentWeatherTextBlock.Text = string.Format("{0}  {1}ºF", currentWeather.summary, Convert.ToInt16(currentWeather.temperature));

            #region Future Weather
            //TODO:This can be better....
            var dailyResponse = weatherResponse.daily;
            string[] forecasts = new string[NUMBER_OF_FORECASTS];
            string[] dailyIcon = new string[NUMBER_OF_FORECASTS];
            if (dailyResponse.data.Count >= NUMBER_OF_FORECASTS)
            {
                for (int i = 0; i < NUMBER_OF_FORECASTS; i++)
                {
                    var daily = dailyResponse.data[i];
                    var forecastDate = DateTimeOffset.FromUnixTimeSeconds(daily.time);
                    dailyIcon[i] = daily.icon;
                    forecasts[i] = FormatForecast(forecastDate.ToString("ddd"), daily.temperatureMin, daily.temperatureMax);
                }
                future1TextBlock.Text = forecasts[0];
                future1Image.Source = new BitmapImage(new Uri(WeatherImage.BaseUri, string.Format("Assets/WeatherImages/Small/{0}-small.png", dailyIcon[0])));
                future2TextBlock.Text = forecasts[1];
                future2Image.Source = new BitmapImage(new Uri(WeatherImage.BaseUri, string.Format("Assets/WeatherImages/Small/{0}-small.png", dailyIcon[1])));
                future3TextBlock.Text = forecasts[2];
                future3Image.Source = new BitmapImage(new Uri(WeatherImage.BaseUri, string.Format("Assets/WeatherImages/Small/{0}-small.png", dailyIcon[2])));
            }

            #endregion
        }

        private string FormatForecast(string forecastDate, double lowTemp, double highTemp)
        {
            return
                string.Format("{0}: L:{1}ºF H:{2}ºF ", forecastDate, Convert.ToInt16(lowTemp), Convert.ToInt16(highTemp));
        }

        /// <summary>
        /// Put the form into full screen mode.
        /// </summary>
        private async void EnterFullScreen()
        {
            var view = ApplicationView.GetForCurrentView();
            if (!view.TryEnterFullScreenMode())
            {
                var dialog = new MessageDialog("Unable to enter the full-screen mode.");
                await dialog.ShowAsync();
            }
        }
    }
}
