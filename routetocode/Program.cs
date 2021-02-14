using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(builder =>
    {
        builder.UseStartup<Startup>();
    }).Build().Run();

class Startup
{
    public Startup(IConfiguration configuration) => Configuration = configuration;

    public IConfiguration Configuration { get; }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<BooksContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("BooksConnection"));
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        (IServiceScope, BooksContext) GetBooksContext()
        {
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<BooksContext>();
            return (scope, context);
        }

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            (var scope, var booksContext) = GetBooksContext();
            using (scope)
            {

                booksContext.Database.EnsureCreated();
            }
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapPost("/api/books", async context =>
            {
                (var scope, var booksContext) = GetBooksContext();
                using var _ = scope;
                if (!context.Request.HasJsonContentType())
                {
                    context.Response.StatusCode = StatusCodes.Status415UnsupportedMediaType;
                    return;
                };
                var book = await context.Request.ReadFromJsonAsync<Book>();
                if (book == null)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    return;
                }
                booksContext.Books.Add(book);
                await booksContext.SaveChangesAsync();
                await context.Response.WriteAsJsonAsync(book);
            });
            endpoints.MapGet("/api/books", async context =>
            {
                (var scope, var booksContext) = GetBooksContext();
                using var _ = scope;
                var books = await booksContext.Books.ToListAsync();
                await context.Response.WriteAsJsonAsync(books);
            });
        });
    }
}

public class BooksContext : DbContext
{
    public BooksContext(DbContextOptions<BooksContext> options) : base(options) { }

    public DbSet<Book> Books => Set<Book>();
}

public record Book(
    [StringLength(50)] string Title,
    [StringLength(20)] string? Publisher,
    int Id = 0);