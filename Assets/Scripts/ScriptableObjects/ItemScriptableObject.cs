using UnityEngine;

[CreateAssetMenu(fileName = "ItemScriptableObject", menuName = "ScriptableObjects/Items")]
public class ItemScriptableObject : ScriptableObject
{
    public string itemName;
    public int boxCapacity;

}
