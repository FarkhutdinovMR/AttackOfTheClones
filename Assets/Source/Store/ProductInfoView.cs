using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductInfoView : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Image _icon;

    public void Render(AbilityData ability)
    {
        _name.SetText(ability.Name);
        _icon.sprite = ability.Icon;
    }
}