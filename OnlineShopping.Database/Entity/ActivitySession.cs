using System;
using System.Collections.Generic;

namespace OnlineShopping.Database.Entity
{
    public class ActivitySession : EntityBase
    {
        public string Session { get; set; }

        public ICollection<Activity> Activities { get; set; }
    }
}
