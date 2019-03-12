using Newtonsoft.Json;
using restlessmedia.Module.Category;
using System;
using System.Web;

namespace restlessmedia.Module.Blog
{
  public class PostEntity : Entity, IPost
  {
    public PostEntity()
    {
    }

    public override EntityType EntityType
    {
      get
      {
        return EntityType.Post;
      }
    }

    public override int? EntityId
    {
      get
      {
        return PostId;
      }
    }

    /// <summary>
    /// This is either the actual summary or a truncated version of the body
    /// </summary>
    public string DisplaySummary(int maxChars)
    {
      if (string.IsNullOrEmpty(Summary))
      {
        return "[calculated summary]";
      }
      else
      {
        return Summary;
      }
    }

    public virtual string Summary { get; set; }

    public int? PostId { get; set; }

    public virtual DateTime? PostedDate { get; set; }

    /// <summary>
    /// Encoded html
    /// </summary>
    public virtual string BodyHtml { get; set; }

    /// <summary>
    /// Decoded version
    /// </summary>
    [JsonIgnore]
    public string DecodedBodyHtml
    {
      get
      {
        if (string.IsNullOrEmpty(BodyHtml))
        {
          return BodyHtml;
        }

        return HttpUtility.HtmlDecode(BodyHtml);
      }
    }

    public int? CategoryId { get; set; }

    public ICategory Category { get; set; }
  }
}