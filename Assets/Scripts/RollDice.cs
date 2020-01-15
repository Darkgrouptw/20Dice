using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDice : MonoBehaviour
{
    public GameObject DicePrefab;
    private GameObject DiceTemp = null;


    public void RollDiceEvent()
    {
        //if (DiceTemp != null)
        //    return;

        GameObject temp = GameObject.Instantiate(DicePrefab);
        temp.transform.position += new Vector3(Random.Range(0, 1000), Random.Range(0, 1000), Random.Range(0, 1000)) / 10000;
        temp.GetComponent<Rigidbody>().AddForce(new Vector3(-Random.Range(1000, 2000) / 10, -Random.Range(1000, 2000) / 10 + 150, Random.Range(1000, 2000) / 10 - 150));
        temp.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(0, 3) - 1, Random.Range(0, 3) - 1, Random.Range(0, 3) - 1) * Random.Range(1000, 20000));


        DiceTemp = temp;
    }
}
