using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButtonView : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private Image _icon;

    public void Render(AbilityData ability)
    {
        _cost.SetText(ability.IsBought ? "" : ability.Cost.ToString());
        _button.interactable = !ability.IsBought;
        _icon.gameObject.SetActive(!ability.IsBought);
    }
}