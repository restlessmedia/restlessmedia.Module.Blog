using FakeItEasy;
using restlessmedia.Module.Blog.Data;
using restlessmedia.Module.Configuration;
using restlessmedia.Module.Data;
using SqlBuilder;
using SqlBuilder.DataServices;
using Xunit;

namespace restlessmedia.Module.Blog.UnitTest
{
  public class UnitTest1
  {
    [Fact]
    public void TestMethod1()
    {

      BlogSqlDataProvider blogSqlDataProvider = CreateInstance();
    }

    private BlogSqlDataProvider CreateInstance()
    {
      IDataContext dataContext = A.Fake<IDataContext>();
      IModelDataService<Data.DataModel.VPost> modelDataService = A.Fake<IModelDataService<Data.DataModel.VPost>>();
      ILicenseSettings licenseSettings = A.Fake<ILicenseSettings>();
      BlogSqlDataProvider blogSqlDataProvider = new BlogSqlDataProvider(dataContext, modelDataService, licenseSettings);
      return blogSqlDataProvider;
    }
  }
}
