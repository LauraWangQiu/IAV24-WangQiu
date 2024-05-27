using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    [Action("Vector3/GetInstancePosition")]
    [Help("Gets a the position from a given GameObject")]
    public class GetInstancePosition : GOAction
    {
        [InParam("GameObject")]
        [Help("Reference to the GameObject")]
        public GameObject obj { get; set; }

        [OutParam("position")]
        [Help("Position taken from the given GameObject")]
        public Vector3 position { get; set; }

        public override void OnStart()
        {
            if (obj == null)
            {
                Debug.LogError("The GameObject is null", gameObject);
                return;
            }
            Transform transform = obj.transform.Find("InstancePosition");
            if (transform == null)
            {
                Debug.LogError("The GameObject does not have a child named InstancePosition", gameObject);
                return;
            }
            position = transform.position;
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}
