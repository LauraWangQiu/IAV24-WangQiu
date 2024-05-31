using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;

namespace BBCore.Conditions
{
    [Condition("Basic/CheckToGive")]
    [Help("Checks if there is any client to give order")]
    public class CheckToGive : ConditionBase
    {
        [InParam("Register")]
        [Help("Reference to the Register of the GameObject")]
        public Register register;

        public override bool Check()
        {
            if (register == null)
            {
                return false;
            }
            return register.toGive.Count > 0;
        }
    }
}
