using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;

namespace BBCore.Conditions
{
    [Condition("Basic/CheckAssigned")]
    [Help("Checks if the object is assigned to a seat")]
    public class CheckAssigned : ConditionBase
    {
        [InParam("register")]
        [Help("Reference to Register of the GameObject")]
        public Register register;

        [OutParam("assigned seat")]
        [Help("Reference to the seat assigned")]
        public GameObject seat;

        public override bool Check()
		{
            if (register == null || register.seat == null)
            {
                return false;
            }
            seat = register.seat;
			return seat != null;
		}
    }
}
