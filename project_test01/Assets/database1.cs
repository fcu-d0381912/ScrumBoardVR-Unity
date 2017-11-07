using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class database1 : MonoBehaviour
{

    // Use this for initialization
    IEnumerator Start()
    {
        WWW test = new WWW("http://10.22.28.42/scrumboard/ItemsData.php");
        yield return test;
        string testString = test.text;
      

        print(testString);

    }

    // Update is called once per frame
    void Update()
    {

    }
}