﻿using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Koinonia.Models;


//Implementing Database storage
//https://docs.microsoft.com/en-us/xamarin/get-started/quickstarts/database?pivots=windows

namespace Koinonia.Data
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Contact>().Wait();
            _database.CreateTableAsync<Profile>().Wait();
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

        public Task<int> DeleteAllContactsAsync()
        {
            return _database.DeleteAllAsync<Contact>();
        }

        public Task<Profile> GetProfileAsync(int id)
        {
            return _database.Table<Profile>()

                            .Where(i => i.ContactID == id)
                            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Profile>> GetProfiles()
        {
            return  await _database.Table<Profile>().ToListAsync();
        }

        public Task<int> SaveProfileAsync(Profile profile)
        {

            if (profile.ContactID == 0)
            {
                return _database.UpdateAsync(profile);
            }
            else
            {
                return _database.InsertAsync(profile);                
            }
        }

        //For testing purposes. May also be used for 'Delete profile' functionality?
        public Task<int> DeleteProfileAsync()
        {
            return _database.DeleteAllAsync<Profile>();
        }  

    }


}
