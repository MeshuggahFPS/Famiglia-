using FamigliaPlus.Api.Models;
using FamigliaPlus.Api.Utils;
using Microsoft.AspNetCore.Mvc;

namespace FamigliaPlus.Api.Validators
{
    public static class FamilyMemberValidator
    {
        public static ActionResult ValidateGetAllFamilyMembers(List<FamilyMember> familyMembers)
        {
            if (!familyMembers.Any())
            {
                return new NoContentResult();
            }

            return null;
        }

        public static ActionResult ValidateFamilyMember(
            FamilyMember member,
            List<FamilyMember> existingMembers
        )
        {
            if (member == null)
            {
                return new BadRequestObjectResult(ApiMessages.FamilyMember.NullInput());
            }

            InputSanitizer.Clean(member);

            var validationErrors = new List<string>();

            if (
                string.IsNullOrWhiteSpace(member.Name)
                || string.IsNullOrWhiteSpace(member.Relation.ToString())
                || string.IsNullOrWhiteSpace(member.Gender.ToString())
                || member.BirthDate == default
            )
            {
                validationErrors.Add(ApiMessages.FamilyMember.ValueRequired(member.Name));
            }
            else if (
                existingMembers.Any(m =>
                    m.Name.Equals(member.Name, StringComparison.OrdinalIgnoreCase)
                )
            )
            {
                validationErrors.Add(ApiMessages.FamilyMember.DuplicateName(member.Name));
            }

            if (!Enum.IsDefined(typeof(Relation), member.Relation))
            {
                validationErrors.Add(ApiMessages.FamilyMember.InvalidRelationType());
            }

            if (!Enum.IsDefined(typeof(Gender), member.Gender))
            {
                validationErrors.Add(ApiMessages.FamilyMember.InvalidGender());
            }

            if (member.BirthDate == default)
            {
                validationErrors.Add(ApiMessages.FamilyMember.BirthDateRequired());
            }
            else if (member.BirthDate > DateTime.UtcNow)
            {
                validationErrors.Add(ApiMessages.FamilyMember.BirthDateInFuture());
            }

            if (validationErrors.Any())
            {
                return new BadRequestObjectResult(
                    ApiMessages.FamilyMember.InvalidInput(string.Join(", ", validationErrors))
                );
            }

            return null;
        }

        public static ActionResult ValidateGetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return new BadRequestObjectResult(ApiMessages.FamilyMember.NameRequired());
            }

            name = name.Trim();

            if (name.Any(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c)))
            {
                return new BadRequestObjectResult(ApiMessages.FamilyMember.InvalidNameCharacters());
            }

            if (name.Length > 100)
            {
                return new BadRequestObjectResult(ApiMessages.FamilyMember.NameTooLong());
            }

            return null;
        }
    }
}
