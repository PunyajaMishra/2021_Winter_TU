//COIS 3040 Assignment 2
//Group 10 : Badrit Bin Imran, Punyaja Mishra, Gowri Nandana

//observer class
public abstract class Observer {

	//the protected property of arryalistsubject class subject
	protected ArrayListSubject subj;

	//the update method that will print the message that something was deleted
	public abstract void update();
}
