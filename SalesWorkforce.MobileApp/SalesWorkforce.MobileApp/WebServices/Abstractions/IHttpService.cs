using System.Net;
using System.Net.Http;

namespace SalesWorkforce.MobileApp.WebServices.Abstractions
{
	public interface IHttpService
	{
		CookieContainer Cookie { get; }

		HttpClientHandler HttpClientHandler { get; }

		IAppHttpClient AppHttpClient { get; }

		void ResetServerBaseAddress(string baseAddress);
	}
}
