using UnityEngine;

[CreateAssetMenu(fileName = "ItemScriptableObject", menuName = "ScriptableObjects/Items")]
public class ItemScriptableObject : ScriptableObject
{
    public string productName;
    public string productDescription;
    public int productCost;

}
