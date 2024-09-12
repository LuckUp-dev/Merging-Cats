using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DeathTimer());
    }

    private IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
