using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RottenCandySprayer : MonoBehaviour
{
    public GameObject RefCandy;
    public ColorCode MColorCode;
    public bool CanMakeCandyRot;



    private void OnTriggerEnter(Collider other)
    {
        Candy _candy = other.GetComponent<Candy>();

        if (_candy)
        {
            _candy.MColorCode = MColorCode;
            _candy.IsCandyRoten = CanMakeCandyRot;
            GameUtils.SwapMaterialsAndMesh(_candy.gameObject, RefCandy);
            _candy.MColorCode = ColorCode.Roten;

        }
    }
}
