//SJF -> Shortest Job First
//FIFO -> First in first out
//C program to implement the SJF scheduling algorithm

//Arrival Time = when the processes arrive - they are stored in an array

//Execution Time = time it needs to run for 

//Waiting Time = the time process needs to wait for 

//Turn Around Time = Waiting Time + Execution Time

//to make things easier, we are assuming that 
//all processes arrive at time t=0
//therefore, we sort the array of execution times in ascending order
//and then basicaly the same process as FIFO ::

//FIFO works on Arrival Time. The sooner you arrive, the sooner you arrive executed

//the arrival array is basically just array of processes - arrival time is same for all t=0; 
//but just to kind of show as an example for FIFO, I said the processes are stored in the order of their arrival

#include<stdio.h>

//finidng the waiting time for each processes
void waitingTime(int number, int execution_time[], int waiting_time[]){
    
    // the first process does not have any waiting time
    //it comes and starts running
    waiting_time[0] = 0;
    
    //calculation of waiting time for all other processes
    //the waiting time will be the execution time of the previous process
    //also, all processes after 2nd process will also have to wait for 
    //the previous processes to finish running
    //thus we add there execution times as well
    
    //we start loop with 1 because 0 has no waiting time
    int i=1;
    for(i; i<number; i++){
        waiting_time[i] = waiting_time[i-1] + execution_time[i-1];
    }
}

//finding the TT (turn around time) for each process
void turntime(int number, int execution_time[], int waiting_time[], int tt[]){
    //simple waiting time + execution time of THAT processes
    int i=0;
    for(i; i<number; i++){
        tt[i] = waiting_time[i] + execution_time[i];
    }
}

//finding ATT for each processes
//take the array of process arrival time
void ATT(int process[], int number, int execution_time[]){
    
    //arrays to send in the methds of finding waiting time and turn around time
    int waiting_time[number], tt[number]; 
    int total_wait_time = 0;
    int total_tt = 0;
    
    //find waiting times of all processes
    waitingTime(number, execution_time, waiting_time);
    //find turn around time of all processes
    turntime(number, execution_time, waiting_time, tt);
    
    //display all values using print function
    printf("Index\t Processes \tExecution Time \t Waiting Time \t Turn Around Time\n");
    int i=0;
    for(i; i<number; i++){
        total_wait_time += waiting_time[i]; //to calculate average wait time
        total_tt += tt[i]; //to calculate ATT
        printf("%d", (i+1)); //print index number
        printf("\t  %d", process[i]); //print process
        printf("\t\t  %d", execution_time[i]); //print execution time
        printf("\t\t\t %d", waiting_time[i]); //print waiting time
        printf("\t\t\t  %d", tt[i]); //print turn around time
        printf("\n");
    }
    
    //caluclate ATT
    float att = (float)total_tt/(float)number;
    printf("ATT =  %f\n", att);
    //calculate average waiting time
    float wt = (float)total_wait_time/(float)number;
    printf("Average Waiting Time =  %f\n", wt);
}

//sort method to sort in ascending order for SJF scheduling algorithm
void sort(int process[], int number, int execution_time[]){
    //now sorting the execution_time array in ascending order
    //also, sorting the processes array
    //using the sort algorithm
    int i=0; 
    int j;
    for(i; i<number - 1; i++){
        //temp position to be able to change the positions in processes
        int temp_position = i;
        int temp;
        
        for(j = i+1; j<number; j++){
            if(execution_time[j] < execution_time[temp_position]){
                temp_position = j;
            }
        }
            //exchange in execution time array
            temp = execution_time[i];
            execution_time[i] = execution_time[temp_position];
            execution_time[temp_position] = temp;
            
            //exchange in processes array
            temp = process[i];
            process[i] = process[temp_position];
            process[temp_position] = temp;
    }
}

//Main Method
int main(){
    //array storing processes
    int process[] = {1, 2, 3, 4, 5, 6};
    //the number of processes
    int number = sizeof process/sizeof process[0];
    //array storing all processes' execution time
    int execution_time[] = {10, 3, 6, 4, 2, 7};
    
    //for FIFO scheduling algorithm 
    printf("FIFO\n");
    ATT(process, number, execution_time);

    
    //sort for SJF
    sort(process, number, execution_time);
    //now the sorting has been done. 
    //This is like a simple FIFO algorithm now
    //so now for SJF scheduling algorithm
    printf("SJF\n");
    ATT(process, number, execution_time);

    return 0;
}
