using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class Title_00 : MonoBehaviour
{ [SerializeField] Button btnStart;
  [SerializeField] TMP_Text textLabel1;
    // Start is called before the first frame update
    void Start()
    {
         btnStart.onClick.AddListener(()=>{
            textLabel1.text="Now start game";
            SceneManager.LoadScene("report_scene");
            
    });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
