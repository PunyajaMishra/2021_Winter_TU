//Description: 
//Write a C program that will output the sum, and the product of integer command line arguments.


#include <stdio.h>

//incrementor variable, sum variable to store sum, product variable to store product

int i=1;
int sum = 0;
int product = 1;

//..main
int main(int argc, char *arguments[]){

    //printing the program name - first argument
    printf("\nThis is program : %s\n", arguments[0]);

    //array of integers - the numbers passed as arguments

    //no arguments were passed
    if(argc < 2){
        printf("No argument was passed\n");
    }
    
    //arguments were passed
    else{
        //loop through the arguments array
        for(i; i < argc; i++){
            //using atoi convert the string arguments into integer 

            //calculating the sum
            sum += atoi(arguments[i]);
            
            //calculating the product
            product *= atoi(arguments[i]);

        }

        
            //print the sum and product
            printf("The Sum of numbers : %d \n", sum);
            printf("The Product of numbers : %d \n", product);

    }
    return 0;
}