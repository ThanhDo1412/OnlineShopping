using OnlineShopping.Database.Entity;
using OnlineShopping.Database.Repository;
using OnlineShopping.Service.Interface;
using System;
using System.Threading.Tasks;

namespace OnlineShopping.Service
{
    public class ActivityService : IActivityService
    {
        private readonly IReponsitory<Activity> _activityRepo;
        private readonly IReponsitory<ActivitySession> _activitySessionRepo;

        public ActivityService(IReponsitory<Activity> activityRepo, IReponsitory<ActivitySession> activitySessionRepo)
        {
            _activityRepo = activityRepo;
            _activitySessionRepo = activitySessionRepo;
        }

        public async Task CreateNewActivity(string activity, ActivityType type)
        {
            var currentTime = DateTime.UtcNow.ToString("MM-yyyy");
            var currentSession = _activitySessionRepo.FindOneByCondition(x => x.Session == currentTime);
            if (currentSession == null)
            {
                currentSession = new ActivitySession
                {
                    Session = DateTime.UtcNow.ToString("MM-yyyy"),
                    CreatedDate = DateTime.UtcNow
                };
                _activitySessionRepo.Create(currentSession);
                
                await _activitySessionRepo.SaveAsync();
            }

            var newActivity = new Activity
            {
                ActivitySessionId = currentSession.Id,
                ActivityRecord = activity,
                Type = type,
                CreatedDate = DateTime.UtcNow
            };
            _activityRepo.Create(newActivity);

            await _activityRepo.SaveAsync();
        }
    }
}
