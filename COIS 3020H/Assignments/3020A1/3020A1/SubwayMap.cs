using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Assignment1
{
	public class SubwayMap
	{
		private List<Station> S; // List of all stations

		// SubwayMap constructor 
		public SubwayMap()
		{
			S = new List<Station>();
		}

		//finding if the given string name is a station
		public int FindStation(string name)
		{
			for (int i = 0; i < S.Count; i++)
				if (S[i].Name.Equals(name))
					return i;
			return -1;
		}

		// 2 stations with same names are not allowed 
		public void InsertStation(string name)
		{
			if (FindStation(name) == -1) 
			{
				Station s = new Station(name);
				S.Add(s);
				Console.WriteLine("New Station built : " + name);
			}
			else
			{ //if already that station exists then nope
				Console.WriteLine("Station already built\n");	
			}
		}

		//Removing a station from the subway that includes removing the stations
		public void RemoveStation(string name)
		{  
			int i, j, k;
			if ((i = FindStation(name)) > -1) //station found so removing the station and its connections
			{
				for (j = 0; j < S.Count; j++)
				{
					//now to remove that station, that station's color connections are removed? and then call remove connection
					
					foreach(Colour col in Enum.GetValues(typeof(Colour)))
					{ //going through the entire enum so for all color routes it is checked if the given station has connection
						//and then remove the connection
						RemoveConnection(name, S[j].Name, col);
					}
				}
				S.RemoveAt(i);
				Console.WriteLine("Station : " + name + " is now burnt to ground "); 
			}
			else
			{ //no such station name , so no such station
				Console.WriteLine("No such station exists");
			}
		}

		//Inserting a new connection between stations
		public void InsertConnection(string name1, string name2, Colour c)
		{
			int i, j;
			Node node; //variable node to go through all the connection nodes for the station

			//check if the given stations exist
			if ((i = FindStation(name1)) > -1 && (j = FindStation(name2)) > -1)
			{
				node = new Node(S[j], c, null);

				//check if the connection/route does not exist already
				if (S[i].E == null)
				{
					S[i].E = node; //so we have a connection node now
					
				}
				else
				{
					//since connection seems to exist for first spot on E 
					//first check for already existing connection
					Node variablenode = S[i].E;
					while (variablenode.Next != null)
					{
						if(variablenode.Connection.Name == name2 && variablenode.Line == c)
						{
							Console.WriteLine("Connection exists with color : " + variablenode.Connection + c);
							return;
						}
						variablenode = variablenode.Next;
					}

					//now no existing connection is found 
					if(variablenode.Connection.Name == name2 && variablenode.Line == c)
					{
						Console.WriteLine("Connection exists with color : " + variablenode.Connection + c);
						return;
					}
					else
					{ 
						variablenode.Next = node;
						Console.WriteLine("Connection between : " + S[i].Name + " AND " + (variablenode.Next).Connection.Name + " color : " + c);
					}

				}
			}
			else
			{ //maybe wrong station name given
				Console.WriteLine("No such station exists");
			}
		}


		//Removing just connection and not the stations
		public void RemoveConnection(string name1, string name2, Colour c)
		{
			int i, j;
			if ((i = FindStation(name1)) > -1 && (j = FindStation(name2)) > -1)
			{
				//remove connection both ways since this is undirected
				Remove_Connection(name1, name2, c);
				Remove_Connection(name2, name1, c);
			}
			else
			{ //no connection between the given stations
				Console.WriteLine("No such conneciton established here yet");
			}
		}

		//to remove connections from both ways
		private void Remove_Connection(string name1, string name2, Colour c)
		{
			int i, j;
			Node variablenode = S[FindStation(name1)].E;

			if(variablenode == null) //no connection - safety if to avoid crashing null exception
			{
				return;
			}
			if((i=FindStation(name1))>-1 && (j=FindStation(name2))>-1)
			{
				//checking for already existing names
				if(variablenode.Connection.Name == name2 && variablenode.Line == c)
				{
					S[i].E = variablenode.Next ?? null; //set it to null if  no next
					Console.WriteLine("Connection removed BETWEEN " + name1 + " and " + name2);
					return;
				}
				else
				{
					while (variablenode.Next != null) //while there are connection nodes
					{
						if(variablenode.Next.Connection.Name == name2 && variablenode.Next.Line == c)
						{ //keep going until found and remove by setting value null
							variablenode.Next.Next = variablenode.Next ?? null;
							return;
						}
						variablenode = variablenode.Next;
						Console.WriteLine("Connection removed BETWEEN " + name1 + " and " + name2);
					}
				}
				Console.WriteLine("No connection from " + name1 + " to " + name2);
			}
			else
			{ //no such node was there
				Console.WriteLine("No connection found");
			}
		}

	

		/*
		 * ###################################################################################################################
		 * 
		 *	Task 2 : Breadh First Search : Fastest Route 
		 *	//using tuple for multiple paramteres
		 *	Source I took help from : https://www.koderdojo.com/blog/breadth-first-search-and-shortest-path-in-csharp-and-net-core
		 * 
		 * ###################################################################################################################
		 */

		public void BreadthFirstSearch()
		{
			int i;

			for (i = 0; i < S.Count; i++)
				S[i].Visited = false;              // Set all vertices as unvisited

			for (i = 0; i < S.Count; i++)
				if (!S[i].Visited)                  // (Re)start with top station (vertex)
				{
					BreadthFirstSearch(S[i]);
					Console.WriteLine();
				}
		}

		private void BreadthFirstSearch(Station s)
		{
			Station w;
			Queue<Station> Q = new Queue<Station>(); //queue to store the stations

			s.Visited = true; //set it as visited
			Q.Enqueue(s);

			while (Q.Count != 0)
			{ //go through the entire queue
				s = Q.Dequeue();
				Console.WriteLine(s.Name);

				if(s.E!=null)
				{
					Node variablenode = s.E; //check thorugh the nodes via the variable
					w = s.E.Connection;
					if (!w.Visited)
					{
						//make sure all the connection have been visited and add them in th queue
						w.Visited = true;
						Q.Enqueue(w);
					}

					while (variablenode.Next != null) 
					{	
						variablenode = variablenode.Next;
						w = variablenode.Connection;
						if (!w.Visited)
						{ //doing the same thing as previous, just this time using the nodes, so all nodes/connections are visited
							w.Visited = true;
							Q.Enqueue(w);
						}
					}
				}
			}
		}

		public void FastestRoute(String from, String to)
		{ //finding the shortest path
			int i;
			for ( i = 0; i < S.Count; i++)
				S[i].Visited = false;  //mark them al unvisited

			int from_station = FindStation(from); //from source
			int to_station = FindStation(to); //to destination

			if(from_station >-1 && to_station>-1)
			{//calling the private function and printing the distance
				String fastestdistance = FastestRoute(S[from_station], S[to_station].Name, to);
				Console.WriteLine("Fastest distance between " + from + " and " + to + " is : " + fastestdistance);
			}
		}

		private String FastestRoute(Station s, String route, String to)
		{
			Station station ;
			Queue<Tuple<Station, String>> tuple = new Queue<Tuple<Station, string>>(); //tuple so we have multiple properties together as package
			
			//variable tuple
			var statiointuple = Tuple.Create(s, route);
			
			//visited
			s.Visited = true;
			
			//enqueue in the list for all visited 
			tuple.Enqueue(statiointuple);

			while (tuple.Count != 0)
			{ //go through the list and check each touple
				
				if (statiointuple.Item1.E != null)
				{
					Node variablenode = statiointuple.Item1.E; //using item 1 of touple because we need the station  and its node E
					station = statiointuple.Item1.E.Connection; //checking connection for that station node
					if (!station.Visited)
					{ //making all stations with connection visited
						station.Visited = true;
						//new route variable with the station name and making a new touple and adding it to the list
						//this is to ensure all the connections and stations are covered
						String route2 = statiointuple.Item2 + " - " + station.Name;
						var tuple2 = Tuple.Create(station, route);
						tuple.Enqueue(tuple2);
					}
					//always a loop to check the nodes while they are not null
					while (variablenode.Next != null)
					{
						//just like we have been doing before, ensure that nodes are covered and do basically the same thing
						//as we did in the if statement
						variablenode = variablenode.Next;
						station = variablenode.Connection;
						if (!station.Visited)
						{
							station.Visited = true;
							String route2 = statiointuple.Item2 + " - " + station.Name;
							var tuple2 = Tuple.Create(station, route2); 
							tuple.Enqueue(tuple2);
						}
					}
				}
				if (statiointuple.Item1.Name == to)
					return statiointuple.Item2; //the entire route of shortest route is stored and made into a touple which enqueued and 
												//that is what we return
				statiointuple = tuple.Dequeue();
			}
			return "there was no connection between them"; //nothing no connection was found, so no distance to return
		}

		/*
		 * ###################################################################################################################
		 * 
		 *	Task 3 : Depth First Search : Critical Connections
		 ** //using tuple for multiple paramteres
		 *	Source I took help from : https://www.koderdojo.com/blog/depth-first-search-algorithm-in-csharp-and-net-core
		 *							: https://leetcode.com/problems/critical-connections-in-a-network/discuss/421595/c-solution
		 * 
		 * ###################################################################################################################
		 */

		public void DepthFirstSearch()
		{
			int i;
			for (i = 0; i < S.Count; i++) //all stations are unvisited
				S[i].Visited = false;

			for (i = 0; i < S.Count; i++)
			{
				if (!S[i].Visited) //calling the private dfs function
				{
					DepthFirstSearch(S[i]);
					Console.WriteLine();
				}
			}
		}

		private void DepthFirstSearch(Station s)
		{
			s.Visited = true;    // Output sation when marked as visited
			Console.WriteLine(s.Name);

			while (s.E != null)       // Visit next adjacent station
			{
				Node variablenode = s.E;
				while (variablenode != null)
				{
					if (!variablenode.Connection.Visited)
					{ //make sure all the connection have been visited 
						DepthFirstSearch(variablenode.Connection);
					}
					variablenode = variablenode.Next;
				}
			}
		}

		public void CriticalConnections()
		{
			//for critical connection, we need to know the shirtest distance, distance between the points and an index
			List<Tuple<int, int, string>> tuple1 = new List<Tuple<int, int, string>>();
			int i;

			for (i = 0; i < S.Count; i++)
				S[i].Visited = false; // make all stations be unvisited. 

			for(i=0; i<S.Count; i++)
			{
				if (!S[i].Visited)
				{    //check for all unvisited stations and connections, nothing should be left
					CriticalConnections(S[i], tuple1, 1, S[i].Name);
					Console.WriteLine();
				}
			}

			if(globals.CriticalPoint.Count > 0) //we have some critical connection points
			{
				//printing the critical connection points
				Console.WriteLine("Critical Connection Points : ");
				for(i=0; i<globals.CriticalPoint.Count; i++)
				{
					Console.WriteLine(globals.CriticalPoint[i].Item3); //printing out the string name
					Console.WriteLine();
				}
			}
			else
			{
				Console.WriteLine("No critical connections were found");
			}
		}

		private void CriticalConnections(Station s, List<Tuple<int,int,string>> tuple2, int depth, string name)
		{
			if (!s.Visited)
			{
				var tuple3 = Tuple.Create(depth, depth, s.Name); //again a touple to store all the required parameters together
				tuple2.Add(tuple3); 
			}

			s.Visited = true; //mark the station as visited to avoid repetition
			int stationaname2, stationname1;
			//checking connections to find distances
			if (s.E != null)
			{
				Node variablenode = s.E;
				while (variablenode != null)
				{
					if(variablenode.Connection.Visited) //the connection has been visited
					{
						//in the connection one is the variable node and the other is the 
						//station (sent we see for distance
						stationaname2 = tuple2.FindIndex(t => t.Item3 == s.Name);
						stationname1 = tuple2.FindIndex(t => t.Item3 == variablenode.Connection.Name);
						if (tuple2[stationname1].Item2 < tuple2[stationaname2].Item2 && variablenode.Connection.Name != s.Name)
							tuple2[stationname1] = Tuple.Create(tuple2[stationname1].Item1, tuple2[stationaname2].Item2, tuple2[stationname1].Item3);
					}
					else
					{
						//further
						CriticalConnections(variablenode.Connection, tuple2, depth + 1, s.Name);
					}

					 stationaname2 = tuple2.FindIndex(t => t.Item3 == s.Name);
					 stationname1 = tuple2.FindIndex(t => t.Item3 == variablenode.Connection.Name);

					//NOW again,, 3 items, the second one is for smallest distance and the stationname2 in the tuple is where the 
					// current node points to. the points are chnaged here. 
					if (tuple2[stationaname2].Item2 >= tuple2[stationname1].Item1)
					{
						if (!globals.CriticalPoint.Contains(tuple2[stationname1]))  //we are looking for crtical connections that will put it in half so we need the stationnames
							globals.CriticalPoint.Add(tuple2[stationname1]);
					}
					else if(tuple2[stationaname2].Item2 < tuple2[stationname1].Item2 && variablenode.Connection.Name != s.Name)
						//checking all cases
					{
						stationaname2 = tuple2.FindIndex(t => t.Item3 == variablenode.Connection.Name); 
						stationname1 = tuple2.FindIndex(t => t.Item3 == s.Name);
					}
					variablenode = variablenode.Next;
				}
			}

		}

		
	}

	public static class globals
	{
		//global tuple for multiple values and since they are all for the same thing this keeps them in a package and list 
		public static List<Tuple<int, int, string>> CriticalPoint = new List<Tuple<int, int, string>>();
	}

}
