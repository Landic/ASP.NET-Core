namespace Сonveyor.Middleware
{
	public class FromOneToTenMiddleware
	{
		private readonly RequestDelegate? _next;
		public FromOneToTenMiddleware(RequestDelegate next) => _next = next;
		public async Task Invoke(HttpContext context)
		{
			string? token = context.Request.Query["number"];
			try
			{
				int number = Convert.ToInt32(token);
				number = Math.Abs(number);
				if (number == 10) await context.Response.WriteAsync("Your number is ten");
				else
				{
					string[] Ones = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten" };
					string existingResult = context.Session.GetString("number") ?? "", thousandResult = context.Session.GetString("thousand") ?? "", thousandProcess = context.Session.GetString("thousandProcess") ?? "true";
					switch (number)
					{
						case int n when n >= 20000 && thousandProcess == "true":
							int two = int.Parse((n / 1000).ToString().Substring(1));
							thousandResult += $" {Ones[two - 1]}";
							context.Session.SetString("thousand", thousandResult);
							break;
						case int n when n >= 1000 && thousandProcess == "true":
							context.Session.SetString("thousand", $" {Ones[n / 1000 - 1]}");
							break;
						case int n when n > 20:
							string str = token!.Substring(token.Length - 1);
							int lastNumber = int.Parse(str);
							if (lastNumber != 0) existingResult += $" {Ones[lastNumber - 1]}";
							else existingResult += $" ten";
							await context.Response.WriteAsync("Your number is " + existingResult);
							context.Session.Remove("number");
							break;
						default:
							existingResult += $" {Ones[number - 1]}";
							await context.Response.WriteAsync("Your number is " + existingResult);
							context.Session.Remove("number");
							break;
					}
				}
			}
			catch (Exception) { await context.Response.WriteAsync("Incorrect parameter"); }
		}
	}
}
