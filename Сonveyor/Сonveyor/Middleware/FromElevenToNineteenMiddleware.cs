namespace Сonveyor.Middleware
{
	public class FromElevenToNineteenMiddleware
	{
		private readonly RequestDelegate? _next;
		public FromElevenToNineteenMiddleware(RequestDelegate next) => _next = next;
		public async Task Invoke(HttpContext context)
		{
			string? token = context.Request.Query["number"];
			if (token == null) return;
			try
			{
				int number = Convert.ToInt32(token);
				number = Math.Abs(number);
				if (number < 11) await _next!.Invoke(context);
				string[] nums = { "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
				string existingResult = context.Session.GetString("number") ?? "", thousandResult = context.Session.GetString("thousand") ?? "", thousandProcess = context.Session.GetString("thousandProcess") ?? "false";
				int lastNumber = Convert.ToInt32(token.Substring(token.Length - 2));
				switch (number)
				{
					case int n when number >= 11_000 && number <= 19_000 && thousandProcess == "true":
						thousandResult += $" {nums[(n / 1000) - 11]}";
						context.Session.SetString("thousand", thousandResult);
						break;
					case int n when lastNumber >= 11 && lastNumber <= 19 && thousandProcess == "false":
						existingResult += $" {nums[(n / 1000) - 11]}";
						await context.Response.WriteAsync("Your number is " + existingResult);
						context.Session.Remove("number");
						break;
					default:
						await _next!.Invoke(context);
						break;
				}
			}
			catch (Exception) { await context.Response.WriteAsync("Incorrect parameter!"); }
		}
	}
}
