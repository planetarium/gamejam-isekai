using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace LibUnity.Frontend
{
    public class Title : MonoBehaviour
    {
        [SerializeField] private Text addressText;
        [SerializeField] private Text loadingText;

        private void OnEnable()
        {
            Game.Instance.ActionManager.SignUp();
            StartCoroutine(SignUp());
        }

        private IEnumerator SignUp()
        {
            var name = Game.Instance.Agent.Address.ToHex().Substring(0, 4);
            addressText.text = $"#{name}";
            var sb = new StringBuilder();
            var count = 0;
            while (true)
            {
                if (Game.IsStart)
                {
                    SceneLoader.Instnace.ChangeScene("Title", "Prologue");
                    yield break;
                }
                
                if (count > 5)
                {
                    count = 0;
                    sb.Length = 0;
                }
                
                if (count == 0)
                {
                    sb.Append("Login");
                }
                sb.Append(".");
                count++;
                loadingText.text = sb.ToString();
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}