using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;

namespace BBCore.Conditions
{
    [Condition("Basic/CheckPaid")]
    [Help("Checks if the object is assigned to a seat")]
    public class CheckPaid : ConditionBase
    {
        [InParam("register")]
        [Help("Reference to the register")]
        public Register register;

        public override bool Check()
		{
            if (register == null)
            {
                return true;
            }
            if (register.seat != null)
            {
                SetOnTrigger trigger = register.seat.GetComponent<SetOnTrigger>();
                if (trigger != null)
                {
                    trigger.ResetSeat();
                }
            }
            RestaurantRegister restaurantRegister = GameObject.FindObjectOfType<RestaurantRegister>();
            if (restaurantRegister != null)
            {
                restaurantRegister.RemoveClient(register.gameObject);
            }
            return register.paid;
		}
    }
}
