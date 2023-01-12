namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        // ATTENTION - This formula may skip leap years but it is much simpler
        // and much easier to understand than the DateTime formula
        public static int CalculateAge(this DateOnly dateOfBirth)
        {
            // Get the today's time using DateOnly, much preferred
            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var age = today.Year - dateOfBirth.Year;

            // If the date of birth is bigger than today we take off a year from age
            // As in they haven't had their birthday yet so we take a year off them
            if (dateOfBirth > today.AddYears(-age))
            {
                // 23/01/1998 > 09/01/2023(-25) = // 23/01/1998 > 09/01/1998 then we lower
                // 23/01/1998 > 25/01/2023(-25) = // 23/01/1998 > 25/01/1998 we do nothing
                age--;
            }

            return age;
        }
    }
}