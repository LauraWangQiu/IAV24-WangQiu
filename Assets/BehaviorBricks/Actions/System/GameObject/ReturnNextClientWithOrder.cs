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
            RestaurantRegister register = GameObject.FindAnyObjectByType<RestaurantRegister>();
            if (register != null && register.orders.Count > 0)
            {
                client = register.orders[0].transform.Find("ContactPosition").gameObject;
            }
            else
            {
                client = null;
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
