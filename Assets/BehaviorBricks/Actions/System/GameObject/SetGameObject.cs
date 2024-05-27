using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    [Action("GameObject/SetGameObject")]
    [Help("Sets the game object")]
    public class SetGameObject : GOAction
    {
        [InParam("register")]
        [Help("Reference to Register of the GameObject")]
        public Register register;

        [InParam("game object")]
        [Help("Set GameObject")]
        public GameObject gameobject;

        [OutParam("game object")]
        [Help("Set GameObject")]
        public GameObject setobject;

        public override void OnStart()
        {
            RegisterObject registerObject = gameobject.GetComponent<RegisterObject>();
            if (registerObject != null)
            {
                registerObject.client = register.gameObject;
            }
            setobject = gameobject;

            if (register != null)
            {
                register.wish = gameobject;
            }
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}
