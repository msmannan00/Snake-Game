using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorCode {Pink,Blue,SeaBlue,Orange,Gray,Green,Roten}
public class CandyColorFormatter : MonoBehaviour
{
    public ColorCode MColorCode;
    public GameObject RefCandy;
    // Start is called before the first frame update




    private void OnTriggerEnter(Collider other)
    {
        Candy _candy;
        if(_candy = other.gameObject.GetComponent<Candy>())
        {
            if (_candy.IsCandyRoten) return;
            _candy.MColorCode = MColorCode;

            GameUtils.SwapMaterialsAndMesh(_candy.gameObject, RefCandy);
           
        }
    }
}
