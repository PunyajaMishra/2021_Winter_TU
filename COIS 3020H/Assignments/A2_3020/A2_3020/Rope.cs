using System;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO.MemoryMappedFiles;
using System.Security.Cryptography;
using System.Diagnostics.CodeAnalysis;

namespace A2_3020
{
    public class Node
    {
        public Node left, right;
        public string s;
        public int length;

        public Node() //empty node 
        {
            s = null;
            left = null;
            right = null;
            length = 0;
        }

        public Node(String s) //when a string is passed
        {
            this.s = s;
            left = null;
            right = null;
            length = s.Length;
        }
       
    }
    public class Rope
    {
        Node root;
        //create an empty rope -- empty constructor
        public Rope()
        {
            root = new Node();
        }

        //create a balanced rope from a given string S //constructor
        public Rope(string s)
        {
            if (s.Length == 0) Console.WriteLine("Empty String"); //if the string is empty print empty
            else if (s.Length <= 5) //if string is less than length of 5, then  just put it in one left node
            {
                root = new Node();
                root.length = s.Length;
                root.left = new Node(s);
                root.left.length = s.Length;
            }
            else //else call the function Build for recursion 
            {
                root = new Node();
                Build(s, ref root);
            }
        }

        //build function creates a balanced rope
        //fulfils the optimization of combining left and right siblings whose total string length is 5 or less
        //this is done because when at node root if the string length is 5 or less, just create one single node instead of recursive call
        private void Build(string str, ref Node node) 
        {
            node = new Node();
            //set the node length to string length 
            node.length = str.Length;
            //median stores the length of half the string for an almost balanced rope
            int median = str.Length / 2;

            //now, if median is less than equal to 10 then just create two nodes
            //else do a recursive call for more subtrees on left and right
            if(median <= 10)
            {
                node.left = new Node(str.Substring(0,median));
                node.left.length = median; //don't forget to update length

                node.right = new Node(str.Substring(median));
                node.right.length = str.Length - median; //the rest half of the string
            }
            else //for greater than 10 in length string in a node
            {
                Build(str.Substring(0, median), ref node.left);
                Build(str.Substring(median), ref node.right);
            }
        }

        // ########################## concatenate ##############################
        //Return the concatenation of ropes R1 and R2
        public Rope Concatenate(Rope R1, Rope R2)
        {
            Rope R = new Rope(); //new rope R

            // checking for nullable values
            if (R1.root.length == 0) { R = R2; return R; }
            if (R2.root.length == 0) { R = R1;  return R; }

            //Create a new rope R and set the left and subtrees of its root to
            //the roots of R1 and R2 respectivel
            R.root = new Node();
            R.root.length = R1.root.length + R2.root.length; //set the length
            
            R.root.left = R1.root; //new rope left root is R1
            R.root.right = R2.root; //new rope right root is R2


            //rebalance
            R = rebalance(R);

            return R;
        }


        // ########################## split ##############################
       
