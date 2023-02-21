using Masa.BuildingBlocks.Ddd.Domain.Entities.Full;
using System.Runtime.CompilerServices;

namespace Demo.WebApi.Domain.AuthManager.Entities
{
    public class RoleEntity : FullAggregateRoot<Guid,Guid>
    {
        public string Name { get; set; }
        public string Remark { get; set; }


        public override IEnumerable<(string Name, object Value)> GetKeys()
        {
            return new List<(string Name, object Value)>();
        }
    }
}
