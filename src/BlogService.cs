using restlessmedia.Module.Blog.Data;
using restlessmedia.Module.Category;
using restlessmedia.Module.Category.Data;
using System;

namespace restlessmedia.Module.Blog
{
  internal class BlogService : IBlogService
  {
    public BlogService(IBlogDataProvider blogDataProvider, ICategoryDataProvider categoryDataProvider)
    {
      _blogDataProvider = blogDataProvider ?? throw new ArgumentNullException(nameof(blogDataProvider));
      _categoryDataProvider = categoryDataProvider ?? throw new ArgumentNullException(nameof(categoryDataProvider));
    }

    public PostEntity Read(int postId)
    {
      return _blogDataProvider.Read(postId);
    }

    public T Read<T>(int postId)
      where T : PostEntity
    {
      return _blogDataProvider.Read<T>(postId);
    }

    public void Save(PostEntity post)
    {
      if (post == null)
      {
        throw new ArgumentNullException(nameof(post));
      }

      _blogDataProvider.Save(post);
    }

    public void Delete(int postId)
    {
      _blogDataProvider.Delete(postId);
    }

    public PostCollection List(int page, int maxPerPage, int? categoryId = null)
    {
      ICategory category = categoryId.HasValue ? _categoryDataProvider.Read(categoryId.Value) : null;
      ModelCollection<PostEntity> list = _blogDataProvider.List(page, maxPerPage, categoryId);
      list.Paging.Page = page;
      list.Paging.MaxPerPage = maxPerPage;
      return new PostCollection(list, category);
    }

    public PostEntity Latest()
    {
      return Latest<PostEntity>();
    }

    public T Latest<T>()
      where T : PostEntity
    {
      return _blogDataProvider.Latest<T>();
    }

    private readonly IBlogDataProvider _blogDataProvider;

    private readonly ICategoryDataProvider _categoryDataProvider;
  }
}