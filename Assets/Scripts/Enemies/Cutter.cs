using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter : Robot {

    [Header("Cutter")]
    public float floatHeight;
    public float floatSpeed;
    public LayerMask cutterMask;
    public Transform blade;
    public float bladeSpeed;
    public float attackDistance;
    public float acceleration;

    private float speed;

    private void Awake()
    {
        Setup();
    }

    private void FixedUpdate()
    {
        if(model != null)
        {
            //Move
            Vector3 pos = transform.position;

            Ray ray = new Ray(model.position - transform.up, -transform.up);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000.0f ,cutterMask))
            {
                pos.y = hit.point.y + floatHeight + (Mathf.Sin(Time.time) / 10.0f);
            }

            transform.position = pos;

            // Rotate blade
            blade.Rotate(Vector3.up, bladeSpeed, Space.Self);

            // Detect player
            if(!persuit && target != null && (target.position - transform.position).magnitude <= attackDistance)
            {
                persuit = true;
            }

            // Follow or roam
            if (persuit)
            {
                speed = Mathf.Lerp(speed, floatSpeed, Time.fixedDeltaTime * acceleration);

                Vector3 lookAtPos = new Vector3(target.position.x, transform.position.y, target.position.z);
                transform.LookAt(lookAtPos);
                transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime, Space.Self);
            }
            else
            {
                speed = Mathf.Lerp(speed, 0.0f, Time.fixedDeltaTime * acceleration);
            }
        }
    }
}
