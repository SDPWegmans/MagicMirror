using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using System;
using System.Threading;

namespace MagicMirror.Services.Responses
{
    public class CalendarResponse
    {
        public async void GetResponse()
        {
            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                            new Uri("ms-appx:///client_secret.json"),
                            new[] { Uri.EscapeUriString(CalendarService.Scope.Calendar) },
                            "scottroot2@gmail.com",
                            CancellationToken.None);
            var calendarService = new CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "MagicMirror"
            });
            var calendarListResource = await calendarService.CalendarList.List().ExecuteAsync();
        }
    }
}