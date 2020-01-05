<Query Kind="Program">
  <Namespace>System.ComponentModel</Namespace>
</Query>

void Main()
{
	var type = typeof(LinqSamples);
	var methodInfo = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

	int i = 20;
	foreach (var info in methodInfo)
	{
		Console.WriteLine($"Sample {i}".PadBoth(100));
		Console.WriteLine("-".PadLeft(100, '-'));
		i++;

		ParameterInfo[] parameters = info.GetParameters();
		var instance = System.Activator.CreateInstance(type);
		var result = info.Invoke(instance, null);
	}
}

// Define other methods and classes here
public class Order
{
	public int OrderID { get; set; }
	
	public DateTime OrderDate { get; set; }
	
	public decimal Total { get; set; }	
}

public class Customer
{
	public string CustomerID { get; set; }
	
	public string CompanyName { get; set; }
	
	public string Address { get; set; }
	
	public string City { get; set; }
	
	public string Region { get; set; }
	
	public string PostalCode { get; set; }
	
	public string Country { get; set; }
	
	public string Phone { get; set; }
	
	public string Fax { get; set; }
	
	public Order[] Orders { get; set; }
}

public class LinqSamples
{
	private List<Customer> _customerList;
	
	[Category("Partitioning Operators")]
	[Description(@"This sample uses Take to get only the first 3 elements
				   of the array.")]
	public void Sample20()
	{
		var numbers = new int[]
		{
			5, 4, 1, 3, 9, 8, 6, 7, 2, 0 
		};
		
		var first3Numbers = numbers.Take(3);
		
		Console.WriteLine("First 3 numbers: ");
		
		foreach (var n in first3Numbers)
		{
			Console.WriteLine(n);
		}
	}

	[Category("Partitioning Operators")]
	[Description(@"This sample uses Take to get the first 3 orders from 
					customers in Washington")]
	public void Sample21()
	{
		var customers = GetCustomerList();


		var first3WAOrders = (from cust in customers
							 from order in cust.Orders
							 where cust.Region == "WA"
							 select new
							 {
							 	cust.CustomerID,
								order.OrderID,
								order.OrderDate
							 }).Take(3);


		//versioni duke perdorur extended method
		/*
		var tmp = customers.SelectMany(cust => cust.Orders, (cust, order) => new
		{
			cust,
			order
		}).Where(t => t.cust.Region == "WA")
		  .Select(x => new
		  {
		  	x.cust.CustomerID,
			x.order.OrderID,
			x.order.OrderDate
		  })
		  .Take(3);
		*/
		
		Console.WriteLine("First 3 orders in WA: ");
		foreach (var order in first3WAOrders)
		{
			order.Dump();
		}		 
	}
	
	[Category("Partitioning Operators")]
	[Description(@"This sample uses Skip to get all but the first four elements of
				   the array")]
	public void Sample22()
	{
		var numbers = new int[]
		{
			 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 
		};
		
		var allButFirst4Numbers = numbers.Skip(4);
		
		Console.WriteLine("All but first 4 numbers: ");
		foreach (var n in allButFirst4Numbers)
		{
			Console.WriteLine(n);
		}
	}
	
	[Category("Partitioning Operators")]
	[Description(@"This sample uses Take to get all but the first 2 orders
				   from customers in Washington.")]
	public void Sample23()
	{
		var customers = GetCustomerList();
		
		var waorders = from cust in customers
					   from order in cust.Orders
					   where cust.Region == "WA"
					   select new 
					   {
					   	cust.CustomerID,
						order.OrderID,
						order.OrderDate
					   };
					   
		var allButFirst2Orders = waorders.Skip(2);

		// using extended methods
		/*
		var tmp = (customers.SelectMany(cust => cust.Orders, (cust, order) => new
		{
			cust,
			order
		}).Where(t => t.cust.Region == "WA")
		  .Select(o => new
		  {
		  	o.cust.CustomerID,
			o.order.OrderID,
			o.order.OrderDate
		  })).Skip(2);
		*/
		
		Console.WriteLine("All but first 2 orders in WA: ");
		
		foreach (var order in allButFirst2Orders)
		{
			order.Dump();
		}
	}
	
	[Category("Partitioning Operators")]
	[Description(@"This sample uses TakeWhile to return elements starting from 
					the beging of the array until a number is read whose value is not
					less than 6.")]
	public void Sample24()
	{
		int[] numbers =  { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
		
		var firstNumbersLessThan6 = numbers.TakeWhile(n => n < 6);
		
		Console.WriteLine("First numbers less than 6: ");
		foreach (var num in firstNumbersLessThan6)
		{
			Console.WriteLine(num);
		}
	}
		
	[Category("Partitioning Operators")]
	[Description(@"This sample uses TakeWhile to return elements starting from
				   the begining of the array until a number is hit that is less than
				   its position in the array")]
	public void Sample25()
	{
		var numbers = new int[]
		{
			5, 4, 1, 3, 9, 8, 6, 7, 2, 0
		};
		
		var firstSmallNumbers = numbers.TakeWhile((n,index) => n >= index);
		
		Console.WriteLine("First numbers less than their position: ");
		foreach (var n in firstSmallNumbers)
		{
			Console.WriteLine(n);
		}
	}
	
	[Category("Partitioning Operators")]
	[Description(@"This sample uses SkipWhile to get elements of the array
				   starting from the first element divisible by 3.")]
	public void Sample26()
	{
		int[] numbers = {5, 4, 1, 3, 9, 8, 6, 7, 2, 0};
		
		var allButFirst3Numbers = numbers.SkipWhile(n => n % 3 != 0 );
		
		Console.WriteLine("All elements starting from first element divisible by 3: ");
		
		foreach (var n in allButFirst3Numbers)
		{
			Console.WriteLine(n);
		}
		
		// Logjika e SkipWhile
		// fillon nga elementi i pare
		// persa kohe kushti eshte i verete, kalo tek elementi pasardhes
	}
	
	[Category("Partitioning Operators")]
	[Description(@"This sample uses SkipWhile to get the elements of the array
				   starting from the first element less than its position. ")]
	public void Sample27()
	{
		var numbers = new int[]
		{
			5, 4, 1, 3, 9, 8, 6, 7, 2, 0
		};
		
		var laterNumbers = numbers.SkipWhile((n , index) => n >= index );
		
		Console.WriteLine("All elements starting from first element greater than its position: ");
		foreach (var n in laterNumbers)
		{
			Console.WriteLine(n);
		}
	}

private List<Customer>  GetCustomerList()
{
		if (_customerList == null)
			CreateList();

		return _customerList;
}

private void CreateList()
{
		_customerList = (
					   from e in XDocument.Load(@"C:\Users\o.kreka\Documents\LINQPad Queries\101 LINQ Samples\Customers.xml").
								 Root.Elements("customer")
					   select new Customer
					   {
						   CustomerID = (string)e.Element("id"),
						   CompanyName = (string)e.Element("name"),
						   Address = (string)e.Element("address"),
						   City = (string)e.Element("city"),
						   Region = (string)e.Element("region"),
						   PostalCode = (string)e.Element("postalcode"),
						   Country = (string)e.Element("country"),
						   Phone = (string)e.Element("phone"),
						   Fax = (string)e.Element("fax"),
						   Orders = (
							   from o in e.Elements("orders").Elements("order")
							   select new Order
							   {
								   OrderID = (int)o.Element("id"),
								   OrderDate = (DateTime)o.Element("orderdate"),
								   Total = (decimal)o.Element("total")
							   })
							   .ToArray()
					   })
					   .ToList();
	}
}

public static class StringExtensions
{
	public static string PadBoth(this string str, int length)
	{
		int spaces = length - str.Length;
		int padLeft = spaces / 2 + str.Length;
		return str.PadLeft(padLeft).PadRight(length);
	}
}