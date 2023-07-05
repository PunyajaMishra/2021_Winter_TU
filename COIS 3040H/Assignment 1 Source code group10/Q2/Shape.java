
public abstract class Shape {
	protected IColor color;
	protected String thickness;
	
	//constructor Shape
	public Shape(IColor color) {
		this.color = color;
	}
	
	//get thickness getter
	public String getThickness() {
		return thickness;
	}
	
	//set thickness setter
	public void setThickness(String thickness) {
		this.thickness = thickness;
	}
	
	//get color 
	public IColor getColor() {
		return color;
	}
	
	public void setColor(IColor color) {
		this.color = color;
	}
	
	public String toString() {
		return "Shape " + color.fill() + " and thickness is " + thickness ;
	}
}
