using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuantumQuasars
{
    public class MarketItem : MonoBehaviour
    {
        private void Update()
        {
            transform.forward = -Camera.main.transform.forward;
        }
    }
}
