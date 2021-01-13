using NUnit.Framework;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ExpressSharp.Tests
{
	[TestFixture(TestName = "GET Tests")]
	public class Get
	{
		public string StringFromResponseStream(Stream stream) => new StreamReader(stream).ReadToEnd();

		public async Task<HttpWebResponse> DoRequest(string url)
		{
			WebRequest request = WebRequest.Create(url);
			request.Method = "GET";
			HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync().ConfigureAwait(false);
			Assert.AreEqual(HttpStatusCode.OK,  response.StatusCode);
			return response;
		}

		[Test(Description = "Tests a standard GET request with plaintext return type")]
		public async Task PlainText()
		{
			HttpWebResponse response = await DoRequest("http://127.0.0.1:3000/PlainText").ConfigureAwait(false);

			string body = StringFromResponseStream(response.GetResponseStream());

			Assert.AreEqual("Hello, World!", body);
		}

		[Test(Description = "Tests a standard GET request with JSON return type")]
		public async Task Json()
		{
			HttpWebResponse response = await DoRequest("http://127.0.0.1:3000/Json").ConfigureAwait(false);

			string body = StringFromResponseStream(response.GetResponseStream());

			Assert.AreEqual("application/json", response.ContentType);
			Assert.AreEqual("{\"Id\":300}", body);
		}
	}
}