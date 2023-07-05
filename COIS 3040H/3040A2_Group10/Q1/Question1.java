//Question1.java
//Assignment 2 Question no 1
//March 2021 COIS 3040 
//Badrit Bin Imran, Punyaja Mishra, Gowri Nandana

import java.util.ArrayList;
import java.util.List;
import java.util.StringTokenizer;

//Main method
public class Question1 {

	public static void main(String[] args) {
		//Print Message
		System.out.println("Welcome to the Composite pattern and Builder pattern implementation");
		System.out.println("Only input in the form of [ int int ] is accepted");
		System.out.println("The value of our ListComponent object obtained from ListBuilder: ");
		//Initializing the builder class
		ListBuilder builder = new ListBuilder();
		//Input String
		String input = "[ 1 [ 2 3 ] [ 4 5 ] [ 6 4 ] ]";
		//Tokenizing the input string
		StringTokenizer tokens = new StringTokenizer(input);
	    int number; //Integer value for integer input
	    String s; //String for each token
	    boolean opened=false; //Bracket open error checks
	    boolean error = false; //For error checking
	    int openBrackets=0; //Counting the number of brackets opened
	    ListComponent comp = new ListComposite(); //Intiailizing the Type ListComponent) to be returned from getlist()
		//While Loop for looping through input
	    while(tokens.hasMoreTokens())
		{
	    	//Storing value into s
			s= tokens.nextToken();	
				//Try Catch block for error check
			     try {
			    	 //Storing the integer
			        number = Integer.parseInt(s); 
			        builder.BuildElement(number);		
			        
			    } catch (NumberFormatException e) {
			    	//Case open bracket
			    	if (s.equals("[")) {
			    		if(openBrackets>=1)builder.buildOpenBracket();
			    		openBrackets++;
			    	}	
			    	//Case close brackets
			    	else if(s.equals("]")) {
			    		if(openBrackets<1) {
			    			error = true;
			    			break;
			    		}	
			    		//Since our first input is a "[" we start from second bracket
			    		if(openBrackets>1)builder.buildCloseBracket();				    		
			    		openBrackets--;
			    	}			 
			    	//Error found
			    	else {			    	
			    	error = true;
			        break;
			    	}
			    }										
		}
	    //Error case: too many brackets opened
		if(openBrackets!=0) error=true;
		//Printing our outputs only when program is error free
		if(!error)
		{
			//Using the getlist method
			comp = builder.getList();
			comp.printValue();
			System.out.println();
			//Testing get child
			System.out.println("Testing get child for index 1 on the ListComponent");
			comp.getChild(1).printValue();
			System.out.println();
			//Testing remove child
			System.out.println("Testing remove child for index 0 on the ListComponent");
			comp.removeChild(0);
			//Printing new List
			System.out.println("New list after removal of index 0: ");
			comp.printValue();
			System.out.println();
		}
		//Error message for incorrect inputs
		else System.out.println("Error! Invalid Input. Execution Stopped");
		
	}
}







