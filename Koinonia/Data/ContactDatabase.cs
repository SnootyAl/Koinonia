using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Koinonia.Models;

namespace Koinonia.Data
{
    public class ContactDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public ContactDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Contact>().Wait();
        }

        public Task<List<Contact>> GetContactsAsync()
        {
            return _database.Table<Contact>().ToListAsync();
        }

        public Task<Contact> GetContactAsync(int id)
        {
            return _database.Table<Contact>()
                            .Where(i => i.ContactID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveContactAsync(Contact contact)
        {
            if (contact.ContactID != 0)
            {
                return _database.UpdateAsync(contact);
            }
            else
            {
                return _database.InsertAsync(contact);
            }
        }

        public Task<int> DeleteContactAsync(Contact contact)
        {
            return _database.DeleteAsync(contact);
        }
    }
}
