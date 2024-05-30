using Pada1.BBCore.Framework;
using Pada1.BBCore;

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
            return register.paid;
		}
    }
}
