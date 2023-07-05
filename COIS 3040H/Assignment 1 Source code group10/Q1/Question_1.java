import java.util.ArrayList; // import the ArrayList class


public class Question_1 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		System.out.println("Creating a new ClassAdapter class called 'birdAdapter'! We add a few variables in the beginning to continue testing the Adapter");
		ClassAdapter birdAdapter = new ClassAdapter();
	    birdAdapter.arrList.add("Duck");
	    birdAdapter.arrList.add("Chicken");
	    birdAdapter.arrList.add("Eagle");
	    birdAdapter.arrList.add("Turkey");
	    System.out.println("   Now, we print out our birdAdapter arraylist : " + birdAdapter.arrList );
		System.out.println("1. Testing count method : " + birdAdapter.count() );		
		System.out.println("2. Testing get method for index = 2 :  " + birdAdapter.get(2));
		System.out.println("3. Testing first  method:  " + birdAdapter.first());
		System.out.println("4. Testing last method:  " + birdAdapter.last());
		System.out.println("5. Testing include method for eagle:  " + birdAdapter.include("Eagle"));
		System.out.println("6. Testing append method for Ostrich:  " );
		birdAdapter.append("Ostrich");
		System.out.println("   New birdAdapter arrayList:  " + birdAdapter.arrList);
		System.out.println("7. Testing prepend method for Falcon:  ");
		birdAdapter.prepend("Falcon");
		System.out.println("   New birdAdapter arrayList:  " + birdAdapter.arrList);
		System.out.println("8. Testing delete method for Duck:  ");
		birdAdapter.delete("Duck");
		System.out.println("   New birdAdapter arrayList:  " + birdAdapter.arrList);
		System.out.println("9. Testing delete last :  ");
		birdAdapter.deleteLast();
		System.out.println("   New birdAdapter arrayList:  " + birdAdapter.arrList);
		System.out.println("10.Testing delete first :  ");
		birdAdapter.deleteFirst();
		System.out.println("   New birdAdapter arrayList:  " + birdAdapter.arrList);
		System.out.println("11.Testing delete all : ");
		birdAdapter.deleteAll();
		System.out.println("   New birdAdapter arrayList:  " + birdAdapter.arrList);
		System.out.println("So we see that our new birdAdapter class has access to all methods of the interface List from Gof book and all methods native to the java ArrayList");
		
	}

}

//---------------------------------------------------------
interface List {
	 int count(); //return the current number of elements in the list
	 Object get(int index); //return the object at the index in the list
	 Object first(); //return the first object in the list
	 Object last(); //return the last object in the list
	 boolean include(Object obj); //return true is the object in the list
	 void append(Object obj); //append the object to the end of the list
	 void prepend(Object obj); //insert the object to the front of the list
	 void delete(Object obj); //remove the object from the list
	 void deleteLast(); //remove the last element of the list
	 void deleteFirst(); //remove the first element of the list
	 void deleteAll(); //remove all elements of the list
	}




class ClassAdapter  extends ArrayList<Object> implements List {
    ArrayList<Object> arrList;
	
	public ClassAdapter() {
		this.arrList = new ArrayList<Object>();
		// TODO Auto-generated constructor stub
	}

	@Override
	public int count() {
		// TODO Auto-generated method stub
		return this.arrList.size();
	}

	@Override
	public Object first() {
		// TODO Auto-generated method stub
		return this.arrList.get(0);
	}

	@Override
	public Object last() {
		// TODO Auto-generated method stub
		return this.arrList.get(this.count()-1);
	}

	@Override
	public boolean include(Object obj) {
		// TODO Auto-generated method stub
		if(this.arrList.contains(obj))
		return true;
		else return false;
	}

	@Override
	public void append(Object obj) {
		// TODO Auto-generated method stub
		this.arrList.add(obj);
		
	}

	@Override
	public void prepend(Object obj) {
		// TODO Auto-generated method stub
		this.arrList.add("DummyBird");
		
		
		for(int i =this.arrList.size()-1;i>0;i--) {
			this.arrList.set(i,this.arrList.get(i-1));
		}
		
		this.arrList.set(0,obj);
	}

	@Override
	public void delete(Object obj) {
		// TODO Auto-generated method stub
		for(int i=0;i<this.arrList.size();i++) {
			if(obj==arrList.get(i)) {
				this.arrList.remove(i);
				break;
			}
			
		}
		
	}

	@Override
	public void deleteLast() {
		// TODO Auto-generated method stub
		this.arrList.remove(this.arrList.size()-1);
	}

	@Override
	public void deleteFirst() {
		// TODO Auto-generated method stub
		this.arrList.remove(0);
		
	}

	@Override
	public void deleteAll() {
		// TODO Auto-generated method stub
		this.arrList=null;	
	}

	@Override
	public Object get(int index) {
		// TODO Auto-generated method stub
		return this.arrList.get(2);
	}

}
