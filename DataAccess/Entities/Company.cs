using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; } = default!;

        public ICollection<Offer>? Offers { get; set; } = Enumerable.Empty<Offer>().ToList();
    }
}
