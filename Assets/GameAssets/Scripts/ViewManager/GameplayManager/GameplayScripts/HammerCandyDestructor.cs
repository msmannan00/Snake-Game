using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerCandyDestructor : RottenCandySprayer
{

    public bool isTriggered = false;
    public GameObject ExplodeableCandy;

    protected override void OnTriggerEnter(Collider other)
    {
        if (isTriggered)
            return;
        isTriggered = true;
        base.OnTriggerEnter(other);

        Candy _candy = other.GetComponent<Candy>();

        if (_candy)
        {
            if (_candy.IsCandyRoten)
            {
                MoveSpiralRoot.Instance.CandyTrashed(_candy);
                StaticAudioManager.Instance.PlayHammerHitSound();
                Instantiate(ExplodeableCandy, _candy.transform.position, Quaternion.identity);
                Destroy(_candy.gameObject);
            }
        }
    }

    public void MakeItAvailable()
    {
        isTriggered = false;
    }
}
