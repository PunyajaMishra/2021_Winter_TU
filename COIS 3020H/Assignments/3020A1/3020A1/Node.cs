using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
	//enum Color to store color values
	public enum Colour { RED, YELLOW, GREEN, BLUE, ORANGE, PINK, VIOLET }

	public class Node
	{
		public Station Connection { get; set; } // Adjacent station (connection)
		public Colour Line { get; set; } // Colour of its subway line
		public Node Next { get; set; } // Link to the next adjacent station (Node)

		public Node(Station connection, Colour c, Node next)
		{ //Node constructor setting all values of the properties
			Connection = connection;
			Line = c;
			Next = next;
		}

	}
}
