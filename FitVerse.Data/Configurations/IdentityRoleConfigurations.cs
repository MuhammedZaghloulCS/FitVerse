using FitVerse.Core.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Configurations
{
    internal class IdentityRoleConfigurations : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
    new IdentityRole
    {
        Id = "1",
        Name = Statics.ADMIN,
        NormalizedName = Statics.ADMIN.ToUpper()
    },
    new IdentityRole
    {
        Id = "2",
        Name = Statics.COACH,
        NormalizedName = Statics.COACH.ToUpper()
    },
    new IdentityRole
    {
        Id = "3",
        Name = Statics.CLIENT,
        NormalizedName = Statics.CLIENT.ToUpper()
    }
);

        }
    }
}
