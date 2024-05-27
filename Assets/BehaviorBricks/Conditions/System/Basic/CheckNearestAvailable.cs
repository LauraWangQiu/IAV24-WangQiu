using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;

namespace BBCore.Conditions
{
    [Condition("Basic/CheckNearestAvailable")]
    [Help("Finds the nearest available GameObject")]
    public class CheckNearestAvailable : ConditionBase
    {
        [InParam("RestaurantRegister")]
        [Help("Reference to the RestaurantRegister")]
        public RestaurantRegister register;

        [OutParam("NearestAvailable")]
        [Help("Reference to the nearest seat available")]
        public GameObject nearestAvailable;

        public override bool Check()
		{
            if (register == null)
            {
                return false;
            }
            nearestAvailable = register.GetNextAvailableSeat();
            if (nearestAvailable != null)
            {
                register.SetSeatAvailable(nearestAvailable, false);
            }
            return nearestAvailable != null;
		}
    }
}
