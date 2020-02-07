using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class RollDice : MonoBehaviour
{
    public GameObject DicePrefab;
    private List<GameObject> DiceList = new List<GameObject>();

    [Header("===== UI相關 =====")]
    public Text TextLabel;
    public InputField TextNumber;
    public Button StartButton;
    public Text ResultText;

    [Header("===== 骰子相關 =====")]
    [Range(0, 1)]
    public float ProbabilityGood = 1.0f - 1.0f / 20;

    public void RollDiceEvent()
    {
        //if (DiceTemp != null)
        //    return;

        int number = 0;
        if (!int.TryParse(TextNumber.text, out number))
            return;

        #if UNITY_EDITOR
        if (number <= 0 || number >= 10)
        {
            EditorUtility.DisplayDialog("警告", "請輸入 1 ~ 9 以內的數字!!", "確定");
            return;
        }
        #endif

        Debug.Log(number);

        // UI 消失
        TextLabel.gameObject.SetActive(false);
        TextNumber.gameObject.SetActive(false);
        StartButton.gameObject.SetActive(false);

        // 刪除骰子
        for (int i = DiceList.Count - 1; i >= 0; i--)
            GameObject.Destroy(DiceList[i]);
        DiceList.Clear();

        // 產生 N 個骰子
        for (int i = 0; i < number; i++)
        {
            GameObject temp = GameObject.Instantiate(DicePrefab);
            temp.transform.position += new Vector3(Random.Range(0, 1000), Random.Range(0, 10), Random.Range(0, 1000)) / 1000;
            temp.GetComponent<Rigidbody>().AddForce(new Vector3(-Random.Range(100, 200) / 10, -Random.Range(100, 200) / 10 + 150, Random.Range(100, 200) / 10 - 150));
            temp.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(0, 3) - 1, Random.Range(0, 3) - 1, Random.Range(0, 3) - 1) * Random.Range(1000, 20000));

            DiceList.Add(temp);
        }
        StartCoroutine(DiceResult());
    }

    public IEnumerator DiceResult()
    {
        // 等骰子結束
        yield return new WaitForSeconds(2);

        // 顯示 UI
        ResultText.gameObject.SetActive(true);
        ResultText.text = "結果：\n";

        int GoodResultCount = 0;
        int BadResultCount = 0;
        for(int i = 0; i < int.Parse(TextNumber.text); i++)
        {
            int result = Random.Range(0, 100);
            Debug.Log(result);
            if (result <= ProbabilityGood * 100)
                GoodResultCount++;
            else
                BadResultCount++;
        }
        ResultText.text += "吉：" + GoodResultCount + "\n";
        ResultText.text += "兇：" + BadResultCount + "\n";

        StartCoroutine(RollAgain());
        yield return null;
    }

    public IEnumerator RollAgain()
    {
        // 等一陣子
        yield return new WaitForSeconds(1);

        // UI 顯示
        TextLabel.gameObject.SetActive(true);
        TextNumber.gameObject.SetActive(true);
        StartButton.gameObject.SetActive(true);
        yield return null;
    }
}
