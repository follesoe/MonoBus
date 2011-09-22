using System;

namespace MonoBus
{
	public class BusStop
	{
		public int BusStopId { get; set; }
		public string Name { get; set; }
		public string NameWithAbbreviations { get; set; }
		public string BusStopMaintainer { get; set; }
		public int LocationId { get; set; }
		public double Longitude { get; set; }
		public double Latitude { get; set; }
	}
}