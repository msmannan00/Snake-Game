using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StripeBlock
{
    public ColorCode BaseColor;
    public GameObject RefCandy;
}
public class CandyColorStripper : MonoBehaviour
{
    public ColorCode MColorCode;
    public List<StripeBlock> StripeBlocks = new List<StripeBlock>();



    private void OnTriggerEnter(Collider other)
    {
        Candy _candy = other.GetComponent<Candy>();

        if (_candy)
        {
            GameObject RefCandy = null;
            foreach (var strBlock in StripeBlocks)
            {
                if (strBlock.BaseColor == _candy.MColorCode)
                    RefCandy = strBlock.RefCandy;
            }
           
            Debug.LogError("changing mesh of candy");

            if (RefCandy == null) return;
            GameUtils.SwapMaterialsAndMesh(_candy.gameObject, RefCandy);
           
        }
        StaticAudioManager.Instance.playPaperSound();
    }
}
