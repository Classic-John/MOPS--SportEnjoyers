using Mops_fullstack.Server.Core;
using Mops_fullstack.Server.Core.Services;
using Mops_fullstack.Server.Datalayer.Interfaces;
using Mops_fullstack.Server.Datalayer.Repositories;
using Mops_fullstack.Server.Datalayer.Service_interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<FieldRepo>();
builder.Services.AddScoped<GroupRepo>();
builder.Services.AddScoped<MatchRepo>();
builder.Services.AddScoped<MessageRepo>();
builder.Services.AddScoped<OwnerRepo>();
builder.Services.AddScoped<PlayerRepo>();
builder.Services.AddScoped<ThreadRepo>();

builder.Services.AddScoped<UnitOfWork>();

builder.Services.AddScoped<IFieldService, FieldService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IMatchService, MatchService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IOwnerService, OwnerService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IThreadService, ThreadService>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

/* Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
*/
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
