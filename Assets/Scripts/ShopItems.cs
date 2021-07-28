using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItems : MonoBehaviour
{
    public static ShopItems current;
    private Dictionary<ItemName, Upgrade> dictionary = new Dictionary<ItemName, Upgrade>();

    void Start()
    {
        current = this;
        dictionary.Add(ItemName.PROJECTILE_STRENGTH, GameObject.Find("Projectile Strength").GetComponent<Upgrade>());
        dictionary.Add(ItemName.DASH_ATTACK_COOLDOWN, GameObject.Find("Dash Attack Cooldown").GetComponent<Upgrade>());
        dictionary.Add(ItemName.BURST_ATTACK_COOLDOWN, GameObject.Find("Burst Attack Cooldown").GetComponent<Upgrade>());
    }

    public Upgrade Get(ItemName itemName)
    {
        return dictionary[itemName];
    }

    public float GetValueOf(ItemName itemName)
    {
        return Get(itemName).GetValue();
    }

    

}

public enum ItemName
{
    PROJECTILE_STRENGTH,
    DASH_ATTACK_COOLDOWN,
    BURST_ATTACK_COOLDOWN
}
