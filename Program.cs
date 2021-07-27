using System;

using MaxMind.MinFraud;
using MaxMind.MinFraud.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

using MaxMind.GeoIP2;

namespace dotnet_test
{
    class Program
    {
        static void Main(string[] args)
        {
	    MinFraudAsync().Wait();
            Console.WriteLine("Hello World!");
        }

	static public async Task MinFraudAsync()
	{
		var transaction = new Transaction(
			device: new Device(System.Net.IPAddress.Parse("1.1.1.1"),
				userAgent:
				"Mozilla/5.0 (X11; Linux x86_64)",
				acceptLanguage: "en-US,en;q=0.8",
				sessionAge: 3600,
				sessionId: "a333a4e127f880d8820e56a66f40717c"
			)
		);

		using (var client = new MaxMind.MinFraud.WebServiceClient(11, "<snip>"))
		{
			var score = await client.ScoreAsync(transaction);
			Console.WriteLine(score);
		}

		using (var client = new MaxMind.GeoIP2.WebServiceClient(11, "<snip>"))
		{
			var response = await client.CountryAsync("8.8.8.8");
			Console.WriteLine(response);
		}
    	}
    }
}
