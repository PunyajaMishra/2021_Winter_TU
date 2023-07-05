#include<stdio.h>
#include<stdlib.h>
#include<time.h>

//generating random numbers from 0, 1 and 2
int GenerateRandomNumber(){
    srand(time(NULL));
    return rand() % 3; 
}

//ask  user for input
char GetUserSelection(){
    char userchoice;
    printf("\nEnter your choice : "); 
    scanf(" %c", &userchoice); //take user choice
    return toupper(userchoice); //convert to upper case letter for switch case 
}

char GetComputerSelection(){
   int random_number = GenerateRandomNumber(); //get random number generated

   char computerchoice;
   //assign the char to the random number
   if(random_number == 0) computerchoice = 'H';
   else if (random_number == 1) computerchoice = 'S';
   else computerchoice = 'A';

    return toupper(computerchoice); //convert to upper case letter for switch case 
}

//Function to compare the selections
int GetWinner(char user_choice, char computer_choice){
    int winner_number;
    //H->A ; S->H; A->S; ==> user wins : 1, tie = 0, computer wins = -1. 
    if(user_choice == 'H' && computer_choice == 'A' || user_choice == 'S' && computer_choice == 'H' || user_choice == 'A' && computer_choice == 'S') 
    {winner_number = 1;}
    else if(user_choice == computer_choice ) 
    {winner_number = 0;}
    else {winner_number = -1;}

    return winner_number;
}

int main(){
    char user_c = ' ';
    char comp_c = ' ';
    int winner ;

    printf("\n#########################################");
    //Time pass welcome timtle to make the game look good, eh. 
    printf("\n\nWelcome to the Asgard hammer, Sword, Armor game");
    //explain the rules of the game
    printf("\n\nThis is a simple game of luck. Choose your weapon of defense and if it can tolerate the opponent's weapon's choice, You WIN."); 
    printf("\n You have three options - Hammer, Sword, Armor. \n HAMMER defeats ARMOR\n ARMOR defeats SWORD\n SWORD defeats HAMMER\n To choose  Hammer, press H or h;\n To choose Armor, press A or a;\n To choose Sword, press S or s;\n To wuit the Game, press Q or q; "); 
    printf("\nLet's start the game!!!");

    //Loop for calculating the results
     do{
        user_c = GetUserSelection(); //asking user for input
        //switcj case for different user inputs
        switch(user_c){
            case 'Q' : //Quit
                printf("Have a nice day!");
                break;
            //Comparison and calculating
            case 'H' :
            case 'A' :
            case 'S' : 
                comp_c = GetComputerSelection(); //computer plays
                printf("\nYour choice : %c", user_c);
                printf("\nComputer choice : %c", comp_c);
        
                //calculating result calling the number
                winner = GetWinner(user_c, comp_c);
        
                if(winner == 0) { //Tie
                    printf("\nIt's a tie! Better luck next time\n");
                }
                else if(winner == 1){ //User wins
                    printf("\nCongratulations You WIN!\n");
                }
                else{ //Computer wins
                    printf("\nComputer wins! Sorry.\n");
                }
                break;
            default : //any wrong input
                printf("Wrong Input");
        }
    }while(user_c != 'Q'); //the loop goes on until the user enters Q/q to signal quitting the game

    return 0;

}