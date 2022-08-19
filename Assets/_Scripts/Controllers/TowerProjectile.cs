using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class TowerProjectile : MonoBehaviour
{

    public MeshRenderer currentMesh;
    public GameObject EnemyToFollow;
    public float MoveSpeed = 0.5f;
    public float RotationSpeed = 0.5f;
    public bool IsLaunched;
    public E_AttackType damageType;
    public float Damage;
    // Start is called before the first frame update
    void Start()
    {
        SetSettings();
    }
    public void SetSettings()
    {
        currentMesh = GetComponent<MeshRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (IsLaunched)
        {
            if (EnemyToFollow != null)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(EnemyToFollow.transform.position - transform.position), RotationSpeed * Time.deltaTime);

                transform.position += transform.forward * Time.deltaTime * MoveSpeed;
            }
            else
            {
                Destroy(this.gameObject);
            }

        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (other.gameObject == EnemyToFollow)
            {
                EnemyToFollow.GetComponent<EnemyController>().AcceptDamage(Damage, damageType);
                Destroy(this.gameObject);
            }
        }
    }
}
