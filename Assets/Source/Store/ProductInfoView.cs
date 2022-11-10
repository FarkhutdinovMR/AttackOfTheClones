using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductInfoView : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Image _icon;

    public void Render(Product product)
    {
        _name.SetText(product.Name.ToString());
        _icon.sprite = product.Icon;
    }
}