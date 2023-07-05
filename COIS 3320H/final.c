
#include<stdio.h>
#include<stdlib.h>

//addition method that will add and print the addition of the 2 numbers
void Addition(int a, int b){
    //add varibale stores the addition
    int add = a + b;
    //print the result
    printf("\n%d + %d is %d \n\n", a, b, add);
}

//subtraction method that will subtract and print the subtraction of the 2 numbers
void Subtraction(int a, int b){
    //sub varibale stores the subtraction
    int sub = a - b;
    printf("\n%d - %d is %d \n\n", a, b, sub);
}

//multiplication methos that will multiply and print the product of the 2 numbers
void Multiplication(int a, int b){
    //mul varibale stores the multiplication
    int mul = a * b;
    //print the result
    printf("\n%d multiplied by %d is %d \n\n", a, b, mul);
}

//division method that will divide and print the quotient of the 2 numbers
void Division(int a, int b){
    //div varibale stores the division
    double div = (double)(a/b);
    //print the result
    printf("\n%d divided by %d is %f \n\n", a, b, div);
}


int i=1;
int ch;
int main(int argc, char *arguments[]){

    //prints the first argument that is the program name
    printf("\nThis is program : %s\n", arguments[0]);

    if(argc < 2){
        //no arguments were passed
        printf("\nNo argument was passed\n\n");
    }
    else if(argc != 4){
        //too many or too less arguments were passed
        printf("\nNot right number of arguments passed. Only needed 3\n\n");
    }
    else{
        //we got all the right number of arguments

        //get the character to decide the operation
            ch = (int)(*arguments[i]);

            //store the 2 integers passd and print them 
            int a = atoi(arguments[2]);
            int b = atoi(arguments[3]);

            printf("Integer 1 : %d\n", a);
            printf("Integer 2 : %d\n", b);

            //switch case to decide which method to call
            switch(ch){
                case 'd':  //division
                    Division(a,b);
                    break;

                case 'm':   //multiplication
                    Multiplication(a,b);
                    break;

                case 'a':   //addition
                    Addition(a,b);
                    break;

                case 's':  //subtraction
                    Subtraction(a,b);
                    break;

                default : 
                //wrong character passed
                    printf("\nNot right Argument called \n\n");
                    break;
            }
    }
    return 0;
}