﻿using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    [Action("GameObject/ReturnNextClientWithOrder")]
    [Help("Returns the client with order")]
    public class ReturnNextClientWithOrder : GOAction
    {
        [OutParam("client")]
        [Help("The client with order")]
        public GameObject client;

        public override void OnStart()
        {
            Register register = gameObject.GetComponent<Register>();
            if (register != null && register.toGive.Count > 0)
            {
                client = register.toGive[0];
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (client == null)
                return TaskStatus.FAILED;
            return TaskStatus.COMPLETED;
        }
    }
}
