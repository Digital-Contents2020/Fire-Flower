using UnityEngine;
using TMPro;
using Photon.Pun;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Kasoku : MonoBehaviour
{
    private TextMeshProUGUI kasoku;

    private void Awake() {
        kasoku = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        // 加速度センサの値を取得
        Vector3 val = Input.acceleration;
        kasoku.text = val.ToString(); // 小数点以下2桁表示
    }
}
