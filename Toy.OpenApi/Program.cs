using System.Reflection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Sample API",
        Description = "An ASP.NET Core Web API for managing sample items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
    
    options.SwaggerGeneratorOptions.Servers = new List<OpenApiServer>()
    {
        // set the urls folks can reach server
        new() {Url = "https://localhost:5496" }
    };

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    options.EnableAnnotations(
        enableAnnotationsForInheritance: true,
        enableAnnotationsForPolymorphism: true);
});
// Install-Package Swashbuckle.AspNetCore.Newtonsoft
builder.Services.AddSwaggerGenNewtonsoftSupport(); // explicit opt-in - needs to be placed after AddSwaggerGen()


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x => { x.SwaggerEndpoint("/swagger/v1/swagger.yaml", "Sample API"); });

    // install Swashbuckle.AspNetCore.ReDoc
    app.UseReDoc(c =>
    {
        c.DocumentTitle = "My API Docs";
        c.RoutePrefix = "docs";

        //c.SpecUrl("/v1/swagger.json");
        c.EnableUntrustedSpec();
        c.ScrollYOffset(10);
        c.HideHostname();
        c.HideDownloadButton();
        c.ExpandResponses("200,201");
        c.RequiredPropsFirst();
        c.NoAutoAuth();
        c.PathInMiddlePanel();
        c.HideLoading();
        c.NativeScrollbars();
        c.DisableSearch();
        c.OnlyRequiredInSamples();
        c.SortPropsAlphabetically();
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();