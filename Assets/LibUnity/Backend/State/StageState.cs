using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Bencodex.Types;
using Libplanet;
using LibUnity.Backend.Action;

namespace LibUnity.Backend.State
{
    [Serializable]
    public class StageState : BaseState
    {
        [Serializable]
        public struct StageHistory
        {
            public readonly Address AgentAddress;
            public readonly long ConquestBlockIndex;

            public StageHistory(Address address, long blockIndex)
            {
                AgentAddress = address;
                ConquestBlockIndex = blockIndex;
            }

            public StageHistory(List serialized)
            {
                AgentAddress = serialized.First().ToAddress();
                ConquestBlockIndex = serialized.Last().ToLong();
            }

            public IValue Serialize()
            {
                return new List()
                    .Add(AgentAddress.Serialize())
                    .Add(ConquestBlockIndex.Serialize());
            }
        }
        private static Address _baseAddress = default;
        public static Address Derive(int level) => _baseAddress.Derive(level.ToString(CultureInfo.InvariantCulture));
        public const long ConquestInterval = 10;

        public readonly int Level;
        public List<StageHistory> Histories = new List<StageHistory>();

        public StageState(int level) : base(Derive(level))
        {
            Level = level;
        }

        public StageState(Dictionary serialized) : base(serialized)
        {
            Level = serialized["l"].ToInteger();
            Histories = serialized["h"].ToList(h => new StageHistory((List)h)).OrderBy(h =>
                    h.ConquestBlockIndex).ToList();
        }

        public void Add(Address agentAddress, long blockIndex)
        {
            if (Histories.Any(h => h.ConquestBlockIndex + ConquestInterval > blockIndex))
            {
                throw new Exception();
            }

            Histories.Add(new StageHistory(agentAddress, blockIndex));
        }

        public override IValue Serialize()
        {
            return new Dictionary(new Dictionary<IKey, IValue>
            {
                [(Text)"l"] = Level.Serialize(),
                [(Text)"h"] = new List(Histories.OrderBy(h => h.ConquestBlockIndex).Select(h => h.Serialize())),
            }.Union((Dictionary) base.Serialize()));
        }
    }
}
