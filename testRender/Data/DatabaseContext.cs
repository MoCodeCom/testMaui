﻿using System;
using System.Linq.Expressions;
using SQLite;
using testRender.Model;

namespace testRender.Data
{
	public class DatabaseContext:IAsyncDisposable
	{
        /*
		//Database name file.
		private const string DbName = "sqlDb.db";

		//When database file stored.
		
		private static string DbPath =>
			Path.Combine(FileSystem.AppDataDirectory, DbName);

		

		//To connect with database if there is no connection should build.
		private SQLiteAsyncConnection _connection;
		private SQLiteAsyncConnection Database =>
			(_connection ??=
			new SQLiteAsyncConnection(DbPath, SQLiteOpenFlags.Create
				                            | SQLiteOpenFlags.ReadWrite
				                            | SQLiteOpenFlags.SharedCache));
		*/
        // ++
        /*
		public async Task Try()
		{
			await Database.CreateTableAsync<Product>();
			Database.Table<Product>().ToListAsync();
		}*/

        //To get data form table or initial new table if not exist
        /*
		//If not exist create table
		private async Task CreateTableIfNotExists<TTable>() where TTable:class, new()
		{
			await Database.CreateTableAsync<TTable>();
		}

		//Get table
		private async Task<AsyncTableQuery<TTable>>GetTableAsync<TTable>() where TTable:class, new()
		{
			await CreateTableIfNotExists<TTable>();
			return Database.Table<TTable>();
		}

		//Get All data form table
		public async Task<IEnumerable<TTable>> GetAllAsync<TTable>() where TTable:class, new()
		{
			await Database.CreateTableAsync<TTable>();
			return await Database.Table<TTable>().ToListAsync();
		}

        public async Task<IEnumerable<TTable>> GetFilteredAsync<TTable>(Expression<Func<TTable, bool>> predicate) where TTable : class, new()
        {
			var table = await GetTableAsync<TTable>();
			return await table.Where(predicate).ToListAsync();
        }

		//***************************************************************************§§//

		private async Task<TResult> Execute<TTable, TResult>(Func<Task<TResult>>action) where TTable:class, new()
		{
			await CreateTableIfNotExists<TTable>();
			return await action();
		}

		public async Task<TTable>GetItemByKeyAsync<TTable>(object primaryKey) where TTable:class, new()
		{
			return await Execute<TTable, TTable>(async ()=> await Database.GetAsync<TTable>(primaryKey));
		}

		public async Task<bool> AddItemAsync<TTable>(TTable item) where TTable:class, new()
		{
			return await Execute<TTable, bool>(async ()=> await Database.InsertAsync(item) > 0);
		}

        public async Task<bool> UpdateItemAsync<TTable>(TTable item) where TTable : class, new()
		{
			await CreateTableIfNotExists<TTable>();
			return await Database.UpdateAsync(item) > 0;
		}

        public async Task<bool> DeleteItemAsync<TTable>(TTable item) where TTable : class, new()
		{
			await CreateTableIfNotExists<TTable>();
			return await Database.DeleteAsync(item) > 0;
		}

        public async Task<bool> DeleteItemByKeyAsync<TTable>(object primaryKey) where TTable : class, new()
		{
			await CreateTableIfNotExists<TTable>();
			return await Database.DeleteAsync<TTable>(primaryKey) > 0;
		}

		public async ValueTask DisposeAsync() => await _connection?.CloseAsync();

		*/

        private const string DbName = "MyDatabase.db3";
        private static string DbPath => Path.Combine(FileSystem.AppDataDirectory, DbName);

        private SQLiteAsyncConnection _connection;
        private SQLiteAsyncConnection Database =>
            (_connection ??= new SQLiteAsyncConnection(DbPath,
                SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache));

        private async Task CreateTableIfNotExists<TTable>() where TTable : class, new()
        {
            await Database.CreateTableAsync<TTable>();
        }

        private async Task<AsyncTableQuery<TTable>> GetTableAsync<TTable>() where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return Database.Table<TTable>();
        }

        public async Task<IEnumerable<TTable>> GetAllAsync<TTable>() where TTable : class, new()
        {
            var table = await GetTableAsync<TTable>();
            return await table.ToListAsync();
        }

        public async Task<IEnumerable<TTable>> GetFileteredAsync<TTable>(Expression<Func<TTable, bool>> predicate) where TTable : class, new()
        {
            var table = await GetTableAsync<TTable>();
            return await table.Where(predicate).ToListAsync();
        }

        private async Task<TResult> Execute<TTable, TResult>(Func<Task<TResult>> action) where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return await action();
        }

        public async Task<TTable> GetItemByKeyAsync<TTable>(object primaryKey) where TTable : class, new()
        {
            //await CreateTableIfNotExists<TTable>();
            //return await Database.GetAsync<TTable>(primaryKey);
            return await Execute<TTable, TTable>(async () => await Database.GetAsync<TTable>(primaryKey));
        }

        public async Task<bool> AddItemAsync<TTable>(TTable item) where TTable : class, new()
        {
            //await CreateTableIfNotExists<TTable>();
            //return await Database.InsertAsync(item) > 0;
            return await Execute<TTable, bool>(async () => await Database.InsertAsync(item) > 0);
        }

        public async Task<bool> UpdateItemAsync<TTable>(TTable item) where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return await Database.UpdateAsync(item) > 0;
        }

        public async Task<bool> DeleteItemAsync<TTable>(TTable item) where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return await Database.DeleteAsync(item) > 0;
        }

        public async Task<bool> DeleteItemByKeyAsync<TTable>(object primaryKey) where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return await Database.DeleteAsync<TTable>(primaryKey) > 0;
        }

        public async ValueTask DisposeAsync() => await _connection?.CloseAsync();
    }
}

