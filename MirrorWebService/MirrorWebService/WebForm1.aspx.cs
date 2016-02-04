using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography.X509Certificates;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Linq;
using Cal = MagicMirror.DTO.Calendar;


namespace MirrorWebService
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private string activeCalendarId = "primary";
        private string calendarOwner = "scottroot2@gmail.com";
        public List<Cal.Event> Events { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadCalendar();
        }

        private async void LoadCalendar()
        {
            //string userAccountEmail = "scottroot2@gmail.com";
            string serviceAccount = "magicmirrorserviceaccount@modular-house-120423.iam.gserviceaccount.com";
            string fileName = Server.MapPath(".") + "\\MagicMirror.p12";

            var certificate = new X509Certificate2(fileName, "notasecret", X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);

            ServiceAccountCredential credential = new ServiceAccountCredential(
               new ServiceAccountCredential.Initializer(serviceAccount)
               {
                   Scopes = new[] { CalendarService.Scope.CalendarReadonly },
                   User = serviceAccount
               }.FromCertificate(certificate));


            var calendarService = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "MagicMirror"
            });

            //Activity activity = service.Activities.Get(ACTIVITY_ID).Execute();
            var calendarListResource = await calendarService.CalendarList.List().ExecuteAsync(); //calendarService.CalendarList.List().Execute().Items; 

            var myCalendar = calendarListResource.Items[0]; //calendarListResource.First(@c => @c.Id == activeCalendarId);
            //var items = calendarListResource.Items;
            //output.Text = "Number of Items: "+ items.Count.ToString();

            string pageToken = null;
            Events = new List<Cal.Event>();

            //do
            //{
            var activeCalendarList = calendarService.Events.List(calendarOwner);
            activeCalendarList.PageToken = pageToken;
            activeCalendarList.MaxResults = 3;
            activeCalendarList.TimeMax = DateTime.Now.AddMonths(3);
            activeCalendarList.TimeMin = DateTime.Now;
            activeCalendarList.SingleEvents = true;
            activeCalendarList.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            var calEvents = activeCalendarList.Execute();

            var items = calEvents.Items;
            foreach (var item in items)
            {
                Events.Add(new Cal.Event()
                {
                    Summary = item.Summary,
                    StartTime = item.Start.DateTime.HasValue ? item.Start.DateTime.Value : DateTime.Parse(item.Start.Date),
                    EndTime = item.End.DateTime.HasValue ? item.End.DateTime.Value : DateTime.Parse(item.End.Date)
                });
            }
            //    pageToken = calEvents.NextPageToken;
            //} while (pageToken != null);

            //return Events;
            var i = Events;

        }
    }
}