using restlessmedia.Module.Data;
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
  }
}