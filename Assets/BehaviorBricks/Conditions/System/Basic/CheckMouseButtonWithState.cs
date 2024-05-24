using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;

namespace BBCore.Conditions
{
    /// <summary>
    /// It is a basic condition to check if the mouse button has been pressed.
    /// </summary>
    [Condition("Basic/CheckMouseButtonWithState")]
    [Help("Checks whether a mouse button is pressed")]
    public class CheckMouseButtonWithState : ConditionBase
    {
        ///<value>Enum to identify the type of mouse button.</value>
        ///<summary></summary>
        public enum MouseButton {
            /// <summary>Left button mouse.</summary>
            left = 0,
            /// <summary>right button mouse.</summary>
            right = 1,
            /// <summary>center button mouse.</summary>
            center = 2};

        ///<value>Input Mouse Button Name Parameter, left mouse button by default.</value>
        [InParam("button", DefaultValue = MouseButton.left)]
        [Help("Mouse button expected to be pressed")]
        public MouseButton button = MouseButton.left;

        [InParam("register")]
        [Help("Reference to the register of the GameObject")]
        public Register register;

        /// <summary>
        /// Checks whether the mouse button is pressed.
        /// </summary>
        /// <returns>True if the mouse button is pressed.</returns>
        public override bool Check()
        {
            if (register != null && register.currentState == Register.State.SIT)
            {
                return false;
            }
            return Input.GetMouseButton((int)button);
        }
    }
}
