using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDrop : MonoBehaviour
{

    [HideInInspector]
    public PoisonDripper PoisonDripper;
    // Start is called before the first frame update
    void Start()
    {
        DestroyMe();
    }


    void DestroyMe()
    {
        Destroy(this.gameObject,3);
    }


    private void OnTriggerEnter(Collider other)
    {
        Candy _candy = other.GetComponent<Candy>();

        if (_candy)
        {
            _candy.MColorCode = PoisonDripper.MColorCode;
            GameUtils.SwapMaterialsAndMesh(_candy.gameObject, PoisonDripper.RefCandy.gameObject);
            _candy.IsCandyRoten = true ;
            _candy.MColorCode = ColorCode.Roten;

        }
    }
}
