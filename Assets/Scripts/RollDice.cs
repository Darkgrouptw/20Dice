using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class RollDice : MonoBehaviour
{
    public GameObject DicePrefab;
    private List<GameObject> DiceTemp = new List<GameObject>();

    [Header("UI")]
    public Text TextLabel;
    public InputField TextNumber;
    public Button StartButton;
    public Text ResultText;

    public void RollDiceEvent()
    {
        //if (DiceTemp != null)
        //    return;

        int number = 0;
        if (!int.TryParse(TextNumber.text, out number))
            return;
        if (number <= 0 || number >= 10)
        {
            EditorUtility.DisplayDialog("警告", "請輸入 1 ~ 9 以內的數字!!", "確定");
            return;
        }

        Debug.Log(number);

        // UI 消失
        TextLabel.gameObject.SetActive(false);
        TextNumber.gameObject.SetActive(false);
        StartButton.gameObject.SetActive(false);

        // 產生 N 個骰子
        for(int i = 0; i < number; i++)
        {
            GameObject temp = GameObject.Instantiate(DicePrefab);
            temp.transform.position += new Vector3(Random.Range(0, 1000), Random.Range(0, 10), Random.Range(0, 1000)) / 1000;
            temp.GetComponent<Rigidbody>().AddForce(new Vector3(-Random.Range(100, 200) / 10, -Random.Range(100, 200) / 10 + 150, Random.Range(100, 200) / 10 - 150));
            temp.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(0, 3) - 1, Random.Range(0, 3) - 1, Random.Range(0, 3) - 1) * Random.Range(1000, 20000));

            DiceTemp.Add(temp);
        }
        StartCoroutine(DiceResult());
        //StartCoroutine()
    }

    public IEnumerator DiceResult()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("Result");
        yield return null;
    }
}
