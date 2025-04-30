using FamigliaPlus.Api.Models;
using FamigliaPlus.Api.Validators;
using Microsoft.AspNetCore.Mvc;

namespace FamigliaPlus.Api.Controllers
{
    [ApiController]
    [Route("api/familymembers")]
    public class FamilyMembersController : ControllerBase
    {
        public static List<FamilyMember> _familyMembers = new();

        [HttpGet]
        public ActionResult<IEnumerable<FamilyMember>> GetAllFamilyMembers()
        {
            var getAllMembersValidation = FamilyMemberValidator.ValidateGetAllFamilyMembers(
                _familyMembers
            );
            if (getAllMembersValidation != null)
            {
                return getAllMembersValidation;
            }

            return Ok(_familyMembers);
        }

        [HttpGet("{name}")]
        public ActionResult<FamilyMember> GetFamilyMemberByName(string name)
        {
            // We roepen een statische methode aan om het familielid te zoeken
            var nameValidation = FamilyMemberValidator.ValidateGetByName(name);
            if (nameValidation != null)
            {
                return nameValidation;
            }

            var familyMember = FamilyMember.GetFamilyMemberByName(name);
            return Ok(familyMember);
        }

        [HttpPost]
        public ActionResult<FamilyMember> CreateFamilyMember(FamilyMember member)
        {
            var memberValidation = FamilyMemberValidator.ValidateFamilyMember(
                member,
                _familyMembers
            );
            if (memberValidation != null)
            {
                return memberValidation;
            }

            member.Id = _familyMembers.Count + 1;
            _familyMembers.Add(member);

            return CreatedAtAction(
                nameof(GetFamilyMemberByName),
                new { name = member.Name },
                member
            );
        }
    }
}
