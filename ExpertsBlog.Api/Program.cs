using ExpertsBlog.Entities;
using ExpertsBlog.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// add services to DI container

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ExpertsBlogDbContext>(options => options.UseSqlServer(connectionString));
// builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Expoerts Blog API",
        Description = "An API for Experts Blog",
        TermsOfService = new Uri("https://ideastud.io/terms"),
        Contact = new OpenApiContact
        {
            Name = "Contact Andrés Talavera",
            Url = new Uri("https://linkedin.com/in/andres-talavera")
        },
        License = new OpenApiLicense
        {
            Name = "License",
            Url = new Uri("https://ideastud.io/license")
        }
    });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
app.UseHttpsRedirection();

ConfigureRoutesFor<Category>(app, "Categories");
ConfigureRoutesFor<BlogPost>(app, "BlogPosts");
ConfigureRoutesFor<Tag>(app, "Tags");
ConfigureRoutesFor<Address>(app, "Addresses");

app.Run();

void ConfigureRoutesFor<TEntity>(WebApplication app, string controllerName)
    where TEntity : EntityBase
{
    app.MapGet(controllerName, async (ExpertsBlogDbContext context) => await context.Set<TEntity>().AsNoTracking().ToListAsync());
    app.MapGet($"{controllerName}/{{id}}", async (ExpertsBlogDbContext context, int id) => await context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(b => b.Id == id));
    app.MapPost(controllerName, async (ExpertsBlogDbContext context, [FromBody]TEntity entity) =>
    {
        if (entity is not null)
        {
            context.Set<TEntity>().Add(entity);

            //TODO: Check if value exists before creation
            // Expression<Func>?
            await context.SaveChangesAsync();
        }
        throw new ArgumentNullException(nameof(entity));
    });
    app.MapPut($"{controllerName}/{{id}}", async (ExpertsBlogDbContext context, int id, [FromBody]TEntity entity) =>
    {
        if (entity is not null)
        {
            if (entity.Id == id)
            {
                context.Set<TEntity>().Update(entity);
                await context.SaveChangesAsync();
            }
        }
        throw new ArgumentNullException(nameof(entity));
    });
    app.MapDelete($"{controllerName}/{{id?}}", async (ExpertsBlogDbContext context, int? id) =>
    {
        if (id.HasValue)
        {
            TEntity entity = await context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(b => b.Id == id);
            if (entity is not null)
            {
                context.Set<TEntity>().Remove(entity);
                await context.SaveChangesAsync();
            }
        }
        throw new ArgumentNullException();
    });
}