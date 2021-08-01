using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class IncrementingTextAnimation : MonoBehaviour
{

    int _threshold = 300;
    int _control = 0;

    public Text scoreText; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_control >= _threshold) return;
        if (_control < _threshold)
        {
            _control++;
            scoreText.text = _control.ToString();
            //PlayAudioVoice (tikking / increasing score).


           


        }
        
    }

}
