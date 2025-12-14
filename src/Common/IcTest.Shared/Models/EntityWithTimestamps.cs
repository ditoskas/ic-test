using IcTest.Shared.Models.Contracts;
using System.Text.Json;

namespace IcTest.Shared.Models
{
    public class EntityWithTimestamps : Entity, IEntityWithTimestamps
    {
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
