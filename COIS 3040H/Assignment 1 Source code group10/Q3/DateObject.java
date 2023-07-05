
//Assignment 1 COIS 3040 Winter 2021
//Question 3
//Done by: Group 10 
//Members : Badrit Bin Imran, Punyaja Mishra, Gowri Nandana
//This program uses Abstract Factory and Singleton Design pattern to display date
//time in either of the two formats according to the user's choice.

import java.text.DateFormat;// imports the data/time formatting class called DateFormat
import java.text.SimpleDateFormat;    // imports concrete SimpleDateFormat class
import java.util.Date; // Class Date import and it represents specific instance in time


//Singleton class with getInstance() method
public final class DateObject
{ 
 //static variable single_instance of type DateObject
 private static DateObject single_instance;
  //variable that stores the type of formating
 private int formatType = 0;
 
 //private constructor restricted to this class itself
 private DateObject()
 {
      formatType=1;
 }
   

//Method that assigns the type of format the user chooses to the formatType variable
 public void Format(int format_type)
 {
   formatType=format_type;
 }


// static method to create instance of DateClass class
public static DateObject getInstance()
{
//creates an object of the class and return it to the variable
if (single_instance == null)
single_instance = new DateObject();
  
return single_instance;
}

//Method of type String that returns the date as per user choice
public String Date(){
	  
	  //variable date_Format of type DateFormat that stores the required date
	  DateFormat date_Format;
	  
	if(formatType==1){  
		
	date_Format= new SimpleDateFormat("MM/dd/YYYY");}

	else{
		
	date_Format= new SimpleDateFormat("dd-MM-YYYY");}
	  
	   Date date = new Date();
	   return date_Format.format(date);
	}

 }


