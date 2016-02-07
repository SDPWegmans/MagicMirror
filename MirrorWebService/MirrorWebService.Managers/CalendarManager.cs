using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Cal = MagicMirror.DTO.Calendar;
namespace MirrorWebService.Managers
{
    public class CalendarManager
    {
        //private string activeCalendarId = "primary";
        private string calendarOwner = "scottroot2@gmail.com";
        public List<Cal.Event> Events { get; set; }

        /// <summary>
        /// Loads calendar events
        /// </summary>
        /// <returns>Awaitable task of Events in list form</returns>
        public List<Cal.Event> GetData()
        {
            string serviceAccount = "magicmirrorserviceaccount@modular-house-120423.iam.gserviceaccount.com";
            string fileName = System.Web.HttpContext.Current.Server.MapPath("..\\") + "MagicMirror.p12";

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

            var calendarListResource = calendarService.CalendarList.List().Execute(); //await calendarService.CalendarList.List().ExecuteAsync(); //calendarService.CalendarList.List().Execute().Items; 

            var myCalendar = calendarListResource.Items[0];

            string pageToken = null;
            Events = new List<Cal.Event>();

            //do
            //{
            //TODO: Make configuration items
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
                    StartTimeStr = (item.Start.DateTime.HasValue ? item.Start.DateTime.Value : DateTime.Parse(item.Start.Date)).ToString(),
                    EndTimeStr = (item.End.DateTime.HasValue ? item.End.DateTime.Value : DateTime.Parse(item.End.Date)).ToString()
                });
            }

            return Events;
        }
    }
}