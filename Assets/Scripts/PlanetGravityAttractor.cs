using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlanetGravityAttractor : MonoBehaviour
{
    public Vector3 gravityCenter = Vector3.zero; // attractor center of gravity
    public float gravitationConstant = -9.8f; // our attractor gravity

    private float attractorMass; // mass of our planet

    void Start()
    {
        attractorMass = this.GetComponent<Rigidbody>().mass;
    }

    /// <summary>
    /// Attract other body to this GameObject
    /// </summary>
    /// <param name="Body"></param>
    /// <returns></returns>
    public void Attract(Rigidbody attractedBody)
    {
        Vector3 pullVector = FindSurfaceNormal(attractedBody); // Find the surface normal of Attractor
        OrientBody(attractedBody, pullVector); // Orient the player to surface normal

        // Calculate the pull force of attractor
        float pullForce = 0.0f;
        float attractedBodyDistance = Vector3.Distance(this.transform.position + gravityCenter, attractedBody.transform.position);
        
        // Inverse square law - GravityConstant * ((mass1 * mass2) / distance^2)
        pullForce = gravitationConstant * ((attractorMass * attractedBody.mass) / Mathf.Pow(attractedBodyDistance, 2));
        
        // find direction to attractedbody
        pullVector = attractedBody.transform.position - gravityCenter;
        // apply gravitation force
        attractedBody.AddForce(pullVector.normalized * pullForce * Time.deltaTime);
    }

    /// <summary>
    /// Find the surface normal of planet
    /// used for rotation for attractedBody according to 
    /// surface of planet
    /// </summary>
    /// <param name="attractedBody"></param>
    /// <returns></returns>
    Vector3 FindSurfaceNormal(Rigidbody attractedBody)
    {
        float distance = Vector3.Distance(this.transform.position, attractedBody.position);
        Vector3 surfaceNorm = Vector3.zero;

        RaycastHit hit;
        if (Physics.Raycast(attractedBody.transform.position, -attractedBody.transform.up, out hit, distance))
        {
            surfaceNorm = hit.normal;
        }
        return surfaceNorm;
    }

    /// <summary>
    /// Orient attracted body to surface normal of attractor
    /// </summary>
    /// <param name="attractedBody"></param>
    /// <param name="surfaceNormal"></param>
    /// <returns></returns>
    void OrientBody(Rigidbody attractedBody, Vector3 surfaceNormal)
    {
        attractedBody.transform.rotation = Quaternion.FromToRotation(attractedBody.transform.up, surfaceNormal) * attractedBody.rotation;
    }
}
