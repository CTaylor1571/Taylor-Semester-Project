using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LockOnMouse : MonoBehaviour
{

    public float maxTurnSpeed = 240;
    public float smoothTime = 0.3f;
    float angle;
    float currentVelocity;

    // Update is called once per frame
    void Update()
    {
        PointToMouse();
    }

    void PointToMouse() //https://gamedevbeginner.com/make-an-object-follow-the-mouse-in-unity-in-2d/#look_at_mouse_2D
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        float targetAngle = Vector2.SignedAngle(Vector2.right, direction);
        angle = Mathf.SmoothDampAngle(angle, targetAngle, ref currentVelocity, smoothTime, maxTurnSpeed);
        transform.eulerAngles = new Vector3(0, 0, angle);

    }
}
