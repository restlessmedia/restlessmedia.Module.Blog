using SqlBuilder;
using SqlBuilder.Attributes;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace restlessmedia.Module.Blog.Data.DataModel
{
  [Table("VPost", Schema = "dbo")]
  public class VPost : DataModel<VPost, PostEntity>
  {
    [Key]
    public int PostId { get; set; }

    //TODO: add this with order?
    //[OrderBy(ascending: true)]
    public string Title { get; set; }

    public string BodyHtml { get; set; }

    [OrderBy(ascending: false)]
    public DateTime? PostedDate { get; set; }   

    public string Summary { get; set; }

    [ReadOnly(true)]
    public int? EntityGuid { get; set; }

    [ReadOnly(true)]
    public int? LicenseId { get; set; }

    public int? CategoryId { get; set; }
  }
}