using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public void DamagePlayer()
    {
        this.gameObject.SetActive(false);
    }
}
