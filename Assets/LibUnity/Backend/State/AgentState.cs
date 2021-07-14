using System;
using System.Collections.Generic;
using System.Linq;
using Bencodex.Types;
using Libplanet;

namespace LibUnity.Backend.State
{
    [Serializable]
    public class AgentState : BaseState
    {
        public List<int> ClearedStageList = new List<int>();
        public AgentState(Address address) : base(address)
        {
        }

        public AgentState(Dictionary serialized) : base(serialized)
        {
            ClearedStageList = serialized["c"].ToList(c => c.ToInteger());
        }

        public override IValue Serialize()
        {
            return new Dictionary(new Dictionary<IKey, IValue>
            {
                [(Text)"c"] = new List(ClearedStageList.Select(s => s.Serialize()))
            }.Union((Dictionary) base.Serialize()));
        }

        public void Add(int stageId)
        {
            if (ClearedStageList.Contains(stageId))
            {
                return;
            }
            ClearedStageList.Add(stageId);
        }
    }
}
