using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabbedpage.Models;

namespace tabbedpage.Data
{
    public class AlarmDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public AlarmDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(AlarmItem).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(AlarmItem)).ConfigureAwait(false);
                    initialized = true;
                }

                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(StorageItem).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(StorageItem)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        //Alarm
        public Task<List<AlarmItem>> GetItemsAsync()
        {
            return Database.Table<AlarmItem>().ToListAsync();
        }


        public Task<AlarmItem> GetItemAsync(int id)
        {
            return Database.Table<AlarmItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(AlarmItem item)
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

        public Task<int> DeleteItemAsync(AlarmItem item)
        {
            return Database.DeleteAsync(item);
        }


        //Storage
        public Task<List<StorageItem>> GetSItemsAsync()
        {
            return Database.Table<StorageItem>().ToListAsync();
        }


        public Task<StorageItem> GetSItemAsync(int id)
        {
            return Database.Table<StorageItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<List<StorageItem>> GetAlmostExpire(DateTime timenow)
        {
            return Database.Table<StorageItem>().Where(w => w.ExpiryDate < timenow).ToListAsync();
        }        

        public Task<int> SaveSItemAsync(StorageItem item)
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

        public Task<int> DeleteSItemAsync(StorageItem item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
