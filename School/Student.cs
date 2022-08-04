namespace School
{
    public class Student : Person
    {
        public Student(string name, string id)
        {
            Name = name;
            Id = id;
        }

        public string Id { get; private set; }
    }
}
