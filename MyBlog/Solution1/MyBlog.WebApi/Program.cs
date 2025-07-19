using MyBlog.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using MyBlog.Application.Interfaces;
using MyBlog.Application.Repositories;
using MyBlog.Application.Usecasess.ArticleServices;
using MyBlog.Application.Usecasess.CategoryServices;
using MyBlog.Application.Usecasess.CommentServices;
using MyBlog.Application.Usecasess.SubcategoryServices;
using MyBlog.Application.Usecasess.TechnologyServices;
using MyBlog.Application.Services.Jwt;
using MyBlog.Application.Usecasess.UserServices;
using Microsoft.AspNetCore.Identity;
using MyBlog.Domain.Entites;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISubcategoryService, SubcategoryService>();
builder.Services.AddScoped<ITechnologyService, TechnologyService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext DI
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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

app.Run();