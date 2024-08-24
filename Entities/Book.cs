using System.Text.Json.Serialization;

namespace WebApplication1.Entities
{
    public class Book
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        [JsonNumberHandling(JsonNumberHandling.AllowNamedFloatingPointLiterals)]
        public decimal Price { get; set; } = 0;
    }
}
