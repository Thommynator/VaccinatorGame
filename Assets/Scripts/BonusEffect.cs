using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BonusEffect : MonoBehaviour
{

    private float factor;
    private TextMeshProUGUI textField;

    void Start()
    {
        factor = ShopItems.current.GetValueOf(ItemName.REWARD_BONUS);
        textField = GetComponentInChildren<TextMeshProUGUI>();
        StartCoroutine(ShowEffectCoroutine());
    }

    private IEnumerator ShowEffectCoroutine()
    {
        textField.text = string.Format("{0:F1}", factor) + "x";
        transform.localScale = Vector3.zero;
        LeanTween.scale(this.gameObject, Vector3.one, 0.3f).setEaseInCubic();
        yield return new WaitForSeconds(1.5f);
        LeanTween.scale(this.gameObject, Vector3.zero, 0.3f).setEaseOutCubic().setOnComplete(() => Destroy(this.gameObject));
    }


}
