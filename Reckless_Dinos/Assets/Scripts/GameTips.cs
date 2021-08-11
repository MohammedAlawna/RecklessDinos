using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTips : MonoBehaviour
{
    bool _showGameTips = false;

    [SerializeField] GameObject _tips;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_showGameTips)
        {
            _tips.SetActive(true);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _showGameTips = true;
        }
        else
        {
            _showGameTips = false;
        }
    }
}
