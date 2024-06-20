using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Title_01 : MonoBehaviour
{  [SerializeField] Button btnTitle;
    // Start is called before the first frame update
    void Start()
    {btnTitle.onClick.AddListener(()=>{
           
            SceneManager.LoadScene("SampleScene");
            
    });
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
