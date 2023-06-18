using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IMyDbContext
    {
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync();
    }

}
