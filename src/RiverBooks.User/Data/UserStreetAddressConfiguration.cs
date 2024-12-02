using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RiverBooks.User.Data;
internal class UserStreetAddressConfiguration : IEntityTypeConfiguration<UserStreetAddress>
{
  public void Configure(EntityTypeBuilder<UserStreetAddress> builder)
  {
    builder.HasKey(a => a.Id);

    builder.ComplexProperty(a => a.StreetAddress);
  }
}
