using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;

namespace WebAPI.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                new Book { Id = 1, Title = "Şeker Portakalı", Price = 150 },
                new Book { Id = 2, Title = "Güneşi Uyandıralım", Price = 200 },
                new Book { Id = 3, Title = "Delifişek", Price = 75 }
                );
        }
    }
}
