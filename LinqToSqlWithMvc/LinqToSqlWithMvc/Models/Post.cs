namespace LinqToSqlWithMvc.Models
{
	using System;
	using System.Collections.Generic;
	using System.Data.Linq;
	using System.Data.Linq.Mapping;

	[Table(Name = "Posts")]
	public class Post : IUrlRoutable
	{
		[Column(IsPrimaryKey = true, IsDbGenerated = true)]
		public int Id { get; set; }

		[Column]
		public string Title { get; set; }

		[Column]
		public string Content { get; set; }

		[Column]
		public int UserId { get; set; }

		private EntityRef<User> user;

		[Association(
			IsForeignKey = true, 
			Storage = "user", 
			ThisKey = "UserId")]
		public User User
		{
			get { return user.Entity; }
			set { user.Entity = value; }
		}

		private EntitySet<Comment> comments = new EntitySet<Comment>();

		[Association(IsForeignKey = true, OtherKey = "PostId", Storage = "comments")]
		public IList<Comment> Comments
		{
			get { return comments; }
			set { comments.Assign(value); }
		}

		public object GetRouteParameter()
		{
			return Id;
		}
	}
}