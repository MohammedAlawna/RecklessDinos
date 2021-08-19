using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTips : MonoBehaviour
{
    [SerializeField] bool _showGameTips = false;
    [SerializeField] bool _showCoinTips = false;

    public GameObject _tipsBox;
    public GameObject _tipsTools;
    public GameObject _coinTipsBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
         if(_showCoinTips)
        {
            _tipsBox.gameObject.GetComponent<Renderer>().enabled = false;
            _tipsTools.SetActive(false);
            _coinTipsBox.SetActive(true);
        }

        else if (_showGameTips)
        {
            // _tipsBox.SetActive(false);
            _tipsTools.SetActive(true);
            _tipsBox.gameObject.GetComponent<Renderer>().enabled = false;


            //_coinTipsBox.SetActive(false);
        }


        else
        {
            // _tipsBox.SetActive(true);
            _tipsBox.gameObject.GetComponent<Renderer>().enabled = true;
            _tipsTools.SetActive(false);
            _coinTipsBox.SetActive(false);
        }

    }

 
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.tag == "Player" && gameObject.name == "TipsBox")
        {
            Debug.Log("Collided with TipsBox");
            _showGameTips = true;
        }

        if(collision.gameObject.tag == "Player" && gameObject.name == "CoinTipsBox")
        {
            Debug.Log("Collided with CoinsTipsBox");
            _showCoinTips = true;
        }
        

       
        /*
        if (collision.gameObject.tag == "Player")
        {
            _showGameTips = true;
        }*/
      
    }

    private void OnTriggerExit2D(Collider2D collision)
    {      
      
            _showGameTips = false;
      
     
            _showCoinTips = false;
      
       
       
    }


}
