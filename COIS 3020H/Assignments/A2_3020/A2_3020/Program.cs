using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;
//biggerstring_trying_makingrope_printing_post_traversal
namespace A2_3020
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int input; //switch case for choosing the test case
            int index; //split at index, return character at index
            int start, end; //for finding substring and delete substring
            string str_rope; //string to make rope

            Rope rope;
            Rope empty_rope;

            //Empty rope :
            empty_rope = new Rope(); //empty rope
            Console.WriteLine("\nEmpty Rope Created : \n");
            empty_rope.PrintRope();

            //Rope with string created
            //ask for string
            Console.WriteLine("\nEnter string to create Rope : \n");
            str_rope = Console.ReadLine();
            //create rope
            rope = new Rope(str_rope);
            //print rope
            Console.WriteLine("\nRope created with string : \n");
            rope.PrintRope();

            Console.WriteLine("\n########## Assignment 2 Rope ###########");

            //multiple options for switch case
            Console.WriteLine("\nChoose a number for various method actions (Print Method is being tested in literally every step :) : ");
            Console.WriteLine("" +
                "\n 1. Concatenate 2 ropes with 3 conditions - first rope null, second rope null, no ropes null" +
                "\n 2. Split rope at index i " +
                "\n 3. Insert string S entered by user at index i, at end and at begining " +
                "\n 4. Delete a substring between 2 indices" +
                "\n 5. Substring method to get substring in between 2 indices " +
                "\n 6. Return character at index i charat" +
                "\n 7. indexof method returns first index of character" +
                "\n 8. Reverse string " +
                "\n 9. Length of string" + 
                "\n 10. Return string represented by current rope "  +
                "");
            input = Convert.ToInt32(Console.ReadLine());

            do
            {
                switch (input)
                {
                    //concatenate method
                    case 1:
                        Console.WriteLine("\n ############# Concatenate Method ###########\n");
                        Console.WriteLine("\nThree options : \n");
                        Console.WriteLine("\nWe will use the empty rope we created in the begining\n");

                        //empty rope is the first rope passed into concatenate
                        Console.WriteLine("\nPassing empty rope as first rope\n");
                        Rope newrope = empty_rope.Concatenate(empty_rope, rope);
                        newrope.PrintRope();

                        //empty rope is the second rope passed into concatenate
                        Console.WriteLine("\nPassing empty rope as second rope\n");
                        Rope newrope1 = empty_rope.Concatenate(rope, empty_rope);
                        newrope1.PrintRope();

                        //creating a new rope for concatenation
                        Console.WriteLine("\nCreating new rope, passed the string " +
                            " 'this_is_thenewstring_i_passed_to_makea_new_rope_lala_for_lala_concatenation': \n");
                        string s = "this_is_thenewstring_i_passed_to_makea_new_rope_lala_for_lala_concatenation";
                        Rope rope_concatenate = new Rope(s); //new rope created
                        rope_concatenate.PrintRope();

                        Console.WriteLine("Concatenation : \n");
                        Rope R = rope.Concatenate(rope, rope_concatenate); //the new rope that will store new concatenated rope
                        R.PrintRope();
                        break;

                    case 2:
                        //split method
                        Console.WriteLine("\n ############# Split Method ###########\n");

                        //ask for the input from the user to split at
                        Console.WriteLine("\n Enter the index you want to split at \n");
                        index = Convert.ToInt32(Console.ReadLine());

                        //call the split method
                        Rope R1 = new Rope();
                        Rope R2 = new Rope();
                        rope.Split(index, ref R1, ref R2);

                        //print the ropes
                        R1.PrintRope();
                        R2.PrintRope();

                        break;
                        
                    case 3:
                        //insert method
                        Console.WriteLine("\n ############# Insert Method ###########\n");
                        
                        //asking for index to be inserted
                        Console.WriteLine("\n Enter the index you want to enter at \n");
                        index = Convert.ToInt32(Console.ReadLine());
                        
                        //asking for the string to be inserted
                        Console.WriteLine("\nEnter the string to be inserted \n");
                        string ss = Console.ReadLine();

                        //insert method
                        rope.Insert(ss, index);

                        break;

                    case 4:
                        //delete method
                        Console.WriteLine("\n ############# Delete Method ###########\n");

                        //asking for start and end index
                        do
                        {
                            Console.WriteLine("\nEnter the start index for substring range to be deleted : \n");
                            start = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("\nEnter the end index for substring range to be deleted : \n");
                            end = Convert.ToInt32(Console.ReadLine());

                            //print error for out of range index
                            if (start < 0 || end > str_rope.Length || end == start)
                            {
                                Console.WriteLine("\nInvalid indices.\n");
                            }

                        } while (start < 0 || end > str_rope.Length || end == start);
                        
                        if (end < start)
                        {
                            end += start;
                        }

                        //delete the siubstring and print the new rope -- 
                        //rebalancing is also called : ///////////////////////////ONE OF THE OPTIMIZATIONS
                        rope.Delete(start, end);
                        rope.PrintRope();

                        break;

                    case 5:
                        //substring method
                        Console.WriteLine("\n ############# Substring Method ###########\n");

                        //asking for start and end index
                        do
                        {
                            Console.WriteLine("\nEnter the start index for substring range : \n");
                            start = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("\nEnter the end index for substring range : \n");
                            end = Convert.ToInt32(Console.ReadLine());

                            //print error for out of range index
                            if (start < 0 || end > str_rope.Length)
                            {
                                Console.WriteLine("\nInvalid indices.\n");
                            }

                        } while (start < 0 || end > str_rope.Length);

                        //get the substring of that range
                        Console.WriteLine(rope.Substring(start, end));

                        break;

                    case 6:
                        //return character charat method
                        Console.WriteLine("\n ############# CharAt Method ###########\n");

                        //ask for the index for char at method 
                        do
                        {
                            Console.WriteLine("\nEnter the index you want to know the character of : \n");
                            index = Convert.ToInt32(Console.ReadLine());
                            
                            //print error for out of range index
                            if(index<0 || index > str_rope.Length)
                            {
                                Console.WriteLine("\nInvalid index.\n");
                            }

                        } while (index < 0 || index > str_rope.Length);

                        //call charat
                        Console.WriteLine(rope.CharAt(index));

                        break;

                    case 7:
                        //index of method
                        Console.WriteLine("\n ############# Index Method ###########\n");
                        char character;
                        //ask for the index for index of method 
                        Console.WriteLine("\nEnter the char whose index you want to know : \n");
                        character = Convert.ToChar(Console.ReadLine());

                        //call indexof

                        //if the return integer is equal to the length of string that means, the character was never found, and the index kept incrementing till the end 
                        //(hence, it will be exactly equal to the length of the string)
                        //Note : this is not true for if a character was at the end because that will be at index length-1
                        if (rope.indexof(character) == str_rope.Length) Console.WriteLine("\nCharacter does not exist in the string in rope ");
                        //else print the index
                        else Console.WriteLine(rope.indexof(character));
                        break;

                    case 8:
                        //reverse method
                        Console.WriteLine("\n ############# Reverse Method ###########\n");
                        //calling the reverse string method
                        rope.Reverse();
                        rope.PrintRope();
                        break;

                    case 9:
                        //length of string method
                        Console.WriteLine("\n ############# Length Method ###########\n");
                        //calling the length method
                        Console.WriteLine(rope.Length());
                        break;

                    case 10:
                        //return the string in the rope
                        Console.WriteLine("\n ############# ToString Method ###########\n");
                        Console.WriteLine("\nThe string of the rope is : \n");
                        Console.WriteLine(rope.ToString());
                        break;

                    default:
                        Console.WriteLine("\nError Input");
                        break;
                }
            } while (input < 1 || input > 13);
        }
    }
}
