namespace LinqToSqlWithMvc.Models
{
	using System.Data.Linq.Mapping;

	[Table(Name = "Users")]
	public class User
	{
		[Column(IsPrimaryKey = true, IsDbGenerated = true)]
		public int Id { get; set; }
		[Column]
		public string UserName { get; set; }
	}
}