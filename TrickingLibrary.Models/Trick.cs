namespace TrickingLibrary.Models
{
    public class Trick : BaseModel<string>
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public string? DifficultyId { get; set; }

        // todo do we need this?
        public Difficulty? Difficulty { get; set; }

        public IList<TrickRelationship>? Prerequisites { get; set; }
        public IList<TrickRelationship>? Progressions { get; set; }

        public IList<TrickCategory>? TrickCategories { get; set; }
    }
}