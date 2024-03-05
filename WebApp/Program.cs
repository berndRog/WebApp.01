using Microsoft.AspNetCore.HttpLogging;
using WebApp;

var people =  new List<Person>() {
   new Person { Id = 1, Name = "Erika Mustermann" },
   new Person { Id = 2, Name = "Max Mustermann" }
};

// WebApplication Builder Pattern
var builder = WebApplication.CreateBuilder(args);

// add http logging 
builder.Services.AddHttpLogging(opts =>
   opts.LoggingFields = HttpLoggingFields.All);

// Build the WebApplication
var app = builder.Build();

// use http logging
app.UseHttpLogging();

// Routing static web pages (static files)
// http://localhost:5100 or https://localhost:5100/
// app.MapGet("/", () => {
//    var filePath = Path.Combine(app.Environment.WebRootPath, "index.html");
//    return Results.Content(File.ReadAllText(filePath), "text/html");
// });
app.MapGet("/",      () => GetStaticFiles(app, "index.html"));
// http://localhost:5100/index.html
app.MapGet("/index", () => GetStaticFiles(app, "index.html"));
// http://localhost:5010/page1
// http://localhost:5010/page2
app.MapGet("/page1", () => GetStaticFiles(app, "page1.html"));
app.MapGet("/page2", () => GetStaticFiles(app, "page2.html"));


// Run the WebApplication
app.Run();

// method to get static files
IResult GetStaticFiles(WebApplication app, string page) {
   string filePath = Path.Combine(app.Environment.WebRootPath, page);
   string content = File.ReadAllText(filePath);
   return Results.Content(content, "text/html");
}
