using restlessmedia.Module.Data;
using restlessmedia.Module.Data.EF;
using System.Data.Entity;

namespace restlessmedia.Module.Blog.Data
{
  public class DatabaseContext : restlessmedia.Module.Data.EF.DatabaseContext
  {
    public DatabaseContext(IDataContext dataContext, bool autoDetectChanges)
      : base(dataContext, autoDetectChanges) { }

    public DbSet<VPost> Post
    {
      get
      {
        return Set<VPost>();
      }
    }

    protected override void Configure(DbModelBuilder modelBuilder)
    {
      base.Configure(modelBuilder);
      modelBuilder.Configurations.Add(new LicensedEntityConfiguration<VPost>());
    }
  }
}