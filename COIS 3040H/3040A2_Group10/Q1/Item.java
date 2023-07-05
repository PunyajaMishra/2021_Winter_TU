//Item.java
//March 2021 COIS 3040 
//Badrit Bin Imran, Punyaja Mishra, Gowri Nandana

import java.util.ArrayList;
import java.util.List;

//Item class
public class Item extends ListComponent
{
	//Object fields
	int value;
	//Constructor
	public Item(int value) 
	{
		this.value = value;
	}
	//Print Value
	//Parameters: None
	//Return type: Void

	public void printValue()
	{
		//Printing the value
		System.out.print(value);
		System.out.print(" ");
	}
	// This is a leaf node so addChild is uninitialized		
	void addChild(int index, ListComponent child) {}
	
	// This is a leaf node so removeChild is uninitialized		
	void removeChild(int index){}
	
	// This is a leaf node so getChild returns null as well
	ListComponent getChild(int index) {return null;}
	
}
