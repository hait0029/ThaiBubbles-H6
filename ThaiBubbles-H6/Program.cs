var builder = WebApplication.CreateBuilder(args);
// Configure the database context with SQL Server
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
// Add services to the container
builder.Services.AddControllers();
builder.Services.AddScoped<ICityRepositories, CityRepositories>();
builder.Services.AddScoped<ILoginRepositories, LoginRepository>();
builder.Services.AddScoped<IOrderRepositories, OrderRepositories>();
builder.Services.AddScoped<IProductRepositories, ProductRepositories>();
builder.Services.AddScoped<IUserRepositories, UserRepository>();
builder.Services.AddScoped<IProductListRepositories, ProductListRepositories>();
builder.Services.AddScoped<IRoleRepositories, RoleRepository>();
builder.Services.AddScoped<ICategoryRepositories, CategoryRepositories>();
builder.Services.AddScoped<IFavoriteRepositories, FavoriteRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Configure CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("DarthVader", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// Add JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = false, // Set to true if you want to validate the issuer
            ValidateAudience = false, // Set to true if you want to validate the audience
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero // Optional: To make sure token expiration is accurate
        };
    });




var app = builder.Build();

// Apply migrations and seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<DatabaseContext>();
        context.Database.Migrate(); // Apply any pending migrations
        DatabaseContext.SeedData(context); // Seed initial data
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("DarthVader");
// Add the authentication middleware

app.UseAuthentication(); // <-- This middleware ensures authentication happens
app.UseAuthorization();

app.MapControllers();

app.Run();
