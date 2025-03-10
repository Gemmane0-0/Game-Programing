using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityHor = 9.0f;
        

    // Update is called once per frame
    void Update() {
        if (axes == RotationAxes.MouseX) {
            // Horizontal rotation
            transform.Rotate(0, sensitivityHor * Input.GetAxis("Mouse X"), 0);
        }
    }
}
