using System;
using System.Text.RegularExpressions;

namespace MonoBus
{
	public class BusStop
	{
		private static Regex _nameRegex = new Regex(@"\((\d+)\)");
		
		public int BusStopId { get; set; }
		
		private string _name;
		public string Name 
		{
			get { return _name; }
			set 
			{
				if(_nameRegex.IsMatch(value)) {
					_name = _nameRegex.Replace(value, "").Trim();
				} else {
					_name = value;
				}
			}
		}
		
		public string NameWithAbbreviations { get; set; }
		public string BusStopMaintainer { get; set; }
		public int LocationId { get; set; }
		public double Longitude { get; set; }
		public double Latitude { get; set; }
	}
}