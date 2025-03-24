using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Data.Contexts;
public class DataContextFacotry : IDesignTimeDbContextFactory<DataContext>
{
  public DataContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
    optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Projects\Alpha\Data\Db\dbAlpha.mdf;Integrated Security=True;Connect Timeout=30");

    return new DataContext(optionsBuilder.Options);
  }
}
