using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationUpdate : MonoBehaviour
{
    public Transform cameraTransform;
    private LocationInfo lastLocation;
    public float moveScale = 1f;
    public float lerpSpeed = 1f;

    void Start()
    {
        cameraTransform = transform;
        lastLocation = Input.location.lastData;
        StartCoroutine(UpdateLocation());
    }

    IEnumerator UpdateLocation()
    {
        while (true)
        {
            float latChange = Input.location.lastData.latitude - lastLocation.latitude;
            float lonChange = Input.location.lastData.longitude - lastLocation.longitude;

            Vector3 movement = new Vector3(lonChange, 0, latChange) * moveScale;
            Vector3 newPosition = cameraTransform.position + movement;

            cameraTransform.position = Vector3.Lerp(cameraTransform.position, newPosition, Time.deltaTime * lerpSpeed);

            lastLocation = Input.location.lastData;

            yield return new WaitForSeconds(1);
        }
    }
}
