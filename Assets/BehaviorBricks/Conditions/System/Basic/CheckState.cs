using Pada1.BBCore.Framework;
using Pada1.BBCore;
using Unity.VisualScripting;

namespace BBCore.Conditions
{
    /// <summary>
    /// It is a basic condition to check if States have the same value.
    /// </summary>
    [Condition("Basic/CheckState")]
    [Help("Checks whether two booleans have the same value")]
    public class CheckState : ConditionBase
    {
        [InParam("register")]
        [Help("Reference to the register of the GameObject")]
        public Register register;

        ///<value>Input the State Parameter to compare.</value>
        [InParam("value", DefaultValue = Register.State.MOVING)]
        [Help("State to compare")]
        public Register.State value = Register.State.MOVING;

        /// <summary>
        /// Checks whether two states have the same value.
        /// </summary>
        /// <returns>the value of compare first State with the second State.</returns>
		public override bool Check()
		{
            if (register != null)
            {
                return register.currentState == value;
            }
			return false;
		}
    }
}