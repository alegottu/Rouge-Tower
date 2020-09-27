using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public float dropRate = 0;
    public int rarity = 1; //1 = common, 2 = uncommon, 3 = rare 

    protected Player player = null;

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Player player))
        {
            this.player = player;
            player.GetComponent<PlayerUI>().AddItem(this, gameObject);
        }
    }
}
