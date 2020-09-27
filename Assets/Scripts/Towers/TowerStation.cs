using System.Collections.Generic;
using UnityEngine;

public class TowerStation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer towerDisplay = null;
    [SerializeField] private TowerArray towers = null;

    private Player player = null;
    private GameObject[] possibleTowers = new GameObject[3];

    private void Awake()
    {
        List<int> possibleNums = new List<int>();
        for (int i = 0; i < towers.towers.Length; i++)
        {
            possibleNums.Add(i);
        }

        for (int n = 0; n < possibleTowers.Length; n++)
        {
            int index = Random.Range(0, possibleNums.Count - 1);
            possibleNums.Remove(index);
            possibleTowers[n] = towers.towers[index];
        }
    }

    private void OnEnable()
    {
        ClickSelect.OnSelectedObjectChange += SelectedObjectEventHandler;
    }

    private void Update()
    {
        towerDisplay.enabled = player;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        this.player = collider.TryGetComponent(out Player player) ? player : this.player;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        this.player = collider.TryGetComponent(out Player player) ? null : this.player;
    }

    private void SelectedObjectEventHandler(GameObject obj, Vector3 clickPoint)
    {
        if (obj.Equals(gameObject) && player)
        {
            BoxCollider2D[] colliders = obj.GetComponents<BoxCollider2D>();
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].OverlapPoint(clickPoint))
                {
                    Instantiate(possibleTowers[i], transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnDisable()
    {
        ClickSelect.OnSelectedObjectChange -= SelectedObjectEventHandler;
    }
}
