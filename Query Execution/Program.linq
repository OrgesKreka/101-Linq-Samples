<Query Kind="Program">
  <Namespace>System.ComponentModel</Namespace>
</Query>

void Main()
{
	var type = typeof(LinqSamples);
	var methodInfo = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

	int i = 94;
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
public class LinqSamples
{
	private List<Product> _productList;
	private List<Customer> _customerList;
	
	[Category("Query Execution")]
	[Description(@"The following sample shows how query execution is deferred until the query is
				   enumerated at a foreach statement.")]
	public void Sample95()
	{
		// Queries are not executed until you enumerate over them.
		int[] numbers = new int[] {5,4,1,3,9,8,6,7,2,0};
		
		int i = 0;
		var simpleQuery = from num in numbers
						  select ++i;

		// The local variable'i' is not incremented
		// until the query is executed in the foreach loop.
		Console.WriteLine($"The current value of i is {i}");
		
		foreach (var item in simpleQuery)
		{
			Console.WriteLine($"v = {item}, i = {i}");
		}
	}
	
	[Category("Query Execution")]
	[Description(@"The following sample shows how queries can be executed immediately, and their results
				   sored in memory, with methods as ToList.")]
	public void Sample96()
	{
		// Methods like ToList(), Max() and Count() cause the query 
		// to be executed immediately
		int[] numbers = new int[] {5,4,1,3,9,8,6,7,2,0};
		
		var i = 0;
		var immediateQuery = (from num in numbers
							  select ++i).ToList();

		Console.WriteLine($"The current value of i is {i}"); // i has been incremented
		
		foreach (var item in immediateQuery)
		{
			Console.WriteLine($"v = {item}, i = {i}");
		}
	}
	
	[Category("Query Execution")]
	[Description(@"The following sample shows how, because of deferred execution, queries can be used
				   again after data changes and will then operate on the new data.")]
	public void Sample97()
	{
		int[] numbers = {5,4,1,3,9,8,6,7,2,0};
		
		var lowNumbers = from num in numbers
						 where num <= 3
						 select num;
						 
		Console.WriteLine("First run numbers <= 3:");
		foreach (var n in lowNumbers)
		{
			Console.WriteLine(n);
		}
		
		// Modify the source data.
		for (int i = 0; i < 10; i++)
		{
			numbers[i] = -numbers[i];
		}
		
		// During this second run, the same query object,
		// lowNumbers, will be iterating over the new state
		// of numbers[], producing different results:
		Console.WriteLine("Second run numbers <= 3: ");
		foreach (var n in lowNumbers)
		{
			Console.WriteLine(n);
		}
	}
}

public class Product
{
	public int ProductID { get; set; }
	
	public string ProductName { get; set; }
	
	public string Category { get; set; }
	
	public decimal UnitPrice { get; set; }
	
	public int UnitsInStock { get; set; }
}

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

public static class StringExtensions
{
	public static string PadBoth(this string str, int length)
	{
		int spaces = length - str.Length;
		int padLeft = spaces / 2 + str.Length;
		return str.PadLeft(padLeft).PadRight(length);
	}
}