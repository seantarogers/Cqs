namespace Cqs.Infrastructure.Dto
{
    using System.ComponentModel;

    public class CreateCustomerDto : Dto
    {
        public int Id { get; set; }
        [DisplayName("First name")]
        public string FirstName { get; set; }
        [DisplayName("Last name")]
        public string LastName { get; set; }
        [DisplayName("Email address")]
        public string EmailAddress { get; set; }

        public string Password { get; set; }
    }
}