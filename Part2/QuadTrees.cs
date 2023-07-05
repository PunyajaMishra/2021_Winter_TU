using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quadtree
{
    // Leaf nodes are BLACK or WHITE
    // Interior nodes are GRAY
    public enum Color { BLACK, WHITE, GRAY};

    // Quadtree Node

    public class Node
    {
        public Color C { get; set; }        // Color of a node (BLACK, WHITE, or GRAY)
        public Node NW { get; set; }        // Northwest quadrant
        public Node NE { get; set; }        // Northeast quadrant
        public Node SE { get; set; }        // Southeast quadrant
        public Node SW { get; set; }        // Southwest quadrant

        // Constructor
        // Creates a node with color c and four empty children
        // Time complexity: O(1)

        public Node(Color c)
        {
            C = c;
            NW = NE = SE = SW = null;
        }
    }

    // Region Quadtree

    public class Quadtree
    {
        // Assumptions:
        // 1) The image is a square with dimensions 2^k x 2^k
        // 2) The image is colored either BLACK or WHITE

        private Node root;      // Root of the quadtree
        private int  size;      // Length of its side (n = 2^k)

        // Constructor A
        // Creates an empty quadtree
        // Time complexity: O(1)

        public Quadtree()
        {
            root = null;
        }

        // Constructor
        // Option 1: Builds a quadtree with compression
        // Option 2: Builds an initial quadtree without compression
        // For both options, the final quadtree is compressed

        public Quadtree(Color[,] image, int size, int option = 1)
        {
            this.size = size;
            if (option == 1)
                root = ConstructWithCompression(image, 0, 0, size);
            else
            {
                root = ConstructWithoutCompression(image, 0, 0, size);

                // Compress the quadtree
                Compress(root);
            }
        }

        // ConstructWithCompression
        // Returns the root of a region
        // A square region is defined by its starting index [x,y] and side length n

        private Node ConstructWithCompression(Color[,] image, int x, int y, int n)
        {
            Node p;
            int h;

            if (Region(image, x, y, n))         // Check if a region is a single color
                p = new Node(image[x, y]);      // If so, create a leaf node with that color (i.e. compress)
           else
           {
                h = n / 2;
                p = new Node(Color.GRAY);       // Set the color of the node to GRAY

                // Recursively build each subtree (quadrant)
                p.NW = ConstructWithCompression(image, x, y, h);
                p.NE = ConstructWithCompression(image, x, y + h, h);
                p.SE = ConstructWithCompression(image, x + h, y + h, h);
                p.SW = ConstructWithCompression(image, x + h, y, h);
            }
            return p;
        }

        // Region
        // Returns TRUE if a region is a single color; FALSE otherwise
        // A square region is defined by its starting index [x,y] and side length n
        // Time complexity: O(n^2)

        private bool Region(Color[,] image, int x, int y, int n)
        {
            int i, j;
            Color c = image[x, y];          // Set color to the that of the first index

            // Traverse the region row-by-row
            for (i = x; i < x + n; i++)
                for (j = y; j < y + n; j++)
                    if (c != image[i, j])
                        return false;       // BLACK and WHITE region: FALSE
            return true;                    // Single color: TRUE
        }

        // ConstructWithoutCompression
        // Returns the root of a complete quadtree
        // Each leaf node represents one of the n^2 pixels
        // Time complexity: O(n^2)

        private Node ConstructWithoutCompression(Color[,] image, int x, int y, int n)
        {
            Node p;
            int h;

            if (n == 1)                     // Leaf node representing one pixel
                p = new Node(image[x, y]);
            else
            {
                h = n / 2;
                p = new Node(Color.GRAY);   // Set the color of the node to GRAY

                // Recursively build each subtree (quadrant)
                p.NW = ConstructWithoutCompression(image, x, y, h);
                p.NE = ConstructWithoutCompression(image, x, y + h, h);
                p.SE = ConstructWithoutCompression(image, x + h, y + h, h);
                p.SW = ConstructWithoutCompression(image, x + h, y, h);
            }
            return p;
        }

        // Compress (Public)
        // Compresses the current quadtree such that no node has children
        // that are all BLACK or all WHITE
        // Time complexity: O(n^2)

        public void Compress()
        {
            Compress(root);
        }

        // Compress (Private)

        private void Compress(Node p)
        {
            if (p.NW != null)
            {
                // Compress each GRAY (non-leaf) quadrant of p
                if (p.NW.C == Color.GRAY)
                    Compress(p.NW);
                if (p.NE.C == Color.GRAY)
                    Compress(p.NE);
                if (p.SE.C == Color.GRAY)
                    Compress(p.SE);
                if (p.SW.C == Color.GRAY)
                    Compress(p.SW);

                // If all children are the same color (BLACK or WHITE)
                // Then set p to the color its children and set all children to null

                if (p.NW.C != Color.GRAY)
                    if (p.NW.C == p.NE.C && p.NW.C == p.SE.C && p.NW.C == p.SW.C)             
                    {
                        p.C = p.NW.C;
                        p.NW = p.NE = p.SE = p.SW = null;
                    }
            }
        }

        // Switch (Public)
        // Modifies the quadtree when a single pixel at index [i,j] changes color
        // Time complexity: O(log n) [depth of the quadtree]

        public void Switch(int i, int j)
        {
            if (i < size && j < size)
                Switch(i, j, 0, 0, size, root);
        }

        // Switch (Private)

        private void Switch(int i, int j, int x, int y, int n, Node p)
        {
            int h;

            if (n == 1)                     // Change color of a single pixel
                if (p.C == Color.BLACK)
                    p.C = Color.WHITE;
                else
                    p.C = Color.BLACK;
            else {
                h = n / 2;        

                // If p is a leaf node (representing nxn pixels, n>1)
                // Create four children of p with the same color as p

                if (p.NW == null)
                {
                    p.NW = new Node(p.C);
                    p.NE = new Node(p.C);
                    p.SE = new Node(p.C);
                    p.SW = new Node(p.C);
                    p.C = Color.GRAY;
                }

                // Descend to the quadrant that contains index [i,j]

                if (i < x + h && j < y + h)
                    Switch(i, j, x, y, h, p.NW);
                else
                if (i < x + h )
                    Switch(i, j, x, y + h, h, p.NE);
                else
                if (j < y + h)
                    Switch(i, j, x + h, y, h, p.SW);
                else
                    Switch(i, j, x + h, y + h, h, p.SE);

                // Compress but only along the path to index [i,j]

                if (p.NW.C != Color.GRAY)
                    if (p.NW.C == p.NE.C && p.NW.C == p.SE.C && p.NW.C == p.SW.C)    
                    {
                        p.C = p.NW.C;
                        p.NW = p.NE = p.SE = p.SW = null;
                    }
            } 
        }

        // Union (Public)
        // Returns the quadtree R which is the union of the current quadtree and Q
        // Union implies for corresponding pixels that:
        //     BLACK + BLACK = BLACK
        //     BLACK + WHITE = BLACK
        //     WHITE + WHITE = WHITE
        // Time complexity: O(n^2)
        // Assumption: Quadtrees represent images of the same size

        public Quadtree Union(Quadtree Q)
        {
            Quadtree R = new Quadtree();

            if (size == Q.size)
            {
                R.root = Union(root, Q.root);
                R.size = size;
            }
            return R;
        }

        // Union (Private)

        private Node Union(Node p, Node q)
        {
            Node r;

            if (p.C == Color.BLACK || q.C == Color.BLACK)
            {
                return new Node(Color.BLACK);   // Return a BLACK leaf node
            }
            else
            if (p.C == Color.WHITE)
            {
                return Clone(q);            // Return a copy of the tree rooted at q
            }
            else
            if (q.C == Color.WHITE)
                return Clone(p);            // Return a copy of the tree rooted at p
            else
            {
                r = new Node(Color.GRAY);   // Create a GRAY node

                // Recursively take the Union of the corresponding quadrants of p and q
                r.NW = Union(p.NW, q.NW);
                r.NE = Union(p.NE, q.NE);
                r.SE = Union(p.SE, q.SE);
                r.SW = Union(p.SW, q.SW);

                return r;
            }
        }

        // Clone
        // Returns a clone of the quadtree rooted at p
        // Time complexity: O(m) where m is the number of nodes of p

        private Node Clone(Node p)
        {
            Node q;

            if (p == null)
                return null;
            else
            {
                q = new Node(p.C);
                q.NW = Clone(p.NW);
                q.NE = Clone(p.NE);
                q.SE = Clone(p.SE);
                q.SW = Clone(p.SW);
                return q;
            }
        }

        // Print (Public)
        // Outputs the image represented by the current quadtree
        // Time complexity: O(n^2)

        public void Print()
        {
            int i, j;
            Color[,] image = new Color[size, size];

            FillIn(image, 0, 0, size, root);

            for (i = 0; i < size; i++)
            {
                for (j = 0; j < size; j++)
                    if (image[i, j] == Color.BLACK)
                        Console.Write("B");
                    else
                        Console.Write("W");
                Console.WriteLine();
            }
        }

        // FillIn
        // Fills in a square image starting at index [x,y] with side length n
        // Time complexity: O(n^2)

        private void FillIn(Color[,] image, int x, int y, int n, Node p)
        {
            int h;

            if (p.C != Color.GRAY)
                ColorRegion(image, x, y, n, p.C);
            else
            {
                h = n / 2;
                FillIn(image, x, y, h, p.NW);
                FillIn(image, x, y + h, h, p.NE);
                FillIn(image, x + h, y + h, h, p.SE);
                FillIn(image, x + h, y, h, p.SW);
            }
        }

        // ColorRegion
        // Sets the region starting at index [x,y] with side length n to color c
        // Time complexity: O(n^2)

        private void ColorRegion(Color[,] image, int x, int y, int n, Color c)
        {
            int i, j;

            for (i = x; i < x + n; i++)
                for (j = y; j < y + n; j++)
                    image[i, j] = c;
        }

        // PrintQuadtree (Public)
        // Prints the current quadtree in side view
        // Time complexity: O(m) where m is the number of nodes

        public void PrintQuadtree ()
        {
            PrintQuadtree(root, 0);
        }

        // PrintQuadtree (private)
        // Prints the inorder traversal of the quadtree

        private void PrintQuadtree(Node p, int n)
        {
            if (p != null)
            {
                PrintQuadtree(p.SW, n + 2);
                PrintQuadtree(p.SE, n + 2);
                if (p.C == Color.BLACK)
                    Console.WriteLine("".PadLeft(n) + "B");
                else
                if (p.C == Color.WHITE)
                    Console.WriteLine("".PadLeft(n) + "W");
                else
                    Console.WriteLine("".PadLeft(n) + "G");
                PrintQuadtree(p.NE, n + 2);
                PrintQuadtree(p.NW, n + 2);
            }
        }
    }

    class Program
    {
        static public void PrintImage(Color[,] image, int size)
        {
            int i, j;

            for (i = 0; i < size; i++)
            {
                for (j = 0; j < size; j++)
                    if (image[i, j] == Color.BLACK)
                        Console.Write("B");
                    else
                        Console.Write("W");
                Console.WriteLine();

            }
        }

        static void Main(string[] args)
        {
            int i, j ,size;
            Quadtree P, Q, R;

            Console.Write("Enter size of image as a power of two (-1 to end): ");
            size = Convert.ToInt32(Console.ReadLine());
            while (size != -1)
            {
                Color[,] image1 = new Color[size, size];
                Color[,] image2 = new Color[size, size];

                // Checkerboard

                for (i = 0; i < size; i++)
                    for (j = 0; j < size; j++)
                        if ((i + j) % 2 == 0)
                        {
                            image1[i, j] = Color.WHITE;
                            image2[i, j] = Color.BLACK;
                        }
                        else
                        {
                            image1[i, j] = Color.BLACK;
                            image2[i, j] = Color.WHITE;
                        }

                P = new Quadtree(image1, size);
                Console.WriteLine("Original Image");
                PrintImage(image1, size);
                Console.WriteLine("Quadtree Image P");
                P.Print();

                Q = new Quadtree(image2, size, 2);
                Console.WriteLine("Original Image");
                PrintImage(image2, size);
                Console.WriteLine("Quadtree Image Q");
                Q.Print();

                R = P.Union(Q);
                Console.WriteLine("Resultant R = Union(P,Q)");
                R.Print();

                Console.WriteLine("Switch index[1,1]");
                P.Switch(1, 1);

                Console.WriteLine("Print Quadtree P");
                P.PrintQuadtree();
                Console.WriteLine("Print Image");
                P.Print();

                Console.Write("Enter size of image as a power of two (-1 to end): ");
                size = Convert.ToInt32(Console.ReadLine());
            }
        }
    }
}
