using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ColliderChecker : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered1");
        if (other.tag == "Enemy")
        {
            Debug.Log("Entered2");
            transform.parent.GetComponent<TowerController>().AcceptTriggerInfo(other,true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            transform.parent.GetComponent<TowerController>().AcceptTriggerInfo(other,false);
        }
    }

}
