using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackableEntities;

namespace Repository.EF
{
    public class StateHelper
    {
        public static EntityState ConvertState(TrackingState state)
        {
            switch (state)
            {
                case TrackingState.Added:
                    return EntityState.Added;

                case TrackingState.Modified:
                    return EntityState.Modified;

                case TrackingState.Deleted:
                    return EntityState.Deleted;

                default:
                    return EntityState.Unchanged;
            }
        }

        public static TrackingState ConvertState(EntityState state)
        {
            switch (state)
            {
                case EntityState.Detached:
                    return TrackingState.Unchanged;

                case EntityState.Unchanged:
                    return TrackingState.Unchanged;

                case EntityState.Added:
                    return TrackingState.Added;

                case EntityState.Modified:
                    return TrackingState.Modified;

                case EntityState.Deleted:
                    return TrackingState.Deleted;

                default:
                    throw new ArgumentOutOfRangeException();

            }
        }
    }
}
