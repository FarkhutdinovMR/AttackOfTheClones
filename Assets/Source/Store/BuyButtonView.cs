using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButtonView : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private Image _icon;

    public void Render(AbilityProduct abilityProduct)
    {
        _cost.SetText(abilityProduct.IsBought ? "" : abilityProduct.Info.Cost.ToString());
        _button.interactable = !abilityProduct.IsBought;
        _icon.gameObject.SetActive(!abilityProduct.IsBought);
    }
}