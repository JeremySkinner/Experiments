using System.Web.Mvc;

namespace LinqToSqlWithMvc.Controllers
{
	using System.Linq;
	using System.Web;
	using LinqToSqlWithMvc.Models;

	//This sample uses a SQL Server Compact Edition database - please don't do this in a real application :)

    public class PostsController : Controller
    {
		BlogDataContext context;

		//Injected via StructureMap
    	public PostsController(BlogDataContext context)
    	{
    		this.context = context;
    	}

		[EagerlyLoad(typeof(PostWithComments), typeof(CommentWithCommenter))]
		public ActionResult Show(int id) {
			var post = context.Posts.SingleOrDefault(x => x.Id == id);
			if (post == null) {
				throw new HttpException(404, "The post could not be found.");
			}

			return View(post);
		}


		public ActionResult Create()
		{
			return View();
		}

		[AcceptVerbs(HttpVerbs.Post), AutoCommit]
		public ActionResult Create(Post post)
		{
			context.Posts.InsertOnSubmit(post);
			return RedirectToAction("Show", new{ id = post });
		}
    }
}
