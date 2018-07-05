using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class newScene : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        //StartCoroutine(SceneLoad());
	}

    IEnumerator SceneLoad()
    {

        yield return new WaitForSeconds(3);

        Debug.Log("씬로딩");

        SceneManager.LoadScene("Roll-a-ball");
    }


    public void SceneLoad1()
    {
        StartCoroutine(SceneLoad());
    }


	// Update is called once per frame
	void Update ()
    {
		
	}
}
