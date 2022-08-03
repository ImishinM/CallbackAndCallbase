using School;

namespace UnitTests
{
    public class ClassTests
    {
        private readonly Action<string> _writeToConsole;
        private readonly Mock<SchoolClass> _schoolClass;
        private readonly School.School _school;

        public ClassTests(ITestOutputHelper output)
        {
            _writeToConsole = output.WriteLine;

            _schoolClass = new Mock<SchoolClass>("A");
            _schoolClass.Object.AddStudent(new Student("Moshe", "009"));
            _schoolClass.Object.AddStudent(new Student("Solomon", "003"));
            _schoolClass.Object.AddTeacher(new Teacher("Sarah"));
            _schoolClass.Object.AddTeacher(new Teacher("Abraham"));
            _schoolClass.Object.AddTeacher(new Teacher("Levy"));
            _school = new School.School();
            _school.AddClass(_schoolClass.Object);
        }

        [Fact]
        public async Task SimpleCallBase()
        {
            _schoolClass.Setup(c => c.AddComment(It.IsAny<string>())).CallBase();

            await _school.Open();

            var comments = _schoolClass.Object.Comments;
            _writeToConsole(string.Join($";{Environment.NewLine}", comments.Select(c => c)));
        }

        [Fact]
        public async Task SimpleCallBack()
        {
            _schoolClass.Setup(c => c.AddComment(It.IsAny<string>())).Callback(_writeToConsole);

            await _school.Open();
        }

        [Fact]
        public async Task SophisticatedCallBase()
        {
            _schoolClass.Setup(c => c.AddComment(It.IsAny<string>())).CallBase(); // we have to do it for populating comments with text 
            _schoolClass
                .Setup(c => c.AddNoteOnWhiteBoard(It.IsAny<string>()))
                .Callback(() => _writeToConsole("Somebody wrote something about the class on the whiteboard :) Check it out!"))
                .CallBase();

            await _school.Open();

            _writeToConsole(_schoolClass.Object.WhiteBoard);
        }

        [Fact]
        public async Task SophisticatedCallBack()
        {
            _schoolClass.Setup(c => c.AddComment(It.IsAny<string>())).CallBase(); // we have to do it for populating comments with text 
            _schoolClass
                .Setup(c => c.AddNoteOnWhiteBoard(It.IsAny<string>()))
                .Callback<string>(note => _writeToConsole($"The message on the whiteboard was: \"{note}\""))
                .Returns<string>(async _ =>
                {
                    _writeToConsole("But somebody erased the message on the whiteboard :(");
                    await Task.CompletedTask;
                })
                ;

            await _school.Open();
        }
    }
}