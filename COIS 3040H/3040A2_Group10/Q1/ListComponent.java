//ListComponent.java
//March 2021 COIS 3040 
//Badrit Bin Imran, Punyaja Mishra, Gowri Nandana

import java.util.ArrayList;
import java.util.List;

public abstract class ListComponent {
ArrayList<ListComposite> list;
//This is an abstract class so constructor is not initialized
	public ListComponent() {};
	//Abstract methods
	abstract void printValue();	
	abstract void addChild(int index,ListComponent child);	
	abstract void removeChild(int index) ;	
	abstract ListComponent getChild(int index) ;
	
}
