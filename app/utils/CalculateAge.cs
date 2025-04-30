namespace FamigliaPlus.Api.Utils;

public static class CalculateAge
{
    public static int Age(DateTime birthDate)
      {
        var today = DateTime.Today;
        var age = today.Year - birthDate.Year;
        if (birthDate > today.AddYears(-age)) age--;
        return age;
    }
}
