var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

string time = "";

app.Use(async (context, next) =>
{
    time = DateTime.Now.ToLongTimeString();
    await next.Invoke();
});

app.Map("/time", appBuilder =>
{
    appBuilder.Use(async (context, next) =>
    {
        Console.WriteLine($"Time: {time}");
        await next.Invoke();
    });

    appBuilder.Run(async (context) =>
    {
        await context.Response.WriteAsync($"Time: {time}");
    });
});

app.MapWhen(
    context => context.Request.Path == "/time",
    appBuilder =>
    {
        appBuilder.Use(async (context, next) =>
        {
            Console.WriteLine($"Time: {time}");
            await next.Invoke();
        });

        appBuilder.Run(async (context) =>
        {
            await context.Response.WriteAsync($"Time: {time}");
        });
    });

app.Run(async (context) =>
{
    await context.Response.WriteAsync($"Hello world");
});


//string date = "";

//app.Use(GetDate);

/*
app.UseWhen((context) => context.Request.Path == "/date",
    appBuilder =>
    {
        appBuilder.Use(async (context, next) =>
        {
            date = DateTime.Now.ToShortDateString();
            Console.WriteLine($"Date: {date}");
            await next.Invoke();
            
        });
    });

app.Run(async (context) =>
{
    //await Task.Delay(10000);
    //await context.Response.WriteAsync("<p>Good by world</p>");
    await context.Response.WriteAsync($"Hello world {date}");
    date = "";
});
*/

/*         
app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;

    response.ContentType = "text/html; charset=utf-8";

    if (request.Path == "/upload" && request.Method == "POST")
    {
        var files = request.Form.Files;
        var uploadPath = $"{Directory.GetCurrentDirectory()}/uploads";
        Directory.CreateDirectory(uploadPath);

        foreach (var file in files)
        {
            string fullPath = $"{uploadPath}/{file.FileName}";

            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }
        await response.WriteAsync("Files is uploads");
    }
    else
        await response.SendFileAsync("html/index.html");
});
*/

app.Run();


/*
async Task GetDate(HttpContext context, RequestDelegate next)
{
    date = DateTime.Now.ToShortDateString();
    //await context.Response.WriteAsync("<p>Hello world</p>");
    if (context.Request.Path.Value?.ToLower() == "/date")
        await context.Response.WriteAsync($"Current date: {date}");
    else
        await next.Invoke(context);

    //Console.WriteLine(date);
}
*/