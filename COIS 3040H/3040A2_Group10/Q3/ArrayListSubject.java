//COIS 3040 Assignment 2
//Group 10 : Badrit Bin Imran, Punyaja Mishra, Gowri Nandana

import java.util.*;
//Subject class
//the OBSERVABLE ---> this notifies
public class ArrayListSubject {
	
	public Observer observer; //observer
	
	ArrayList<Object> objects; //make a new array list
	
	//constructor
	public ArrayListSubject() {
		//initialize the array list
		objects = new ArrayList<Object>();
	}
	
	//get the observer
	//register observer
	public void attach(Observer o) {
		observer = o;
	}
	
	//notify the observer
	public void notifyObserver() {
		//call the update method in the observer
		observer.update();
	}
	
	//append the object to the end of the array list
	public void append(Object obj) {
		//append using the add method
		objects.add(obj);
	}
	
	//remove the object from the array list
	public void delete(Object obj) {
		//unless the object exists in the array list. i can't delete
		//check if the array list does not comtain the object then print item not found
		if(!objects.contains(obj)) 
			System.out.println("\nItem was not found in array");
		//else delete and notify observer (call observer)
		else {
			//iterating through the array list
			for(int i = 0; i< objects.size() ; i++) {
				//if condition to check if object exists in array
				if(objects.get(i).equals(obj)) {
					objects.remove(i);
					notifyObserver(); //notify the observer that an item was deleted
				}
			}
		}
		
		
	}
	
}
