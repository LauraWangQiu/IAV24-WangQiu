using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;

namespace BBCore.Conditions
{
    [Condition("Basic/CheckBathroom")]
    [Help("Checks if any of the bathrooms are available")]
    public class CheckBathroom : ConditionBase
    {
        [InParam("Register")]
        [Help("Reference to register of the GameObject")]
        public Register register;

        [OutParam("Seat")]
        [Help("Reference to seat")]
        public GameObject seat;

        [OutParam("Bathroom")]
        [Help("Reference to bathroom")]
        public GameObject bathroom;

        public override bool Check()
        {
            if (register == null || register.seat == null)
            {
                Debug.LogError("Register component or seat not found");
                return false;
            }
            GameObject restaurant = GameObject.Find("Register");
            if (restaurant == null)
            {
                Debug.LogError("RestaurantRegister object not found");
                return false;
            }
            RestaurantRegister restaurantRegister = restaurant.GetComponent<RestaurantRegister>();
            if (restaurantRegister == null)
            {
                Debug.LogError("RestaurantRegister component not found");
                return false;
            }
            seat = register.seat;

            if (restaurantRegister.bathrooms.Count == 0)
            {
                return false;
            }

            foreach (GameObject br in restaurantRegister.bathrooms)
            {
                Bathroom bathroomComponent = br.GetComponent<Bathroom>();
                ID clientID = register.gameObject.GetComponent<ID>();
                if (bathroomComponent != null && clientID != null &&
                   (bathroomComponent.id == -1 || bathroomComponent.id == clientID.id))
                {
                    if (bathroomComponent.id == -1 && register.bathroom == null)
                    {
                        bathroomComponent.id = clientID.id;
                        register.bathroom = bathroom = br;
                    }
                    return true;
                }
            }
            return true;
        }
    }
}
