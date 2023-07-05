//COIS 3040 Assignment 2
//Group 10 : Badrit Bin Imran, Punyaja Mishra, Gowri Nandana

//Client class
public class Client {
	//main method
	public static void main(String[] args) {
		
		//the string we will write
		String write = "Hello check, write only";
		
		//First write only to avoid null exception
		System.out.println("\n \n #### WRITE ONLY ####");
		//create write only proxy object and send the string for content
		Subject writeonly = new proxywriteonly(write);
		//setting the content
		writeonly.setContent(write); 
		//trying to access read function - get content method but shows error
		System.out.println("\n Trying to read : ");
		System.out.println(writeonly.getContent()); //trying to get content but getting error instead
		

		//Now checking read only proxy
		System.out.println("\n \n #### READ ONLY ####");
		//create read only proxy object and send the string for content
		Subject readonly = new proxyreadonly(write);
		System.out.println("\n Trying to write : ");
		//trying to access write function - setcontent method but shows error
		readonly.setContent(write); 
		//trying to get content - trying to READ
		System.out.println(readonly.getContent()); 
		
		
		///now doing read and write proxy
		System.out.println("\n \n #### READ AND WRITE ####");
		//create read/write proxy object and send the string for content
		Subject readwrite = new proxyreadwrite(write);
		//WRITE method is accessed
		System.out.println("\n Trying to write : ");
		//setting the content
		readwrite.setContent(write); 
		//READ method is accessed
		System.out.println("\n Trying to read : ");
		//getting the content
		System.out.println(readwrite.getContent()); //trying to get content
	}
}
