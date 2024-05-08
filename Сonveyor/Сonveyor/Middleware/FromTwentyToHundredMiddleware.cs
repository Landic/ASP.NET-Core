namespace Сonveyor.Middleware
{
	public class FromTwentyToHundredMiddleware
	{
		private readonly RequestDelegate? _next;
		public FromTwentyToHundredMiddleware(RequestDelegate next) => _next = next;
		public async Task Invoke(HttpContext context)
		{
			string? token = context.Request.Query["number"];
			try
			{
				int number = Convert.ToInt32(token);
				number = Math.Abs(number);
				if (number < 20) await _next!.Invoke(context);
				else
				{
					string[] nums = { "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
					string existingResult = context.Session.GetString("number") ?? "", thousandResult = context.Session.GetString("thousand") ?? "", thousandProcess = context.Session.GetString("thousandProcess") ?? "false";
					switch (number)
					{
						case int n when n >= 20_000 && thousandProcess == "true":
							thousandResult += $" {nums[(n / 1000) / 10 - 2]}";
							context.Session.SetString("thousand", thousandResult);
							if (n % 1000 != 0) await _next!.Invoke(context);
							break;
						case int n when n >= 20 && thousandProcess == "false":
							if (token!.Substring(token.Length - 2, 1) != "0" && token.Substring(token.Length - 2, 1) != "1")
							{
								int lastNumber = int.Parse(token.Substring(token.Length - 2));
								existingResult += $" {nums[lastNumber / 10 - 2]}";
								if (number % 10 == 0)
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
							else await _next!.Invoke(context);
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
