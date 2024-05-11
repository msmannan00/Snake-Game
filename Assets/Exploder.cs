using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{

    public float ExlosionForce = 50f;
    // Start is called before the first frame update
    void Start()
    {
        StaticAudioManager.Instance.PlayCandyBreakSound();
        Breakable[] AllBreakables = GetComponentsInChildren<Breakable>();
        foreach (var breakable in AllBreakables)
        {
            breakable.GetComponent<Rigidbody>().AddExplosionForce(ExlosionForce, transform.position, 10);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
