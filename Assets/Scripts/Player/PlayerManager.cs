using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static List<Player> players = new List<Player>();
    public static Action<Player> OnPlayerActive;

    [SerializeField] private GameObject[] characters = null;

    private void Update()
    {
        if (StageManager.currentStage && players.Count < 1)
        {
            //if other characters are added add a placeholder menu for choosing a character later and store the index in a variable

            GameObject playerObj = Instantiate(characters[0], Vector3.zero, Quaternion.identity);
            Player player = playerObj.GetComponent<Player>();

            players.Add(player);
            OnPlayerActive?.Invoke(player);
        }
    }
}
