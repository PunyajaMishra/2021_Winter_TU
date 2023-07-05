using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntervalTrees
{
    // Interfaces used for an Interval Tree

    public interface IContainer
    {
        void MakeEmpty();         // Reset to empty
        bool Empty();             // Return true if empty; false otherwise
        int Size();               // Return size
    }

    //-------------------------------------------------------------------------

    public interface ISearchable : IContainer
    {
        void Add(Interval period);          // Add the interval to the interval tree
        void Remove(Interval period);       // Remove the interval from the interval tree
        bool Contains(Interval period);     // Return true if interval found; false otherwise

        // Augmented method
        Interval Overlap(Interval period);  // Return an interval (if any) that overlaps with the given interval
    }

    //-----------------------------------------------------------------------------

    public class Interval : IComparable
    {
        public int Low  { get; set; }      // Start of interval
        public int High { get; set; }      // End of interval

        // Constructor
        public Interval(int low, int high)
        {
            Low = low;
            High = high;
        }

        // CompareTo (from IComparable)
        // Returns -ve if Low of the current interval < Low of the given interval
        //             or [ Low of the current interval = Low of the given interval and
        //                  High of the current interval < High of the given interval ]
        // Returns 0 if Low and High are same for both intervals
        // Returns +ve otherwise  

        public int CompareTo(object obj)
        {
            Interval other = (Interval)obj;
            if (other != null)
            {
                if (Low == other.Low)
                    return High - other.High;
                else
                    return Low - other.Low;
            }
            else
                return +1;
        }

        // ToString (from Object class)
        // Returns a string representation of an instance of Interval

        public override string ToString()
        {
            return "(" + Low + "," + High + ")";
        }
    }

    //-------------------------------------------------------------------------

    // Node class for an Interval Tree

    public class Node
    {
        private static Random R = new Random();

        // Read/write properties

        public Interval Period { get; set; }
        public int Priority    { get; set; }    // Randomly generated
        public int MaxHigh     { get; set; }    // Augmented information (day)
        public Node Left       { get; set; }
        public Node Right      { get; set; }

        // Node constructor
        public Node(Interval period)
        {
            Period  = period;                // Given interval
            Priority = R.Next(10, 100);
            MaxHigh = Period.High;           // Augmented data
            Left = Right = null;
        }
    }

    //-------------------------------------------------------------------------

    // Implementation:  Treap

    class IntervalTree : ISearchable
    {
        private Node Root;  // Reference to the root of the Treap

        // Constructor IntervalTree
        // Creates an empty Interval Tree
        // Time complexity:  O(1)

        public IntervalTree()
        {
            MakeEmpty();
        }

        // CalcMax
        // Updates MaxHigh of subtree rooted at p (if necessary) to:
        //         max(p.Period.High, p.Left.MaxHigh, p.Right.MaxHigh)
        // Time complexity:  O(1)

        private void CalcMax(Node p)
        {
            p.MaxHigh = p.Period.High;
            if (p.Left != null)
                if (p.Left.MaxHigh > p.MaxHigh)
                    p.MaxHigh = p.Left.MaxHigh;

            if (p.Right != null)
                if (p.Right.MaxHigh > p.MaxHigh)
                    p.MaxHigh = p.Right.MaxHigh;
        }

        // LeftRotate
        // Performs a left rotation around the given root
        // Time complexity:  O(1)

        private Node LeftRotate(Node root)
        {
            Node temp = root.Right;
            root.Right = temp.Left;
            temp.Left = root;
            return temp;
        }

        // RightRotate
        // Performs a right rotation around the given root
        // Time complexity:  O(1)

        private Node RightRotate(Node root)
        {
            Node temp = root.Left;
            root.Left = temp.Right;
            temp.Right = root;
            return temp;
        }

        // Public Add
        // Inserts the given period into the Interval Tree
        // Calls Private Add to carry out the actual insertion
        // Expected time complexity:  O(log n)

        public void Add(Interval period)
        {
            Root = Add(period, Root);
        }

        // Add 
        // Inserts period into the Interval Tree and returns a reference to the root
        // Intervals are ordered by Low and secondarily by High
        // Duplicate periods are not inserted
        // Expected time complexity:  O(log n)

        private Node Add(Interval period, Node root)
        {
            int cmp;  // Result of a comparison

            if (root == null)
                return new Node(period);
            else
            {
                cmp = period.CompareTo(root.Period);
                if (cmp > 0)
                {
                    root.Right = Add(period, root.Right);     // Move right
                    if (root.Right.Priority > root.Priority)  // Rotate left (if necessary)
                    {
                        root = LeftRotate(root);
                        CalcMax(root.Left);                   // Update MaxHigh for the left child
                    }
                }
                else if (cmp < 0)
                {
                    root.Left = Add(period, root.Left);       // Move left
                    if (root.Left.Priority > root.Priority)   // Rotate right (if necessary)
                    {
                        root = RightRotate(root);
                        CalcMax(root.Right);                  // Update MaxHigh for the right child
                    }
                }
                CalcMax(root);                                // (Re)calculate MaxHigh for the (new) root
                return root;
            }
        }

        // Public Remove
        // Removes the given period from the Interval Tree
        // Calls Private Remove to carry out the actual removal
        // Expected time complexity:  O(log n)

        public void Remove(Interval period)
        {
            Root = Remove(period, Root);
        }

        // Remove 
        // Removes the given period from the Interval Tree
        // Nothing is performed if the period is not found
        // Expected time complexity:  O(log n)

        private Node Remove(Interval period, Node root)
        {
            int cmp;  // Result of a comparison

            if (root == null)   // Item not found
                return null;
            else
            {
                cmp = period.CompareTo(root.Period);
                if (cmp < 0)
                    root.Left = Remove(period, root.Left);      // Move left
                else if (cmp > 0)
                    root.Right = Remove(period, root.Right);    // Move right
                else if (cmp == 0)                              // Item found
                {
                    // Case: Two children
                    // Rotate the child with the higher priority to the given root
                    if (root.Left != null && root.Right != null)
                    {
                        if (root.Left.Priority > root.Right.Priority)
                            root = RightRotate(root);
                        else
                            root = LeftRotate(root);
                    }

                    // Case: One child
                    // Rotate the left child to the given root
                    else if (root.Left != null)                 
                        root = RightRotate(root);
                    // OR
                    // Rotate the right child to the given root
                    else if (root.Right != null)
                        root = LeftRotate(root);

                    // Case: No children (i.e. a leaf node)
                    // Snip off the leaf node containing item
                    else
                        return null;

                    // Recursively move item down the Treap
                    root = Remove(period, root);
                }
                CalcMax(root);                                  // (Re)calculate MaxHigh for the (new) root)
                return root;
            }
        }

        // Contains
        // Returns true if the given item is found in the Interval Tree; false otherwise
        // Expected time complexity:  O(log n)

        public bool Contains(Interval period)
        {
            Node curr = Root;
            int cmp;

            while (curr != null)
            {
                cmp = period.CompareTo(curr.Period);
                if (cmp == 0)                       // Found
                    return true;
                else if (cmp < 0)
                    curr = curr.Left;               // Move left
                else
                    curr = curr.Right;              // Move right
            }
            return false;
        }

        // Overlap
        // Returns an interval that overlaps with the given period; otherwise (0,0)
        // Expected time complexity:  O(log n)

        public Interval Overlap(Interval period)
        {
            Node curr = Root;

            while ((curr != null) && 
                   (curr.Period.Low > period.High || period.Low > curr.Period.High))  // No overlap
            {
                if ((curr.Left != null) && (period.Low <= curr.Left.MaxHigh))
                    curr = curr.Left;
                else
                    curr = curr.Right;
            }

            if (curr != null)
                return curr.Period;
            else
                return new Interval(0, 0);        // Default interval (no overlap)
        }
     
        // MakeEmpty
        // Creates an empty Interval Tree

        public void MakeEmpty()
        {
            Root = null;
        }

        // Empty
        // Returns true if the Interval Tree is empty; false otherwise

        public bool Empty()
        {
            return Root == null;
        }

        // Public Size
        // Returns the number of items in the Interval Tree
        // Calls Private Size to carry out the actual calculation
        // Time complexity:  O(n)

        public int Size()
        {
            return Size(Root);
        }

        // Size
        // Returns the number of items in the given Interval Tree
        // Time complexity:  O(n)

        private int Size(Node root)
        {
            if (root == null)
                return 0;
            else
                return 1 + Size(root.Left) + Size(root.Right);
        }

        // Public Height
        // Returns the height of the Interval Tree
        // Calls Private Height to carry out the actual calculation
        // Time complexity:  O(n)

        public int Height()
        {
            return Height(Root);
        }

        // Private Height
        // Returns the height of the given Treap
        // Time complexity:  O(n)

        private int Height(Node root)
        {
            if (root == null)
                return -1;    // By default for an empty Treap
            else
                return 1 + Math.Max(Height(root.Left), Height(root.Right));
        }

        // Public Print
        // Prints out the items of the Interval Tree inorder
        // Calls Private Print to carry out the actual print

        public void Print()
        {
            Print(Root, 0);
        }

        // Print
        // Inorder traversal of the Interval Tree
        // Time complexity:  O(n)

        private void Print(Node root, int index)
        {
            if (root != null)
            {
                Print(root.Right, index + 9);
                Console.WriteLine(new String(' ', index) + root.Period + " " + root.MaxHigh);
                Print(root.Left, index + 9);
            }
        }
    }

    //-----------------------------------------------------------------------------

    public class Program
    {
        static Random V = new Random();

        static void Main(string[] args)
        {
            IntervalTree B = new IntervalTree();
            Interval p, q;
            int low, high;

            for (int i = 0; i < 20; i++)
            {
                low = V.Next(10, 91);               // Low = [10..90]
                high = low + V.Next(0, 10);         // Intervals of length 0 to 9
                B.Add(new Interval(low, high));     // Add random intervals
            }
            B.Print();

            do
            {
                // Read in an interval
                string s = Console.ReadLine();
                string[] values = s.Split(' ');
                low = int.Parse(values[0]);
                high = int.Parse(values[1]);

                if (low == 0) break;

                p = new Interval(low, high);
                q = B.Overlap(p);
                if (q != null)
                {
                    Console.WriteLine(q.ToString());
                    B.Remove(q);
                }
                B.Print();
            } while (true);

            Console.ReadLine();
        }
    }
}
