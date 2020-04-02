using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BoardPopup : MonoBehaviour
{
    public GameObject popup;
    public Text uiText;
    public string texttoshow;
    // Start is called before the first frame update
    void Start()
    {
        uiText.text = texttoshow;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        popup.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            popup.SetActive(false);
    }
}
