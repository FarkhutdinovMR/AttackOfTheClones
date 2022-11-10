using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButtonView : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private Image _icon;

    public void Render(Product abilityProduct, Inventory inventory)
    {
        bool isBought = inventory.Contain(Type.GetType(abilityProduct.Name));
        _cost.SetText(isBought ? "" : abilityProduct.Cost.ToString());
        _button.interactable = !isBought;
        _icon.gameObject.SetActive(!isBought);
    }
}