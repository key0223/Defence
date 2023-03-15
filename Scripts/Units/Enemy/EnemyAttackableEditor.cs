using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(EnemyAttackable))]
public class EnemyAttackableEditor :Editor
{
    private void OnSceneGUI()
    {
        EnemyAttackable attackable = (EnemyAttackable)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(attackable.transform.position, Vector3.up, Vector3.forward, 360, attackable.range);
        Vector3 viewAngleA = attackable.DirFromAngle(-attackable.viewAngle / 2, false);
        Vector3 viewAngleB = attackable.DirFromAngle(attackable.viewAngle / 2, false);

        Handles.DrawLine(attackable.transform.position, attackable.transform.position + viewAngleA * attackable.range);
        Handles.DrawLine(attackable.transform.position, attackable.transform.position + viewAngleB * attackable.range);

        Handles.color = Color.red;
        //foreach (transform visible in attackable.visibletargets)
        //{
        //    handles.drawline(attackable.transform.position, visible.transform.position);
        //}
    }
}
