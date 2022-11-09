using UnityEngine;

[CreateAssetMenu (fileName = "NewAbilityProduct", menuName = "Products/AbilityProduct")]
public class AbilityProductInfo : ProductInfo
{
    [field: SerializeField] public Ability Ability;
}