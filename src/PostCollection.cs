using restlessmedia.Module.Category;
using System.Collections.Generic;

namespace restlessmedia.Module.Blog
{
  public class PostCollection : ModelCollection<PostEntity>
  {
    public PostCollection(ModelCollection<PostEntity> collection, ICategory category = null, IEnumerable<ICategory> categories = null)
      : base(collection, collection.Paging)
    {
      Category = category;
      Categories = categories;
    }

    public IEnumerable<ICategory> Categories;

    public readonly ICategory Category;
  }
}