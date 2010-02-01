namespace LinqToSqlWithMvc.Models
{
	using System.Data;
	using System.Data.Linq;
	using System.Data.SqlServerCe;
	using System.Web;

	public class BlogDataContext : DataContext
	{
		IDbConnection conn;

		//Please don't use SQL Compact in a real web app.
		public BlogDataContext(string pathToDatabase) 
			: this(new SqlCeConnection("Data Source=" + pathToDatabase + ";Persist Security Info=False;"))
		{
			conn.Open();
		}

		protected BlogDataContext(IDbConnection connection) : base(connection)
		{
			this.conn = connection;	
		}

		protected override void Dispose(bool disposing) {
			conn.Close();
			conn.Dispose();
			base.Dispose(disposing);
		}

		public Table<Post> Posts
		{
			get { return base.GetTable<Post>(); }
		}

		public Table<User> Users
		{
			get { return base.GetTable<User>(); }
		}

		public Table<Comment> Comments
		{
			get { return base.GetTable<Comment>(); }
		}
	}
}