//COIS 3040 Assignment 2
//Group 10 : Badrit Bin Imran, Punyaja Mishra, Gowri Nandana

import java.util.Scanner;

public class ObserverDemo {
	//main class
	public static void main(String[] args) {
		
		//create a new array list subject  object
		ArrayListSubject sub = new ArrayListSubject();
		
		//instantiate the observer
		new DeleteObjectObserver(sub);
		
		//Scanner to take string input
		Scanner input = new Scanner(System.in);
		
		//Asking user to enter 5 strings to append into the array
		//we know this could have been hard-coded but we were like why not
		for(int i=0; i< 5 ; i++ ) {
			System.out.println("\nEnter a string to be appended: ");
			//append the string 
			sub.append(input.nextLine());
		}
		
		//ask user for string to be deleted
		System.out.println("\nEnter a string to be deleted: ");
		//delete the string
		sub.delete(input.nextLine());
	}
}
