using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MirrorCompanion
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            WriteNote();
        }

        /// <summary>
        /// 
        /// </summary>
        private async void WriteNote()
        {
            //TODO: Add Serialization for note construction
            WebRequest request = WebRequest.Create("http://mirrorservice.azurewebsites.net/api/notes");
            request.ContentType = "application /json; charset=utf-8"; //"application/x-www-form-urlencoded"; 
            request.Method = "POST";
            string requestText = "{\"Id\": 3,\"NoteText\": \"From Companion\"}";
            byte[] byteData = System.Text.UTF8Encoding.UTF8.GetBytes(requestText);

            using (var reqStream = await request.GetRequestStreamAsync())
            {
                reqStream.Write(byteData, 0, byteData.Length);
                await reqStream.FlushAsync();
            }

            var response = (HttpWebResponse)await request.GetResponseAsync();
            var i = 0;
        }
    }
}

class Note
{
    public int Id { get; set; }
    public string NoteText { get; set; }
}