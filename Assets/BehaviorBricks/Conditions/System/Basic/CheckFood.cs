using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;

namespace BBCore.Conditions
{
    [Condition("Basic/CheckFood")]
    [Help("Checks if there is food and the cooldown of any is less than value")]
    public class CheckFood : ConditionBase
    {
        [OutParam("Value")]
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
            if (foodPointComp == null || !foodPointComp.isThereFood)
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
            return foodPointComp.isThereFood && (cooldown || (restaurantRegister.orders.Count == 0 && !isThereMoney));
        }
    }
}
