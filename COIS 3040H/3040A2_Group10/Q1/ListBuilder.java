//ListBuilder.java
//March 2021 COIS 3040 
//Badrit Bin Imran, Punyaja Mishra, Gowri Nandana

import java.util.ArrayList;
import java.util.List;

class ListBuilder
	{
	//ListBuilder fields
	private boolean bracket_open;
	private ListComponent listComponent;	//This is returned
	private ListComposite listComposite;	//This is not returned it is used for adding
											//ListComposite objects with multiple item children
	private int childIndex;
	//Constructor
	public ListBuilder() 
	{
		this.listComponent = new ListComposite();
		this.bracket_open = false;
		this.childIndex =0;
	}
	//Build Element
	//Parameters: int element
	//Return Type: void
	public void BuildElement(int element)
	{
		//Initializing a new item with given value
		ListComponent e = new Item(element);
		//If bracket is not open we add ListComposite object with single item
		if(!this.bracket_open) 
		{
			this.listComposite = new ListComposite(e); //ListComposite object
			listComponent.addChild(childIndex, listComposite);	//Adding child
			this.childIndex++; //Incrementing index
		}	
		else 
		//Else we just add the Item to the nested ListComposite object
		//It is later added to the main ListComposite when we close brackets
		{
			listComposite.addChild(listComposite.list.size(),e);
		}		
	}
	//BuildOpenBracket
	//Parameters:None
	//Return Type: Void
	public void buildOpenBracket()
	{
		this.bracket_open = true; //Opening a bracket
		this.listComposite = new ListComposite(); //Iniatiling a ListComposite which can hold multiple objects
	}
	//BuildCloseBracket
	//Parameters:None
	//Return Type: Void
    public void buildCloseBracket() 
    {
    	listComponent.addChild(childIndex, listComposite);	 //Adding of the ListComposite which can hold multiple objects
    	this.childIndex++; //Incrementing the index
    	this.bracket_open = false; //Closing the bracket
    }
    //ListComponent
  	//Parameters: None
  	//Return Type: ListComponent
    public ListComponent getList() 
    {
    	return this.listComponent;
    }
	
}