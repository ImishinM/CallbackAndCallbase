namespace School
{
    public class Teacher : Person
    {
        public Teacher(string name)
        {
            Name = name;
            Comments = new List<string>();
        }
    }
}
