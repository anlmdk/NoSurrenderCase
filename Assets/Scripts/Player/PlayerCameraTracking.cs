using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraTracking : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Vector3 distanceToTarget;
    [SerializeField] private float smoothTrackingTime;

    private void LateUpdate()
    {
        playerTracking();
    }

    private void playerTracking()
    {
        transform.position = Vector3.Lerp(transform.position, distanceToTarget + targetTransform.position, smoothTrackingTime * Time.deltaTime);
    }
}
