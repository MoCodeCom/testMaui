using System;
using SQLite;

namespace testRender.Model
{
	[Table("users")]
	public class Users
	{
		[AutoIncrement, PrimaryKey]
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}

	public class UsersLst
	{
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

