namespace Сonveyor.Middleware
{
	public class FromThousandToHundredThousandMiddleware
	{
		private readonly RequestDelegate? _next;
		public FromThousandToHundredThousandMiddleware(RequestDelegate next) => _next = next;
		public async Task Invoke(HttpContext context)
		{
			string? token = context.Request.Query["number"];
			if (token == null) return;
			try
			{
				context.Session.Remove("number");
				context.Session.Remove("thousandProcess");
				context.Session.Remove("thousand");
				int number = Convert.ToInt32(token);
				number = Math.Abs(number);
				if (number < 1000) await _next!.Invoke(context);
				else
				{
					context.Session.SetString("thousandProcess", "true");
					string existingResult = context.Session.GetString("number") ?? "";
					await _next!.Invoke(context);
					string thousandResult = context.Session.GetString("thousand") ?? "";
					existingResult += $"{thousandResult} thousand";
					context.Session.SetString("thousandProcess", "false");
					if (number % 1000 == 0)
					{
						await context.Response.WriteAsync("Your number is " + existingResult);
						context.Session.Remove("number");
						context.Session.Remove("thousandProcess");
						context.Session.Remove("thousand");
					}
					else
					{
						context.Session.SetString("number", existingResult);
						await _next.Invoke(context);
					}
				}
			}
			catch (Exception) { await context.Response.WriteAsync("Incorrect parameter!"); }
		}
	}
}
