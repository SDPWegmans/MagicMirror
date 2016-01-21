using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace MagicMirror.Services
{
    /// <summary>
    /// Class to manage all service calls from
    /// within this app
    /// </summary>
    public class ServiceManager
    {
        public Uri RestURI { get; set; }

        /// <summary>
        /// Each instance of ServiceManager requires the URI endpoint
        /// </summary>
        /// <param name="serviceURI"></param>
        public ServiceManager(Uri serviceURI)
        {
            RestURI = serviceURI;
        }

        /// <summary>
        /// Invoke an async call to the configured service.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Deserialized object specified by T</returns>
        public async Task<T> CallService<T>()
        {
            try
            {
                WebRequest request = WebRequest.Create(RestURI);
                request.ContentType = "application/json; charset=utf-8";
                var response = await request.GetResponseAsync();  
                string responseFromServer = string.Empty;

                using (Stream dataStream = response.GetResponseStream())
                {
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    responseFromServer = reader.ReadToEnd();
                }

                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(responseFromServer));
                T dataObject = (T)serializer.ReadObject(ms);

                return dataObject;
            }
            catch (Exception)
            {
                //TODO: Handle these exceptions better.
                return default(T);
            }
        }
    }
}