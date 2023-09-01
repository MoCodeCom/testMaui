using System;
using SQLite;
using System.IO;
using testRender.Model;

namespace testRender.Data
{
	public class usersRepo
	{
        private readonly SQLiteConnection _database;
		public static string DbPath { get; set; }
			// = Path.Combine(FileSystem.AppDataDirectory, "sqlDb.db");
			= Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "sqlDb.db");

        public usersRepo()
		{
			//App.Current.MainPage.DisplayAlert("database path",DbPath,"OK");
			_database = new SQLiteConnection(DbPath);
			_database.CreateTable<Users>();
		}

		public List<Users> List()
		{
			return _database.Table<Users>().ToList();
		}
	}
}

