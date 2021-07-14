using System;

namespace LibUnity.Frontend
{
    public interface IEvent
    {
        public void Initialize(Action<bool> callback);
    }
}