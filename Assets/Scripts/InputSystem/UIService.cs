using UnityEngine;

namespace UISystem
{
    public class UIService : IService
    {
        RegionShower regionShower;
        OrderDrawer orderDrawer;
        public void StartWork()
        {
            regionShower = new RegionShower();
            orderDrawer = new OrderDrawer(GameObject.FindGameObjectWithTag("GUICanvas").transform);
        }
        public void EndWork()
        {
            throw new System.NotImplementedException();
        }

    }

}
