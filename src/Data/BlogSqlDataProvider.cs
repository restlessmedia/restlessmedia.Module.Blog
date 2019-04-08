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
    internal BlogSqlDataProvider(IDataContext context, IModelDataProvider<DataModel.VPost> modelDataProvider, ILicenseSettings licenseSettings)
      : base(context)
    {
      _modelDataProvider = modelDataProvider ?? throw new ArgumentNullException(nameof(modelDataProvider));
      _licenseSettings = licenseSettings ?? throw new ArgumentNullException(nameof(licenseSettings));
    }

    public PostEntity Read(int postId)
    {
      return Read<PostEntity>(postId);
    }

    public T Read<T>(int postId)
      where T : IPost
    {
      Select<DataModel.VPost> select = _modelDataProvider.NewSelect();
      select.Where(x => x.PostId, postId);

      IDynamicMetaObjectProvider post = _modelDataProvider.QueryDynamic(select, connection => select.WithLicenseId(connection, _licenseSettings)).FirstOrDefault();

      return ObjectMapper.Map<IDynamicMetaObjectProvider, T>(post, config =>
      {
        config.For(x => x.Category).ResolveWith<TCategory>();
      });
    }

    public void Save(PostEntity post)
    {
      using (DatabaseContext context = CreateDatabaseContext())
      {
        PostRepository postRepository = new PostRepository(context);
        VPost dataModel = postRepository.Save(post);

        context.SaveChanges();

        if (!post.PostId.HasValue)
        {
          post.PostId = dataModel.PostId;
        }
      }
    }

    public void Delete(int postId)
    {
      using (DatabaseContext context = CreateDatabaseContext())
      {
        PostRepository postRepository = new PostRepository(context);
        postRepository.Delete(postId);
      }
    }

    public ModelCollection<PostEntity> List(int page, int maxPerPage, int? categoryId = null)
    {
      Select<DataModel.VPost> select = _modelDataProvider.NewSelect();
      select.Paging(page, maxPerPage);
      select.IncludeCount(true);

      if (categoryId.HasValue)
      {
        select.Where(x => x.CategoryId, categoryId);
      }

      DataPage<dynamic> dataPage = _modelDataProvider.QueryPage<dynamic>(select, connection => select.WithLicenseId(connection, _licenseSettings));
      return new ModelCollection<PostEntity>(ObjectMapper.MapAll<PostEntity>(dataPage.Data), dataPage.Count);
    }

    public T Latest<T>()
      where T : PostEntity
    {
      Select<DataModel.VPost> select = _modelDataProvider.NewSelect();
      select.Paging(1, 1);
      return ObjectMapper.Map<T>(_modelDataProvider.QueryDynamic(select, connection => select.WithLicenseId(connection, _licenseSettings)).FirstOrDefault());
    }

    protected DatabaseContext CreateDatabaseContext(bool autoDetectChanges = false)
    {
      return new DatabaseContext(DataContext, autoDetectChanges);
    }

    private readonly IModelDataProvider<DataModel.VPost> _modelDataProvider;

    private readonly ILicenseSettings _licenseSettings;
  }
}