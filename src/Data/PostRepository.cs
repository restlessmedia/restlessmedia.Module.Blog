using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using restlessmedia.Module.Data.EF;

namespace restlessmedia.Module.Blog.Data
{
  internal class PostRepository : LicensedEntityRepository<VPost>
  {
    public PostRepository(DatabaseContext context)
      : base(context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public override int Count()
    {
      return Licensed().Count();
    }

    public void Delete(int postId)
    {
      const string sql = "dbo.SPDeletePost @licenseId, @postId";
      Context.Database.ExecuteSqlCommand(sql,
        new SqlParameter("@licenseId", SqlDbType.Int)
        {
          Value = Context.LicenseId
        },
        new SqlParameter("@postId", SqlDbType.Int)
        {
          Value = postId
        }
      );
    }

    public VPost Save(PostEntity post)
    {
      VPost dataModel = new VPost(post);

      if (_context.Post.Any(x => x.PostId == post.PostId))
      {
        return Update(dataModel,
          x => x.PostId,
          x => x.Title,
          x => x.Summary,
          x => x.BodyHtml,
          x => x.PostedDate,
          x => x.CategoryId
        );
      }
      else
      {
        return Add(dataModel);
      }
    }

    private readonly DatabaseContext _context;
  }
}