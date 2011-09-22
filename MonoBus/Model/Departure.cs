using System;

namespace MonoBus
{
	public class Departure
	{
		public string Line { get; set; }
		public string Destination { get; set; }
		public string RegisteredDepartureTime { get; set; }
		public string ScheduledDepartureTime { get; set; }
		public bool IsRealtimeData { get; set; }
		
		public string DisplayTime 
		{
			get
			{
				var date = Convert.ToDateTime(RegisteredDepartureTime);
				return "Avgang kl. " + date.ToString("HH:MM:ss");
			}
		}
	}
}