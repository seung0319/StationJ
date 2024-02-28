using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemainingDistance : MonoBehaviour
{
    Transform player;
    [SerializeField]Text metersText;

    private void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(player.position,transform.position);
            metersText.text = $"{(int)distance}M ÈÄ";
        }
    }
    public void PlayerTransformSet(Transform player)
    {
        this.player = player;
    }
}
