using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Array", menuName = "Enemy Array", order = 4)]
public class EnemyArray : ScriptableObject
{
    [SerializeField] private GameObject[] _enemies = null;
    public GameObject[] enemies { get { return _enemies; } }

    [SerializeField] private float[] _timePerEnemy = null;
    public float[] timePerEnemy { get { return _timePerEnemy; } }
}
