﻿using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    /// It is an action to obtain the Gameobject where the mouse cursor is pointing.
    /// </summary>
    [Action("Physics/FromMouseToWorldWithState")]
    [Help("Gets the game object and position that is under the mouse cursor")]
    public class FromMouseToWorldWithState : GOAction
    {
        ///<value>OutPut Game object under the mouse cursor Parameter.</value>
        [OutParam("selectedGameObject")]
        [Help("Game object under the mouse cursor")]
        public GameObject selectedGameObject;

        ///<value>OutPut Position under the mouse cursor Parameter.</value>
        [OutParam("selectedPosition")]
        [Help("Position under the mouse cursor")]
        public Vector3 selectedPosition;

        ///<value>Input Camera Parameter that is currently used to rendering the scene.</value>
        [InParam("camera")]
        [Help("Camera that is currently used to rendering the scene. If not assigned Camera.main is used")]
        public Camera camera;

        ///<value>Input LayerMask with layers Parameter that must be considered relevant to 
        ///calculate the game object and position under the mouse cursor.</value>
        [InParam("mask")]
        [Help("LayerMask with layers that must be considered relevant to calculate the game object and position under the mouse cursor")]
        public LayerMask mask;

        [InParam("register")]
        [Help("Reference to the register of the GameObject")]
        public Register register;

        /// <summary>Initialization Method of FromMouseToWorld.</summary>
        public override void OnStart()
        {
        }


        /// <summary>Method of Update of FromMouseToWorld</summary>
        /// <remarks>Verify the status of the task, if there is no associated camera assigns the main camera, launches a raycast where it is
        /// pointing the mouse and if it finds an object it returns that object and Complete the task. Otherwise, the task fails. </remarks>
        public override TaskStatus OnUpdate()
        {
            if (camera == null)
                camera = Camera.main;

            if (register != null && register.currentState == Register.State.IDLE)
            {
                register.currentState = Register.State.MOVING;
                selectedPosition = register.GetExitPosition();
                Ray ray = camera.ScreenPointToRay(selectedPosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000, mask))
                {
                    selectedGameObject = hit.collider.gameObject;
                }
                return TaskStatus.COMPLETED;
            }
            else
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000, mask))
                {
                    selectedPosition = hit.point;
                    selectedGameObject = hit.collider.gameObject;

                    return TaskStatus.COMPLETED;
                }
                return TaskStatus.FAILED;
            }
        }
    }
}
