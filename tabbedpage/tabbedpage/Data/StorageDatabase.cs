using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabbedpage.Models;

namespace tabbedpage.Data
{
    public class StorageDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.SDatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public StorageDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(StorageItem).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(StorageItem)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<StorageItem>> GetItemsAsync()
        {
            return Database.Table<StorageItem>().ToListAsync();
        }


        public Task<StorageItem> GetItemAsync(int id)
        {
            return Database.Table<StorageItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(StorageItem item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(StorageItem item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
