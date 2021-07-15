using System;

namespace LibUnity.Frontend
{
    public interface IEvent
    {
        public void Initialize(int index, Action<bool, EventInfo> callback);
    }
}