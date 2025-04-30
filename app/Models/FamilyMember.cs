using FamigliaPlus.Api.Controllers;
using FamigliaPlus.Api.Utils;

namespace FamigliaPlus.Api.Models;

public class FamilyMember
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public Relation Relation { get; set; }
    public Gender Gender { get; set; }
    public int Age => CalculateAge.Age(BirthDate);

    public static FamilyMember GetFamilyMemberByName(string name)
    {
        var familyMembers = FamilyMembersController._familyMembers;
        return familyMembers.FirstOrDefault(m =>
            StringNormalizer.Normalize(m.Name) == StringNormalizer.Normalize(name)
        );
    }
}
