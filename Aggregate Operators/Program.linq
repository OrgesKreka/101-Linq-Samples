<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Accessibility.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Deployment.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.Formatters.Soap.dll</Reference>
  <Namespace>System.ComponentModel</Namespace>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>


void Main()
{
	var type = typeof(LinqSamples);
	var methodInfo = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

	int i = 73;
	foreach (var info in methodInfo)
	{
		Console.WriteLine($"Sample {i}".PadBoth(100));
		Console.WriteLine("-".PadLeft(100, '-'));
		i++;

		ParameterInfo[] parameters = info.GetParameters();
		var instance = System.Activator.CreateInstance(type);
		var result = info.Invoke(instance, null);
	}
	
		
    SendKeys.SendWait("%1");
}

// Define other methods and classes here
public class LinqSamples
{
	private List<Product> _productList;
	private List<Customer> _customerList;
	
	[Category("Aggregate Operators")]
	[Description("This sample uses Count to get the number of unique prime factors of 300.")]
	public void Sample73()
	{
		int[] primeFactorsOf300 = {2,2,3,5,5};
		
		int uniqueFactors = primeFactorsOf300.Distinct().Count();

		Console.WriteLine($"There are {uniqueFactors} unique prime factors of 300.");
	}
	
	[Category("Aggregate Operators")]
	[Description("This sample uses Count to get the number of odd ints in the array.")]
	public void Sample74()
	{
		int[] numbers = {5,4,1,3,9,8,6,7,2,0};
		
		int oddNumbers = numbers.Count(n => n%2==1);

		Console.WriteLine($"There are {oddNumbers} odd numbers in the list.");
	}
	
