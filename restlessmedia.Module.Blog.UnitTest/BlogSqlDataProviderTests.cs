using FakeItEasy;
using restlessmedia.Module.Blog.Data;
using restlessmedia.Module.Data;
using restlessmedia.Test;
using SqlBuilder.DataServices;
using Xunit;

namespace restlessmedia.Module.Blog.UnitTest
{
  public class BlogSqlDataProviderTests
  {
    public BlogSqlDataProviderTests()
    {
      IDataContext dataContext = A.Fake<IDataContext>();
      _modelDataService = A.Fake<IModelDataService<Data.DataModel.VPost>>();
      _blogSqlDataProvider = new BlogSqlDataProvider(dataContext, _modelDataService);
    }

    [Fact]
    public void create_sets_id_on_model()
    {
      // set-up
      const int id = 1234;
      PostEntity post = new PostEntity
      {
        PostId = null // this should be set once created
      };
      A.CallTo(() => _modelDataService.Create(A<Data.DataModel.VPost>.Ignored)).Returns(id);

      // call
      _blogSqlDataProvider.Create(post);

      // assert
      post.PostId.MustBe(id);
    }

    private readonly BlogSqlDataProvider _blogSqlDataProvider;

    private readonly IModelDataService<Data.DataModel.VPost> _modelDataService;
  }
}
