using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisjointSetsForest
{
    public interface IDisjointSets
    {
        bool Union(int s1, int s2);  // Return the union of sets s1 and s2
        int Find(int item);          // Return the set that item belongs
    }

    //------------------------------------------------------------------------------

    public class DisjointSets : IDisjointSets
    {
        private int[] set;           // If set[i] = j < 0 then set i has size |j|
                                     // (i.e. set i exists with the given size)
                                     // If set[i] = j > 0 then set i points to set j
                                     // (i.e. set i no longer exists)
        private int numItems;        // Number of items

        // Constructor
        // Item i is placed into set i with a size of 1
        // Time complexity:  O(n)

        public DisjointSets(int numItems)
        {
            int i;

            this.numItems = numItems;
            set = new int[numItems];
            for (i = 0; i < numItems; i++)
                set[i] = -1;         // Each set has an initial size of 1
                                     // Assumption: item i belongs to set i
        }

        /* The total time complexity for any sequence of m >= n Finds and n-1 Unions is:
                                   
                                      O(m*a(m,n))
          
           where the function a(m,n) is the inverse of Ackermann's function.  The function
           a(m,n) grows so slowly that it is nearly constant.
        */

        public bool Union(int s1, int s2)
        {
            // Union by size
            if (set[s1] < 0 && set[s2] < 0)  // Both sets exist
            {
                if (set[s2] < set[s1])       // If size of set s2 > size of set  s1
                {                            // Then
                    set[s2] += set[s1];      // Increase the size of s2 by the size of s1
                    set[s1] = s2;            // Have set s1 point to set s2
                }                            // (i.e. s2 = s1 U s2)

                else                         // If the size of set s1 >= size of set s2
                {                            // Then
                    set[s1] += set[s2];      // Increase the size of s1 by the size of s2
                    set[s2] = s1;            // Have set s2 point to set s1
                }                            // (i.e. s1 = s1 U s2)
                return true;
            }
            else
                return false;
        }

        public int Find(int item)
        {
            if (item < 0 || item >= numItems) // Item is not in the range 0..n-1
                return -1;
            else
                if (set[item] < 0)            // set[item] exists
                    return item;
                else
                    // Path Compression
                    // Recurse to the root set 
                    // And then update all sets along the path to point to the root
                    return set[item] = Find(set[item]);
        }

        // Print
        // Outputs the ranks/set pointers
        // Time complexity: O(n)

        public void Print()
        {
            int i;

            for (i = 0; i < numItems; i++)
            {
                Console.Write(set[i] + " ");
            }
            Console.WriteLine();
        }
    }

    //----------------------------------------------------------------------------------

    // Test for DisjointSets

    public class Test
    {
        static void Main(string[] args)
        {
            DisjointSets D = new DisjointSets(8);

            D.Union(0, 1);   // S0 = S0 U S1 (same size)
            D.Union(2, 3);   // S2 = S2 U S3 (same size)
            D.Union(5, 4);   // S5 = S4 U S5 (same size)
            D.Union(2, 1);   // Set 1 does not exist
            D.Union(0, 6);   // S0 = S0 U S6 (S0 has a greater size)
            D.Union(5, 7);   // S5 = S5 U S7 (s5 has a greater size)
            D.Union(5, 0);   // S5 = S5 U S0 (same size)
            D.Union(2, 5);   // S5 = S2 U S5 (S5 has a greater size)

            D.Print();

            D.Find(5);
            D.Find(6);
            D.Find(1);

            D.Print();

            Console.ReadKey();
        }
    }
}
