using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtEmission : MonoBehaviour
{
    public GameObject foam;
    private ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        foam = GameObject.Find("Foam Particle System");
        ps = foam.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        var emission = ps.emission;
        emission.enabled = true;
    }
}
