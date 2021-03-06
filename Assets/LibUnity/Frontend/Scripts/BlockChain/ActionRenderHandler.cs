using System;
using System.Collections.Generic;
using LibUnity.Backend.Action;
using LibUnity.Backend.Renderer;
using LibUnity.Backend.State;
using LibUnity.Frontend.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LibUnity.Frontend.BlockChain
{
    /// <summary>
    /// 현상태 : 각 액션의 랜더 단계에서 즉시 게임 정보에 반영시킴. 아바타를 선택하지 않은 상태에서 이전에 성공시키지 못한 액션을 재수행하고
    ///       이를 핸들링하면, 즉시 게임 정보에 반영시길 수 없기 때문에 에러가 발생함.
    /// 참고 : 이후 언랜더 처리를 고려한 해법이 필요함.
    /// 해법 1: 랜더 단계에서 얻는 `eval` 자체 혹은 변경점을 queue에 넣고, 게임의 상태에 따라 꺼내 쓰도록.
    /// </summary>
    public class ActionRenderHandler : ActionHandler
    {
        private static class Singleton
        {
            internal static readonly ActionRenderHandler Value = new ActionRenderHandler();
        }

        public static ActionRenderHandler Instance => Singleton.Value;

        private readonly List<IDisposable> _disposables = new List<IDisposable>();

        private ActionRenderer _renderer;

        private ActionRenderHandler()
        {
        }

        public void Start(ActionRenderer renderer)
        {
            _renderer = renderer;
            _renderer.EveryRender<SignUp>().Subscribe(RenderSignUp);
            _renderer.EveryRender<Conquest>().Subscribe(RenderConquest);
        }

        public void Stop()
        {
            _disposables.DisposeAllAndClear();
        }

        private void RenderSignUp(BaseAction.ActionEvaluation<SignUp> eval)
        {
            var agent = eval.OutputStates.GetState(Game.Instance.Agent.Address);
            Game.IsStart = true;
            Debug.Log($"[RenderSignUp] : {agent}");
        }

        private void RenderConquest(BaseAction.ActionEvaluation<Conquest> eval)
        {
            TextTyper.IsRendered = true;
            TextTyper.IsSuccess = eval.Exception is null;
            var agent = eval.OutputStates.GetState(Game.Instance.Agent.Address);
            Debug.Log($"[RenderConquest] : {agent}");
        }
    }
}