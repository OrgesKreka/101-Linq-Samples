<Query Kind="Program">
  <Namespace>System.ComponentModel</Namespace>
</Query>

void Main()
{
	var type = typeof(LinqSamples);
	var methodInfo = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

	int i = 54;
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
	[Category("Conversion Operators")]
	[Description(@"This sample uses ToArray to immediately evaluate a sequence into an array.")]
	public void Sample54()
	{
		var doubles = new double[] {1.7,2.3,1.9,4.1,2.9};
		
		var sortedDouble = from d in doubles
						   orderby d descending
						   select d;
						   
		var doublesArray = sortedDouble.ToArray();
		
		Console.WriteLine("Every other double from highest to lowest: ");
		for (int i = 0; i < doublesArray.Length; i+=2)
		{
			Console.WriteLine(doublesArray[i]);
		}
	}
	
	[Category("Conversion Operators")]
	[Description(@"This sample uses ToList to immediately ")]
	public void Sample55()
	{
		var words = new string[] {"cherry", "apple", "blueberry"};
		
		var sortedWords = from w in words
						  orderby w
						  select w;
		
		var wordList = sortedWords.ToList();
		Console.WriteLine("The sorted word list: ");
		
		foreach (var w in wordList)
		{
			Console.WriteLine(w);
		}
		
	}
	
	[Category("Conversion Operators")]
	[Description(@"This sample uses ToDictionary to immediately evaluate a sequence and a
				   related key expression into a dictionary.")]
	public void Sample56()
	{
		var scoreRecords = new[]
		{
			new {Name = "Alice", Score = 50},
			new {Name = "Bob", Score = 40},
			new {Name = "Cathy", Score = 45}
		};
		
		var scoreRecordsDict = scoreRecords.ToDictionary(sr => sr.Name );

		Console.WriteLine($"Bob's score: {scoreRecordsDict["Bob"]}");
	}
	
	[Category("Conversion Operators")]
	[Description(@"This sample uses OfType to return only the elements of the array that are of type double.")]
	public void Sample57()
	{
		var numbers = new object[]{null, 1.0, "two", 3, "four", 5, "six", 7.0};
		
		var doubles = numbers.OfType<double>();
		
		Console.WriteLine("Numbers stored as doubles: ");
		foreach (var d in doubles)
		{
			Console.WriteLine(d);
		}
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
