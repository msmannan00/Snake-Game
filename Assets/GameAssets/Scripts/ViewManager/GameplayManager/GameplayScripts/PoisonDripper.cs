using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PoisonDripper : MonoBehaviour
{

    public GameObject PoisonDropPrefab;
    public Transform SpawnPoint;
    public float DripperSpeed = 1;
    public GameObject RefCandy;
    public ColorCode MColorCode;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Dripper());
    }

  

    IEnumerator Dripper()
    {

        while (true)
        {
            yield return new WaitForSeconds(DripperSpeed);
            GameObject poisonDrop = Instantiate(PoisonDropPrefab, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
            poisonDrop.GetComponent<PoisonDrop>().PoisonDripper = this;
            poisonDrop.transform.DOScale(Vector3.one, 0.2f).From(Vector3.zero);
        }
    }
}
