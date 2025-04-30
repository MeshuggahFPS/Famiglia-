using FamigliaPlus.Api.Models;

namespace FamigliaPlus.Api.Utils
{
    // Dit is een statische class - dat betekent dat je geen instantie hoeft te maken om de methods te gebruiken
    // Je kunt direct ErrorHelper.Build() aanroepen
    public static class ErrorHelper
    {
        // Dit is een statische method die een ErrorResponse object bouwt
        // Het accepteert een verplicht message parameter en een optionele Exception parameter (ex)
        // De '= null' betekent dat de ex parameter optioneel is
        public static ErrorResponse Build(string message, Exception ex = null)
        {
            // Hier maken we een nieuwe ErrorResponse aan met object initializer syntax
            return new ErrorResponse
            {
                // Het message argument wordt direct toegewezen
                Message = message,

                // ex?.Message gebruikt de null-conditional operator (?.)
                // Als ex null is, wordt Details null
                // Anders krijg je de Message property van de Exception
                Details = ex?.Message,

                // Dit gebruikt de null-conditional operator (?.) EN de null-coalescing operator (??)
                // Als ex null is OF GetType().Name null is, wordt "Error" gebruikt
                // Anders krijg je de naam van het Exception type
                Type = ex?.GetType().Name ?? "Error",
            };
        }
    }
}