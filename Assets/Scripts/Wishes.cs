using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameObjectWithSprite
{
    public GameObject gameObject;
    public Sprite sprite;
    public float cost;
}

public class Wishes : MonoBehaviour
{
    public List<GameObjectWithSprite> wishes = new List<GameObjectWithSprite>();

    public Sprite GetWishSprite(GameObject wish)
    {
        foreach (var wishWithSprite in wishes)
        {
            if (wishWithSprite.gameObject == wish)
            {
                return wishWithSprite.sprite;
            }
        }
        return null;
    }

    public float GetWishCost(GameObject wish)
    {
        foreach (var wishWithSprite in wishes)
        {
            if (wishWithSprite.gameObject.CompareTag(wish.tag))
            {
                return wishWithSprite.cost;
            }
        }
        return 0;
    }
}
