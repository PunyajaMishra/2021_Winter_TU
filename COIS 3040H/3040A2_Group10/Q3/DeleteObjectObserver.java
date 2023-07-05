//COIS 3040 Assignment 2
//Group 10 : Badrit Bin Imran, Punyaja Mishra, Gowri Nandana

//observer : observer extended from the abstract
public class DeleteObjectObserver extends Observer {
 
	//constructor
	public DeleteObjectObserver(ArrayListSubject s) {
		//set the subject to the arraylistsubject
		subj = s;
		subj.attach(this); //attach this observer - observer attaches itself
	}
	
	//the update method that will print message when something is deleted
	public void update() {
		System.out.println("An item being deleted");
	}
}
