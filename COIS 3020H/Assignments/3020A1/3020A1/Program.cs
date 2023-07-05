using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            int i, j;

            Console.WriteLine("My Subway Station Implementation");

            SubwayMap map = new SubwayMap();
            string[] namearray = new string[10] {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            
            //for loop to go through the array for inserting the stations
            for (i = 0; i < namearray.Length; i++)
                map.InsertStation(namearray[i]);
            
            //NOW INSETING THE CONNECTIONS BETWEENT THE STATIONS
            Console.WriteLine("Connection between : J AND A color : RED" );
            Console.WriteLine("Connection between : A AND B color : RED");
            Console.WriteLine("Connection between : B AND C color : BLUE");
            Console.WriteLine("Connection between : C AND D color : YELLOW");
            Console.WriteLine("Connection between : D AND H color : PINK");
            Console.WriteLine("Connection between : H AND F color : VIOLET");
            Console.WriteLine("Connection between : E AND G color : ORANGE");

            
            //FINDING A STATION THAT DOES NOT EXIST
            int x = map.FindStation("A");
            Console.WriteLine("If -1 then not found else found at position is printed : " + x);
            
            //BFS , FASTEST ROUTE
            map.FastestRoute("F", "B");
            
            //DFS, CRITICAL CONNECTIONS
            map.CriticalConnections();

            //REMOVING A CONNECTION
            map.RemoveConnection("B", "D", Colour.YELLOW);

            //REMOVING A STATION
            map.RemoveStation("A");

             x = map.FindStation("A");
            Console.WriteLine("If -1 then not found else found at position is printed : " + x);

            Console.ReadKey();

        }
    }

}

