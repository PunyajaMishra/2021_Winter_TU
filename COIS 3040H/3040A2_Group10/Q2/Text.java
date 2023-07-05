//COIS 3040 Assignment 2
//Group 10 : Badrit Bin Imran, Punyaja Mishra, Gowri Nandana

//realSubject
public class Text implements Subject{
	
	private String content; //private property for protection level
	//this is the string content that will be set
	
	//constructor is called
	public Text(String sample_content) {
		//just an empty constructor 
		//we do not need the constructor for anything right now for this program
		//but for the READ ONLY proxy : since we are not able to write anything on it
		//so this takes a sample content which it sets to the content
		this.content = sample_content;
	}
	
	//set the content
	public void setContent(String newContent) {
		this.content = newContent; //setting the content to the new content
	}
	
	//get the content
	public String getContent() {
		return content;
	}
}
