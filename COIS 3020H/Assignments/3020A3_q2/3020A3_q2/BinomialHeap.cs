//I watched a couple of youTube videos on Lazy Implementation of Binomial Heap
//and used them as reference
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace _3020A3_q2
{
    //the binomial node is the NODE class. It is of Generic type. 
    public class BinomialNode<T>
    {
        public T Item { get; set; } //value
        public int Degree { get; set; }
        public BinomialNode<T> parent { get; set; }
        public BinomialNode<T> LeftMostChild { get; set; }
        public BinomialNode<T> RightSibling { get; set; }

        // Constructor

        public BinomialNode(T item)
        {
            this.Item = item;
            this.Degree = 0;
            this.parent = null;
            this.LeftMostChild = null;
            this.RightSibling = null;
        }
    }

    //--------------------------------------------------------------------------------------

   

    //--------------------------------------------------------------------------------------



    //--------------------------------------------------------------------------------------

    // Binomial Heap
    // Implementation:  Leftmost-child, right-sibling

    public class BinomialHeap<T>  where T : IComparable
    {
        private BinomialNode<T> head;  // Head of the root list
        private int size;              // Size of the binomial heap
        private BinomialNode<T> highRoot = null; //the node storing the min
        private T highest_value; //this helps in deleting the node so that the node minimum calls delete. 
        
        // Contructor
        public BinomialHeap(T highest_value)
        {
            head = new BinomialNode<T>(default(T));   // Header node
            size = 0;
            this.highest_value = highest_value;
        }

        //-------------------------------------------------------------------------------------- 
        //Check if the lazy binomial heap is empty
        //Returns true if size is 0 because that means it is empty
        public bool isEmpty()
        {
            return size == 0;
        }

        //-------------------------------------------------------------------------------------- 
        // Size
        // Returns the number of items in the binomial heap
        // Time complexity:  O(1)

        public int Size()
        {
            return size;
        }

        //-------------------------------------------------------------------------------------- 
        // MakeEmpty
        // Creates an empty binomial heap
        // Time complexity:  O(1)

        public void MakeEmpty()
        {
            head.RightSibling = null;
            size = 0;
        }


        //-------------------------------------------------------------------------------------- 
        //Insert
        // Inserts an item into the binomial heap
        //returns head

        public BinomialNode<T> Insert(T item)
        {
            BinomialNode<T> newRoot = new BinomialNode<T>(item);

            //LAZILY add item
            if (this.head != null) newRoot.RightSibling = this.head;
            this.head = newRoot;

            if (this.highRoot == null) this.highRoot = this.head;
            if (this.head.Item.CompareTo(this.highRoot.Item) < 0) this.highRoot = this.head;
            size += 1;
            return this.head;
        }

        //-------------------------------------------------------------------------------------- 
        // Remove
        // Removes the item from the binomial heap

        public BinomialNode<T> Remove()
        {
            BinomialNode<T> highest_root = this.removeHighestRoot();
            this.size -= 1; //we decrease the size by one because we are removing the hgihest priority one NODE

            //returning the deleted node
            if (highest_root != null) return highest_root;

            //if root has child then addd them
            if(highest_root.LeftMostChild != null)
            {
                //temp child
                BinomialNode<T> child = highest_root.LeftMostChild;
                //temp last child (lowest)
                BinomialNode<T> lastchild = null;

                //traverse and  change the child value
                while(child != null)
                {
                    lastchild = child;
                    child.parent = null;
                    child = child.RightSibling;
                }
                if(this.head != null)
                {
                    lastchild.RightSibling = this.head;
                }

                this.head = highest_root.LeftMostChild;
            }

            this.head = this.Coalesce();

            //if we remove highest root, re calculate the pointer to point to the highest root
            if (this.highRoot == highest_root) this.repointHigh();

            return highest_root; //return the highest root

        }

        //delete the node
        public BinomialNode<T> deleteNode(BinomialNode<T> node)
        {
            this.changevalue(node, this.highest_value);
            return this.Remove();
        }

        //private method called by the Remove method highest root remove
        private BinomialNode<T> removeHighestRoot()
        {
            //always check for null values
            if (this.head == null) return null;

            //making a temp value
            BinomialNode<T> cur = this.head;
            BinomialNode<T> previous = cur; //a previous pointer

            BinomialNode<T> high = cur;
            BinomialNode<T> prev_high = null;
            cur = cur.RightSibling;

            ///traverse through the heap using the temp
            while(cur.RightSibling != null && high.RightSibling!=null)
            {
                //a new variable to know whether the temp is higher priority
                bool isHigh = cur.Item.CompareTo(high.Item) > 0;
                if (isHigh)
                {
                    //change to higher priority
                    high = cur;
                    prev_high = previous;
                }
                previous = cur;
                cur = cur.RightSibling; //keep going next
            }

            //if highest priority is head then move heap.head pointer one root ahead
            if(previous == null || prev_high == null)
            {
                this.head = this.head.RightSibling;
            }
            else
            {
                //else we make the rpevious root with high's right root
                prev_high.RightSibling = high.RightSibling;
            }
            return high;
        }


        //change the value of given node to the new value
        public bool changevalue(BinomialNode<T> node, T newValue)
        {
            //false is returned when the item value is unable to be changed
            if (node.Item.CompareTo(newValue) > 0) return false;

            node.Item = newValue;

            //making temp node and parent
            BinomialNode<T> cur = node;
            BinomialNode<T> parent = cur.parent;

            //go through the heap and change the values respectively
            while(parent!=null && cur.Item.CompareTo(parent.Item) > 0)
            {
                T tempItem = parent.Item;
                parent.Item = cur.Item;
                cur.Item = tempItem;
                cur = parent;
                parent = cur.parent;
            }
            return true;
        }

        //re change the pointer to point to th ehighest root (highest priority)
        private void repointHigh()
        {
            if (this.head == null) return; //head is null so return
            BinomialNode<T> cur = this.head.RightSibling; //tmep value to traverse
            BinomialNode<T> high = this.head;

            while(cur != null)
            {
                //if the higher value is not set to highest priority then change it to point to that
                if(cur.Item.CompareTo(high.Item) > 0)
                {
                    cur = cur.RightSibling;
                }

                this.highRoot = high;
            }

        }

        //-------------------------------------------------------------------------------------- 
        // Front
        // Returns the item with the highest priority
        // Time complexity:  O(log n)

        public T Front()
        {
            //make sure we do not have a null head
            if (this.head == null) return default(T);

            return this.highRoot.Item;
        }

        //-------------------------------------------------------------------------------------- 
        //coalesce
        //the method to consolidate like in binomial heap
        public BinomialNode<T> Coalesce()
        {
            //first we sort the tree using the highest priority 
            //this helps in traversing easily
            BinomialNode<T>[] sortedTree = sortTree();
            BinomialNode<T>[] degreetree = null;
            for(int i=0; i<sortedTree.Length; i++)
            {
                degreetree[i] = sortedTree[i];

                //in case the nod eis empty, keep going
                if (degreetree == null) continue;
                int numberofdegreetree = degreetree.Length;
                while(numberofdegreetree >= 2)
                {
                    BinomialNode<T> treeA = degreetree[i];
                    BinomialNode<T> treeB = degreetree[i];

                    BinomialNode<T> linkedheap = (treeA.Item.CompareTo(treeB.Item) > 0) ? BinomialLinkTrees(treeA, treeB) : BinomialLinkTrees(treeB, treeA);

                    sortedTree[i + 1] = linkedheap;
                    numberofdegreetree -= 2;
                }
            }

            //temp values
            BinomialNode<T> cur = null;
            BinomialNode<T> head = null;
            BinomialNode<T>[] node = null;
            //reverse traverse
            for(int i=sortedTree.Length - 1; i>=0; i--)
            {
                node[i] = sortedTree[i];
                if (node.Length == 0) continue;

                BinomialNode<T> tree = node[0];

                if(cur == null) { cur = tree;
                    head = null;
                }
                else
                {
                    cur.RightSibling = tree;
                    cur = cur.RightSibling;
                }

                
            }
            return head;
        }

        //the binomial link to link trees - the main part of being binomial heap (even for lazy)
        private BinomialNode<T> BinomialLinkTrees(BinomialNode<T> TreeA, BinomialNode<T> TreeB)
        {
            TreeA.parent = TreeA;
            TreeB.RightSibling = TreeA.LeftMostChild;

            TreeA.LeftMostChild = TreeB;
            TreeA.Degree += 1;

            TreeA.RightSibling = null;

            return TreeA;
        }

        //sort the tree
        private BinomialNode<T>[] sortTree()
        {
            BinomialNode<T>[] sortedtree = new BinomialNode<T>[this.size + 1];

            //initiliaze the nodes in sorted trees array
            for(int i=0; i<sortedtree.Length; i++)
                sortedtree[i] = null;
            //temp cur to  traverse and set values to array
            BinomialNode<T> cur = this.head;
            //distribute the heaps into nodes
            while(cur != null)
            {
                //setting all values 
                BinomialNode<T> nextcur = cur.RightSibling;
                cur.RightSibling = null;
                int degree = cur.Degree;
                //set on array
                sortedtree[degree] = cur;
                //next pointer
                cur = nextcur;
            }

            return sortedtree;
        }
        //--------------------------------------------------------------------------------------
        //print funciton to print the heap
        public void print()
        {
            BinomialNode<T> cur = this.head;
            while (cur!=null && cur.RightSibling != null)
            {
                Console.WriteLine(cur.Item.ToString());
                try
                {
                    cur = cur.LeftMostChild;
                }
                catch (Exception e1)
                {
                    return;
                }
                
                try
                {
                    cur = cur.RightSibling;
                }
                catch(Exception e)
                {
                    return;
                }
                
            }
        }
    }

    //--------------------------------------------------------------------------------------

    // Used by class BinomailHeap<T>
    // Implements IComparable and overrides ToString (from Object)

    public class PriorityClass : IComparable
    {
        private int priorityValue;
        private char letter;

        public PriorityClass(int priority, char letter)
        {
            this.letter = letter;
            priorityValue = priority;
        }

        public int CompareTo(Object obj)
        {
            PriorityClass other = (PriorityClass)obj;   // Explicit cast
            return priorityValue - other.priorityValue;  // High values have higher priority
        }

        public override string ToString()
        {
            return letter.ToString() + " with priority " + priorityValue;
        }
    }


}

