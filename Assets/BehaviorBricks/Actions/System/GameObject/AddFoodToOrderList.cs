using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    [Action("GameObject/AddFoodToOrderList")]
    [Help("Adds food to order list")]
    public class AddFoodToOrderList : GOAction
    {
        public override void OnStart()
        {
            RestaurantRegister register = GameObject.FindAnyObjectByType<RestaurantRegister>();
            if (register != null)
            {
                // Anade el cliente a la lista de ordenes
                register.AddOrder(gameObject);
                register.AddOrderToComplete(gameObject);
            }
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}
