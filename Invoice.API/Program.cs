
using Invoice.Application.Interfaces.Services;
using Invoice.Application.Services;
using Invoice.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ==========================================================
// 1) FileHelper image path & URL
// ==========================================================
var imageFolderPath = builder.Configuration["FileSettings:ImageFolderPath"]
                      ?? "/srv/assets/v2/";

var baseUrl = builder.Configuration["FileSettings:BaseUrl"]
              ?? "https://portalcdn.sca-cis.com/v2/";

if (!imageFolderPath.EndsWith(Path.DirectorySeparatorChar) && !imageFolderPath.EndsWith("/"))
    imageFolderPath += Path.DirectorySeparatorChar;

if (!baseUrl.EndsWith("/"))
    baseUrl += "/";


// ==========================================================
// 2) DB Context
// ==========================================================
var connStr = builder.Configuration.GetConnectionString("LiveOracleConnection");
builder.Services.AddDbContext<ModelContext>(options =>
    options.UseOracle(connStr, b => b.MigrationsAssembly(typeof(ModelContext).Assembly.FullName))
);

// ==========================================================
// Infrastructure
// ==========================================================


// ==========================================================
// HttpContextAccessor
// ==========================================================
builder.Services.AddHttpContextAccessor();

// ==========================================================
// Controllers & JSON
// ==========================================================

builder.Services.AddControllers();

// ==========================================================
// Swagger
// ==========================================================
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "SCA Invoice API", Version = "v1" });

    // ===== JWT Auth =====
    var jwtScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        Description = "Enter JWT like: Bearer {token}"
    };

    c.AddSecurityDefinition("Bearer", jwtScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

    // Accept-Language

    c.AddSecurityDefinition("Accept-Language", new OpenApiSecurityScheme
    {
        Name = "Accept-Language",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Description = "Specify the language (e.g., en or ar)"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Accept-Language"
                }
            },
            new List<string>()
        }
    });

    // DateOnly Mapping
    c.MapType<DateOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date"
    });

    c.MapType<DateOnly?>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date"
    });
});


builder.Services.AddAuthorization();

// ==========================================================
// CORS
// ==========================================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });

});

// ==========================================================
// App Services
// ==========================================================
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();

builder.Services.AddScoped<IInvoiceService, InvoiceService>();

// ==========================================================
// Build App
// ==========================================================
var app = builder.Build();



// ==========================================================
// Middleware
// ==========================================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();