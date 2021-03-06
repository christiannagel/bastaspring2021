using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

WebHost.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var settings = context.Configuration;
        services.AddDbContext<BooksContext>(options =>
        {
            options.UseSqlServer(settings.GetConnectionString("BooksConnection"));
        });
    })
    .Configure((context, app) =>
    {
        (IServiceScope, BooksContext) GetBooksContext()
        {
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<BooksContext>();
            return (scope, context);
        }

        if (context.HostingEnvironment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            (var scope, var booksContext) = GetBooksContext();
            using var _ = scope;
            booksContext.Database.EnsureCreated();
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
            endpoints.Map("/", async context =>
            {
                await context.Response.WriteAsync("<h2>Hello</h2>");
            });

        });
    })
    .Build().Run();

public class BooksContext : DbContext
{
    public BooksContext(DbContextOptions<BooksContext> options) : base(options) { }

    public DbSet<Book> Books => Set<Book>();
}

public record Book(
    [property: StringLength(50)] string Title,
    [property: StringLength(20)] string? Publisher,
    int Id = 0);