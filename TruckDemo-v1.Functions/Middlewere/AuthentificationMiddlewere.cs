using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruckDemo.Function.Middlewere
{
    public class AuthentificationMiddlewere : Attribute
    {
        private readonly string _requiredRole;

        public AuthentificationMiddlewere(string requiredRole)
        {
            _requiredRole = requiredRole;
        }

        public async Task InvokeAsync(FunctionContext context, FunctionExecutionDelegate next)
        {
            if (context.BindingContext.BindingData.TryGetValue("Headers", out var headers) && headers is IDictionary<string, string> headerDictionary)
            {
                if (headerDictionary.TryGetValue("Authorization", out string authorization) && authorization.StartsWith("Bearer "))
                {
                    var tokenString = authorization.Substring("Bearer ".Length);

                    try
                    {
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var validationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ClockSkew = TimeSpan.Zero
                        };

                        var principal = tokenHandler.ValidateToken(tokenString, validationParameters, out _);

                        // Verifica los roles requeridos
                        if (principal.IsInRole(_requiredRole))
                        {
                            // El token es válido y el usuario tiene el rol requerido
                            await next(context);
                            return;
                        }
                    }
                    catch (SecurityTokenException)
                    {
                        // El token no es válido
                    }
                }
            }

            // El token no se proporcionó o no tiene el formato adecuado, o el usuario no tiene el rol requerido
            var response = context.GetHttpResponseData();
            response!.StatusCode = System.Net.HttpStatusCode.Unauthorized;
            await response.WriteStringAsync("Unauthorized");
        }

    }
}
