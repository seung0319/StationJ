using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoiRotationSet : MonoBehaviour
{
    Transform playerTransform;

    // Update is called once per frame
    void Update()
    {
        if(playerTransform != null)
        {
            Vector3 dir = this.transform.position - playerTransform.transform.position;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), 0.05f);
        }
    }

    public void SetPlayerTransform(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
    }
}
