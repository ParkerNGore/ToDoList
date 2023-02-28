using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Repositories;
using WebAPI.Repositories.Interfaces;
using WebAPI.Services.ListItems;
using WebAPI.Services.ListItems.Interfaces;
using WebAPI.Services.ListTypes;
using WebAPI.Services.ListTypes.interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<DataContext>(options =>
                options
                .UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options =>
    {
        options.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddScoped<IGetAllListsService, GetAllListsService>();
builder.Services.AddScoped<IUpdateListService, UpdateListItemService>();
builder.Services.AddScoped<IDeleteListService, DeleteListItemService>();
builder.Services.AddScoped<IFilterListItemsService, FilterListItemsService>();
builder.Services.AddScoped<ICreateListService, CreateListItemService>();
builder.Services.AddScoped<IGetListService, GetListService>();
builder.Services.AddScoped<IListItemService, ListItemService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IGetListTypeService, GetListTypeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowOrigin");

app.Run();
