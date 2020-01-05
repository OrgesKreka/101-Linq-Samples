<Query Kind="Program">
  <Namespace>System.ComponentModel</Namespace>
</Query>

void Main()
{
	var type = typeof(LinqSamples);
	var methodInfo = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

	int i = 65;
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
	[Category("Generation Operators")]
	[Description(@"This sample uses Range to generate a sequence of numbers from 100 to 149
				   that is used to find which numbers in that range are odd and even.")]
	public void Sample65()
	{
		var numbers = from n in Enumerable.Range(100, 50)
					  select new
					  {
					  	Number = n,
						OddEven = n %2 == 1 ? "odd" : "even"
					  };
		foreach (var n in numbers)
		{
			Console.WriteLine($"The number {n.Number} is {n.OddEven}");
		}
	}
	
	[Category("Generation Operators")]
	[Description(@"This sample uses Repeat to generate a sequence that contains the number 7 ten times.")]
	public void Sample66()
	{
		var numbers = Enumerable.Repeat(7,10);
		
		foreach (var n in numbers)
		{
			Console.WriteLine(n);
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