using System;
using System.Collections;
using System.IO;
using Boscohyun;
using Libplanet;
using Libplanet.Crypto;
using LibUnity.Frontend.BlockChain;
using LibUnity.Frontend.State;
using UnityEngine;

namespace LibUnity.Frontend
{
    using UniRx;

    [RequireComponent(typeof(Agent))]
    public class Game : MonoSingleton<Game>
    {
        public IAgent Agent { get; private set; }
        
        public States States { get; private set; }
        public ActionManager ActionManager { get; private set; }
        public bool IsInitialized { get; private set; }

        public static bool IsStart;

        private CommandLineOptions _options;
        
        public Story Story { get; set; }

        private static readonly string CommandLineOptionsJsonPath =
            Path.Combine(Application.streamingAssetsPath, "clo.json");

        #region Mono & Initialization

        protected void Awake()
        {
            Screen.SetResolution(540, 960, false);
            if (!IsValidInstance())
            {
                return;
            }
            
            _options = CommandLineOptions.Load(
                CommandLineOptionsJsonPath
            );
            
            
            Agent = GetComponent<Agent>();
            States = new States();
        }

        private IEnumerator Start()
        {
            // Initialize Agent
            var agentInitialized = false;
            var agentInitializeSucceed = false;
            yield return StartCoroutine(
                CoLogin(
                    succeed =>
                    {
                        Debug.Log($"Agent initialized. {succeed}");
                        agentInitialized = true;
                        agentInitializeSucceed = succeed;
                    }
                )
            );

            yield return new WaitUntil(() => agentInitialized);
            
            ActionManager = new ActionManager(Agent);
            
            if (agentInitializeSucceed)
            {
                IsInitialized = true;
                SceneLoader.Instnace.Load("Title");
            }
        }

        #endregion

        private IEnumerator CoLogin(Action<bool> callback)
        {
            var privateKey = string.IsNullOrEmpty(_options.PrivateKey)
                ? new PrivateKey()
                : new PrivateKey(ByteUtil.ParseHex(_options.PrivateKey));

                    Agent.Initialize(
                _options,
                privateKey,
                callback
            );
                    yield break;
//             if (_options.Maintenance)
//             {
//                 var w = Widget.Create<SystemPopup>();
//                 w.CloseCallback = () =>
//                 {
// #if UNITY_EDITOR
//                     UnityEditor.EditorApplication.ExitPlaymode();
// #else
//                     Application.Quit();
// #endif
//                 };
//                 w.Show(
//                     "UI_MAINTENANCE",
//                     "UI_MAINTENANCE_CONTENT",
//                     "UI_OK"
//                 );
//                 yield break;
//             }
//
//             if (_options.TestEnd)
//             {
//                 var w = Widget.Find<Confirm>();
//                 w.CloseCallback = result =>
//                 {
//                     if (result == ConfirmResult.Yes)
//                     {
//                         Application.OpenURL(GameConfig.DiscordLink);
//                     }
//
// #if UNITY_EDITOR
//                     UnityEditor.EditorApplication.ExitPlaymode();
// #else
//                     Application.Quit();
// #endif
//                 };
//                 w.Show("UI_TEST_END", "UI_TEST_END_CONTENT", "UI_GO_DISCORD", "UI_QUIT");
//
//                 yield break;
//             }
//
//             var settings = Widget.Find<UI.Settings>();
//             settings.UpdateSoundSettings();
//             settings.UpdatePrivateKey(_options.PrivateKey);
//
//             var loginPopup = Widget.Find<LoginPopup>();
//
//             if (Application.isBatchMode)
//             {
//                 loginPopup.Show(_options.KeyStorePath, _options.PrivateKey);
//             }
//             else
//             {
//                 var intro = Widget.Find<Intro>();
//                 intro.Show(_options.KeyStorePath, _options.PrivateKey);
//                 yield return new WaitUntil(() => loginPopup.Login);
//             }
//
//             Agent.Initialize(
//                 _options,
//                 new PrivateKey(),
//                 callback
//             );
        }

        public void ResetStore()
        {
//             var confirm = Widget.Find<Confirm>();
//             var storagePath = _options.StoragePath ?? BlockChain.Agent.DefaultStoragePath;
//             confirm.CloseCallback = result =>
//             {
//                 if (result == ConfirmResult.No)
//                 {
//                     return;
//                 }
//
//                 StoreUtils.ResetStore(storagePath);
//
// #if UNITY_EDITOR
//                 UnityEditor.EditorApplication.ExitPlaymode();
// #else
//                 Application.Quit();
// #endif
//             };
//             confirm.Show("UI_CONFIRM_RESET_STORE_TITLE", "UI_CONFIRM_RESET_STORE_CONTENT");
        }
    }
}
