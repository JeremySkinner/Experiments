namespace LinqToSqlWithMvc.Models
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;

	[Table(Name = "Comments")]
	public class Comment
	{
		[Column(IsPrimaryKey = true, IsDbGenerated = true)]
		public int Id { get; set; }

		[Column]
		public int PostId { get; set; }

		[Column]
		public int UserId { get; set; }

		[Column(Name = "Comment")]
		public string CommentValue { get; set; }

		private EntityRef<Post> post;
		private EntityRef<User> commenter;

		[Association(IsForeignKey = true, ThisKey = "UserId", Storage = "commenter")]
		public User Commenter
		{
			get { return commenter.Entity; }
			set { commenter.Entity=value; }
		}

		[Association(IsForeignKey = true, ThisKey = "PostId", Storage = "post")]
		public Post Post
		{
			get { return post.Entity; }
			set { post.Entity = value; }
		}
	}
}