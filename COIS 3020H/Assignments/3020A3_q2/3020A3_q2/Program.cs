using System;

namespace _3020A3_q2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //increment for for loop to insert in heap
            int i;
            //to generate random number for priority queueu
            Random r = new Random();
            //priority queuee
            PriorityClass pc = new PriorityClass(r.Next(50), (char)('a'));

            //lazy binomial heap implementation
            BinomialHeap<PriorityClass> BH = new BinomialHeap<PriorityClass>(pc);

            //insert function
            for (i = 0; i < 10; i++)
            {
                pc = new PriorityClass(r.Next(50), (char)('a'));
                Console.WriteLine(BH.Insert(pc).Item.ToString());
            }

            //prints the size
            Console.WriteLine("Size : "+ BH.Size());


            //checking remove function, front function and print function
            while (!BH.isEmpty())
            {
                Console.WriteLine("Front : " + BH.Front().ToString());
                BH.Remove();
                Console.WriteLine("Now printing");
                BH.print();
                Console.ReadLine();
            }
            Console.ReadLine();
        }
    }
}
