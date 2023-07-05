//COIS 3040 Assignment 2
//Group 10 : Badrit Bin Imran, Punyaja Mishra, Gowri Nandana

//the interface subject
public interface Subject {
	
	//the interface method that takes the server host to check the protection levels
	//first method set content that should work in write only and read/write

	 public void setContent(String content);

	 //second method that should work in read only and read/write
	 public String getContent();
}
