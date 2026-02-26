using UnityEngine;
[CreateAssetMenu(
    fileName = "Item",
    menuName = "NewItem"
)]
public class itemso : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public int maxStackSize;
    public GameObject itemPrefab;
    public GameObject HanditemPrefab;
    internal Sprite icon;
}
