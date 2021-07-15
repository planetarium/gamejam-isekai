using System.Linq;
using Bencodex.Types;
using LibUnity.Backend.State;
using UnityEngine;
using UnityEngine.UI;
using Text = UnityEngine.UI.Text;

namespace LibUnity.Frontend
{
    public class ActionResultPopup : MonoBehaviour
    {
        [SerializeField] private GameObject success;
        [SerializeField] private GameObject failed;
        [SerializeField] private Text contentText;
        [SerializeField] private Button okButton;

        public void Initialize(int index, bool isSuccess)
        {
            var state = Game.Instance.Agent.GetState(StageState.Derive(index));
            if (state is null)
            {
                return;
            }

            var stageState = new StageState((Dictionary) state);
            var conqueror = stageState.Histories.Last().AgentAddress.ToHex().Substring(0, 4);
            contentText.text = isSuccess
                ? $"{index + 1}이벤트를 50블록 동안 점령합니다.\n 스토리 조각과 리워드를 획득했습니다."
                : $"{conqueror}가 먼저 점령했습니다.\n 스토리 조각과 리워드 획득에 실패했습니다.";

            okButton.onClick.AddListener(() => gameObject.SetActive(false));

            success.SetActive(isSuccess);
            failed.SetActive(!isSuccess);
        }
    }
}