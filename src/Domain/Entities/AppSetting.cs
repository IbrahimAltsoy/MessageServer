using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class AppSetting:Entity<Guid>
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
