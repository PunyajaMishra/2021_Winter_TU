#include <stdio.h>
#include<stdlib.h>
#include<math.h>
int count =0;
int i=1;
int j=0;
int main(int argc, char *arguments[]){ 
    printf("\nThis is program : %s\n", arguments[0]);
 
 //no argument passed
 if(argc < 2){
 printf("No argument passed");
 }

 //odd number of arguments
 else if((argc+1)%2!=0){
 printf("Enter even number of numbers/arguments please");
 }

 //all of that is fine
 else{
 //initialize arrays
 int arraylength = (argc-1)/2;
 int a1[arraylength]; //array 1
 int a2[arraylength]; //array 2
 
 //fill in the arrays
 for(i; i< arraylength ; i++){
     //number of intergers passed, excluding the name
     int k = argc -1; 
     //if it reached half the number of integers passed -> then switch to array 2
     if(i <= k/2){
         //for example there were total 5 arguments ==> 4 numbers ==> first 2 goes in array 1
        a1[i-1] = atoi(arguments[i]);
     }
     else{
         //becuse now i is already half of the number of aguments passed +1
         a2[i - (k/2) - 1] = atoi(arguments[i]);
     }
 

 }
 
 //now printing the calculations of exponents
 for(j; j<arraylength; j++){
 double value = pow(a1[j], a2[j]);

printf("\narray 1 %d value = %d",j,a1[j]);
printf("\narray 1 %d value = %d",j,a2[j]);

 printf("\n index = %d",j);
 printf(" value = %f \n", value); 
 }
 
 }
 
 return 0;
}