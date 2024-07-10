using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSet : Singleton<EffectSet>
{
    [Header("# Monster")]
    public GameObject duckAtkEffect;
    public GameObject duckDmgEffect;

    [Header("# Player")]
    public GameObject playerAtkEffect;
    public GameObject playerDmgEffect;
}
