namespace Сonveyor.Middleware
{
	public class FromHundredToThousandMiddleware
	{
		private readonly RequestDelegate? _next;
		public FromHundredToThousandMiddleware(RequestDelegate next) => _next = next;
		public async Task Invoke(HttpContext context)
		{
			string? token = context.Request.Query["number"];
			try
			{
				int number = Convert.ToInt32(token);
				number = Math.Abs(number);
				if (number < 100) await _next!.Invoke(context);
				else
				{
					string[] nums = { "one hundred", "two hundred", "three hundred", "four hundred", "five hundred", "six hundred", "seven hundred", "eight hundred", "nine hundred" };
					string existingResult = context.Session.GetString("number") ?? "", thousandResult = context.Session.GetString("thousand") ?? "", thousandProcess = context.Session.GetString("thousandProcess") ?? "false";
					switch (number)
					{
						case int n when n >= 100000 && thousandProcess == "true":
							thousandResult += $" {nums[(n / 1000) - 1]}";
							thousandResult = thousandResult.Replace("hundred", "");
							context.Session.SetString("thousand", thousandResult);
							if ((n / 1000) < 10) break;
							else if ((n / 1000) % 100 != 0) await _next!.Invoke(context);
							break;
						case int n when n >= 100 && thousandProcess == "false":
							string str = token!.Substring(token.Length - 3);
							if (str.Substring(0, 1) == "0") await _next!.Invoke(context);
							else
							{
								int lastNumber = Convert.ToInt32(token.Substring(token.Length - 3));
								existingResult += $" {nums[lastNumber / 100 - 1]}";
								if (lastNumber % 100 == 0)
								{
									await context.Response.WriteAsync("Your number is " + existingResult);
									context.Session.Remove("number");
								}
								else
								{
									context.Session.SetString("number", existingResult);
									await _next!.Invoke(context);
								}
							}
							break;
						default:
							await _next!.Invoke(context);
							break;
					}
				}
			}
			catch (Exception) { await context.Response.WriteAsync("Incorrect parameter!"); }
		}
	}
}
