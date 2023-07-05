
#include<stdio.h>
#include<stdlib.h>


void Printa(){
    printf("Argument a called\n");
}

void PrintA(){
    printf("Argument A called\n");
}

void Printb(){
    printf("Argument b called\n");
}

void Printx(){
    printf("Argument x called\n");
}

void Printv(){
    printf("Argument v called\n");
}

void Printz(){
    printf("Argument z called\n");
}

void PrintR(){
    printf("Argument R called\n");
}


int i=1;
int ch;
int main(int argc, char *arguments[]){

    printf("This is program : %s\n", arguments[0]);

    if(argc < 2){
        printf("No argument was passed\n");
    }
    else{
        for(i; i < argc; i++){
            ch = (int)(*arguments[i]);
            printf("\n ch is : %d \n", ch);
            switch(ch){
                case 97:  //a
                    Printa();
                    break;

                case 65:   //A
                    PrintA();
                    break;

                case 98:   //b
                    Printb();
                    break;

                case 120:  //x
                    Printx();
                    break;

                case 118:  //v
                    Printv();
                    break;

                case 122:  //z
                    Printz();
                    break;

                case 82:   //R
                    PrintR();
                    break;

                default : 
                    printf("Not right Argument called \n");
                    break;
            }
        }

    }

    
    return 0;
}