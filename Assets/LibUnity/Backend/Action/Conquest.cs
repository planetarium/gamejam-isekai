using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Bencodex.Types;
using Libplanet.Action;
using LibUnity.Backend.State;

namespace LibUnity.Backend.Action
{
    [Serializable]
    [ActionType("conquest")]
    public class Conquest : GameAction
    {
        public int StageLevel;

        public override IAccountStateDelta Execute(IActionContext context)
        {
            var states = context.PreviousStates;
            var stageAddress = StageState.Derive(StageLevel);
            var agentAddress = context.Signer;
            if (context.Rehearsal)
            {
                return states
                    .SetState(stageAddress, MarkChanged)
                    .SetState(agentAddress, MarkChanged)
                    .MarkBalanceChanged(GoldCurrencyMock, Addresses.GoldCurrency, agentAddress);
            }

            if (!(states.GetState(agentAddress) is Dictionary agentDict) || !(states.GetState(stageAddress) is Dictionary stageDict))
            {
                throw new Exception();
            }

            var agentState = new AgentState(agentDict);
            agentState.Add(StageLevel);
            var stageState = new StageState(stageDict);
            stageState.Add(agentAddress, context.BlockIndex);
            return states
                .SetState(stageAddress, stageState.Serialize())
                .SetState(agentAddress, agentState.Serialize())
                .TransferAsset(Addresses.GoldCurrency, agentAddress, states.GetGoldCurrency() * 50);
        }

        protected override IImmutableDictionary<string, IValue> PlainValueInternal => new Dictionary<string, IValue>
        {
            ["s"] = StageLevel.Serialize()
        }.ToImmutableDictionary();

        protected override void LoadPlainValueInternal(IImmutableDictionary<string, IValue> plainValue)
        {
            StageLevel = plainValue["s"].ToInteger();
        }
    }
}
