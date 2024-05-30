using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;

namespace BBCore.Conditions
{
    [Condition("Basic/CheckBill")]
    [Help("Checks if the object is assigned to a seat")]
    public class CheckBill : ConditionBase
    {
        [OutParam("Check")]
        [Help("Reference to the checkout point")]
        public GameObject check;

        [OutParam("Exit")]
        [Help("Reference to the exit point")]
        public GameObject exit;

        public override bool Check()
		{
            check = GameObject.Find("CheckPoint");
            exit = GameObject.Find("SpawnPoint");
			return check != null && exit != null;
		}
    }
}
