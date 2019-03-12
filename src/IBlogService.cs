namespace restlessmedia.Module.Blog
{
  public interface IBlogService : IService
  {
    PostEntity Read(int postId);

    T Read<T>(int postId) where T : PostEntity;

    void Save(PostEntity post);

    void Delete(int postId);

    PostCollection List(int page, int maxPerPage, int? categoryId = null);

    PostEntity Latest();

    T Latest<T>() where T : PostEntity;
  }
}