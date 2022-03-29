using System;

namespace OnlineShopping.Database.Entity
{
    public class Activity : EntityBase
    {
        public string ActivityRecord { get; set; }
        public ActivityType Type { get; set; }

        public long ActivitySessionId { get; set; }
        public virtual ActivitySession ActivitySession { get; set; }
    }

    public enum ActivityType
    {
        Search,
        ViewDetail
    }    
}
