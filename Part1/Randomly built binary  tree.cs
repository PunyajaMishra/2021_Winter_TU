
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomlyBuiltBinaryTree
{
    public class Node<T>
    {
        public T Item        { get; set; }     // Item stored in the Node
        public Node<T> Left  { get; set; }     // Left subtree
        public Node<T> Right { get; set; }     // Right subtree

        // Node constructor
        public Node(T item, Node<T> L, Node<T> R)
        {
            Item = item;
            Left = L;
            Right = R;
        }
    }

    public class RandomBinaryTree<T>
    {
        private Node<T> root;        // Reference to the root of the binary tree
        private Random r;            // For building the random binary tree

        // RandomBinaryTree constructor
        // Builds a random binary tree on n nodes

        public RandomBinaryTree(int n)
        {
            r = new Random();        // Creates a random variable r
            root = RandomBuild(n);
        }

        // RandomBuild (in a preorder fashion)
        // Creates and returns a randomly built binary tree on n nodes
        // Devroye and Kruszewski, Botanical beauty of random binary trees, Springer, 1996

        public Node<T> RandomBuild(int n)
        {
            int left;

            if (n == 0)
                return null;
            else
            {
                // Randomly determine number of nodes in the left subtree
                left = (int)(n * r.NextDouble());

                // Recursively build tree
                return new Node<T>(default(T), RandomBuild(left), RandomBuild(n - left - 1));
            }
        }

        // Public Print (Inorder)
        // Outputs the binary tree in a 2-D format without edges and rotated 90 degrees

        public void Print()
        {
            Print(root, 0);
        }

        // Private Print
        // Recursively implements the public Print

        private void Print(Node<T> root, int indent)
        {
            if (root != null)
            {
                Print(root.Right, indent + 3);
                Console.WriteLine(new String(' ', indent) + "*");
                Print(root.Left, indent + 3);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int n;

            n = Convert.ToInt32(Console.ReadLine());
            while (n != 0 ) 
            {
                RandomBinaryTree<int> BT = new RandomBinaryTree<int>(n);
                BT.Print();
                n = Convert.ToInt32(Console.ReadLine());
            }
        }
    }
}
