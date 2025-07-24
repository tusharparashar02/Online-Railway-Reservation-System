using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
// using Serilog;
// using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure Serilog
// var logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "Car_Wash.txt");
// Directory.CreateDirectory(Path.GetDirectoryName(logFilePath)!);

// var logger = new LoggerConfiguration()
//     .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
//     .MinimumLevel.Information()
//     .CreateLogger();

// builder.Host.UseSerilog(); // Add this line

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen();

builder.Services.AddScoped<JwtToken>();
builder.Services.AddScoped<ITrainRepository, TrainRepository>();
builder.Services.AddScoped<ITrainService, TrainService>();
// builder.Services.AddScoped<ITrainScheduleRepository, TrainScheduleRepository>();
// builder.Services.AddScoped<ITrainScheduleService, TrainScheduleService>();
builder.Services.AddScoped<IPassengerDetailRepository, PassengerDetailRepository>();
builder.Services.AddScoped<IPassengerDetailService, PassengerDetailService>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IEmailService, SmtpEmailService>();
builder.Services.AddScoped<ICateringOrderRepository, CateringOrderRepository>();
builder.Services.AddScoped<ICateringOrderService, CateringOrderService>();

builder.Services.AddScoped<IWellnessKitRequestRepository, WellnessKitRequestRepository>();
builder.Services.AddScoped<IWellnessKitRequestService, WellnessKitRequestService>();



builder
    .Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

//Add database
builder.Services.AddDbContext<RailwayDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// builder.Services.AddAutoMapper(typeof(AutoMappersProfile));
builder.Services.AddAutoMapper(typeof(MappingProfile));


// //add containers



builder.Services.AddHttpContextAccessor();

// builder.Services.AddScoped<IUserRepository, UserRepository>();
// builder.Services.AddScoped<UserService>();

//add service to conatiner
//builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
    
//Enable JWT Authorization in swagger UI
    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter 'Bearer {token} to authenticate",
        }
    );
    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer",
                    },
                },
                Array.Empty<string>()
            },
        }
    );
});
builder.WebHost.UseKestrel();

//Add identity
builder
    .Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<RailwayDbContext>()
    .AddDefaultTokenProviders();

//Add Authentication (Jwt)
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
        ),
        NameClaimType = ClaimTypes.NameIdentifier // ✅ This is what was missing
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseSerilogRequestLogging();
//app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();
app.UseCors("AllowAll"); 
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.Run();
