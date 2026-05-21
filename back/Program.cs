using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using back.Repositories;
using back.Services;

var builder = WebApplication.CreateBuilder(args);

// =========================================================
// 1. ตั้งค่า CORS (อนุญาต Angular ให้เข้ามาคุยด้วยได้)
// =========================================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://localhost:4200") // Angular dev server
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// =========================================================
// 2. ลงทะเบียน Services & Repositories (Dependency Injection)
// =========================================================
builder.Services.AddSingleton<IPrimeHistoryRepository, PrimeHistoryRepository>();
builder.Services.AddSingleton<IAtmHistoryRepository, AtmHistoryRepository>();
builder.Services.AddScoped<IPrimeService, PrimeService>();
builder.Services.AddScoped<IAtmService, AtmService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// =========================================================
// 3. ตั้งค่า Swagger (เพิ่มปุ่ม Authorize ไว้ใส่ Token เทสต์ API)
// =========================================================
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "type token in the format: Bearer {your token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(doc => new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference("Bearer", hostDocument: doc, externalResource: null),
            new List<string>()
        }
    });
});

// =========================================================
// 4. ตั้งค่าระบบตรวจตั๋ว Token (JWT Authentication)
// =========================================================
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKeyString = jwtSettings["SecretKey"];

// ดักจับ Error เผื่อว่าลืมใส่การตั้งค่าใน appsettings.json
if (string.IsNullOrEmpty(secretKeyString))
{
    throw new InvalidOperationException("JwtSettings:SecretKey is not configured in appsettings.json");
}

var secretKey = Encoding.ASCII.GetBytes(secretKeyString);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"],
        ValidateLifetime = true, // ตรวจวันหมดอายุของ Token ด้วย
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// =========================================================
// 5. ลำดับของ Middleware (การทำงานของ Request Pipeline) 
// *ลำดับตรงนี้สำคัญมาก ห้ามสลับที่กันครับ*
// =========================================================

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// In local development, allow HTTP to avoid self-signed HTTPS cert issues for SPA clients.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

// ด่าน 1: ตรวจสอบ CORS ก่อนเลยว่ามาจากเว็บ Angular หรือเปล่า
app.UseCors("AllowAngular");

// ด่าน 2: ตรวจบัตร (ว่ามี Token แนบมาใน Header ไหม และ Token ถูกต้องไหม)
app.UseAuthentication(); 

// ด่าน 3: ตรวจสิทธิ์ (ถ้ามี [Authorize] ถึงจะยอมให้เข้า Controllers)
app.UseAuthorization();  

// ด่าน 4: ส่งไปหา Controller เพื่อทำงานต่อ
app.MapControllers();

app.Run();
