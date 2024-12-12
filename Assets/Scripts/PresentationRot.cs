using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentationRot : MonoBehaviour
{
    // Vitesse de rotation en degrés par seconde
    public float rotationSpeed = 50f;

    void Update()
    {
        // Rotation autour de l'axe Y (axe vertical)
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
