using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTips : MonoBehaviour
{
    [SerializeField] bool _showGameTips = false;

    public GameObject _tipsBox;
    public GameObject _tipsTools;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_showGameTips)
        {
            // _tipsBox.SetActive(false);
            //_tipsBox.gameObject.GetComponent<Renderer>().enabled = false;

            _tipsTools.SetActive(true);
        }
        else
        {
            // _tipsBox.SetActive(true);
            //_tipsBox.gameObject.GetComponent<Renderer>().enabled = true;
            _tipsTools.SetActive(false);

        }
        
    }

 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _showGameTips = true;
        }
        else
        {
            _showGameTips = false;
        }
    }

   
}
