namespace Legumaretu.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stars { get; set; }
        public bool Official { get; set; }
        public string ImgLink { get; set; }
        public List<ChTask> ChTasks { get; set; }

        public Recipe(int id, string name, string description, int stars, bool official, string imglink)
        {
            Id = id;
            Name = name;
            Description = description;
            Stars = stars;
            Official = official;
            ImgLink = imglink;
        }
        public Recipe() { }
    }
}
