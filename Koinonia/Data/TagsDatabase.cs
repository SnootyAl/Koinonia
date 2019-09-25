using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using Koinonia.Models;


namespace Koinonia.Data
{
    public class TagsDatabase
    {

        readonly SQLiteAsyncConnection _database; 

        public TagsDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Tags>().Wait();
        }

        // returns all tags with in the table for the list view
        public Task<List<Tags>> getTagsAsync()
        {
            return _database.Table<Tags>().ToListAsync();
        }

        // inserts new tag into database
        public Task<int> SavetagAsync(Tags tags)
        {
            return _database.InsertAsync(tags);
        }



    }
}
