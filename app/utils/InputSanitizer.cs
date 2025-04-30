using FamigliaPlus.Api.Models;

namespace FamigliaPlus.Api.Utils
{
    public static class InputSanitizer
    {
        public static void Clean(FamilyMember member)
        {
            member.Name = member.Name?.Trim();
            member.Relation = member.Relation;
            member.Gender = member.Gender;
            member.BirthDate = member.BirthDate.Date;
        }
    }
}