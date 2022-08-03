namespace School
{
    public class SchoolClass : ICommentable
    {
        public SchoolClass(string id)
        {
            Students = new List<Student>();
            Teachers = new List<Teacher>();
            Comments = new List<string>();
            Id = id;
        }

        public ICollection<Student> Students { get; private set; }

        public ICollection<Teacher> Teachers { get; private set; }

        public string WhiteBoard { get; private set; }

        public string Id { get; private set; }

        public IList<string> Comments { get; set; }

        public void AddStudent(Student student)
        {
            Students.Add(student);
        }

        public void AddTeacher(Teacher teacher)
        {
            Teachers.Add(teacher);
        }

        public virtual void AddComment(string comment)
        {
            Comments.Add(comment);
        }

        public virtual async Task AddNoteOnWhiteBoard(string note)
        {
            WhiteBoard = $"The administration wrote on the whiteboard: \"{note}\"";
            await Task.CompletedTask;
        }

        public async Task Open()
        {
            foreach (var teacher in Teachers)
            {
                teacher.AddComment($"Starting educating students {string.Join(",", Students.Select(s => s.Name))} in the {Id} class.");
            }

            foreach (var student in Students)
            {
                student.AddComment($"Education started with teachers {string.Join(",", Teachers.Select(t => t.Name))} in the {Id} class.");
            }

            AddComment($"The {Id} class is open for students {string.Join(", ", Students.Select(s => s.Name))} and they will be tutored by {string.Join(", ", Teachers.Select(t => t.Name))}.");
            await AddNoteOnWhiteBoard(string.Join($";{Environment.NewLine}", Comments.Select(c => c)));
        }
    }
}
