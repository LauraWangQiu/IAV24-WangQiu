using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;

namespace BBCore.Conditions
{
    [Condition("Basic/CheckWaitPoint")]
    [Help("Checks where is the WaitPoint")]
    public class CheckWaitPoint : ConditionBase
    {
        [OutParam("WaitPoint")]
        [Help("The WaitPoint")]
        public GameObject waitPoint;

        public override bool Check()
        {
            GameObject wp = GameObject.Find("WaitPoint");
            if (wp == null)
            {
                Debug.LogError("WaitPoint object not found");
                return false;
            }
            waitPoint = wp;
            return true;
        }
    }
}