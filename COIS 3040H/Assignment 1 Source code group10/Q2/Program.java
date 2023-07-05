
public class Program {
	public static void main(String[]args) {
		
		//making an array of shapes since there are 4, so just
		//loop it out to print
		Shape[] shape = new Shape[4];
		
		shape[0] = new Triangle(new Red());
		shape[0].setThickness("Thick");
		
		shape[1] = new Triangle(new Blue());
		shape[1].setThickness("Thin");
		
		shape[2] = new Square(new Red());
		shape[2].setThickness("Thin");
		
		shape[3] = new Square(new Blue());
		shape[3].setThickness("Thick");
		
		for(int i=0; i<4; i++) 
			System.out.println(shape[i].toString());
	}
}
