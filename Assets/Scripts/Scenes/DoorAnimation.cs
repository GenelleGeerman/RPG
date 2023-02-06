using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorAnimation : MonoBehaviour
{
    [SerializeField] private bool isDoorLocked;
    [SerializeField] private Color color;
    [SerializeField] private Sprite closeSprite;
    [SerializeField] private Sprite openSprite;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Player" || collision.tag == "npc") && !isDoorLocked)
        {
            Physics2D.IgnoreCollision(this.GetComponent<EdgeCollider2D>(), collision, true);
            GetComponent<SpriteRenderer>().sprite = openSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "npc")
        {
            Physics2D.IgnoreCollision(this.GetComponent<EdgeCollider2D>(), collision, false);
            GetComponent<SpriteRenderer>().sprite = closeSprite;
        }
    }
}
