using restlessmedia.Module.Configuration;
using restlessmedia.Module.Data;
using SqlBuilder.DataServices;

namespace restlessmedia.Module.Blog.Data
{
  internal class BlogDataProvider : BlogSqlDataProvider, IBlogDataProvider
  {
    public BlogDataProvider(IDataContext context, IModelDataService<DataModel.VPost> modelDataProvider, ILicenseSettings licenseSettings)
      : base(context, modelDataProvider, licenseSettings) { }
  }
}