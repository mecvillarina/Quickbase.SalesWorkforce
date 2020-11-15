using System.Net.Http;
using System.Threading.Tasks;

namespace SalesWorkforce.MobileApp.WebServices.Abstractions
{
    public interface IHttpMessageHelper
    {
        HttpContent EncodeObjectToJsonHttpContent<T>(T content);

        Task<T> DecodeJsonResponseToObject<T>(HttpResponseMessage responseMessage);
    }
}
