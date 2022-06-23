using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateGameUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(SwitchScene());
        }

    }

    IEnumerator SwitchScene()
    {
        SceneManager.LoadScene("Level 1", LoadSceneMode.Additive);
        yield return null;
        SceneManager.UnloadSceneAsync("Menu");
    }

    void OnMouseDown()
    {

    }
}