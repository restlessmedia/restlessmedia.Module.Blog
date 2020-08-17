using FastMapper;
using restlessmedia.Module.Category;
using restlessmedia.Module.Configuration;
using restlessmedia.Module.Data;
using restlessmedia.Module.Data.Sql;
using SqlBuilder;
using SqlBuilder.DataServices;
using System;
using System.Dynamic;
using System.Linq;

namespace restlessmedia.Module.Blog.Data
{
  internal class BlogSqlDataProvider : SqlDataProviderBase
  {
    internal BlogSqlDataProvider(IDataContext context, IModelDataService<DataModel.VPost> modelDataService)
      : base(context)
    {
      _modelDataService = modelDataService ?? throw new ArgumentNullException(nameof(modelDataService));
    }

    public PostEntity Read(int postId)
    {
      return Read<PostEntity>(postId);
    }

    public T Read<T>(int postId)
      where T : IPost
    {
      Select<DataModel.VPost> select = _modelDataService.DataProvider.NewSelect();
      select.Where(x => x.PostId, postId);

      IDynamicMetaObjectProvider post = _modelDataService.DataProvider.QueryDynamic(select, connection => select.WithLicenseId(connection, DataContext.LicenseSettings)).FirstOrDefault();

      return ObjectMapper.Map<IDynamicMetaObjectProvider, T>(post, config =>
      {
        config.For(x => x.Category).ResolveWith<TCategory>();
      });
    }

    public void Save(PostEntity post)
    {
      if (post.PostId.HasValue)
      {
        Update(post);
      }
      else
      {
        Create(post);
      }
    }

    public void Delete(int postId)
    {
      _modelDataService.Delete(postId);
    }

    public ModelCollection<PostEntity> List(int page, int maxPerPage, int? categoryId = null)
    {
      Select<DataModel.VPost> select = _modelDataService.DataProvider.NewSelect();
      select.Paging(page, maxPerPage);
      select.IncludeCount(true);

      if (categoryId.HasValue)
      {
        select.Where(x => x.CategoryId, categoryId);
      }

      DataPage<dynamic> dataPage = _modelDataService.DataProvider.QueryPage<dynamic>(select, connection => select.WithLicenseId(connection, DataContext.LicenseSettings));
      return new ModelCollection<PostEntity>(ObjectMapper.MapAll<PostEntity>(dataPage.Data), dataPage.Count);
    }

    public T Latest<T>()
      where T : PostEntity
    {
      Select<DataModel.VPost> select = _modelDataService.DataProvider.NewSelect();
      select.Paging(1, 1);
      return ObjectMapper.Map<T>(_modelDataService.DataProvider.QueryDynamic(select, connection => select.WithLicenseId(connection, DataContext.LicenseSettings)).FirstOrDefault());
    }

    public void Create(PostEntity post)
    {
      DataModel.VPost dataModel = ObjectMapper.Map<PostEntity, DataModel.VPost>(post);
      _modelDataService.Create(dataModel);
    }

    public void Update(PostEntity post)
    {
      DataModel.VPost dataModel = ObjectMapper.Map<PostEntity, DataModel.VPost>(post);
      _modelDataService.Update(post.PostId, dataModel);
    }

    private readonly IModelDataService<DataModel.VPost> _modelDataService;
  }
}