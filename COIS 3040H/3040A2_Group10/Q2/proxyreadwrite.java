//COIS 3040 Assignment 2
//Group 10 : Badrit Bin Imran, Punyaja Mishra, Gowri Nandana

//proxy class - READ / WRITE
public class proxyreadwrite implements Subject{
	
	Text readwrite;
	//the constructor
	//create object in the constructor
	public proxyreadwrite(String content) {
		//the instance object of text created for this Proxy - this will be read and write both
		readwrite = new Text(content);
	}
	
	//set the content to the string since write is allowed
	//use the initialized object to call the methods
	public void setContent(String content) {
		readwrite.setContent(content);
	}
	
	//return the content since read is allowed
	//use the initialized object to call the methods
	public String getContent(){
		return readwrite.getContent();
	}
} 