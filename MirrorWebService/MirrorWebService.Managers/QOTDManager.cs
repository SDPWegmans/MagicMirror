using MagicMirror.DTO.QOTD;
using MagicMirror.Web;
using System;
using System.Threading.Tasks;
namespace MirrorWebService.Managers
{
    public class QOTDManager
    {

        public Quote GetData()
        {
            var result = Task.Run(() => GetQuoteAsync()).Result;
            return result;
        }

        private async Task<Quote> GetQuoteAsync()
        {
            ServiceManager QOTDServiceManager =
               new ServiceManager(new Uri("http://quotes.rest/qod.json?category=inspire"));

            string quote = string.Empty; string author = string.Empty;
            var QOTDResponse = await QOTDServiceManager.CallService<QOTDResponse>();
            Quote currentQuote = new Quote();

            if (QOTDResponse == null)
            {
                //TODO: Configuration
                currentQuote.quote = "Love your husband!";
                currentQuote.author = "Lobberman";
            }
            else
            {
                currentQuote.quote = QOTDResponse.contents.quotes[0].quote;
                currentQuote.author = QOTDResponse.contents.quotes[0].author;
            }
            return currentQuote;
        }
    }
}
