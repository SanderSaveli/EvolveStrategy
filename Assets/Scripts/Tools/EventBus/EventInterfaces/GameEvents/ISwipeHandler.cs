using UnityEngine;

namespace EventBusSystem 
{ 
    public interface ISwipeHandler : IGlobalSubscriber
    {
        public void LeftSwipe(Vector3 swipeStartPosition, Vector3 swipeEndPosition);

        public void RightSwipe(Vector3 swipeStartPosition, Vector3 swipeEndPosition);
    }
}
