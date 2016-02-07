using MagicMirror.OS;
using MagicMirror.DTO.Note;
using MagicMirror.DTO.Settings;
using MagicMirror.Services.Responses;
using MagicMirror.Web;
using System;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;


namespace MagicMirror
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region Page Setup
        #region Properties
        public ServiceManager weatherServiceManager { get; set; }
        public CalendarSetting[] CalendarSettings { get; set; }
        const int NUMBER_OF_FORECASTS = 3;
        public MirrorDataResponse MirrorData { get; set; }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
            //TODO: Rotate the screen orientation
            DispatcherTimer mainTimer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 10) };
            mainTimer.Tick += MainTimer_Tick;
            mainTimer.Start();
            DisplayTime();
            ConfigureApp();
        }

        private void MainTimer_Tick(object sender, object e)
        {
            ConfigureApp();
        }

        private async void ConfigureApp()
        {
            //TODO: Will this bomb other code since it is async and things depend on it?
            //ServiceManager calConfigServiceManager = new 
            //    ServiceManager(new Uri("http://mirrorservice.azurewebsites.net/api/CalendarSettings"));

            ServiceManager mirrorServiceManager = new
                ServiceManager(new Uri("http://mirrorservice.azurewebsites.net/api/MirrorData"));

            MirrorData = (await mirrorServiceManager.CallService<MirrorDataResponse[]>())[0];
            DisplayWeatherContent();
            DisplayQOTDContent();
            DisplayStickyNote();
            DisplayGreeting();
            DisplayCalendarContent();
        }
        #endregion

        #region Display Setup
        #region Greeting
        /// <summary>
        /// 
        /// </summary>
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
            GreetingTextBlock.Text = string.Format("{0} Lobberella!", greeting);
        }
        #endregion

        #region Sticky Note
        /// <summary>
        /// Sticky notes when they exist!
        /// </summary>
        private void DisplayStickyNote()
        {
            string stickyImgName = "StickyNote.png";
            StickyNoteImage.Source = new BitmapImage(new Uri(WeatherImage.BaseUri, string.Format("Assets/{0}", stickyImgName)));
            StickyNoteImage.Visibility = Visibility.Visible;
            StickyNoteTextBlock.Text = MirrorData.Note.NoteText;
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
            dispatcherTimer.Interval = new TimeSpan(0, 1, 0); //TODO: put in config db for companion app
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

        #region Quote of the Day

        /// <summary>
        /// Displays Quote of the Day content on the screen
        /// </summary>
        private void DisplayQOTDContent()
        {
            string quote = string.Empty; string author = string.Empty;
            var QOTDResponse = MirrorData.QuoteOfTheDay;
            if (QOTDResponse == null)
            {
                quote = "Love your husband!";
                author = "Lobberman";
            }
            else
            {
                quote = QOTDResponse.quote;
                author = QOTDResponse.author;
            }

            QOTDtextBlock.Text = string.Format(" \"{0}\" - {1}", quote, author);
        }
        #endregion

        #region Weather

        /// <summary>
        /// Displays weather content on the screen
        /// </summary>
        private void DisplayWeatherContent()
        {
            DispatcherTimer weatherTimer = new DispatcherTimer();
            weatherTimer.Tick += WeatherTimer_Tick;
            weatherTimer.Interval = new TimeSpan(1, 0, 0);
            weatherTimer.Start();
            UpdateWeather();
        }

        private void UpdateWeather()
        {
            var weatherResponse = MirrorData.WeatherForecast;
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

        private void WeatherTimer_Tick(object sender, object e)
        {
            UpdateWeather();
        }
        #endregion

        #region Calendar
        private void DisplayCalendarContent()
        {
            int maxNumChars = 40;

            EventsListView.Items.Clear();
            foreach (var evt in MirrorData.CalendarEvents)
            {
                string txt = string.Format("{0}\r\n{1}\r\n", evt.StartTimeStr, evt.Summary);
                if (txt.Length > maxNumChars)
                {
                    txt = txt.Substring(0, maxNumChars);
                }

                TextBlock tb = new TextBlock();
                tb.Text = txt;

                ListViewItem lvi = new ListViewItem();
                lvi.BorderThickness = new Thickness(2, 0, 0, 0);
                lvi.FontFamily = new Windows.UI.Xaml.Media.FontFamily("Courier New");
                lvi.Content = txt;
                EventsListView.Items.Add(lvi);
            }
        }
        #endregion
        #endregion

        #region Utilities
        /// <summary>
        /// Formats the forecast presentation
        /// </summary>
        /// <param name="forecastDate"></param>
        /// <param name="lowTemp"></param>
        /// <param name="highTemp"></param>
        /// <returns></returns>
        private string FormatForecast(string forecastDate, double lowTemp, double highTemp)
        { //TODO: Move somewhere else out of this class?
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
        #endregion
    }
}