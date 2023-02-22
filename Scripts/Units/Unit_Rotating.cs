using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Rotating : MonoBehaviour
{
    WeaponType weaponType;
    public GameObject target { get; set; }

    public Transform partToRotate;
    public float turnSpeed = 10f;
    void Update()
    {
        if (target == null)
            return;

        LockOnTarget();
    }

    void LockOnTarget()
    {
        Vector3 dir = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);

        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
}
