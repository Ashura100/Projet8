using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkFootSolver : MonoBehaviour
{
    [SerializeField] LayerMask terrainLayer = default;
    [SerializeField] Transform body = default;
    [SerializeField] IkFootSolver otherFoot = default;
    [SerializeField] float speed = 1;
    [SerializeField] float stepDistance = 4;
    [SerializeField] float stepLength = 4;
    [SerializeField] float stepHeight = 1;
    [SerializeField] Vector3 footOffset = default;
    [SerializeField] Vector3 footRotOffset = default; // Nouveau
    [SerializeField] float footYPosOffset = 0f; // Nouveau
    [SerializeField] float rayStartYOffset = 0f; // Nouveau
    [SerializeField] float rayLength = 1.5f; // Nouveau
    [SerializeField] bool isMovingForward = false; // Nouveau

    float footSpacing;
    Vector3 oldPosition, currentPosition, newPosition;
    Vector3 oldNormal, currentNormal, newNormal;
    float lerp;

    private void Start()
    {
        footSpacing = transform.localPosition.x;
        currentPosition = newPosition = oldPosition = transform.position;
        currentNormal = newNormal = oldNormal = transform.up;
        lerp = 1;
    }

    void Update()
    {
        footRotOffset.z = body.rotation.eulerAngles.z;
        footRotOffset.y = body.rotation.eulerAngles.y - footRotOffset.y;
        // Position du pied
        transform.position = currentPosition + new Vector3(0, footYPosOffset, 0); // Ajout de l'offset en Y
        transform.up = currentNormal;

        // Ajout de la rotation du pied avec l'offset de rotation
        transform.rotation = Quaternion.Euler(footRotOffset.x, footRotOffset.y, footRotOffset.z);

        // Raycast pour détecter le sol avec l'offset de départ du rayon et sa longueur
        Ray ray = new Ray(body.position + (body.right * footSpacing) + new Vector3(0, rayStartYOffset, 0), Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit info, rayLength, terrainLayer.value))
        {
            if (Vector3.Distance(newPosition, info.point) > stepDistance && !otherFoot.IsMoving() && lerp >= 1)
            {
                lerp = 0;
                int direction = body.InverseTransformPoint(info.point).z > body.InverseTransformPoint(newPosition).z ? 1 : -1;

                // Utilisation du paramètre isMovingForward pour déterminer la direction
                if (isMovingForward)
                {
                    newPosition = info.point + (body.forward * stepLength * direction) + footOffset;
                }
                else
                {
                    newPosition = info.point + footOffset;
                }

                newNormal = info.normal;
            }
        }

        if (lerp < 1)
        {
            Vector3 tempPosition = Vector3.Lerp(oldPosition, newPosition, lerp);
            tempPosition.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

            currentPosition = tempPosition;
            currentNormal = Vector3.Lerp(oldNormal, newNormal, lerp);
            lerp += Time.deltaTime * speed;
        }
        else
        {
            oldPosition = newPosition;
            oldNormal = newNormal;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(newPosition, 0.5f);
    }

    public bool IsMoving()
    {
        return lerp < 1;
    }
}
