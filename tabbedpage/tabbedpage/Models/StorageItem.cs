using System;
using SQLite;

namespace tabbedpage.Models
{
    public class StorageItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string DrugsName { get; set; }
        public string NickName { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Note { get; set; }
        public string image_path { get; set; }
    }
}
