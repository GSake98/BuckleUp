namespace API.DTOs
{
    public class MemberUpdateDto
    {
        // All the properties that we want to upgrade
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}