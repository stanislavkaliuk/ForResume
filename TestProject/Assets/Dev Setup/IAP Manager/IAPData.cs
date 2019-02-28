using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_PURCHASING
using UnityEngine.Purchasing;
#endif

namespace Dev.Ads
{
    [System.Serializable]
    public struct IAPproduct
    {
#if UNITY_PURCHASING
        public ProductType type;
#endif
        public string id;

    }

    [System.Serializable]
    public struct IAPdata
    {
        public List<IAPproduct> products;
    }
}
