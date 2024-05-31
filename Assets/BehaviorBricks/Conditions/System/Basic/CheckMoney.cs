using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;

namespace BBCore.Conditions
{
    [Condition("Basic/CheckMoney")]
    [Help("Checks if there is any client to charge money")]
    public class CheckMoney : ConditionBase
    {
        [OutParam("CheckPoint")]
        [Help("Place to charge money")]
        public GameObject checkPoint;

        public override bool Check()
        {
            GameObject cp = GameObject.Find("CheckPoint");
            if (cp != null)
            {
                checkPoint = cp;
            }

            RestaurantRegister restaurantRegister = GameObject.FindAnyObjectByType<RestaurantRegister>();
            if (restaurantRegister == null)
            {
                return false;
            }

            if (restaurantRegister.clients.Count == 0)
            {
                return false;
            }

            foreach (GameObject client in restaurantRegister.clients)
            {
                Register register = client.GetComponent<Register>();
                if (register != null && register.leave && !register.paid)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
