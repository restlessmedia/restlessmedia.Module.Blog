using restlessmedia.Module.Data;

namespace restlessmedia.Module.Blog.Data
{
  public interface IBlogDataProvider : IDataProvider
  {
    PostEntity Read(int postId);

    T Read<T>(int postId) where T : IPost;

    void Save(PostEntity post);

    void Delete(int postId);

    ModelCollection<PostEntity> List(int page, int maxPerPage, int? categoryId = null);

    T Latest<T>() where T : PostEntity;
  }
}