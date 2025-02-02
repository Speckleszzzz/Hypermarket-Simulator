using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float shards;
    public TextMeshProUGUI shardsText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void Start()
    {
        UpdateShards();
    }

    public void UpdateShards()
    {
        shardsText.text = "Shards : " + shards;
    }

}
