namespace TrickingLibrary.Api.Form
{
    public class TrickForm
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Difficulty { get; set; }
        public IEnumerable<string> Categories { get; set; }
        // public IEnumerable Prerequisites { get; set; }
        // public IEnumerable Progressions { get; set; }

    }
}