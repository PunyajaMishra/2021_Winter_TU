//COIS 3040 Assignment 2
//Group 10 : Badrit Bin Imran, Punyaja Mishra, Gowri Nandana

//proxy class - READ ONLY
public class proxyreadonly implements Subject{
	
	Text readonly;
	//the constructor
	//create object in the constructor
	//the constructor could have been a no parameter constructor but then it just prints null
	//because the content is never set to anything. 
	public proxyreadonly(String content) {
		//the instance object of text created for this Proxy - this will be read only
		readonly = new Text(content);
	}
	
	//print the error saying we cannot set content
	public void setContent(String content) {
		System.out.println("Error : Read only class, no changes can be made to content");
	}
	
	//return the content : read
	//use the initialized object to call the methods
	public String getContent() {
		return readonly.getContent();
	}
} 