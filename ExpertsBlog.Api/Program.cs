using ExpertsBlog.Entities;
using ExpertsBlog.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ExpertsBlogDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Experts Blog API",
        Description = "An API for Experts Blog",
        Contact = new OpenApiContact
        {
            Name = "Contact Andrés Talavera",
            Url = new Uri("https://linkedin.com/in/andres-talavera")
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

ConfigureRoutesFor<BlogPost>(app, "BlogPosts");
ConfigureRoutesFor<Address>(app, "Addresses");
ConfigureRoutesFor<Category>(app, "Categories");
ConfigureRoutesFor<Tag>(app, "Tags");

app.Run();

void ConfigureRoutesFor<TEntity>(WebApplication app, string routeBase)
    where TEntity : EntityBase
{
    app.MapGet(routeBase, async (ExpertsBlogDbContext context) => await context.Set<TEntity>().AsNoTracking().ToListAsync());
    app.MapGet($"{routeBase}/{{id}}", async (ExpertsBlogDbContext context, int id) => await context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(b => b.Id == id));
    

    app.MapPost(routeBase, async (ExpertsBlogDbContext context, [FromBody]TEntity entity) =>
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
    app.MapPut($"{routeBase}/{{id}}", async (ExpertsBlogDbContext context, int id, [FromBody]TEntity entity) =>
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
    app.MapDelete($"{routeBase}/{{id?}}", async (ExpertsBlogDbContext context, int? id) =>
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