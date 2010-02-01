namespace LinqToSqlWithMvc.Models
{
	using System.Data.Linq;

	public interface IEagerLoadingSpecification {
		void Build(DataLoadOptions options);
	}

	public class PostWithComments : IEagerLoadingSpecification {
		public void Build(DataLoadOptions options) {
			options.LoadWith<Post>(p => p.Comments);
		}
	}

	public class CommentWithCommenter : IEagerLoadingSpecification {
		public void Build(DataLoadOptions options) {
			options.LoadWith<Comment>(c => c.Commenter);
		}
	}
}