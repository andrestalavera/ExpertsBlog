using ExpertsBlog.Data;
using ExpertsBlog.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

// Créer un BUILDER pour configurer une application Web
var builder = WebApplication.CreateBuilder(args);

// On récupère la chaîne de connexion de la base de données
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// On ajoute ExpertsBlogDbContext dans les services. Par défaut, la durée de vie du DbContext est "Scoped"
// On injecte la configuration la configuration.
builder.Services.AddDbContext<ExpertsBlogDbContext>(options => options.UseSqlServer(connectionString));
// On ajoute Swagger
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

// On construit l'application Web
var app = builder.Build();

// On utilise Swagger (configuré plus haut)
app.UseSwagger();
// On utilise SwaggerUI et on configure l'emplacement du fichier de définition et de l'UI
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
// On utilise la redirection de HTTP vers HTTPS
app.UseHttpsRedirection();

ConfigureRoutesFor<BlogPost>(app, "BlogPosts");
ConfigureRoutesFor<Address>(app, "Addresses");
ConfigureRoutesFor<Category>(app, "Categories");
ConfigureRoutesFor<Tag>(app, "Tags");

// On exécute l'application web
app.Run();

// Configurer les routes pour (équivalent .NET d'une méthode privée dans la classe Program)
// Exemples pour BlogPost, applicable pour BlogPost, Address, Category, Tag
void ConfigureRoutesFor<TEntity>(WebApplication app, string routeBase)
    where TEntity : EntityBase
{
    // HTTP GET https://expertsblogapi.azurewebsites.net/BlogPosts
    app.MapGet(routeBase, async (ExpertsBlogDbContext context) => await context.Set<TEntity>().AsNoTracking().ToListAsync());

    // HTTP GET https://expertsblogapi.azurewebsites.net/BlogPosts/1
    app.MapGet($"{routeBase}/{{id}}", async (ExpertsBlogDbContext context, int id) => await context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(b => b.Id == id));

    // HTTP POST https://expertsblogapi.azurewebsites.net/BlogPosts
    app.MapPost(routeBase, async (ExpertsBlogDbContext context, [FromBody] TEntity entity) =>
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

    // HTTP PUT https://expertsblogapi.azurewebsites.net/BlogPosts/1
    app.MapPut($"{routeBase}/{{id?}}", async (ExpertsBlogDbContext context, int? id, [FromBody] TEntity entity) =>
    {
        if (entity is not null)
        {
            if (id.HasValue)
            {
                if (entity.Id == id)
                {
                    context.Set<TEntity>().Update(entity);
                    await context.SaveChangesAsync();
                }
            }
            throw new ArgumentNullException(nameof(id));
        }
        throw new ArgumentNullException(nameof(entity));
    });

    // HTTP DELETE https://expertsblogapi.azurewebsites.net/BlogPosts/1
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
        throw new ArgumentNullException(nameof(id));
    });
}