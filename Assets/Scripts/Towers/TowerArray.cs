using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Array", menuName = "Tower Array", order = 2)]
public class TowerArray : ScriptableObject
{
    [SerializeField] private GameObject[] _towers = null;
    public GameObject[] towers { get { return _towers; } }
}
