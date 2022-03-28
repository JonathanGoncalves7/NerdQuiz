using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CRLoadScene : MonoBehaviour
{
    public void OnCLickLoadSceneWithButtonClickAudio(int sceneIndex)
    {
        StartCoroutine(this.CROnCLick(sceneIndex));
    }

    IEnumerator CROnCLick(int sceneIndex)
    {
        AudioManager.s_instance.PlayButtonClick();

        yield return new WaitForSeconds(AudioManager.s_instance.GetButtonClick().length);

        SceneManager.LoadScene(sceneIndex);
    }
}