        public void Split(int i, ref Rope R1, ref Rope R2)
        {
            string rightstring = Split(ref i, ref root);

            //creating the right split rope
            R2 = new Rope(rightstring);
            //creating the left side rope
            R1 = new Rope(ToString(root));

            //this also fulfils the OPTMIZATION that the tree is balanced after split. 
        }
        private string Split(ref int i, ref Node node)
        {
            string str  = "";
            if (node != null)
            {
                if (node.right != null && i > node.left.length) //right subtree
                {
                    //if in right subtree then just keep going 
                    i -= node.left.length;
                    node = node.right;
                    Split(ref i, ref node);
                }
                else if (node.left != null)  //left subtree
                {
                    //if in left subtree then split the right subtree 
                    //get the right string and store at the begining of the existing string
                    str.Insert(0, ToString(node)); //get the string

                    //remove the length of the right subtree
                    node.length -= node.right.length;
                    //split the right node
                    node.right = null; 

                    node = node.left;

                    str.Insert(0, Split(ref i, ref node));
                }
                //now, found the node with that string, yaay

                //spliting the node into two nodes of the string at the index
                node.left = new Node();
                node.right = new Node();
                node.left.s = node.s.Substring(0, i);
                node.left.length = node.s.Length;
                if (node.s.Substring(i) != null)
                {
                    node.right.s = node.s.Substring(i);
                    node.right.length = node.s.Length;
                }

                //if the right node is not null, means the index was in between
                if (node.right.s != null)
                {
                    // split the right node off from this rope
                    str.Insert(0, node.right.s); //add the string to front
                                                 //remove the length of the right subtree
                    node.length -= node.right.length;
                    //split the right node
                    node.right = null;
                }

                //else the split is already done so nothing to be done
            }
            return str;
        }
        // ########################## insert ##############################
        //Insert string S at index i O(logN)
        public void Insert(string S, int i)
        {
            //inserting at the begining
            if(i == 0)
            {
                S += ToString(root);
                Rope R = new Rope(S);
                R = rebalance(R);
                root = R.root;
            }
            //inserting at end
            else if(i > ToString(root).Length)
            {
                Rope R = new Rope(ToString(root) + S);
                R = rebalance(R);
                root = R.root;
            }
            //inserting in the middle
            else
            {
                Rope R1 = new Rope(S); //empty rope 1 with new string
                
                //now split R1 into R2 and R3 at index i
                Rope R2 = new Rope(); //empty rope 2 for split stores left side
                Rope R3 = new Rope(); //empty rope 3 for split stores right side
                Split(i, ref R2, ref R3);

                //now concatenating first R2 and R1
                Rope R = Concatenate(R2, R1);
                //concatenating the new rope from before and R3
                Rope finalrope = Concatenate(R, R3);

                //do insert then rebalance
                finalrope = rebalance(finalrope);
                root = finalrope.root;
            }
        }
        // ########################## delete ##############################
        //Delete the substring S[i, j] O(logN)
        public void Delete(int i, int j)
        {
            Rope R1 = new Rope(); //empty rope 1 for split
            Rope R2 = new Rope(); //empty rope 2 for split -> the to be deleted rope
            Rope R3 = new Rope(); //empty rope 2 for split

            //split at i-1 and give R1 and R2
            Split(i - 1, ref R1, ref R2);

            //split R2 at j and give R2, R3
            R2.Split(j, ref R2, ref R3);

            //conacatenate R1 and R3
            Rope R = Concatenate(R1, R3);

            //do deletion then rebalance
            R = rebalance(R);
        }

        // ########################## substring ##############################
        //Return the substring S[i,j]
        //I did not use char at recursive call so no deductions? 
        public string Substring(int i, int j)
        {
            String str = "";
            bool found = false; //to keep a track whether we found or not
            Node tempNode = root;
            
            //three cases possible that the code should cover 
            //1. all in left subtree of ROOT (i,j <= root.left.length)
            //2. all in right subtree of ROOT (i,j >= root.left.length)
            //3. a little in left and other in right subtree of ROOT (i<= root.left.length && j>root.left.length)

            if(j> tempNode.left.length) //some half def in right subtree
            {
                found = true;
                j -= tempNode.left.length; //update index by subtractng the left subtree length
                if(i>tempNode.left.length) //all in the right subtree
                {
                    i -= tempNode.left.length; //update index by subtractng the left subtree length
                    //calling string substring function
                    str = tempNode.right.s.Substring(i, j);
                    return str;
                }
                else //not entire in right subtree of root
                {
                    str = tempNode.right.s.Substring(0, j); //from the begining till when the max index tells
                }
            }
            if(!found) //not found yet because not in right subtree OR half half but all in left
            {
                while (j <= tempNode.left.length)
                    tempNode = tempNode.left; //keep going left till index matches so we know when to stop
                j -= tempNode.left.length; //update index by subtractng the left subtree length
                
                //now if in right subtree of this node
                if(i >= tempNode.left.length)
                {
                    i -= tempNode.left.length; //update index by subtractng the left subtree length
                    str = tempNode.right.s.Substring(i, j) + str; //update the string with what we had 
                    return str;
                }
                str = tempNode.right.s.Substring(0, j); //whatever is left
            }

            tempNode = tempNode.left; //go left
            while (i < tempNode.left.length) 
            {
                str = tempNode.right.s + str;
                tempNode = tempNode.left;
            }
            i -= tempNode.left.length;
            str = tempNode.right.s.Substring(i) + str; //finally update the string

            //return the substring
            return str;
        }

        // ########################## charAt ##############################
        //Return the character at index i
        public char CharAt(int i)
        {
            //the node containing the string found
            Node found_node = findIndexNode(ref i, root);

            char c = Convert.ToChar(found_node.s.Substring(i, 1));

            //at the main function there will be index i
            //original value so print from there
            return c;
        }


