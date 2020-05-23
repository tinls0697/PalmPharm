using SQLite;
using System;

namespace tabbedpage.Models
{
    public class AlarmItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string DrugsName { get; set; }
        public string NickName { get; set; }
        public string Quantity { get; set; }
        public string Unit { get; set; }
        public string Timesperday { get; set; }
        public string Timeinterval { get; set; }
        public string Note { get; set; }
        public int TimesTaken { get; set; }
        public string image_path { get; set; }
        public DateTime DoneDate { get; set; }

        public string NextAlarm { get; set; }

        public string TimesTakenPrint
        {
            get { return $"Times taken today: {TimesTaken} of {Timesperday}"; }
        }

        public string QuantityUnit
        {
            get { return $"{Quantity} {Unit}"; }
        }

        public string DailyIntake
        {
            get { return $"{QuantityUnit} every {Timeinterval} hours"; }
        }
    }
}