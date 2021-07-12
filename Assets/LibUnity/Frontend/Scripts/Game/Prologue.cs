using System.Collections;
using UnityEngine;

namespace LibUnity.Frontend
{
    public class Prologue : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(LoadLobby());
        }

        private static IEnumerator LoadLobby()
        {
            yield return new WaitForSeconds(2.0f);
            SceneLoader.Instnace.Unload("Prologue");
            SceneLoader.Instnace.Load("Lobby");
        }
    }
}