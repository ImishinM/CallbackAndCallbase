namespace School
{
    public interface ICommentable
    {
        IList<string> Comments { get; set; }

        void AddComment(string comment);
    }
}
