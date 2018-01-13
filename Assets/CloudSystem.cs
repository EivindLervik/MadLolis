using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSystem : MonoBehaviour {

    public Transform player;
    public float cloudSpeed;

    private float distance = 10.0f;

    private float height = 0;
    private float heightDamping = 0;
    private float rotationDamping = 0;



    private void Update()
    {
        transform.Rotate(0, Time.deltaTime * cloudSpeed, 0);
        // rotate 90 degrees around the object's local Y axis:
    }

    private void LateUpdate()
    {
        if (!player)
            return;

        float wantedHeight = player.position.y + height;
        float currentHeight = transform.position.y;

        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
        Vector3 newHeight = player.position; ;
        newHeight.y = currentHeight;
        transform.position = newHeight;
    }
}
