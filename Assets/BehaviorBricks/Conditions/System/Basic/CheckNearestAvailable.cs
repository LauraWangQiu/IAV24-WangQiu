using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;

namespace BBCore.Conditions
{
    [Condition("Basic/CheckNearestAvailable")]
    [Help("Finds the nearest available GameObject")]
    public class CheckNearestAvailable : ConditionBase
    {
        [OutParam("NearestAvailable")]
        [Help("Reference to the nearest seat available")]
        public GameObject nearestAvailable;

        public override bool Check()
		{
            RestaurantRegister register = null;
            GameObject registerObj = GameObject.Find("Register");
            if (registerObj != null)
            {
                register = registerObj.GetComponent<RestaurantRegister>();
                if (register == null)
                {
                    return false;
                }
            }
            else
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
