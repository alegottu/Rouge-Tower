using UnityEngine;

[CreateAssetMenu(fileName = "New Item Array", menuName = "Item Array", order = 5)]
public class ItemArray : ScriptableObject
{
    [SerializeField] private Item[] _items = null;
    public Item[] items { get { return _items; } }
}
