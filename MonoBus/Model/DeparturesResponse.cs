using System;
using System.Collections.Generic;

namespace MonoBus
{
	public class DeparturesResponse
	{
		public bool IsGoingTowardsCentrum { get; set; }
		public List<Departure> Departures { get; set; }
	}
}