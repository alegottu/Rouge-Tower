using UnityEngine;

public class RandomGeneration : MonoBehaviour
{
    [SerializeField] public GameObject[] tile;
    [SerializeField] float xChange, yChange = 0;
    [SerializeField] float margin = 0;

    private int[] arr;
    private int rand;
    private int ctr = -1;

    void Start()
    {
        arr = new int[tile.Length];
        for (int i = 0; i < tile.Length; i++)
        {
            rand = Random.Range(0, 2);
            xChange = (Random.Range(0, 2) == 0 && xChange > 0) ? -xChange : xChange;
            yChange = (Random.Range(0, 2) == 0 && yChange > 0) ? -yChange : yChange;

            Vector3 offset = rand == 0 ? new Vector3(0, yChange, 0) : new Vector3(xChange, 0, 0);
            Instantiate(tile[GetRandomInt(0, tile.Length - 1)], offset, Quaternion.identity);
            
            if (rand == 0)
            {
                if (yChange > 0)
                {
                    yChange += margin;
                }
                else
                {
                    yChange -= margin;
                }
            }
            else
            {
                if (xChange > 0)
                {
                    xChange += margin;
                }
                else
                {
                    xChange -= margin;
                }
            }
        }
    }

    private int GetRandomInt(int min, int max)
    {
        ctr++;
        int num = 0;
        num = Random.Range(min, max + 1);
        arr[ctr] = num;
        if (IsDuplicate(arr[ctr], ctr, arr))
        {
            ctr--;
            return GetRandomInt(min, max);
        }
        return num;
    }

    private bool IsDuplicate(int n, int index, int[] array)
    {
        int recurse = 0;
        for (int i = 1; i < array.Length; i++)
        {
            if (n == array[(array.Length - 1) - (recurse + i)] && (array.Length - 1) - (recurse + i) != index)
            {
                return true;
            }
        }
        return false;
    }
}
