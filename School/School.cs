namespace School
{
    public class School
    {
        public School()
        {
            Classes = new List<SchoolClass>();
        }

        public IList<SchoolClass> Classes { get; private set; }

        public void AddClass(SchoolClass currentClass)
        {
            Classes.Add(currentClass);
        }

        public async Task Open()
        {
            foreach (var schoolClass in Classes)
            {
                await schoolClass.Open();
            }
        }
    }
}
