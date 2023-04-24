using UnityEngine;
using UnityEngine.UI;

using BattleSystem;

namespace UISystem 
{ 
    public class ArrowView : MonoBehaviour
    {
        public IComand comand;
        private Image filler;
        private void OnEnable()
        {
            filler = transform.GetChild(0).GetComponent<Image>();
        }
        private void Update()
        {
            if(comand != null) 
            {
                Refill(comand.progress);
            }
        }
        public void Refill(float progress)
        {
            filler.fillAmount = progress;
        }
        public void InstanceColor(Color color)
        {
            color.a = 1f;
            filler.color = color;

            color.a = 0.5f;
            Image substrate = GetComponent<Image>();
            substrate.color = color;
        }
    }
}



