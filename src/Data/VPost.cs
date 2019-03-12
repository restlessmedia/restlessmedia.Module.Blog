using restlessmedia.Module.Category;
using restlessmedia.Module.Data.EF;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace restlessmedia.Module.Blog.Data
{
  [Table("VPost")]
  public class VPost : LicensedEntity, IPost
  {
    public VPost() { }

    public VPost(PostEntity post)
    {
      PostId = post.PostId.GetValueOrDefault(0);
      Title = post.Title;
      Summary = post.Summary;
      BodyHtml = post.BodyHtml;
      PostedDate = post.PostedDate;
      CategoryId = post.CategoryId;
    }

    [Key]
    public int? PostId { get; set; }

    [Varchar]
    public override string Title { get; set; }

    [Varchar]
    public string Summary { get; set; }

    [Varchar]
    public string BodyHtml { get; set; }

    public DateTime? PostedDate { get; set; }

    public int? CategoryId { get; set; }

    public ICategory Category
    {
      get
      {
        return TCategory;
      }
      set
      {
        TCategory = new TCategory(value);
      }
    }

    [ForeignKey("CategoryId")]
    public TCategory TCategory { get; set; }

    public override int? EntityId
    {
      get
      {
        return PostId;
      }
    }

    public override EntityType EntityType
    {
      get
      {
        return EntityType.Post;
      }
    }
  }
}