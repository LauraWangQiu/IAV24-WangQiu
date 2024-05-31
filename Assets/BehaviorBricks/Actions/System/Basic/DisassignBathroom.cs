using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBCore.Actions
{
    [Action("Basic/DisassignBathroom")]
    [Help("Disassigns the bathroom")]
    public class DisassignBathroom : BasePrimitiveAction
    {
        [InParam("Register")]
        [Help("Reference to register of the GameObject")]
        public Register register;

        public override void OnStart()
        {
            if (register == null)
            {
                Debug.LogError("Register component not found");
                return;
            }
            GameObject restaurant = GameObject.Find("Register");
            if (restaurant == null)
            {
                Debug.LogError("RestaurantRegister object not found");
                return;
            }
            RestaurantRegister restaurantRegister = restaurant.GetComponent<RestaurantRegister>();
            if (restaurantRegister == null)
            {
                Debug.LogError("RestaurantRegister component not found");
                return;
            }
            foreach (GameObject br in restaurantRegister.bathrooms)
            {
                Bathroom bathroomComponent = br.GetComponent<Bathroom>();
                ID clientID = register.gameObject.GetComponent<ID>();
                if (bathroomComponent != null && clientID != null &&
                    bathroomComponent.id == clientID.id)
                {
                    bathroomComponent.id = -1;
                    return;
                }
            }
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}
