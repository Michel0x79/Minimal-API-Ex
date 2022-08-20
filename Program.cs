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

// List<AnimalsModel> animais = new(){
//     new(){Id = Guid.NewGuid(), Name = "Fred", Raca = "Gato"},
//     new(){Id = Guid.NewGuid(), Name = "Choquinho", Raca = "Gato"},
//     new(){Id = Guid.NewGuid(), Name = "Estrela", Raca = "Cachorro"}
// };

app.MapGet("/animal", async (DataContext db) => {
    // using(var db = new DataContext(options)){
        return await db.Animal.ToListAsync();
        
});

app.MapGet("/animal/{id}", (Guid id, DataContext db) => {
    // using(var db = new DataContext(options)){
        return db.Animal.FindAsync(id);
    
});

app.MapPost("/animal", async ([FromBody] AnimalsModel animal, DataContext db) =>{
    //  using(var db = new DataContext(options)){
        await db.Animal.AddAsync(animal);
        await db.SaveChangesAsync();
        return animal;
     
});

app.MapDelete("/animal/{id}", async (Guid id, DataContext db) => {
    // using(var db = new DataContext(options)){
    var res = await db.Animal.FirstOrDefaultAsync(a => a.Id == id);
    if(res != null){
        db.Animal.Remove(res);
        await db.SaveChangesAsync();
        return " Removido(a)";
    }
    return "Animal nao encontrado!";
});

app.MapPost("/animal/{id}", async (Guid id, AnimalsModel upAnim, DataContext db) => {
    var res = await db.Animal.FirstOrDefaultAsync(a => a.Id == id);
    if(res.Id == id){
        res.Name = upAnim.Name;
        res.Raca = upAnim.Raca;
        
        await db.SaveChangesAsync();
        return upAnim.Name;
        }
    return "Error.1";
    
});

app.Run();
