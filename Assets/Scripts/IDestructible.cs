using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestructible
{
    void Damage(float amount, Vector3 damageLocation);
}
