using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine;

public class OpeningSceneScript : MonoBehaviour
{
    private PlayableDirector cutscene;

    // Start is called before the first frame update
    void Start()
    {
        cutscene = this.GetComponent<PlayableDirector>();
        cutscene.Play();
        StartCoroutine(endOpening(6f));
    }

    private IEnumerator endOpening(float _time)
    {
        yield return new WaitForSeconds(_time);
        SceneManager.LoadScene("MainMenu");
    }
}
