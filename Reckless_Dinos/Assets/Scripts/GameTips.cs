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
        if(_showGameTips && !_showCoinTips)
        {
            // _tipsBox.SetActive(false);
            _tipsBox.gameObject.GetComponent<Renderer>().enabled = false;

            _tipsTools.SetActive(true);
            _coinTipsBox.SetActive(false);
        }
        if(_showCoinTips && !_showGameTips)
        {
            _tipsBox.gameObject.GetComponent<Renderer>().enabled = false;
            _tipsTools.SetActive(false);
            _coinTipsBox.SetActive(true);
        }
       /* else
        {
            // _tipsBox.SetActive(true);
            _tipsBox.gameObject.GetComponent<Renderer>().enabled = true;
            _tipsTools.SetActive(false);
            _coinTipsBox.SetActive(false);

        }*/
       else if(_showCoinTips && _showGameTips == false)
        {
            // _tipsBox.SetActive(true);
            _tipsBox.gameObject.GetComponent<Renderer>().enabled = true;
            _tipsTools.SetActive(false);
            _coinTipsBox.SetActive(false);
        }
        
    }

 
    private void OnTriggerEnter2D(Collider2D collision)
    {

        _showCoinTips = false;
        _showGameTips = false;

       
        /*
        if (collision.gameObject.tag == "Player")
        {
            _showGameTips = true;
        }*/
      
    }

    private void OnTriggerExit2D(Collider2D collision)
    {      
        if(gameObject.name == "TipsBox")
        {
            _showGameTips = false;
        }
       

        if (gameObject.name == "CoinTipsBox")
        {
            _showCoinTips = false;
        } 
       
       
    }


}
