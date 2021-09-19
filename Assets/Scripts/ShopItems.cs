using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItems : MonoBehaviour
{
    public static ShopItems current;
    private Dictionary<ItemName, ItemUpgrade> dictionary = new Dictionary<ItemName, ItemUpgrade>();

    void Start()
    {
        current = this;
        dictionary.Add(ItemName.PROJECTILE_STRENGTH, GameObject.Find("Projectile Strength").GetComponent<ItemUpgrade>());
        dictionary.Add(ItemName.DASH_ATTACK_COOLDOWN, GameObject.Find("Dash Attack Cooldown").GetComponent<ItemUpgrade>());
        dictionary.Add(ItemName.BURST_ATTACK_COOLDOWN, GameObject.Find("Burst Attack Cooldown").GetComponent<ItemUpgrade>());
        dictionary.Add(ItemName.BURST_SHOTS, GameObject.Find("Burst Attack Amount").GetComponent<ItemUpgrade>());
        dictionary.Add(ItemName.VISIBLE_AREA_GAIN, GameObject.Find("Visible Area Gain").GetComponent<ItemUpgrade>());
        dictionary.Add(ItemName.CELL_REGENERATION, GameObject.Find("Cell Regeneration").GetComponent<ItemUpgrade>());
        dictionary.Add(ItemName.REWARD_BONUS, GameObject.Find("Reward Bonus").GetComponent<ItemUpgrade>());
    }

    public ItemUpgrade Get(ItemName itemName)
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
    BURST_ATTACK_COOLDOWN,
    BURST_SHOTS,
    VISIBLE_AREA_GAIN,
    CELL_REGENERATION,
    REWARD_BONUS
}
