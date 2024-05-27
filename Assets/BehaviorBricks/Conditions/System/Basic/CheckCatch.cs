using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;

namespace BBCore.Conditions
{
    [Condition("Basic/CheckCatch")]
    [Help("Checks if the object is caught")]
    public class CheckCatch : ConditionBase
    {
        [InParam("GameObject")]
        [Help("Reference to the GameObject")]
        public GameObject obj;

        [InParam("Register")]
        [Help("Reference to register of the GameObject")]
        public Register register;

        [OutParam("Seat")]
        [Help("Reference to seat")]
        public GameObject seat;

        public override bool Check()
		{
            if (obj == null)
            {
                return false;
            }
            RegisterObject registerObject = obj.GetComponent<RegisterObject>();
            if (registerObject == null)
            {
                Debug.LogError("RegisterObject component not found");
                return false;
            }
            if (register == null || register.seat == null)
            {
                Debug.LogError("Register component or seat not found");
                return false;
            }
            seat = register.seat;
			return !registerObject.caught && register.seat != null;
		}
    }
}
