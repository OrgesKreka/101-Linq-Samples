<Query Kind="Program">
  <Namespace>System.ComponentModel</Namespace>
</Query>

void Main()
{
	var type = typeof(LinqSamples);
	var methodInfo = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

	int i = 6;
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

private class LinqSamples
{
	private DataSet testDs;
	
	public LinqSamples()
	{
		testDs = TestHelper.CreateTestDataSet();
	}

	[Category("Projection Operators")]
	[Description(@"This sample uses select to produce a sequence of ints one higher than
	      		   those in an existing array of ints.")]
	public void Sample6()
	{
		var numbers = testDs.Tables["Numbers"].AsEnumerable();
		
		var numsPlusOne = from n in numbers
						  select  n.Field<int>(0)+1;
				
		/*
		*	Lambda Expression Version
		*   var numsPlusOne = numbers.Select( num => num.Field<int>(0) + 1);
		*/
				
		Console.WriteLine("Numbers + 1: ");
		numsPlusOne.ToList().ForEach( n => Console.WriteLine(n));
	}
	
	[Category("Projection Operators")]
	[Description(@"This sample uses select to return a sequence of just the names
				   of a list of products. ")]
	public void Sample7()
	{
		var products = testDs.Tables["Products"].AsEnumerable();
		
		var productNames = from p in products
						   select p.Field<string>( "ProductName" );
						   
		Console.WriteLine("Product Names: ");
		
		foreach (var product in productNames)
		{
			Console.WriteLine(product);		
		}
	}	
	
	[Category("Projection Operators")]
	[Description(@"This sample uses select to produce a sequence of strings representing
				   the text version of a sequence of ints.")]
	public void Sample8()
	{
		var numbers = testDs.Tables["Numbers"].AsEnumerable();
		var strings = new string [] 
		{
			"zero",
			"one",
			"two", 
			"three",
			"four",
			"five", 
			"six", 
			"seven", 
			"eight", 
			"nine" 
		};
		
		var textNums = numbers.Select(p => strings[p.Field<int>("number")]).ToList();

		/*
		*	Linq Style query
		*	textNums = (from n in numbers
		*		   select strings[n.Field<int>("number")]).ToList();
		*
		*/
		
		Console.WriteLine("Number strings: ");
		
		textNums.ForEach( n => Console.WriteLine(n));
	}

	[Category("Projection Operators")]
	[Description(@"This sample uses select to produce a sequence of the uppercase
				   and lowercase versions of each word in the original array.")]
	public void Sample9()
	{
		var words = testDs.Tables["Words"].AsEnumerable();

		var upperLowerWords = words.Select(p => new
		{
			Upper = (p.Field<string>(0)).ToUpper(),
			Lower = (p.Field<string>(0)).ToLower()
		});
		
		
		/*
		*	Linq Version
		*	var upperLowerWords = from w in words
		*						  select new 
		*						  { 
		*							Upper = (w.Field<string>(0)).ToUpper(),
		*							Lower = (p.Field<string>(0)).ToLower()
		*						   }
		*/
		
		foreach (var ul in upperLowerWords)
		{
			Console.WriteLine($"UpperCase: {ul.Upper}, Lowercase: {ul.Lower}");
		}
	}
	
	[Category("Projection Operators")]
	[Description(@"This sample uses select to produce a sequence of containing text
					representations of digits and whether their length is even or odd.")]
	public void Sample10()
	{
		var numbers = testDs.Tables["Numbers"].AsEnumerable();
		var digits = testDs.Tables["Digits"];
		
		var strings = new string[]
		{
		 "zero",
		 "one", 
		 "two",
		 "three", 
		 "four", 
		 "five",
		 "six", 
		 "seven", 
		 "eight", 
		 "nine"
		};

		var digitOddNumbers = numbers.Select(n => new
		{
			Digit = digits.Rows[n.Field<int>("number")]["digit"],
			Even = (n.Field<int>("number") % 2 == 0 )
		}).ToList();

		digitOddNumbers.ForEach(n => Console.WriteLine("The digit {0} is {1}.", n.Digit , n.Even ? "even" : "odd" ));
	}
	
	[Category("Projection Operators")]
	[Description(@"This sample uses select to produce a sequence containing some properties 
				  of products, including UnitPrice which is rename to Price
				  in the resulting type.")]
	public void Sample11()
	{
		var products = testDs.Tables["Products"].AsEnumerable();

		var productInfos = products.Select(n => new
		{
			ProductName = n.Field<string>("ProductName"),
			Category = n.Field<string>("Category"),
			Price  = n.Field<decimal>("UnitPrice")
		});
		
		Console.WriteLine("Product info: ");
		foreach (var productInfo in productInfos)
		{
			Console.WriteLine($"{productInfo.ProductName} is in the category {productInfo.Category} and costs {productInfo.Price} per unit");
		}
	}
	
	[Category("Projection Operators")]
	[Description(@"This sample uses an indexed Select clause to determine if the value of ints
				   in an array match their positin in the array.")]
	public void Sample12()
	{
		var numbers = testDs.Tables["Numbers"].AsEnumerable();

		var numsInPlace = numbers.Select((num, index) => new
		{
			Num = num.Field<int>("number"),
			InPlace = (num.Field<int>("number") == index)
		}).ToList();

		Console.WriteLine("Number: In-Place?");
		numsInPlace.ForEach(n => Console.WriteLine($"{n.Num} : {n.InPlace}"));
	}
	
	[Category("Projection Operators")]
	[Description(@"This sample combines select and where to make a simple query that returs
				   the text form of each digit less than 5.")]
	public void Sample13()	
	{
		var numbers = testDs.Tables["Numbers"].AsEnumerable();
		var digits = testDs.Tables["Digits"];
		
		var lowNums = from n in numbers
					  where n.Field<int>("number") < 5
					  select digits.Rows[n.Field<int>("number")].Field<string>("digit");
					  
		Console.WriteLine("Numbers < 5: ");
		foreach (var num in lowNums)
		{
			Console.WriteLine(num);
		}	
	}
	
	[Category("Projection Operators")]
	[Description(@"This sample uses a compound from clause to make a query that returns all pairs 
				   of numbers from both arrays such that the number from numbersA is less than the number from numbersB.")]
	public void Sample14()
	{
		var numbersA = testDs.Tables["NumbersA"].AsEnumerable();
		var numbersB = testDs.Tables["NumbersB"].AsEnumerable();
		
		var pairs = (from a in numbersA
				    from b in numbersB
					let numberA = a.Field<int>("number")
					let numberB =  b.Field<int>("number")
					where numberA < numberB
					select new {a = numberA, b = numberB}).ToList();
		
					/*
					* 	Nuk ka version me lambda expression
					*	te pakten me njohurite e deritanishme
					*
					*/
		Console.WriteLine("Pairs where a < b: ");

		pairs.ForEach(pair => Console.WriteLine($"{pair.a} is less than {pair.b}"));
	}
	
	[Category("Projection Operators")]
	[Description(@"This sample uses a compound from clause to select all orders where 
				   order total is less than 500.00")]
	public void Sample15()
	{
		var customers = testDs.Tables["Customers"].AsEnumerable();
		var orders = testDs.Tables["Orders"].AsEnumerable();
		
		// nuk ka perdorur join
		var smallOrders = from c in customers
						  from o in orders
						  where c.Field<string>("CustomerID") == o.Field<string>("CustomerID")
						  && o.Field<decimal>("Total") < 500.00M
						  select new 
						  {
						  	CustomerID = c.Field<string>("CustomerID"),
							OrderID = o.Field<int>("OrderID"),
							Total = o.Field<decimal>("Total")
						  };
						  
		smallOrders.Dump();				  
		// versioni duke perdorur join
		
		/*
		var smallOrders = from c in customers 
					  join o in orders 
					  on c.Field<string>("CustomerID") equals o.Field<string>("CustomerID")
					  where o.Field<decimal>("Total") < 500.00M
					  select new 
					  {
						  CustomerID = c.Field<string>("CustomerID"),
						  OrderID = o.Field<int>("OrderID"),
						  Total = o.Field<decimal>("Total")
					   };
		*/
		
		// join duke perdorur extension methods, not linq style
		// or lambda expression
		/*
		customers.Join(orders,
			c => c.Field<string>("CustomerID"),
			o => o.Field<string>("CustomerID"),
			(c, o) =>new
			{
				CustomerId = c.Field<string>("CustomerID"),
				OrderId = o.Field<int>("OrderID"),
				Total = o.Field<decimal>("Total")
			}).Where( o => o.Total < 500.00M);
		*/
	}
	
	[Category("Projection Operators")]
	[Description(@"This sample uses a compound from clause to select all orders where 
					order was made in 1998 or later.")]
	public void Sample16()
	{
		var customers = testDs.Tables["Customers"].AsEnumerable();
		var orders = testDs.Tables["Orders"].AsEnumerable();
		
		var myOrders = from c in customers
					  from o in orders
					  let customerId1 = c.Field<string>("CustomerID")
					  let customerId2 = o.Field<string>("CustomerID")
					  let orderDate = o.Field<DateTime>("OrderDate")
					  where customerId1 == customerId2 && orderDate >= new DateTime(1998,1,1)
					  select new 
					  {
					  	CustomerID = customerId1,
						OrderId = o.Field<int>("OrderID"),
						OrderDate = orderDate
					  };
		
		
		
		// versioni duke perdorur join
		/*myOrders = from c in customers
				   join o in orders
				   on c.Field<string>("CustomerID") equals o.Field<string>("CustomerID")
				   where o.Field<DateTime>("OrderDate") >= new DateTime(1998,1,1)
				   select new
				   {
				   		CustomerID = c.Field<string>("CustomerID"),
					   OrderId = o.Field<int>("OrderID"),
					   OrderDate = o.Field<DateTime>("OrderDate")
				   };
		*/
		
		// versioni duke perdorur extended method
		/*
		myOrders = customers.Join(orders,
				   c => c.Field<string>("CustomerID"),
				   o => o.Field<string>("CustomerID"),
				   (c, o) => new
				   {
					   CustomerID = c.Field<string>("CustomerID"),
					   OrderId = o.Field<int>("OrderID"),
					   OrderDate = o.Field<DateTime>("OrderDate")
				   }).Where( o => o.OrderDate >= new DateTime(1998,1,1));
		*/
		
		
		myOrders.Dump();
					  
	}
	
	[Category("Projection Operators")]
	[Description(@"This sample uses a compound from clause to select all orders where the 
				   order total is greater than 2000.00 and uses from assignment to avoid 
				   requesting the total twice.")]
	public void Sample17()
	{
		var customers = testDs.Tables["Customers"].AsEnumerable();
		var orders = testDs.Tables["Orders"].AsEnumerable();
		
		var myOrders = from c in customers 
					   from o in orders
					   let total = o.Field<decimal>("Total")
					   where c.Field<string>("CustomerID") == o.Field<string>("CustomerID")
					   && total >= 2000.0M
					   select new 
					   {
					   	CustomerID = c.Field<string>("CustomerID"), OrderID = o.Field<int>("OrderID"), total
					   };
					   
					   
		// versioni duke perdorur join
		/*myOrders = from c in customers
				   join o in orders
				   on c.Field<string>("CustomerID") equals o.Field<string>("CustomerID")
				   where o.Field<decimal>("Total") >= 2000.0M
				   select new
				   {
					   CustomerID = c.Field<string>("CustomerID"),
					   OrderID = o.Field<int>("OrderID"),
					   total = o.Field<decimal>("Total")
				   };
		*/
		
		// versioni duke perdorur extend method
		/*myOrders = customers.Join(orders,
			       c =>  c.Field<string>("CustomerID"),
				   o => o.Field<string>("CustomerID"),
				   (c, o) => new
				   {
					   CustomerID = c.Field<string>("CustomerID"),
					   OrderID = o.Field<int>("OrderID"),
					   total = o.Field<decimal>("Total")
				   }).Where( o => o.total >= 2000.0M);
		*/
		
		myOrders.Dump(); // 185 : 736608.40
	}
	
	[Category("Projection Operators")]
	[Description(@"This sample uses multiple from clauses so that filtering on customers can
				   be done before selectiong their orders. this makes the query more efficient by
				   not selecting then discarding orders for customers outside of Washington.")]
	public void Sample18()
	{
		var customers = testDs.Tables["Customers"].AsEnumerable();
		var orders = testDs.Tables["Orders"].AsEnumerable();
		var cutoffDate = new DateTime(1997, 1, 1);
		
		var myOrders = from c in customers 
					   where c.Field<string>("Region") == "WA"
					   from o in orders 
					   where c.Field<string>("CustomerID") == o.Field<string>("CustomerID")
					   && (DateTime)o["OrderDate"] >= cutoffDate
					   select new
					   {
					   	CustomerID = c.Field<string>("CustomerID"), 
						OrderID = o.Field<int>("OrderID") 
					   };
					   
		// duke perdorur join
		/*
		myOrders = from c in customers
				   where c.Field<string>("Region") == "WA"
				   join o in orders  
				   on  c.Field<string>("CustomerID") equals o.Field<string>("CustomerID")
				   where (DateTime)o["OrderDate"] >= cutoffDate
				   select new
				   {
					   CustomerID = c.Field<string>("CustomerID"),
					   OrderID = o.Field<int>("OrderID")
				   };
		*/
		myOrders.Dump(); // 17
		
	}
	 
	[Category("Projection Operators")]
	[Description(@"This sample uses an indexed SelectMany clause to select all orders
				   while referring to customers by the order in which they are returned
				   from the query.")]
	public void Sample19()
	{
		var customers = testDs.Tables["Customers"].AsEnumerable();
		var orders = testDs.Tables["Orders"].AsEnumerable();
		
		var customerOrders = customers.SelectMany(
			(cust, custIndex) =>
			orders.Where(o => cust.Field<string>("CustomerID") == o.Field<string>("CustomerID"))
			.Select(o => new { CustomerIndex = custIndex + 1, OrderID = o.Field<int>("OrderID") } ));

		foreach (var c in customerOrders)
		{
			Console.WriteLine("Customer Index: " + c.CustomerIndex +
								" has an order with OrderID " + c.OrderID);
		}
	}
}



public class TestHelper
{
	public static DataSet CreateTestDataSet()
	{
		var ds = new DataSet();
		ds.Tables.Add(CreateNumbersTable());
		ds.Tables.Add(CreateLowNumbersTable());
		ds.Tables.Add(CreateEmptyNumbersTable());
		ds.Tables.Add(CreateProductList());
		ds.Tables.Add(CreateDigitsTable());
		ds.Tables.Add(CreateWordsTable());
		ds.Tables.Add(CreateWords2Table());
		ds.Tables.Add(CreateWords3Table());
		ds.Tables.Add(CreateWords4Table());
		ds.Tables.Add(CreateAnagramsTable());
		ds.Tables.Add(CreateNumbersATable());
		ds.Tables.Add(CreateNumbersBTable());
		ds.Tables.Add(CreateFactorsOf300());
		ds.Tables.Add(CreateDoublesTable());
		ds.Tables.Add(CreateScoreRecordsTable());
		ds.Tables.Add(CreateAttemptedWithdrawalsTable());
		ds.Tables.Add(CreateEmployees1Table());
		ds.Tables.Add(CreateEmployees2Table());
		
		CreateCustomersAndOrdersTables(ds);

		ds.AcceptChanges();
		return ds;
	}

	static void CreateCustomersAndOrdersTables(DataSet ds)
	{
		DataTable customers = new DataTable("Customers");
		customers.Columns.Add("CustomerID", typeof(string));
		customers.Columns.Add("CompanyName", typeof(string));
		customers.Columns.Add("Address", typeof(string));
		customers.Columns.Add("City", typeof(string));
		customers.Columns.Add("Region", typeof(string));
		customers.Columns.Add("PostalCode", typeof(string));
		customers.Columns.Add("Country", typeof(string));
		customers.Columns.Add("Phone", typeof(string));
		customers.Columns.Add("Fax", typeof(string));

		ds.Tables.Add(customers);

		DataTable orders = new DataTable("Orders");

		orders.Columns.Add("OrderID", typeof(int));
		orders.Columns.Add("CustomerID", typeof(string));
		orders.Columns.Add("OrderDate", typeof(DateTime));
		orders.Columns.Add("Total", typeof(decimal));

		ds.Tables.Add(orders);

		DataRelation co = new DataRelation("CustomersOrders", customers.Columns["CustomerID"], orders.Columns["CustomerID"], true);
		ds.Relations.Add(co);

		var customerList = (
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
			}
			).ToList();

		foreach (Customer cust in customerList)
		{
			customers.Rows.Add(new object[] {cust.CustomerID, cust.CompanyName, cust.Address, cust.City, cust.Region,
												cust.PostalCode, cust.Country, cust.Phone, cust.Fax});
			foreach (Order order in cust.Orders)
			{
				orders.Rows.Add(new object[] { order.OrderID, cust.CustomerID, order.OrderDate, order.Total });
			}
		}
	}

	static DataTable CreateEmployees2Table()
	{
		DataTable table = new DataTable("Employees2");
		table.Columns.Add("id", typeof(int));
		table.Columns.Add("lastname", typeof(string));
		table.Columns.Add("level", typeof(int));

		table.Rows.Add(new object[] { 1, "Jones", 10 });
		table.Rows.Add(new object[] { 2, "Jagger", 5 });
		table.Rows.Add(new object[] { 3, "Thomas", 6 });
		table.Rows.Add(new object[] { 4, "Collins", 11 });
		table.Rows.Add(new object[] { 4, "Collins", 12 });
		table.Rows.Add(new object[] { 5, "Arthur", 12 });

		return table;
	}

	static DataTable CreateEmployees1Table()
	{
		DataTable table = new DataTable("Employees1");
		table.Columns.Add("id", typeof(int));
		table.Columns.Add("name", typeof(string));
		table.Columns.Add("worklevel", typeof(int));

		table.Rows.Add(new object[] { 1, "Jones", 5 });
		table.Rows.Add(new object[] { 2, "Smith", 5 });
		table.Rows.Add(new object[] { 2, "Smith", 5 });
		table.Rows.Add(new object[] { 3, "Smith", 6 });
		table.Rows.Add(new object[] { 4, "Arthur", 11 });
		table.Rows.Add(new object[] { 5, "Arthur", 12 });

		return table;
	}

	static DataTable CreateAttemptedWithdrawalsTable()
	{
		int[] attemptedWithdrawals = { 20, 10, 40, 50, 10, 70, 30 };

		DataTable table = new DataTable("AttemptedWithdrawals");
		table.Columns.Add("withdrawal", typeof(int));

		foreach (var r in attemptedWithdrawals)
		{
			table.Rows.Add(new object[] { r });
		}
		return table;
	}

	static DataTable CreateScoreRecordsTable()
	{
		var scoreRecords = new[] { new {Name = "Alice", Score = 50},
								new {Name = "Bob"  , Score = 40},
								new {Name = "Cathy", Score = 45}
							};

		DataTable table = new DataTable("ScoreRecords");
		table.Columns.Add("Name", typeof(string));
		table.Columns.Add("Score", typeof(int));

		foreach (var r in scoreRecords)
		{
			table.Rows.Add(new object[] { r.Name, r.Score });
		}
		return table;
	}

	static DataTable CreateDoublesTable()
	{
		double[] doubles = { 1.7, 2.3, 1.9, 4.1, 2.9 };

		DataTable table = new DataTable("Doubles");
		table.Columns.Add("double", typeof(double));

		foreach (double d in doubles)
		{
			table.Rows.Add(new object[] { d });
		}
		return table;
	}

	static DataTable CreateFactorsOf300()
	{
		int[] factorsOf300 = { 2, 2, 3, 5, 5 };

		DataTable table = new DataTable("FactorsOf300");
		table.Columns.Add("factor", typeof(int));

		foreach (int factor in factorsOf300)
		{
			table.Rows.Add(new object[] { factor });
		}
		return table;
	}

	static DataTable CreateNumbersBTable()
	{
		int[] numbersB = { 1, 3, 5, 7, 8 };
		DataTable table = new DataTable("NumbersB");
		table.Columns.Add("number", typeof(int));

		foreach (int number in numbersB)
		{
			table.Rows.Add(new object[] { number });
		}
		return table;
	}

	static DataTable CreateNumbersATable()
	{
		int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
		DataTable table = new DataTable("NumbersA");
		table.Columns.Add("number", typeof(int));

		foreach (int number in numbersA)
		{
			table.Rows.Add(new object[] { number });
		}
		return table;
	}

	static DataTable CreateAnagramsTable()
	{
		string[] anagrams = { "from   ", " salt", " earn ", "  last   ", " near ", " form  " };
		DataTable table = new DataTable("Anagrams");
		table.Columns.Add("anagram", typeof(string));

		foreach (string word in anagrams)
		{
			table.Rows.Add(new object[] { word });
		}
		return table;
	}

	static DataTable CreateWords4Table()
	{
		string[] words = { "blueberry", "chimpanzee", "abacus", "banana", "apple", "cheese" };
		DataTable table = new DataTable("Words4");
		table.Columns.Add("word", typeof(string));

		foreach (string word in words)
		{
			table.Rows.Add(new object[] { word });
		}
		return table;
	}

	static DataTable CreateWords3Table()
	{
		string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
		DataTable table = new DataTable("Words3");
		table.Columns.Add("word", typeof(string));

		foreach (string word in words)
		{
			table.Rows.Add(new object[] { word });
		}
		return table;
	}

	static DataTable CreateWords2Table()
	{
		string[] words = { "believe", "relief", "receipt", "field" };
		DataTable table = new DataTable("Words2");
		table.Columns.Add("word", typeof(string));

		foreach (string word in words)
		{
			table.Rows.Add(new object[] { word });
		}
		return table;
	}

	static DataTable CreateWordsTable()
	{
		string[] words = { "aPPLE", "BlUeBeRrY", "cHeRry" };
		DataTable table = new DataTable("Words");
		table.Columns.Add("word", typeof(string));

		foreach (string word in words)
		{
			table.Rows.Add(new object[] { word });
		}
		return table;
	}

	static DataTable CreateDigitsTable()
	{
		string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
		DataTable table = new DataTable("Digits");
		table.Columns.Add("digit", typeof(string));

		foreach (string digit in digits)
		{
			table.Rows.Add(new object[] { digit });
		}
		return table;
	}

	static DataTable CreateProductList()
	{
		var table = new DataTable("Products");
		table.Columns.Add("ProductID", typeof(int));
		table.Columns.Add("ProductName", typeof(string));
		table.Columns.Add("Category", typeof(string));
		table.Columns.Add("UnitPrice", typeof(decimal));
		table.Columns.Add("UnitsInStock", typeof(int));

		var productList = new[] {
			  new { ProductID = 1, ProductName = "Chai", Category = "Beverages",
				UnitPrice = 18.0000M, UnitsInStock = 39 },
			  new { ProductID = 2, ProductName = "Chang", Category = "Beverages",
				UnitPrice = 19.0000M, UnitsInStock = 17 },
			  new { ProductID = 3, ProductName = "Aniseed Syrup", Category = "Condiments",
				UnitPrice = 10.0000M, UnitsInStock = 13 },
			  new { ProductID = 4, ProductName = "Chef Anton's Cajun Seasoning", Category = "Condiments",
				UnitPrice = 22.0000M, UnitsInStock = 53 },
			  new { ProductID = 5, ProductName = "Chef Anton's Gumbo Mix", Category = "Condiments",
				UnitPrice = 21.3500M, UnitsInStock = 0 },
			  new { ProductID = 6, ProductName = "Grandma's Boysenberry Spread", Category = "Condiments",
				UnitPrice = 25.0000M, UnitsInStock = 120 },
			  new { ProductID = 7, ProductName = "Uncle Bob's Organic Dried Pears", Category = "Produce",
				UnitPrice = 30.0000M, UnitsInStock = 15 },
			  new { ProductID = 8, ProductName = "Northwoods Cranberry Sauce", Category = "Condiments",
				UnitPrice = 40.0000M, UnitsInStock = 6 },
			  new { ProductID = 9, ProductName = "Mishi Kobe Niku", Category = "Meat/Poultry",
				UnitPrice = 97.0000M, UnitsInStock = 29 },
			  new { ProductID = 10, ProductName = "Ikura", Category = "Seafood",
				UnitPrice = 31.0000M, UnitsInStock = 31 },
			  new { ProductID = 11, ProductName = "Queso Cabrales", Category = "Dairy Products",
				UnitPrice = 21.0000M, UnitsInStock = 22 },
			  new { ProductID = 12, ProductName = "Queso Manchego La Pastora", Category = "Dairy Products",
				UnitPrice = 38.0000M, UnitsInStock = 86 },
			  new { ProductID = 13, ProductName = "Konbu", Category = "Seafood",
				UnitPrice = 6.0000M, UnitsInStock = 24 },
			  new { ProductID = 14, ProductName = "Tofu", Category = "Produce",
				UnitPrice = 23.2500M, UnitsInStock = 35 },
			  new { ProductID = 15, ProductName = "Genen Shouyu", Category = "Condiments",
				UnitPrice = 15.5000M, UnitsInStock = 39 },
			  new { ProductID = 16, ProductName = "Pavlova", Category = "Confections",
				UnitPrice = 17.4500M, UnitsInStock = 29 },
			  new { ProductID = 17, ProductName = "Alice Mutton", Category = "Meat/Poultry",
				UnitPrice = 39.0000M, UnitsInStock = 0 },
			  new { ProductID = 18, ProductName = "Carnarvon Tigers", Category = "Seafood",
				UnitPrice = 62.5000M, UnitsInStock = 42 },
			  new { ProductID = 19, ProductName = "Teatime Chocolate Biscuits", Category = "Confections",
				UnitPrice = 9.2000M, UnitsInStock = 25 },
			  new { ProductID = 20, ProductName = "Sir Rodney's Marmalade", Category = "Confections",
				UnitPrice = 81.0000M, UnitsInStock = 40 },
			  new { ProductID = 21, ProductName = "Sir Rodney's Scones", Category = "Confections",
				UnitPrice = 10.0000M, UnitsInStock = 3 },
			  new { ProductID = 22, ProductName = "Gustaf's Knäckebröd", Category = "Grains/Cereals",
				UnitPrice = 21.0000M, UnitsInStock = 104 },
			  new { ProductID = 23, ProductName = "Tunnbröd", Category = "Grains/Cereals",
				UnitPrice = 9.0000M, UnitsInStock = 61 },
			  new { ProductID = 24, ProductName = "Guaraná Fantástica", Category = "Beverages",
				UnitPrice = 4.5000M, UnitsInStock = 20 },
			  new { ProductID = 25, ProductName = "NuNuCa Nuß-Nougat-Creme", Category = "Confections",
				UnitPrice = 14.0000M, UnitsInStock = 76 },
			  new { ProductID = 26, ProductName = "Gumbär Gummibärchen", Category = "Confections",
				UnitPrice = 31.2300M, UnitsInStock = 15 },
			  new { ProductID = 27, ProductName = "Schoggi Schokolade", Category = "Confections",
				UnitPrice = 43.9000M, UnitsInStock = 49 },
			  new { ProductID = 28, ProductName = "Rössle Sauerkraut", Category = "Produce",
				UnitPrice = 45.6000M, UnitsInStock = 26 },
			  new { ProductID = 29, ProductName = "Thüringer Rostbratwurst", Category = "Meat/Poultry",
				UnitPrice = 123.7900M, UnitsInStock = 0 },
			  new { ProductID = 30, ProductName = "Nord-Ost Matjeshering", Category = "Seafood",
				UnitPrice = 25.8900M, UnitsInStock = 10 },
			  new { ProductID = 31, ProductName = "Gorgonzola Telino", Category = "Dairy Products",
				UnitPrice = 12.5000M, UnitsInStock = 0 },
			  new { ProductID = 32, ProductName = "Mascarpone Fabioli", Category = "Dairy Products",
				UnitPrice = 32.0000M, UnitsInStock = 9 },
			  new { ProductID = 33, ProductName = "Geitost", Category = "Dairy Products",
				UnitPrice = 2.5000M, UnitsInStock = 112 },
			  new { ProductID = 34, ProductName = "Sasquatch Ale", Category = "Beverages",
				UnitPrice = 14.0000M, UnitsInStock = 111 },
			  new { ProductID = 35, ProductName = "Steeleye Stout", Category = "Beverages",
				UnitPrice = 18.0000M, UnitsInStock = 20 },
			  new { ProductID = 36, ProductName = "Inlagd Sill", Category = "Seafood",
				UnitPrice = 19.0000M, UnitsInStock = 112 },
			  new { ProductID = 37, ProductName = "Gravad lax", Category = "Seafood",
				UnitPrice = 26.0000M, UnitsInStock = 11 },
			  new { ProductID = 38, ProductName = "Côte de Blaye", Category = "Beverages",
				UnitPrice = 263.5000M, UnitsInStock = 17 },
			  new { ProductID = 39, ProductName = "Chartreuse verte", Category = "Beverages",
				UnitPrice = 18.0000M, UnitsInStock = 69 },
			  new { ProductID = 40, ProductName = "Boston Crab Meat", Category = "Seafood",
				UnitPrice = 18.4000M, UnitsInStock = 123 },
			  new { ProductID = 41, ProductName = "Jack's New England Clam Chowder", Category = "Seafood",
				UnitPrice = 9.6500M, UnitsInStock = 85 },
			  new { ProductID = 42, ProductName = "Singaporean Hokkien Fried Mee", Category = "Grains/Cereals",
				UnitPrice = 14.0000M, UnitsInStock = 26 },
			  new { ProductID = 43, ProductName = "Ipoh Coffee", Category = "Beverages",
				UnitPrice = 46.0000M, UnitsInStock = 17 },
			  new { ProductID = 44, ProductName = "Gula Malacca", Category = "Condiments",
				UnitPrice = 19.4500M, UnitsInStock = 27 },
			  new { ProductID = 45, ProductName = "Rogede sild", Category = "Seafood",
				UnitPrice = 9.5000M, UnitsInStock = 5 },
			  new { ProductID = 46, ProductName = "Spegesild", Category = "Seafood",
				UnitPrice = 12.0000M, UnitsInStock = 95 },
			  new { ProductID = 47, ProductName = "Zaanse koeken", Category = "Confections",
				UnitPrice = 9.5000M, UnitsInStock = 36 },
			  new { ProductID = 48, ProductName = "Chocolade", Category = "Confections",
				UnitPrice = 12.7500M, UnitsInStock = 15 },
			  new { ProductID = 49, ProductName = "Maxilaku", Category = "Confections",
				UnitPrice = 20.0000M, UnitsInStock = 10 },
			  new { ProductID = 50, ProductName = "Valkoinen suklaa", Category = "Confections",
				UnitPrice = 16.2500M, UnitsInStock = 65 },
			  new { ProductID = 51, ProductName = "Manjimup Dried Apples", Category = "Produce",
				UnitPrice = 53.0000M, UnitsInStock = 20 },
			  new { ProductID = 52, ProductName = "Filo Mix", Category = "Grains/Cereals",
				UnitPrice = 7.0000M, UnitsInStock = 38 },
			  new { ProductID = 53, ProductName = "Perth Pasties", Category = "Meat/Poultry",
				UnitPrice = 32.8000M, UnitsInStock = 0 },
			  new { ProductID = 54, ProductName = "Tourtière", Category = "Meat/Poultry",
				UnitPrice = 7.4500M, UnitsInStock = 21 },
			  new { ProductID = 55, ProductName = "Pâté chinois", Category = "Meat/Poultry",
				UnitPrice = 24.0000M, UnitsInStock = 115 },
			  new { ProductID = 56, ProductName = "Gnocchi di nonna Alice", Category = "Grains/Cereals",
				UnitPrice = 38.0000M, UnitsInStock = 21 },
			  new { ProductID = 57, ProductName = "Ravioli Angelo", Category = "Grains/Cereals",
				UnitPrice = 19.5000M, UnitsInStock = 36 },
			  new { ProductID = 58, ProductName = "Escargots de Bourgogne", Category = "Seafood",
				UnitPrice = 13.2500M, UnitsInStock = 62 },
			  new { ProductID = 59, ProductName = "Raclette Courdavault", Category = "Dairy Products",
				UnitPrice = 55.0000M, UnitsInStock = 79 },
			  new { ProductID = 60, ProductName = "Camembert Pierrot", Category = "Dairy Products",
				UnitPrice = 34.0000M, UnitsInStock = 19 },
			  new { ProductID = 61, ProductName = "Sirop d'érable", Category = "Condiments",
				UnitPrice = 28.5000M, UnitsInStock = 113 },
			  new { ProductID = 62, ProductName = "Tarte au sucre", Category = "Confections",
				UnitPrice = 49.3000M, UnitsInStock = 17 },
			  new { ProductID = 63, ProductName = "Vegie-spread", Category = "Condiments",
				UnitPrice = 43.9000M, UnitsInStock = 24 },
			  new { ProductID = 64, ProductName = "Wimmers gute Semmelknödel", Category = "Grains/Cereals",
				UnitPrice = 33.2500M, UnitsInStock = 22 },
			  new { ProductID = 65, ProductName = "Louisiana Fiery Hot Pepper Sauce", Category = "Condiments",
				UnitPrice = 21.0500M, UnitsInStock = 76 },
			  new { ProductID = 66, ProductName = "Louisiana Hot Spiced Okra", Category = "Condiments",
				UnitPrice = 17.0000M, UnitsInStock = 4 },
			  new { ProductID = 67, ProductName = "Laughing Lumberjack Lager", Category = "Beverages",
				UnitPrice = 14.0000M, UnitsInStock = 52 },
			  new { ProductID = 68, ProductName = "Scottish Longbreads", Category = "Confections",
				UnitPrice = 12.5000M, UnitsInStock = 6 },
			  new { ProductID = 69, ProductName = "Gudbrandsdalsost", Category = "Dairy Products",
				UnitPrice = 36.0000M, UnitsInStock = 26 },
			  new { ProductID = 70, ProductName = "Outback Lager", Category = "Beverages",
				UnitPrice = 15.0000M, UnitsInStock = 15 },
			  new { ProductID = 71, ProductName = "Flotemysost", Category = "Dairy Products",
				UnitPrice = 21.5000M, UnitsInStock = 26 },
			  new { ProductID = 72, ProductName = "Mozzarella di Giovanni", Category = "Dairy Products",
				UnitPrice = 34.8000M, UnitsInStock = 14 },
			  new { ProductID = 73, ProductName = "Röd Kaviar", Category = "Seafood",
				UnitPrice = 15.0000M, UnitsInStock = 101 },
			  new { ProductID = 74, ProductName = "Longlife Tofu", Category = "Produce",
				UnitPrice = 10.0000M, UnitsInStock = 4 },
			  new { ProductID = 75, ProductName = "Rhönbräu Klosterbier", Category = "Beverages",
				UnitPrice = 7.7500M, UnitsInStock = 125 },
			  new { ProductID = 76, ProductName = "Lakkalikööri", Category = "Beverages",
				UnitPrice = 18.0000M, UnitsInStock = 57 },
			  new { ProductID = 77, ProductName = "Original Frankfurter grüne Soße", Category = "Condiments",
				UnitPrice = 13.0000M, UnitsInStock = 32 }
			};

		foreach (var x in productList)
		{
			table.Rows.Add(new object[] { x.ProductID, x.ProductName, x.Category, x.UnitPrice, x.UnitsInStock });
		}

		return table;
	}

	static DataTable CreateEmptyNumbersTable()
	{
		DataTable table = new DataTable("EmptyNumbers");
		table.Columns.Add("number", typeof(int));
		return table;
	}

	static DataTable CreateLowNumbersTable()
	{
		var lowNumbers = new int[] {1,11,3,9,19,41,65,19};
		var table = new DataTable("LowNumbers");
		table.Columns.Add("number", typeof(int));
		
		foreach (var number in lowNumbers)
		{
			table.Rows.Add(new object[] {number});
		}
		
		return table;
	}

	private static DataTable CreateNumbersTable()
	{
		var numbers = new int[]{5,4,1,3,9,8,6,7,2,0};
		var table = new DataTable("Numbers");
		table.Columns.Add("number", typeof(int));
		
		foreach (var number in numbers)
		{
			table.Rows.Add(new object[] {number});
		}
		
		return table;
	}
}



public class Customer
{
	public Customer(string customerID, string companyName)
	{
		CustomerID = customerID;
		CompanyName = companyName;
		Orders = new Order[10];
	}
	
	public Customer(){}
	
	
	public string CustomerID;
	public string CompanyName;
	public string Address;
	public string City;
	public string Region;
	public string PostalCode;
	public string Country;
	public string Phone;
	public string Fax;
	public Order[] Orders;
	
}



public class Order
{
	public Order(int orderID, DateTime orderDate, decimal total)
	{
		OrderID = orderID;
		OrderDate = orderDate;
		Total = total;
	}
	
	public Order(){}
	
	
	public int OrderID;
	public DateTime OrderDate;
	public decimal Total;
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