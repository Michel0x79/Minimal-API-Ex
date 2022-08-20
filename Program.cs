using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore;
using MinhaApi.Models;
using MinhaApi.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase(databaseName: "database"));

builder.Services.AddScoped<AnimalsModel, AnimalsModel>();

// var options = new DbContextOptionsBuilder().UseInMemoryDatabase("database").Options;

var app = builder.Build();

app.MapGet("/animal", async (DataContext db) => {
    // using(var db = new DataContext(options)){
        return await db.Animal.ToListAsync();
        
});

app.MapGet("/animal/{id}", (int id, DataContext db) => {
    // using(var db = new DataContext(options)){
        return db.Animal.FindAsync(id);
    
});

app.MapPost("/animal", async ([FromBody] AnimalsModel animal, DataContext db) =>{
        // AnimalsModel modelo = new(){Name = animal.Name, Raca = animal.Raca};
        await db.Animal.AddAsync(animal);
        await db.SaveChangesAsync();
        return animal;
     
});

app.MapDelete("/animal/{id}", async (int id, DataContext db) => {
    // using(var db = new DataContext(options)){
    var res = await db.Animal.Include(r => r.Raca).FirstOrDefaultAsync(a => a.Id == id);
    if(res != null){
        db.Animal.Remove(res);
        await db.SaveChangesAsync();
        return " Removido(a)";
    }
    return "Animal nao encontrado!";
});

app.MapPut("/animal/{id}", async (int id, [FromBody] AnimalsModel upAnim, DataContext db) => {
    var res = await db.Animal.Include(r => r.Raca).FirstOrDefaultAsync(a => a.Id == id);
    if(res.Id == id){
        res.Name = upAnim.Name;
        res.Raca = upAnim.Raca;
        
        await db.SaveChangesAsync();
        return upAnim.Name;
        }
    return "Error.1";
    
});

// Requisicoes para racas dos animais //

app.MapPost("/raca", async ( RacaModel raca, DataContext db) =>{
        db.Racas.Add(raca);
        await db.SaveChangesAsync();
        return Results.Created($"/raca/{raca.Id}", raca);
     
});

app.MapGet("/raca", async (DataContext db) => {
        return await db.Racas.AsNoTracking().ToListAsync();
});

app.MapGet("/raca/{id}", async (int id, DataContext db) => {
        return await db.Racas.FindAsync(id) is RacaModel raca ? Results.Ok(raca) : Results.NotFound();
});

app.MapDelete("/raca/{id}", async (int id, DataContext db) => {
    var res = await db.Racas.FirstOrDefaultAsync(a => a.Id == id);
    if(res != null){
        db.Racas.Remove(res);
        await db.SaveChangesAsync();
        return " Removido(a)";
    }
    return "Raca não encontrado!";
});

app.MapPut("/raca/{id}", async (int id, [FromBody] RacaModel upRaca, DataContext db) => {
    var res = await db.Racas.FirstOrDefaultAsync(r => r.Id == id);
    if(res.Id == id){
        res.RacaName = upRaca.RacaName;
        
        await db.SaveChangesAsync();
        return upRaca.RacaName;
    }
    return "Error.1";
    
});

app.MapGet("/racasanimais", async (DataContext db) => 
await db.Racas.Include(a => a.Animais).ToListAsync() );
// .Produces<List<AnimalsModel>>(StatusCodes.Status200OK);

app.Run();
