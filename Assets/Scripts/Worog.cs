using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worog : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        var transformPosition = collider.GetComponent<Nijia>().getPosition();
        collider.transform.position = transformPosition.position;
        if (collider.GetComponent<Nijia>() != null)
        {
            collider.gameObject.GetComponent<HeartSystem>().HeartsPlayer();
        }
    }
}
