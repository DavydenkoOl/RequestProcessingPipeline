namespace RequestProcessingPipeline
{
    public class From1thousandTo10thousandMidleware
    {
        private readonly RequestDelegate _next;

        public From1thousandTo10thousandMidleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Query["number"];
            string[] _num_thousand = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten" };
            string? result = string.Empty;
            try
            {
                int number = Convert.ToInt32(token);
                number = Math.Abs(number);
                
                if(number>1000)
                {
                    
                    if (number > 11000 && number < 20000)
                    {
                        await _next.Invoke(context);
                        result = context.Session.GetString("number");
                        context.Session.SetString("number", result);
                    }
                    else if (number > 20_000)
                    {
                        number %= 10000;
                        if (number / 1000 == 0)
                        {
                            await _next.Invoke(context);
                            result = context.Session.GetString("number");
                            context.Session.SetString("number", "thousand " + result);
                        }
                        else if (number % 1000 == 0)
                        {
                            result = context.Session.GetString("number");
                            context.Session.SetString("number", _num_thousand[number / 1000 - 1] + " thousands ");
                        }
                        
                        else
                        {
                            await _next.Invoke(context);
                            result = context.Session.GetString("number");
                            context.Session.SetString("number", _num_thousand[number / 1000 - 1] + " thousands " + result);
                        }
                    }
                    else
                    {
                        if (number % 1000 == 0)
                        {
                            result = context.Session.GetString("number");
                            await context.Response.WriteAsync("Your number is " + _num_thousand[number / 1000 - 1] + " thousand ");
                        }
                        else
                        {
                            await _next.Invoke(context);
                            result = context.Session.GetString("number");
                            await context.Response.WriteAsync("Your number is " + _num_thousand[number / 1000 - 1] + " thousand" + " " + result);

                        }
                    }

                }
                else 
                {
                    await _next.Invoke(context);
                }

            }
            catch (Exception ex)
            {
                await context.Response.WriteAsync("Incorrect parameter 1000 to 10000" + " " + ex.Message);
            }
        }
    }
}
