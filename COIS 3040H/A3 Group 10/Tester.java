//Assignment 3 COIS 3040 Winter 2021
//Q8
//Done by: Group 10 
//Members : Badrit Bin Imran, Punyaja Mishra, Gowri

//***This class has both the test cases***

//This is a Java Unit class used to test the SetLoan and GetMonthly payment methods

//Imports
import org.junit.Test;
import static org.junit.Assert.*;

//Testing the SetLoan Class
//A successful test is when the method throws an exception
public class Tester {
	@Test (expected = Exception.class)
	//We test if the class throws an exception or not
	public void TestSetLoan() throws Exception 
	{
		Loan A = new Loan(); //using default constructor
		A.setLoanAmount(-1000);
	}
	@Test 
	//Testing the getMonthly Payment
	//A successful test is when the method works correctly
	public void TestGetMontlyPayment() throws Exception
	{
		Loan A = new Loan();
		A.getMonthlyPayment();
	}	
}