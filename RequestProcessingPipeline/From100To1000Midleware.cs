namespace RequestProcessingPipeline
{
    public class From100To1000Midleware
    {
        readonly RequestDelegate _next;
        public From100To1000Midleware(RequestDelegate next)  
        {
            this._next = next;  
        }
        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Query["number"];
            string[] _num_nund = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            string? result = string.Empty;
            try
            {

                int number = Convert.ToInt32(token);
                number = Math.Abs(number);
                if (number < 100)
                {
                    await _next.Invoke(context);
                }
                else if (number > 1000)
                {
                    number = number % 1000;
                    if (number % 100 == 0)
                    {
                        context.Session.SetString("number", _num_nund[number / 100 - 1] + " hundred");
                    }
                    else if (number / 100 != 0)
                    {
                        await _next.Invoke(context);
                        result = context.Session.GetString("number");
                        context.Session.SetString("number", _num_nund[number / 100 - 1] + " hundred " + result);
                    }
                    else
                    {
                        await _next.Invoke(context);
                        result = context.Session.GetString("number");
                        context.Session.SetString("number", result);
                    }
                }
                else
                {
                    if (number % 100 == 0)
                    {
                       
                        await context.Response.WriteAsync("Your number is " + _num_nund[number / 100 - 1] + " hundred");
                    }
                    else
                    {
                        await _next.Invoke(context);
                        result = context.Session.GetString("number");
                        await context.Response.WriteAsync("Your number is " + _num_nund[number / 100 - 1] + " hundred " + result);

                    }
                }

            }
            catch (Exception ex)
            {
                await context.Response.WriteAsync("Incorrect parameter 100 to 999" +" "+ ex.Message);
            }
        }
    }
}
