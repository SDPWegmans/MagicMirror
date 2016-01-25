using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace MagicMirror.Web
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

        /// <summary>
        /// Invoke an async POST call to the configured service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializableObj"></param>
        /// <returns>Boolean successful = true</returns>
        public async Task<bool> CallPOSTService<T>(T serializableObj)
        {
            WebRequest request = WebRequest.Create(RestURI);
            request.ContentType = "application /json; charset=utf-8";
            request.Method = "POST";

            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            serializer.WriteObject(ms, serializableObj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            var data = sr.ReadToEnd();
            byte[] byteData = System.Text.UTF8Encoding.UTF8.GetBytes(data);

            using (var reqStream = await request.GetRequestStreamAsync())
            {
                reqStream.Write(byteData, 0, byteData.Length);
                await reqStream.FlushAsync();
            }

            var resp = (HttpWebResponse)await request.GetResponseAsync();
            return (resp.StatusCode == HttpStatusCode.Created) ? true : false;
        }

        /// <summary>
        /// Invoke an async DELETE call to the configured service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializableObj"></param>
        /// <returns></returns>
        public async Task<bool> CallDeleteService<T>(T serializableObj)
        {
            WebRequest request = WebRequest.Create(RestURI);
            request.ContentType = "application /json; charset=utf-8";
            request.Method = "DELETE";

            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            serializer.WriteObject(ms, serializableObj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            var data = sr.ReadToEnd();
            byte[] byteData = System.Text.UTF8Encoding.UTF8.GetBytes(data);

            using (var reqStream = await request.GetRequestStreamAsync())
            {
                reqStream.Write(byteData, 0, byteData.Length);
                await reqStream.FlushAsync();
            }

            var resp = (HttpWebResponse)await request.GetResponseAsync();
            return (resp.StatusCode == HttpStatusCode.OK) ? true : false;
        }
    }
}