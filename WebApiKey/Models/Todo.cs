namespace WebApiKey.Models
{
    public class Todo
    {
        public Todo()
        {
        }

        public Todo(string description, bool active)
        {
            Description = description;
            Active = active;
        }

        public Todo(int id, string description, bool active)
        {
            Id = id;
            Description = description;
            Active = active;
        }


        public int Id { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; } = true;
    }
}
