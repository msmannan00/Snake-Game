using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CakeBaker : MonoBehaviour
{

    public bool IsEmpty = true;
    Transform candyBaked;

    private void OnTriggerEnter(Collider other)
    {
        if (!IsEmpty) return;
        Candy _candy = other.GetComponent<Candy>();

        if (_candy)
        {
            IsEmpty = false;
            MoveSpiralRoot.Instance.CandyBaked(_candy);

            candyBaked = _candy.transform;
           // _candy.transform.DOMove(transform.position, 0.3f);

        }
    }


    private void Update()
    {
        if (candyBaked)
        {
            candyBaked.transform.position = Vector3.Lerp(candyBaked.position, transform.position, Time.deltaTime * 5);
        }
    }
}
