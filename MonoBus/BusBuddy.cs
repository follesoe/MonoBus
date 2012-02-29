using System;
using System.Collections.Generic;
using RestSharp;

namespace MonoBus
{
	public class BusBuddy
	{
		private RestClient _restClient;
		
		public BusBuddy()		
		{
			_restClient = new RestClient("http://api.busbuddy.no/api/1.3");
			_restClient.AddDefaultParameter("apiKey", "HwSJ6xL9wCUnpegC");
		}
		
		public void GetBusStops(Action<BusStopResponse> callback)
		{
			var request = new RestRequest("busstops");
			request.RequestFormat = DataFormat.Json;
						
			_restClient.ExecuteAsync<BusStopResponse>(request, (RestResponse<BusStopResponse> response) => {
				callback(response.Data);
			});
		}
		
		public void GetDepartures(int locationId, Action<DeparturesResponse> callback) 
		{
			var request = new RestRequest("departures/" + locationId);
			request.RequestFormat = DataFormat.Json;
			
			_restClient.ExecuteAsync<DeparturesResponse>(request, (RestResponse<DeparturesResponse> response) => {
				callback(response.Data);
			});
		}
	}
}

