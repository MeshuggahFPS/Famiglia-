// System.Net bevat HTTP-gerelateerde klassen zoals HttpStatusCode
using System.Net;
// System.Text.Json wordt gebruikt voor JSON serialisatie/deserialisatie
using System.Text.Json;
// Importeert de ErrorResponse model klasse
using FamigliaPlus.Api.Models;

namespace FamigliaPlus.Api.Middleware
{
    // Middleware klasse die globale error handling verzorgt
    public class ErrorHandlingMiddleware
    {
        // RequestDelegate is een delegate die de volgende middleware in de pipeline aanroept
        // readonly omdat deze niet meer veranderd na initialisatie
        // _next is een RequestDelegate die de volgende middleware in de pipeline aanroept
        // Het wordt gebruikt om de request door te geven aan de volgende handler
        // readonly omdat het niet meer verandert na initialisatie in de constructor
        private readonly RequestDelegate _next;

        // ILogger interface voor logging functionaliteit, generiek getypeerd met deze klasse
        // readonly omdat deze niet meer veranderd na initialisatie
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        // Constructor die dependencies injecteert
        // next: delegate naar volgende middleware
        // logger: logging service
        public ErrorHandlingMiddleware(
            RequestDelegate next,
            ILogger<ErrorHandlingMiddleware> logger
        )
        {
            _next = next;
            _logger = logger;
        }

        // Deze methode wordt aangeroepen voor elke HTTP request
        // async omdat er async operaties plaatsvinden
        // HttpContext bevat alle info over de huidige HTTP request/response
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Roept de volgende middleware aan in de pipeline
                await _next(context);
            }
            catch (Exception ex)
            {
                // Logt de error met de ingebouwde logger
                _logger.LogError(ex, "Fout opgetreden");
                // Handelt de exception af door deze om te zetten naar een nette HTTP response
                await HandleExceptionAsync(context, ex);
            }
        }

        // Private helper methode om exceptions om te zetten naar HTTP responses
        // Static omdat deze geen instantie members gebruikt
        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            // Switch expression om het juiste HTTP status code te bepalen op basis van exception type
            var code = ex switch
            {
                UnauthorizedAccessException => HttpStatusCode.Unauthorized, // 401
                InvalidOperationException => HttpStatusCode.BadRequest, // 400
                ArgumentNullException => HttpStatusCode.BadRequest, // 400
                RateLimitExceededException => (HttpStatusCode)429, // 429 Too Many Requests
                _ => HttpStatusCode.InternalServerError, // 500 (default)
            };

            // Maakt een ErrorResponse object met relevante error informatie
            var response = new ErrorResponse
            {
                Message = "Er is iets misgegaan.",
                Details = ex.Message,
                Type = ex.GetType().Name,
            };

            // Serialiseert de response naar JSON
            var result = JsonSerializer.Serialize(response);
            // Stelt response headers in
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            // Schrijft de JSON response naar de output stream
            return context.Response.WriteAsync(result);
        }
    }

    // Custom exception klasse voor rate limiting
    // Erft over van de basis Exception klasse
    public class RateLimitExceededException : Exception
    {
        // Constructor die het error bericht doorgeeft aan de base Exception klasse
        public RateLimitExceededException(string message)
            : base(message) { }
    }
}
