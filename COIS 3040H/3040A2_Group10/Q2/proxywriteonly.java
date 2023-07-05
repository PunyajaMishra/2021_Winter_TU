//COIS 3040 Assignment 2
//Group 10 : Badrit Bin Imran, Punyaja Mishra, Gowri Nandana

//proxy class - WRITE ONLY
public class proxywriteonly implements Subject{
	
	Text writeonly;
	//the constructor
	//initialize object in the constructor
	public proxywriteonly(String content) {
		//the instance object of text created for this Proxy - this will be write only
		 writeonly = new Text(content);
	}
	
	//set the content write
	//use the initialized object to call the methods
	public void setContent(String content) {
		writeonly.setContent(content);
	}
	
	//return the error since it is write only and not read
	public String getContent() {
		return "Error : Write only, cannot access read due to protection level";
	}
} 