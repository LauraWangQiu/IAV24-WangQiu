using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;

namespace BBCore.Conditions
{
    [Condition("Basic/CheckFood")]
    [Help("Checks if there is food and the cooldown of any is less than value")]
    public class CheckFood : ConditionBase
    {
        [InParam("Value")]
        [Help("Limit cooldown time")]
        public float value;

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

            GameObject foodPoint = GameObject.Find("FoodPoint");
            if (foodPoint == null)
            {
                return false;
            }

            FoodPoint foodPointComp = foodPoint.GetComponent<FoodPoint>();
            if (foodPointComp == null)
            {
                return false;
            }

            RestaurantRegister restaurantRegister = GameObject.FindAnyObjectByType<RestaurantRegister>();
            if (restaurantRegister == null)
            {
                return false;
            }

            bool cooldown = false, isThereMoney = false;
            foreach (GameObject food in foodPointComp.foodList)
            {
                if (food == null)
                {
                    continue;
                }

                RegisterObject foodComp = food.GetComponent<RegisterObject>();
                if (foodComp != null && foodComp.timeToCoolDown <= value)
                {
                    cooldown = true;
                    break;
                }
            }

            foreach (GameObject client in restaurantRegister.clients)
            {
                Register clientRegister = client.GetComponent<Register>();
                if (clientRegister != null && clientRegister.leave)
                {
                    isThereMoney = true;
                    break;
                }
            }
            return cooldown || (restaurantRegister.ordersToComplete.Count == 0 && !isThereMoney);
        }
    }
}
