//ListComposite.java
//March 2021 COIS 3040 
//Badrit Bin Imran, Punyaja Mishra, Gowri Nandana

import java.util.ArrayList;
import java.util.List;
public class ListComposite extends ListComponent{
	//Fields
	ArrayList<ListComponent> list;
	//Constructor
	public ListComposite()
	{
		this.list = new ArrayList<ListComponent>();		
	}
	//Constructor II 
	public ListComposite(ListComponent child)
	{
		//Insantiating a new ArrayList
		//Adding the item to the first index
		this.list = new ArrayList<ListComponent>();	
		this.list.add(0,child);
	}
	//AddChild
	//Paramters: int index ,Item child
	//Return Type: void
	@Override
	void addChild(int index, ListComponent child) 
	{
		this.list.add(index ,child);		
	}
	
	//PrintValue
	//Paramters: None
	//Return Type: Void
	//This method prints out other nested ListComponent type objects
	public void printValue()
	{
		if(list.size()!=1)System.out.print("[ "); //Printing Brackets for Objects with more than one child
		//For loop to loop through all ListComponent Type objects
		for (ListComponent e : list)  
        { 
          e.printValue();
        }
		if(list.size()!=1)System.out.print("] "); //Printing Brackets for Objects with more than one child
	}
	
	@Override
	void removeChild(int index) {
		// TODO Auto-generated method stub
		this.list.remove(index);
		
	}
	@Override
	ListComponent getChild(int index) {
		// TODO Auto-generated method stub
		return this.list.get(index);
	}
	
}
