namespace WebAPI.Models
{
    public class Person
    {
        public Person()
        {
        }

        public Person(Guid id, string name, string surname, string email, int phoneNumber)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public int PhoneNumber { get; set; }
    }
}