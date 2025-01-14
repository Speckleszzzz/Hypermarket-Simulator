using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float shards;
    public TextMeshProUGUI shardsText;

    public void Start()
    {
        shardsText.text = "Shards : " + shards;
    }
}
