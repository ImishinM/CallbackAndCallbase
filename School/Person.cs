namespace School
{
    public abstract class Person : ICommentable
    {
        public string Name { get; set; }

        public IList<string> Comments { get; set; }

        public void AddComment(string comment)
        {
            Comments.Add(comment);
        }
    }
}
