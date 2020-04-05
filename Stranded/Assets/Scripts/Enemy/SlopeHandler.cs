using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeHandler : MonoBehaviour
{
	public int SlopeAdjustSpeed;
    public int RotationMinAngle;    
	public Vector3 OriginOffset;
    public float DistanceFromGround;

    // Update is called once per frame
    void Update()
    {
        // Ray Origin Position
        Vector3 origin = transform.position;

        // Get GroundLayerIndex
        int GroundLayerIndex = LayerMask.NameToLayer("Ground");
        int layerMask = (1 << GroundLayerIndex);

        // Ray
        RaycastHit GroundHit;

        // Cast Ray to Ground
        if(Physics.Raycast(origin + OriginOffset, Vector3.down, out GroundHit, 50, layerMask)) {
            // Calculate new Rotatation based on ground angle
            Quaternion newRotation = Quaternion.FromToRotation(transform.up, GroundHit.normal) * transform.rotation;
            
            // Keep distance from ground same
            float Change = GroundHit.distance - DistanceFromGround;
            transform.position = new Vector3(transform.position.x, transform.position.y - Change , transform.position.z);
            
            // If angle is greater than RotationMinAngle Rotate enemy
            if(Vector3.Angle(Vector3.up, GroundHit.normal) >= RotationMinAngle) 
            {
                // Set new Rotation
                transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * SlopeAdjustSpeed);
            }
        }
    }
}
