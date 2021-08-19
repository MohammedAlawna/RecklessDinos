using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsManager : MonoBehaviour
{
    [SerializeField] bool _showGameTips = false;
   

    public GameObject _tipsBox;
    public GameObject _tipsToShow;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
           if (_showGameTips)
        {
            // _tipsBox.SetActive(false);
            _tipsToShow.SetActive(true);
            _tipsBox.gameObject.GetComponent<Renderer>().enabled = false;


            //_coinTipsBox.SetActive(false);
        }

        else
        {
            // _tipsBox.SetActive(true);
            _tipsBox.gameObject.GetComponent<Renderer>().enabled = true;
           _tipsToShow.SetActive(false);
           
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collided with TipsBox");
            _showGameTips = true;
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _showGameTips = false;
    }
}
