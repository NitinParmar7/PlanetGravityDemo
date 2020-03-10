using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameObject of body this will be attract by GravityAttractor
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour
{
    // Gravity Attractor reference
    PlanetGravityAttractor attractor;

    // reference Rigidbody attached to game object
    Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        attractor = GameObject.FindGameObjectWithTag("Planet").GetComponent<PlanetGravityAttractor>();
        body = GetComponent<Rigidbody>();

        body.useGravity = false;
        body.constraints = RigidbodyConstraints.FreezeRotation;
    }

    // LateUpdate is called once after update
    void LateUpdate()
    {
        if (attractor != null && body != null)
        {
            attractor.Attract(body);
        }
    }
}
