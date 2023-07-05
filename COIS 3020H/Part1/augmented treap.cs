using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Augmented_Treap
{
    // Interfaces used for a Treap

    public interface IContainer<T>
    {
        void MakeEmpty();         // Reset to empty
        bool Empty();             // Return true if empty; false otherwise
        int Size();               // Return size
    }

    //-------------------------------------------------------------------------

    public interface ISearchable<T> : IContainer<T>
    {
        void Add(T item);         // Add item to the treap (duplicates are not permitted)
        void Remove(T item);      // Remove item from the treap
        bool Contains(T item);    // Return true if item found; false otherwise

        // Augmented methods
        T Rank(int i);            // Return the item with the given rank
        int Rank(T item);         // Return the rank of the given item
    }

    //-------------------------------------------------------------------------

    // Generic node class for an AugmentedTreap

    public class Node<T> where T : IComparable
    {
        private static Random R = new Random();

        // Read/write properties

        public T Item        { get; set; }
        public int Priority  { get; set; }      // Randomly generated
        public int NumItems  { get; set; }      // Augmented information (data)
        public Node<T> Left  { get; set; }
        public Node<T> Right { get; set; }

        // Node constructor
        public Node(T item)
        {
            Item = item;
            Priority = R.Next(10, 100);
            NumItems = 1;
            Left = Right = null;
        }
    }

    //-------------------------------------------------------------------------

    // Implementation:  Treap

    class AugmentedTreap<T> : ISearchable<T> where T : IComparable
    {
        private Node<T> Root;  // Reference to the root of the Treap

        // Constructor AugmentAugmented Treap
        // Time complexity:  O(1)

        public AugmentedTreap()
        {
            MakeEmpty();
        }

        // CalcSize
        // Determines the number of items in the tree rooted at p
        // Time complexity:  O(1)

        private void CalcSize(Node<T> p)
        {
            p.NumItems = 1;
            if (p.Left != null)
                p.NumItems += p.Left.NumItems;
            if (p.Right != null)
                p.NumItems += p.Right.NumItems;
        }

        // LeftRotate
        // Performs a left rotation around the given root
        // Time complexity:  O(1)

        private Node<T> LeftRotate(Node<T> root)
        {
            Node<T> temp = root.Right;
            root.Right = temp.Left;
            temp.Left = root;
            return temp;
        }

        // RightRotate
        // Performs a right rotation around the given root
        // Time complexity:  O(1)

        private Node<T> RightRotate(Node<T> root)
        {
            Node<T> temp = root.Left;
            root.Left = temp.Right;       
            temp.Right = root;
            return temp;
        }

        // Public Add
        // Inserts the given item into the Augmented Treap
        // Calls Private Add to carry out the actual insertion
        // Expected time complexity:  O(log n)

        public void Add(T item)
        {
            Root = Add(item, Root);
        }

        // Add 
        // Inserts item into the Augmented Treap and returns a reference to the root
        // Duplicate items are not inserted
        // Expected time complexity:  O(log n)

        private Node<T> Add(T item, Node<T> root)
        {
            int cmp;  // Result of a comparison

            if (root == null)
                return new Node<T>(item);
            else
            {
                cmp = item.CompareTo(root.Item);
                if (cmp > 0)
                {
                    root.Right = Add(item, root.Right);       // Move right
                    if (root.Right.Priority > root.Priority)  // Rotate left (if necessary)
                    {
                        root = LeftRotate(root);
                        CalcSize(root.Left);                  // Update NumItems for the left child
                    }  
                }
                else if (cmp < 0)
                {
                    root.Left = Add(item, root.Left);         // Move left
                    if (root.Left.Priority > root.Priority)   // Rotate right (if necessary)
                    {
                        root = RightRotate(root);
                        CalcSize(root.Right);                 // Update NumItems for the right child
                    }
                }
                CalcSize(root);                               // (Re)calculate NumItems for the (new) root
                return root;
            }
        }

        // Public Remove
        // Removes the given item from the Augmented Treap
        // Calls Private Remove to carry out the actual removal
        // Expected time complexity:  O(log n)

        public void Remove(T item)
        {
            Root = Remove(item, Root);
        }

        // Remove 
        // Removes the given item from the Augmented Treap
        // Nothing is performed if the item is not found
        // Time complexity:  O(log n)

        private Node<T> Remove(T item, Node<T> root)
        {
            int cmp;  // Result of a comparison

            if (root == null)   // Item not found
                return null;
            else
            {
                cmp = item.CompareTo(root.Item);
                if (cmp < 0)
                    root.Left = Remove(item, root.Left);      // Move left
                else if (cmp > 0)
                    root.Right = Remove(item, root.Right);    // Move right
                else if (cmp == 0)                            // Item found
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
                    root = Remove(item, root);
                }
                CalcSize(root);                               // (Re)calculate NumItems for the (new) root
                return root;
            }
        }

        // Contains
        // Returns true if the given item is found in the Augmented Treap; false otherwise
        // Expected time complexity:  O(log n)

        public bool Contains(T item)
        {
            Node<T> curr = Root;
            int cmp;

            while (curr != null)
            {
                cmp = item.CompareTo(curr.Item);
                if (cmp == 0)                            // Found
                    return true;
                else
                    if (cmp < 0)
                        curr = curr.Left;               // Move left
                    else
                        curr = curr.Right;              // Move right
            }
            return false;
        }

        // Public Rank I
        // Calls private Rank which returns the item with rank i
        // Expected time complexity:  O(log n)

        public T Rank(int i)
        {
            return Rank(Root, i);
        }

        // Private Rank I
        // Returns the item with the given rank i
        // Expected time complexity:  O(log n)

        public T Rank(Node<T> p, int i)
        {
            int r;
            if (i <= p.NumItems)                  // Correct input
            {
                // Determine the size of the left subtree plus p itself
                if (p.Left != null)
                    r = p.Left.NumItems + 1;
                else
                    r = 1;

                if (i == r)                       // item with rank i found at p
                    return p.Item;
                else if (i < r)                   // item in left subtree
                    return Rank(p.Left, i);
                else                              // item in right subtree
                    return Rank(p.Right, i - r);  // Reduce rank i by the size of the left subtree plus p itself
            }
            else
                // i out of range
                return default(T);
        }

        // Rank II
        // Returns the rank of the given item if found; -1 otherwise
        // Expected time complexity:  O(log n)

        public int Rank(T item)
        {
            Node<T> p = Root;
            bool found = false;

            int r = 0;                           // Initial rank
            while (p != null && !found)
            {
                if (p.Item.CompareTo(item) <= 0)  // item is less than or equal to p.Item
                {
                    if (p.Left != null)
                        r += p.Left.NumItems;     // Increase rank by the size of the left subtree
                    r++;                          // Increase rank by 1 for p itself
                    if (p.Item.CompareTo(item) == 0)
                        found = true;             // item and Rank found
                    else
                        p = p.Right;              // Move down the right path
                }
                else
                    p = p.Left;                   // Rank doesn't increase going down the left path
            }
            return found ? r : -1;                // Return rank if item is found; -1 otherwise
        }

        // MakeEmpty
        // Creates an empty Augmented Treap

        public void MakeEmpty()
        {
            Root = null;
        }

        // Empty
        // Returns true if the Augmented Treap is empty; false otherwise

        public bool Empty()
        {
            return Root == null;
        }

        // Public Size
        // Returns the number of items in the Augmented Treap
        // Time complexity:  O(1)

        public int Size()
        {
            return Root.NumItems;
        }

        // Public Height
        // Returns the height of the Augmented Treap
        // Calls Private Height to carry out the actual calculation
        // Time complexity:  O(n)

        public int Height()
        {
            return Height(Root);
        }

        // Private Height
        // Returns the height of the given Augmented Treap
        // Time complexity:  O(n)

        private int Height(Node<T> root)
        {
            if (root == null)
                return -1;    // By default for an empty Augmented Treap
            else
                return 1 + Math.Max(Height(root.Left), Height(root.Right));
        }

        // Public Print
        // Prints out the items of the Augmented Treap inorder
        // Calls Private Print to carry out the actual print

        public void Print()
        {
            Print(Root, 0);
        }

        // Print
        // Inorder traversal of the Augmented Treap
        // Time complexity:  O(n)

        private void Print(Node<T> root, int index)
        {
            if (root != null)
            {
                Print(root.Right, index + 8);
                Console.WriteLine(new String(' ', index) +
                                             root.Item.ToString() + " " +
                                             root.Priority.ToString() + " " +
                                             root.NumItems + " " +
                                             Rank(root.Item));       // for testing purposes
                Print(root.Left, index + 8);
            }
        }
    }

    //-----------------------------------------------------------------------------

    public class Program
    {
        static Random V = new Random();

        static void Main(string[] args)
        {
            AugmentedTreap<int> B = new AugmentedTreap<int>();
            int x;

            for (int i = 0; i < 20; i++)
            {
                x = V.Next(10, 100);
                B.Add(x);               // Add random integers from 10 to 99
                Console.Write(x + " ");
            }
            Console.WriteLine(); Console.WriteLine();

            B.Print();
            Console.WriteLine();

            Console.WriteLine("Min and max        : " + B.Rank(1) + " " + B.Rank(B.Size()));
            Console.WriteLine("Size of the Treap  : " + B.Size());
            Console.WriteLine("Height of the Treap: " + B.Height());
            Console.WriteLine("Contains 42        : " + B.Contains(42));
            Console.WriteLine("Contains 68        : " + B.Contains(68));

            Console.ReadLine();

            for (int i = 0; i < 20; i++)
            {
                x = V.Next(10, 100);
                B.Remove(x);  // Remove random integers
                Console.Write(x + " ");
            }
            Console.WriteLine(); Console.WriteLine();

            B.Print();
            Console.WriteLine();

            Console.WriteLine("Min and max        : " + B.Rank(1) + " " + B.Rank(B.Size()));
            Console.WriteLine("Size of the Treap  : " + B.Size());
            Console.WriteLine("Height of the Treap: " + B.Height());
            Console.WriteLine("Contains 42        : " + B.Contains(42));
            Console.WriteLine("Contains 68        : " + B.Contains(68));

            Console.ReadLine();
        }
    }
}
