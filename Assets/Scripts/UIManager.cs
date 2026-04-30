using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject floatingTextPrefab;

    public Canvas gameCanvas;
    public TMP_Text Coins;
    public TMP_Text Keys;

    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();
    }

    //handls spawning text objects in space at specified location
    //reports when player collects an item
    public void CollectItem(GameObject character, string itemName)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(floatingTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = itemName + " x 1";
        Keys.text = "x " + character.GetComponent<PlayerInventory>().NumKeys;
    }

    //reports when player uses an item
    public void UseItem(GameObject character, string itemName)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(floatingTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = itemName + " used";
        Keys.text = "x " + character.GetComponent<PlayerInventory>().NumKeys;
    }

    //reports when player is missing an item
    public void NeedItem(GameObject character, string itemName)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(floatingTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = "Need " +  itemName;
    }
}
