using UnityEngine;

namespace EventBusSystem 
{ 
    public interface IClickHandler : IGlobalSubscriber
    {
        public void RightClick(Vector3 position);
        public void LeftClick(Vector3 position);
    }
}

