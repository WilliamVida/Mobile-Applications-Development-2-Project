using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://answers.unity.com/questions/1073407/make-object-move-back-and-forth.html
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBossBehaviour : MonoBehaviour
{
    public enum OccilationFuntion { Sine }

    // make the boss move up and down
    public void Start()
    {
        StartCoroutine(Oscillate(OccilationFuntion.Sine, 5f));
    }

    private IEnumerator Oscillate(OccilationFuntion method, float scalar)
    {
        while (true)
        {
            transform.position = new Vector3(10, Mathf.Sin(Time.time) * scalar, 0);
            yield return new WaitForEndOfFrame();
        }
    }

}
