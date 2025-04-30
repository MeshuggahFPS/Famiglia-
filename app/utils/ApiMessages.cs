namespace FamigliaPlus.Api.Utils
{
    public static class ApiMessages
    {
        public static class FamilyMember
        {
            public static string NotFound(string name) =>
                $"Family member with name '{name}' was not found.";

            public static string DuplicateName(string name) =>
                $"A family member with name '{name}' already exists.";

            public static string ValueRequired(string value) => $"{value} is required for family member.";

            public static string NameRequired() => "Name is required for family member.";

            public static string NullInput() => "Family member cannot be null.";

            public static string InvalidRelationType() => "Invalid relation type specified.";

            public static string InvalidGender() => "Invalid gender specified.";

            public static string BirthDateRequired() => "Birth date is required.";

            public static string BirthDateInFuture() => "Birth date cannot be in the future.";

            public static string InvalidNameCharacters() => "Name can only contain letters, numbers and spaces.";

            public static string DataError() =>
                "An error occurred while retrieving the data.";

            public static string Unauthorized() => "Please login to access this resource.";

            public static string Forbidden() => "You do not have permission to access this resource.";

            public static string Unexpected() => "An unexpected error occurred.";

            public static string TooManyRequests() => "Too many requests. Please try again later.";

            public static string InvalidInput(string message) =>
                $"Invalid input: {message}";

            public static string NameTooLong() => "Name cannot be longer than 100 characters";
        }

        public static class ShoppingList
        {
            public static string NotFound(int id) =>
                $"Boodschappenlijst met ID {id} is niet gevonden.";

            public static string NullInput() => "Je moet wel een lijstje meesturen.";
        }
    }
}
