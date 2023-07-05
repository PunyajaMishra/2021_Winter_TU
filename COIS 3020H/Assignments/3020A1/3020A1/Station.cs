using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment1
{
	public class Station
	{
		public string Name { get; set; } // Name of the subway station
		public bool Visited { get; set; } // Used for depth-first and breadth-first searches
		public Node E { get; set; } // Linked list of adjacent stations

		// Station constructor setting the values of all properties
		public Station(string name)
		{
			Name = name;
			Visited = false;
			E = null;
		}

	}
}
