var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*****************************************************
     * HABILITAR CORS
     *****************************************************/
builder.Services.AddCors(p => p.AddPolicy("Todos", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    //build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));
/*****************************************************
 * FIN HABILITAR CORS
 *****************************************************/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

/*****************************************************
    * HABILITAR CORS
    *****************************************************/
app.UseCors("Todos");
/*****************************************************
 * FIN HABILITAR CORS
 *****************************************************/

app.MapControllers();

app.Run();
