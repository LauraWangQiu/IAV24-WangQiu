using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;

namespace BBCore.Conditions
{
    [Condition("Basic/CheckOrder")]
    [Help("Checks if there is any client to take order")]
    public class CheckOrder : ConditionBase
    {
        [OutParam("GetFoodPoint")]
        [Help("Place where to get food")]
        public GameObject getFoodPoint;

        public override bool Check()
        {
            GameObject gfp = GameObject.Find("GetFoodPoint");
            if (gfp != null)
            {
                getFoodPoint = gfp;
            }

            RestaurantRegister restaurantRegister = GameObject.FindAnyObjectByType<RestaurantRegister>();
            if (restaurantRegister == null)
            {
                return false;
            }

            return restaurantRegister.orders.Count > 0;
        }
    }
}
