using restlessmedia.Module.Category;
using System;

namespace restlessmedia.Module.Blog
{
  public interface IPost
  {
    int? PostId { get; set; }

    string Title { get; set; }

    string Summary { get; set; }

    string BodyHtml { get; set; }

    DateTime? PostedDate { get; set; }

    int? CategoryId { get; set; }

    ICategory Category { get; set; }
  }
}