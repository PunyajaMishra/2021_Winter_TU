import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;

//Singleton class with getInstance method
public final class TimeObject
{

private static TimeObject single_instance;
//
private int format=0;

public void Format(int format_type)
{
  
format=format_type;
}
private TimeObject()
{
    format=1;
}
public String Time(){
   DateFormat dateFormat;
  
if(format==1){    
dateFormat= new SimpleDateFormat("hh:mm:ss");}
else{    
dateFormat= new SimpleDateFormat("ss,mm,hh");}
  
   Date date = new Date();
   return dateFormat.format(date);
}
  
// static method to create instance of TimeClass class
public static TimeObject getInstance()
{
if (single_instance == null)
single_instance = new TimeObject();
  
return single_instance;
}
}


