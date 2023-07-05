

import java.util.Scanner;

public class Main{
  
@SuppressWarnings("resource")

//main method
public static void main(String []args){

//Instantiating DateObject class with variable d
DateObject date = DateObject.getInstance();
//Instantiating TimeObject class with variable t
TimeObject time = TimeObject.getInstance();


System.out.println("Enter 1 for Format1 Date: MM/DD/YY and Time: HH:MM:SS-----Enter 2 for Format 2 Date: DD-MM-YYYY and Time: SS,MM,HH");
//Parse the input 
Scanner inp = new Scanner(System.in);
  
int format = inp.nextInt();
date.Format(format);
time.Format(format);

 
while(true){
	
System.out.println("Enter 'd' to display date, 't' to display time and 'q' to quit");

Scanner input = new Scanner(System.in);
//choice stores the option selected by user
String choice=input.nextLine();


if(choice.equals("d")){
System.out.println("Date : "+date.Date());
}

else if(choice.equals("t")){
System.out.println("Time : "+time.Time());
}

else if(choice.equals("q")){
System.out.println("Program terminates!");
break;
}


else{
System.out.println("The choice is incorrect please chose either d, t or q");
}
}
    
}
}
