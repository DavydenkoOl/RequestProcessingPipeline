namespace RequestProcessingPipeline
{
    public class From11thousendTo20thousandMidleware
    {
        readonly RequestDelegate _next;
        public From11thousendTo20thousandMidleware(RequestDelegate next)  
        {
            this._next = next; 
        }
        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Query["number"];
            try
            {
                int number = Convert.ToInt32(token);
                number = Math.Abs(number);
                
                 if (number > 10999 && number <= 19999)
                {
                    string[] _num_thousand = { "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };

                    if (number % 1000 == 0)
                    {
                        await context.Response.WriteAsync("Your number is " + _num_thousand[number / 1000 - 11] + " thousand");
                    }
                    else
                    {
                        await _next.Invoke(context);
                        string? result = string.Empty;
                        result = context.Session.GetString("number");
                        await context.Response.WriteAsync("Your number is " + _num_thousand[number / 1000 - 11] + " thousand " + result);
                    }
                }
                else if (number < 11000)
                {
                    await _next.Invoke(context);
                }
                else if (number > 20000)
                {
                    await _next.Invoke(context);
                }

            }
            catch (Exception ex)
            {
                await context.Response.WriteAsync("Incorrect parameter 11000 to 19000" + " " + ex.Message);
            }
        }
    }
}
