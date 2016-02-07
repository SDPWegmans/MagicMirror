using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cal = MagicMirror.DTO.Calendar;

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
        public async Task<List<Cal.Event>> GetResponse()
        {
            try
            {
                var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                                new Uri("ms-appx:///client_secret.json"),
                                new[] { Uri.EscapeUriString(CalendarService.Scope.Calendar) },
                                calendarOwner,
                                CancellationToken.None);

                //TokenResponse tokenResponse = new TokenResponse();
                //tokenResponse.RefreshToken = "1/bX3of9MELaLjPa3Q4CSUAWHiglNnxs3JEzjmtDHslxQ";
                //var initializer = 
                //    new AuthorizationCodeFlow.Initializer("https://accounts.google.com/o/oauth2/auth", "https://accounts.google.com/o/oauth2/token");
                //initializer.ClientSecrets = new ClientSecrets()
                //{
                //    ClientId = "1027237486323-hl6kff87ldijo642pj2vv3kg4hrg11bs.appsx.googleusercontent.com",
                //    ClientSecret = "fu79C_u3b9tP9HJw0IDXk2gL"
                //};

                //IAuthorizationCodeFlow flow = new AuthorizationCodeFlow(initializer);

                //var credential = new UserCredential(flow, calendarOwner, tokenResponse);

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
                        StartTimeStr = (item.Start.DateTime.HasValue ? item.Start.DateTime.Value : DateTime.Parse(item.Start.Date)).ToString(),
                        EndTimeStr = (item.End.DateTime.HasValue ? item.End.DateTime.Value : DateTime.Parse(item.End.Date)).ToString()
                    });
                }
                //    pageToken = calEvents.NextPageToken;
                //} while (pageToken != null);

                return Events;
            }
            catch (Exception e)
            {
                var foo = e;
                return null;
            }
        }
    }
}