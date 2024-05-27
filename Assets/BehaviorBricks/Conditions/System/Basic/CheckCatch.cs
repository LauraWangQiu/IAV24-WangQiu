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
			return !registerObject.caught;
		}
    }
}