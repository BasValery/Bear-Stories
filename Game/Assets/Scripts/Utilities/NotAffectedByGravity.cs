using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
class NotAffectedByGravity : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0f;
    }
}
