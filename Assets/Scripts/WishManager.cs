using UnityEngine;
using UnityEngine.UI;

public class WishManager : MonoBehaviour
{
    [SerializeField] private Register register;

    [SerializeField] private Image image;

    void Start()
    {
        if (register == null)
        {
            Debug.LogError("Register is not set");
        }
    }

    void Update()
    {
        if (register != null)
        {
            if (register.wishSprite != null)
            {
                image.sprite = register.wishSprite;
            }
            else
            {
                image.sprite = null;
            }
        }
    }
}
