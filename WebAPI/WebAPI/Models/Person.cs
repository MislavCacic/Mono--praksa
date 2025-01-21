namespace WebAPI.Models
{
    public class Person
    {

        public Person(int id, string name, string surname)
        {
            Id = id;
            Name = name;
            Surname = surname;
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
    }
}