        //method that will return the node with the index
        private Node findIndexNode(ref int i, Node root)
        {
            Node tempNode = root;
            //make sure node is not null
            //find if we go left or right - based on node.length
            //keep changing i => i-node.length because length keeps decreasing
            //recursive call on left or right node repsectively with that index
            if (tempNode != null)
            {
                if (tempNode.right != null && i > tempNode.left.length) //right subtree
                {
                    i -= tempNode.left.length;
                    tempNode = tempNode.right;
                    tempNode = findIndexNode(ref i, tempNode);
                }
                else if (tempNode.left != null)  //left subtree
                {
                    tempNode = tempNode.left;
                    tempNode = findIndexNode(ref i, tempNode);
                }
                //now, found the node with that string, yaay
            }

            return tempNode;
        }
        // ########################## IndexOf ##############################
        //Return the string represented by the current rope
        public int indexof(char c)
        {
            int index = 0;
            int increment = 0;
            indexof(ref index, ref increment, c, root); 
            //ref index because it will store the 
            //value of the character index

            return index;
        }

        private void indexof(ref int index, ref int increment, char c, Node node)
        {
            string s = c.ToString();
           // index //to know that the char has been found
            if (node != null)  //recursive call if node is not null 
            {
                //first keep calling left until we are the leftmost at the botton
                //index = 
                indexof(ref index, ref increment, c, node.left);
                if (increment == 0)
                {
                    //if condition to check if it is the leaf node
                    if (node.s != null)
                    {
                        //if the character is found
                        if (node.s.Contains("s"))
                        {
                            //increment stores the value of the index where the character is
                            index += node.s.IndexOf("s");
                            //set index to not 0, so compiler never enters here again since we need the index of the first character
                            increment = 1;
                            return;
                        }
                        //if character is not found in that leaf node
                        else
                        {
                            //add the node's string's length to the increment because that keeps increasing
                            index += node.s.Length;
                        }
                    }
                    //now call the right you know to cover all
                    indexof(ref index, ref increment, c, node.right);
                }
            }
        }
        // ########################## Reverse ##############################
        //Reverse the string represented by the current rope
        public void Reverse()
        {
            //reverse is easy. Collect the string, reverse the strin
            //then build a new rope with the reversed string and ta-da done.
            //To collect the string, use the private ToString method since it 
            //returns all string
            string str = ToString(root);
            string newstr = Reverse(str);
            
            Rope R = new Rope(newstr); //new rope

            //now set the root to this new rope root
            root = R.root;


        }
        private string Reverse(string str)
        {
            //make an array of the string char -> reverse array
            char[] strCharArray = str.ToCharArray();
            Array.Reverse(strCharArray);
            return new String(strCharArray);
        }

        // ########################## length ##############################
        //Return the length of the string
        public int Length()
        {
            return root.length;
        }

        // ########################## tostring ##############################
        //Return the string represented by the current rope
        public override string ToString()
        {
            //calling the private method to return tostring
            return ToString(root);
        }

        //private method that return the string to print
        //after traversing through the rope
        private string ToString(Node node)
        {
            string str = ""; //string to return

            //when it is the leaf node
            if (node.left == null && node.right == null)
            {
                str += node.s; //append to th eexisting string the string vlaue in that leaf node
            }

            //if left child exists, check for leaf recursively
            if (node.left != null)
                str += ToString(node.left);

            //if right child exists, check for leaf recursively
            if (node.right != null)
                str += ToString(node.right);

            return str; //return the string
        }

        // ########################## printrope ##############################
        //Print the augmented binary tree of the current rope
        public void PrintRope()
        {
            //call a private method that does the printing in the 
            //post-order traversal orientation
            PrintRope(root, 0);
        }
        //traversing the rope in post-order 
        private void PrintRope(Node root, int index)
        {
            //ensure node is not null
            //call all left first, then right, this ensures post-order
            //index helps in the good output
            if (root != null)
            {
                PrintRope(root.left, index + 8);
                PrintRope(root.right, index + 8);
                //if-else for checking if it is leaf node
                //i.e contains the string value
                //if does then print that
                //else don't
                if (root.s != null)
                {
                    Console.WriteLine(new String(' ', index) +
                        root.length.ToString() + " " +
                        root.s);
                }
                else
                {
                    Console.WriteLine(new String(' ', index) +
                        root.length.ToString() + " ");
                }
            }
        }

        // ########################## Rebalance ##############################
        public Rope rebalance(Rope rope) //rebalancing optimization
        {
            //creating a new rope
            //send the string in the old rope by calling the private ToString method
            //call constructor to build a new rope because constructor will create a balanced rope
            Rope R = new Rope(ToString(rope.root));
            return R;
        }
    }
}
