using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketGoldBone : MonoBehaviour
{
    public Text gold, bone;

    public CoinScript paralar;

    public void Update()
    {
        gold.text = "Gold : " + paralar.gold;
        bone.text = "Bone : " + paralar.bone;
    }
}
