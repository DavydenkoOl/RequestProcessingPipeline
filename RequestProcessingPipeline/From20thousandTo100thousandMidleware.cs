namespace RequestProcessingPipeline
{
    public class From20thousandTo100thousandMidleware
    {

        private readonly RequestDelegate _next;

        public From20thousandTo100thousandMidleware(RequestDelegate next)
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
                 if (number > 100000)
                {
                    await context.Response.WriteAsync("Number is greater then one hundred thousand");
                }
                else if(number >= 20000)
                {
                    string[] _num_then = { "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
                    if (number  == 100000)
                    {
                        await context.Response.WriteAsync("Your number is one hundred thousand");
                    }
                    
                    else if (number % 10000 == 0)
                    {
                        await context.Response.WriteAsync("Your number is " + _num_then[number / 10000 - 2] + " thousand");
                    }
                    else
                    {
                        await _next.Invoke(context);
                        string? result = string.Empty;
                        result = context.Session.GetString("number");
                        await context.Response.WriteAsync("Your number is " + _num_then[number / 10000 - 2] + " " + result);
                    }
                }
                else
                {
                    await _next.Invoke(context);
                }


            }
            catch (Exception ex)
            {
                await context.Response.WriteAsync("Incorrect parameter 20000 to 100000" + " " + ex.Message);
            }
        }
    }
}