	[Category("Aggregate Operators")]
	[Description(@"This sample uses Count to return a list of customers and how many order
					each has.")]
	public void Sample75()
	{
		List<Customer> customers = GetCustomerList();
		
		var orderCounts = from cust in customers
						  select new 
						  {
						  	cust.CustomerID,
							OrderCount = cust.Orders.Count()
						  };
						  
		orderCounts.Dump();
	}
	
	[Category("Aggregate Operators")]
	[Description(@"This sample uses Count to return a list of customers and how many orders
					each has.")]
	public void Sample76()
	{
		var products = GetProductList();
		
		var categoryCounts = from prod in products
							 group prod by prod.Category into prodGroup
							 select new 
							 {
							 	Category = prodGroup.Key,
								ProdCount = prodGroup.Count()
							 };


		// inline method syntax
		/*
			products.GroupBy(x => x.Category).Select(y => new 
			{
				Category = y.Key,
				ProdCount = y.Count()
			}).Dump("Inline Method");
		*/
		categoryCounts.Dump();
	}
	
	[Category("Aggregate Operators")]
	[Description(@"This sample uses Sum to add all the numbers in an array.")]
	public void Sample77()
	{
		int[] numbers = {5,4,1,3,9,8,6,7,2,0};
		
		double numSum = numbers.Sum();

		Console.WriteLine($"The sum of the numbers is {numSum}.");
	}
	
	[Category("Aggregate Operators")]
	[Description(@"This sample uses Sum to get the total number of characters of all words
				   in the array.")]
	public void Sample78()
	{
		string[] words = {"cherry", "apple", "blueberry"};
		
		double totalChars = words.Sum(w => w.Length);

		Console.WriteLine($"There are a total of {totalChars} in these words.");
	}
	
	[Category("Aggregate Operators")]
	[Description(@"This sample uses Sum to get the total units in stock for each products category.")]
	public void Sample79()
	{
		var products = GetProductList();
		
		var categories = from prod in products
						 group prod by prod.Category into prodGroup
						 select new 
						 {
						 	Category = prodGroup.Key,
							TotalUnitsInStock = prodGroup.Sum(p => p.UnitsInStock)
						 };

		// inline method, style
		/*
		products.GroupBy(p => p.Category)
				.Select(x => new
				{
					Category = x.Key,
					TotalUnitsInStock = x.Sum(product => product.UnitsInStock)
				}).Dump("Inline Method");
		*/
		
		categories.Dump();
	}
	
	[Category("Aggregate Operators")]
	[Description(@"This sample uses Min to get the lowest number in an array.")]
	public void Sample80()
	{
		int[] numbers = {5,4,1,3,9,8,6,7,2,0};
		
		int minNum = numbers.Min();

		Console.WriteLine($"The minimum numbers is {minNum}");
	}
	
	[Category("Aggregate Operators")]
	[Description(@"This sample uses Min to get the length of the shortest word in an array")]
	public void Sample81()
	{
		string[] words = {"cherry", "apple", "blueberry"};
		
		int shortesWord = words.Min(x => x.Length);

		Console.WriteLine($"The shortes word is {shortesWord} long.");
	}
	
	[Category("Aggregate Operators")]
	[Description(@"This sample uses Min to get the cheapest price among each category's products.")]
	public void Sample82()
	{
		var products = GetProductList();
		
		var categories = from prod in products
						 group prod by prod.Category into prodGroup
						 select new 
						 {
						 	Category = prodGroup.Key,
							CheapestPrice = prodGroup.Min(p => p.UnitPrice)
						 };
						 
		categories.Dump();
	}
	
	[Category("Aggregate Operators")]
	[Description(@"This sample uses Min to get the products with the lowest price in ")]
	public void Sample83()
	{
		var products = GetProductList();
		
		var categories = from prod in products
						 group prod by prod.Category into prodGroup
						 let minPrice = prodGroup.Min(p => p.UnitPrice)
						 select new 
						 {
						 	Category = prodGroup.Key,
							CheapestProducts = prodGroup.Where( p => p.UnitPrice == minPrice)
						 };
						 
		categories.Dump();
	}
	
	
	[Category("Aggregate Operators")]
	[Description(@"This sample uses Max to get the highest number in an array. Note that the method returns a single value.")]
	public void Sample84()
	{
		int[] numbers = {5,4,1,3,9,8,6,7,2,0};
		int maxNum = numbers.Max();

		Console.WriteLine($"The maximum number is {maxNum}");
	}
	
	[Category("Aggregate Operators")]
	[Description(@"This sample uses Max to get the length of the longest word in an array.")]
	public void Sample85()
	{
		string[] words = {"cherry", "apple", "blueberry"};
		
		int longestLength = words.Max(w => w.Length);

		Console.WriteLine($"The longest word is {longestLength} characters long.");
	}
	
	[Category("Aggregate Operators")]
	[Description(@"This sample use Max to get the mos expensive price among each category's product")]
	public void Sample86()
	{
		var products = GetProductList();
		
		var categories = from prod in products
						 group prod by prod.Category into prodGroup
						 select new 
						 {
						 	Category = prodGroup.Key,
							MostExpensivePrice = prodGroup.Max(p => p.UnitPrice)
						 };
						 
		categories.Dump();
	}
	
	[Category("Aggregate Operators")]
	[Description(@"This sample uses Max to get the products with the most expensive price in each category.")]
	public void Sample87()
	{
		var products = GetProductList();
		
		var categories = from prod in products
						 group prod by prod.Category into prodGroup
						 let maxPrice = prodGroup.Max(p => p.UnitPrice)
						 select new 
						 {
						 	Category = prodGroup.Key,
							MostExpensiveProducts = prodGroup.Where(p => p.UnitPrice == maxPrice)
						 };
		
		/*
		products.GroupBy(x => x.Category)
				.Select(x => new
				{
					Category = x.Key,
					MostExpensiveProducts = x.Where(p => p.UnitPrice == x.Max(y => y.UnitPrice))
				}).Dump("Eci?");
		*/
		
		categories.Dump();
	}
	
	[Category("Aggregate Operators")]
	[Description(@"This sample uses  average to get the Average of all numbers in an array.")]
	public void Sample88()
	{
		int[] numbers = {5,4,1,3,9,8,6,7,2,0};
		
		double averageNum = numbers.Average();
		
		Console.WriteLine($"The average number is {averageNum}.");
	}
	
	[Category("Aggregate Operators")]
	[Description(@"This sample uses Average to get the average length of the words in the array.")]
	public void Sample89()
	{
		string[] words = {"cherry", "apple", "blueberry"};
		
		double averageLength = words.Average(w => w.Length);

		Console.WriteLine($"The average word length is {averageLength} characters.");
	}
	
	[Category("Aggregate Operators")]
	[Description(@"This sample uses Average to get the average price of each category's products. ")]
	public void Sample90()
	{
		var products = GetProductList();
		
		var categories = from prod in products
						 group prod by prod.Category into prodGroup
						 select new 
						 {
						 	Category = prodGroup.Key,
							AveragePrice = prodGroup.Average(p => p.UnitPrice)
						 };
						 
			categories.Dump();
	}
	
	[Category("Aggregate Operators")]
	[Description(@"This sample uses Aggregate to create a running product on the array that calculates the total ")]
	public void Sample91()
	{
		double[] doubles = {1.7,2.3,1.9,4.1,2.9};

		double product = doubles.Aggregate((runningProduct, nextFactor) => runningProduct * nextFactor);

		Console.WriteLine($"Total product of all numbers: {product}");
	}
	
	[Category("Aggregate Operators")]
	[Description(@"This sample uses Aggregate to create a running account balance that
				   subtracts each withdrawal from the initial balance of 100, as long as 
				   the balance never drops below 0.")]
	public void Sample92()
	{
		double startBalance = 100.0;

		int[] attemptedWithdrawals = {20,10,40,50,10,70,30};
		double endBalance = attemptedWithdrawals.Aggregate(
		startBalance,(balance, nextWithdrawal )=> (nextWithdrawal <= balance )? (balance - nextWithdrawal) : balance);

		Console.WriteLine($"Ending Balance: {endBalance}");
	}

private List<Product> GetProductList()
{
		if (_productList == null)
			CreateLists();

		return _productList;
	}

	private List<Customer> GetCustomerList()
{
	if(_customerList == null)
		CreateLists();
		
	return _customerList;
}

private void CreateLists()
{
		// Product data created in-memory using collection initializer:
		_productList =
			new List<Product> {
					new Product { ProductID = 1, ProductName = "Chai", Category = "Beverages", UnitPrice = 18.0000M, UnitsInStock = 39 },
					new Product { ProductID = 2, ProductName = "Chang", Category = "Beverages", UnitPrice = 19.0000M, UnitsInStock = 17 },
					new Product { ProductID = 3, ProductName = "Aniseed Syrup", Category = "Condiments", UnitPrice = 10.0000M, UnitsInStock = 13 },
					new Product { ProductID = 4, ProductName = "Chef Anton's Cajun Seasoning", Category = "Condiments", UnitPrice = 22.0000M, UnitsInStock = 53 },
					new Product { ProductID = 5, ProductName = "Chef Anton's Gumbo Mix", Category = "Condiments", UnitPrice = 21.3500M, UnitsInStock = 0 },
					new Product { ProductID = 6, ProductName = "Grandma's Boysenberry Spread", Category = "Condiments", UnitPrice = 25.0000M, UnitsInStock = 120 },
					new Product { ProductID = 7, ProductName = "Uncle Bob's Organic Dried Pears", Category = "Produce", UnitPrice = 30.0000M, UnitsInStock = 15 },
					new Product { ProductID = 8, ProductName = "Northwoods Cranberry Sauce", Category = "Condiments", UnitPrice = 40.0000M, UnitsInStock = 6 },
					new Product { ProductID = 9, ProductName = "Mishi Kobe Niku", Category = "Meat/Poultry", UnitPrice = 97.0000M, UnitsInStock = 29 },
					new Product { ProductID = 10, ProductName = "Ikura", Category = "Seafood", UnitPrice = 31.0000M, UnitsInStock = 31 },
					new Product { ProductID = 11, ProductName = "Queso Cabrales", Category = "Dairy Products", UnitPrice = 21.0000M, UnitsInStock = 22 },
					new Product { ProductID = 12, ProductName = "Queso Manchego La Pastora", Category = "Dairy Products", UnitPrice = 38.0000M, UnitsInStock = 86 },
					new Product { ProductID = 13, ProductName = "Konbu", Category = "Seafood", UnitPrice = 6.0000M, UnitsInStock = 24 },
					new Product { ProductID = 14, ProductName = "Tofu", Category = "Produce", UnitPrice = 23.2500M, UnitsInStock = 35 },
					new Product { ProductID = 15, ProductName = "Genen Shouyu", Category = "Condiments", UnitPrice = 15.5000M, UnitsInStock = 39 },
					new Product { ProductID = 16, ProductName = "Pavlova", Category = "Confections", UnitPrice = 17.4500M, UnitsInStock = 29 },
					new Product { ProductID = 17, ProductName = "Alice Mutton", Category = "Meat/Poultry", UnitPrice = 39.0000M, UnitsInStock = 0 },
					new Product { ProductID = 18, ProductName = "Carnarvon Tigers", Category = "Seafood", UnitPrice = 62.5000M, UnitsInStock = 42 },
					new Product { ProductID = 19, ProductName = "Teatime Chocolate Biscuits", Category = "Confections", UnitPrice = 9.2000M, UnitsInStock = 25 },
					new Product { ProductID = 20, ProductName = "Sir Rodney's Marmalade", Category = "Confections", UnitPrice = 81.0000M, UnitsInStock = 40 },
					new Product { ProductID = 21, ProductName = "Sir Rodney's Scones", Category = "Confections", UnitPrice = 10.0000M, UnitsInStock = 3 },
					new Product { ProductID = 22, ProductName = "Gustaf's Knäckebröd", Category = "Grains/Cereals", UnitPrice = 21.0000M, UnitsInStock = 104 },
					new Product { ProductID = 23, ProductName = "Tunnbröd", Category = "Grains/Cereals", UnitPrice = 9.0000M, UnitsInStock = 61 },
					new Product { ProductID = 24, ProductName = "Guaraná Fantástica", Category = "Beverages", UnitPrice = 4.5000M, UnitsInStock = 20 },
					new Product { ProductID = 25, ProductName = "NuNuCa Nuß-Nougat-Creme", Category = "Confections", UnitPrice = 14.0000M, UnitsInStock = 76 },
					new Product { ProductID = 26, ProductName = "Gumbär Gummibärchen", Category = "Confections", UnitPrice = 31.2300M, UnitsInStock = 15 },
					new Product { ProductID = 27, ProductName = "Schoggi Schokolade", Category = "Confections", UnitPrice = 43.9000M, UnitsInStock = 49 },
					new Product { ProductID = 28, ProductName = "Rössle Sauerkraut", Category = "Produce", UnitPrice = 45.6000M, UnitsInStock = 26 },
					new Product { ProductID = 29, ProductName = "Thüringer Rostbratwurst", Category = "Meat/Poultry", UnitPrice = 123.7900M, UnitsInStock = 0 },
					new Product { ProductID = 30, ProductName = "Nord-Ost Matjeshering", Category = "Seafood", UnitPrice = 25.8900M, UnitsInStock = 10 },
					new Product { ProductID = 31, ProductName = "Gorgonzola Telino", Category = "Dairy Products", UnitPrice = 12.5000M, UnitsInStock = 0 },
					new Product { ProductID = 32, ProductName = "Mascarpone Fabioli", Category = "Dairy Products", UnitPrice = 32.0000M, UnitsInStock = 9 },
					new Product { ProductID = 33, ProductName = "Geitost", Category = "Dairy Products", UnitPrice = 2.5000M, UnitsInStock = 112 },
					new Product { ProductID = 34, ProductName = "Sasquatch Ale", Category = "Beverages", UnitPrice = 14.0000M, UnitsInStock = 111 },
					new Product { ProductID = 35, ProductName = "Steeleye Stout", Category = "Beverages", UnitPrice = 18.0000M, UnitsInStock = 20 },
					new Product { ProductID = 36, ProductName = "Inlagd Sill", Category = "Seafood", UnitPrice = 19.0000M, UnitsInStock = 112 },
					new Product { ProductID = 37, ProductName = "Gravad lax", Category = "Seafood", UnitPrice = 26.0000M, UnitsInStock = 11 },
					new Product { ProductID = 38, ProductName = "Côte de Blaye", Category = "Beverages", UnitPrice = 263.5000M, UnitsInStock = 17 },
					new Product { ProductID = 39, ProductName = "Chartreuse verte", Category = "Beverages", UnitPrice = 18.0000M, UnitsInStock = 69 },
					new Product { ProductID = 40, ProductName = "Boston Crab Meat", Category = "Seafood", UnitPrice = 18.4000M, UnitsInStock = 123 },
					new Product { ProductID = 41, ProductName = "Jack's New England Clam Chowder", Category = "Seafood", UnitPrice = 9.6500M, UnitsInStock = 85 },
					new Product { ProductID = 42, ProductName = "Singaporean Hokkien Fried Mee", Category = "Grains/Cereals", UnitPrice = 14.0000M, UnitsInStock = 26 },
					new Product { ProductID = 43, ProductName = "Ipoh Coffee", Category = "Beverages", UnitPrice = 46.0000M, UnitsInStock = 17 },
					new Product { ProductID = 44, ProductName = "Gula Malacca", Category = "Condiments", UnitPrice = 19.4500M, UnitsInStock = 27 },
					new Product { ProductID = 45, ProductName = "Rogede sild", Category = "Seafood", UnitPrice = 9.5000M, UnitsInStock = 5 },
					new Product { ProductID = 46, ProductName = "Spegesild", Category = "Seafood", UnitPrice = 12.0000M, UnitsInStock = 95 },
					new Product { ProductID = 47, ProductName = "Zaanse koeken", Category = "Confections", UnitPrice = 9.5000M, UnitsInStock = 36 },
					new Product { ProductID = 48, ProductName = "Chocolade", Category = "Confections", UnitPrice = 12.7500M, UnitsInStock = 15 },
					new Product { ProductID = 49, ProductName = "Maxilaku", Category = "Confections", UnitPrice = 20.0000M, UnitsInStock = 10 },
					new Product { ProductID = 50, ProductName = "Valkoinen suklaa", Category = "Confections", UnitPrice = 16.2500M, UnitsInStock = 65 },
					new Product { ProductID = 51, ProductName = "Manjimup Dried Apples", Category = "Produce", UnitPrice = 53.0000M, UnitsInStock = 20 },
					new Product { ProductID = 52, ProductName = "Filo Mix", Category = "Grains/Cereals", UnitPrice = 7.0000M, UnitsInStock = 38 },
					new Product { ProductID = 53, ProductName = "Perth Pasties", Category = "Meat/Poultry", UnitPrice = 32.8000M, UnitsInStock = 0 },
					new Product { ProductID = 54, ProductName = "Tourtière", Category = "Meat/Poultry", UnitPrice = 7.4500M, UnitsInStock = 21 },
					new Product { ProductID = 55, ProductName = "Pâté chinois", Category = "Meat/Poultry", UnitPrice = 24.0000M, UnitsInStock = 115 },
					new Product { ProductID = 56, ProductName = "Gnocchi di nonna Alice", Category = "Grains/Cereals", UnitPrice = 38.0000M, UnitsInStock = 21 },
					new Product { ProductID = 57, ProductName = "Ravioli Angelo", Category = "Grains/Cereals", UnitPrice = 19.5000M, UnitsInStock = 36 },
					new Product { ProductID = 58, ProductName = "Escargots de Bourgogne", Category = "Seafood", UnitPrice = 13.2500M, UnitsInStock = 62 },
					new Product { ProductID = 59, ProductName = "Raclette Courdavault", Category = "Dairy Products", UnitPrice = 55.0000M, UnitsInStock = 79 },
					new Product { ProductID = 60, ProductName = "Camembert Pierrot", Category = "Dairy Products", UnitPrice = 34.0000M, UnitsInStock = 19 },
					new Product { ProductID = 61, ProductName = "Sirop d'érable", Category = "Condiments", UnitPrice = 28.5000M, UnitsInStock = 113 },
					new Product { ProductID = 62, ProductName = "Tarte au sucre", Category = "Confections", UnitPrice = 49.3000M, UnitsInStock = 17 },
					new Product { ProductID = 63, ProductName = "Vegie-spread", Category = "Condiments", UnitPrice = 43.9000M, UnitsInStock = 24 },
					new Product { ProductID = 64, ProductName = "Wimmers gute Semmelknödel", Category = "Grains/Cereals", UnitPrice = 33.2500M, UnitsInStock = 22 },
					new Product { ProductID = 65, ProductName = "Louisiana Fiery Hot Pepper Sauce", Category = "Condiments", UnitPrice = 21.0500M, UnitsInStock = 76 },
					new Product { ProductID = 66, ProductName = "Louisiana Hot Spiced Okra", Category = "Condiments", UnitPrice = 17.0000M, UnitsInStock = 4 },
					new Product { ProductID = 67, ProductName = "Laughing Lumberjack Lager", Category = "Beverages", UnitPrice = 14.0000M, UnitsInStock = 52 },
					new Product { ProductID = 68, ProductName = "Scottish Longbreads", Category = "Confections", UnitPrice = 12.5000M, UnitsInStock = 6 },
					new Product { ProductID = 69, ProductName = "Gudbrandsdalsost", Category = "Dairy Products", UnitPrice = 36.0000M, UnitsInStock = 26 },
					new Product { ProductID = 70, ProductName = "Outback Lager", Category = "Beverages", UnitPrice = 15.0000M, UnitsInStock = 15 },
					new Product { ProductID = 71, ProductName = "Flotemysost", Category = "Dairy Products", UnitPrice = 21.5000M, UnitsInStock = 26 },
					new Product { ProductID = 72, ProductName = "Mozzarella di Giovanni", Category = "Dairy Products", UnitPrice = 34.8000M, UnitsInStock = 14 },
					new Product { ProductID = 73, ProductName = "Röd Kaviar", Category = "Seafood", UnitPrice = 15.0000M, UnitsInStock = 101 },
					new Product { ProductID = 74, ProductName = "Longlife Tofu", Category = "Produce", UnitPrice = 10.0000M, UnitsInStock = 4 },
					new Product { ProductID = 75, ProductName = "Rhönbräu Klosterbier", Category = "Beverages", UnitPrice = 7.7500M, UnitsInStock = 125 },
					new Product { ProductID = 76, ProductName = "Lakkalikööri", Category = "Beverages", UnitPrice = 18.0000M, UnitsInStock = 57 },
					new Product { ProductID = 77, ProductName = "Original Frankfurter grüne Soße", Category = "Condiments", UnitPrice = 13.0000M, UnitsInStock = 32 }
		};

		// Customer/Order data read into memory from XML file using XLinq:
		
		_customerList = (
			from e in XDocument.Load( Configurations.CustomersFilePath ).
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