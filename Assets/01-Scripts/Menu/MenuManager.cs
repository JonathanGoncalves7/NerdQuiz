using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] AudioClip audioButtonClick;

    public void OnCLickLoadScene(int sceneIndex)
    {
        StartCoroutine(this.CROnCLick(sceneIndex));
    }

    IEnumerator CROnCLick(int sceneIndex)
    {
        AudioManager.s_instance.PlaySFX(audioButtonClick);

        yield return new WaitForSeconds(audioButtonClick.length);

        SceneManager.LoadScene(sceneIndex);
    }
}
