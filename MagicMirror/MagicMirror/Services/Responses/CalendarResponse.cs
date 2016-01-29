using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using Cal = MagicMirror.Services.DTO.Calendar;

namespace MagicMirror.Services.Responses
{
    public class CalendarResponse
    {
        private string calendarOwner = "scottroot2@gmail.com";
        private string activeCalendarId = "primary"; //TODO:Get wifes calendar  "cathyroot9@gmail.com";
        public List<Cal.Event> Events { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public async void GetResponse()
        {
            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                            new Uri("ms-appx:///client_secret.json"),
                            new[] { Uri.EscapeUriString(CalendarService.Scope.Calendar) },
                            calendarOwner,
                            CancellationToken.None);

            var calendarService = new CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "MagicMirror"
            });

            var calendarListResource = await calendarService.CalendarList.List().ExecuteAsync();
           
            string pageToken = null;
            Events = new List<Cal.Event>();

            //do
            //{
                var activeCalendarList = calendarService.Events.List(activeCalendarId);
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
                        StartTime = item.Start.DateTime.HasValue ? item.Start.DateTime.Value : DateTime.MinValue,
                        EndTime =item.End.DateTime.HasValue ? item.End.DateTime.Value : DateTime.MinValue
                    });
                }
            //    pageToken = calEvents.NextPageToken;
            //} while (pageToken != null);
        }
    }
}