using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TodoApp.Infra.CommandsHandler;
using TodoAppLogin.Infra.Context;
using TodoAppLogin.Infra.Repositories;
using TodoAppLogin.Web.Services;

namespace TodoAppLogin.Api.Extensions;

public static class SwaggerExtensions
{
  public static IServiceCollection SwaggerConfiguration(this IServiceCollection services)
  {
    services.AddSwaggerGen(c => 
      {
          c.ResolveConflictingActions(apiDescriptions =>
          {
              var descriptions = apiDescriptions as ApiDescription[] ?? apiDescriptions.ToArray();
              var first = descriptions.First(); // build relative to the 1st method
              var parameters = descriptions.SelectMany(d => d.ParameterDescriptions).ToList();

              first.ParameterDescriptions.Clear();
              // add parameters and make them optional
              foreach (var parameter in parameters)
                  if (first.ParameterDescriptions.All(x => x.Name != parameter.Name))
                  {
                      first.ParameterDescriptions.Add(new ApiParameterDescription
                      {
                          ModelMetadata = parameter.ModelMetadata,
                          Name = parameter.Name,
                          ParameterDescriptor = parameter.ParameterDescriptor,
                          Source = parameter.Source,
                          IsRequired = false,
                          DefaultValue = null
                      });
                  }
              return first;
          });
          c.SwaggerDoc("v1", new OpenApiInfo { Title = "apiagenda", Version = "v1" });

                      c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() 
                      { 
                          Name = "Authorization", 
                          Type = SecuritySchemeType.ApiKey, 
                          Scheme = "Bearer", 
                          BearerFormat = "JWT", 
                          In = ParameterLocation.Header, 
                          Description = @"JWT Authorization header using the Bearer scheme.
                        \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.
                          \r\n\r\nExample: \Bearer", 
                      }); 
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
      });
    return services;
  }
}
public static class ApiExtensions
{
  public static WebApplicationBuilder ApiConfiguration(this WebApplicationBuilder builder)
  {
    TokenService.JwtKey = builder.Configuration.GetValue<string>("JwtKey");
    // builder.Services.AddDbContext<UserDbContext>(opt =>
    //   opt.UseSqlServer(builder.Configuration.GetConnectionString("default"), b => b.MigrationsAssembly("TodoAppLogin.Api")).LogTo(Console.WriteLine, LogLevel.Information)
    // );
    // builder.Services.AddScoped<LoginCommandHandler>();
    // builder.Services.AddScoped<UserRepository>();

    builder.Services.AddControllers();
    builder.Services.AddMvc();
    return builder;
  }
}

public static class IdentityExtensions
{

    public static IServiceCollection IdentityConfiguration(this IServiceCollection services)
    {

      
      services.AddAuthentication(x =>
      {
          x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
          x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(x =>
      {
          x.TokenValidationParameters = new TokenValidationParameters
          {
              ValidateIssuerSigningKey = true,
              IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenService.JwtKey)),
              ValidateIssuer = false,
              ValidateAudience = false
          };
      });
    return services;
  }
}