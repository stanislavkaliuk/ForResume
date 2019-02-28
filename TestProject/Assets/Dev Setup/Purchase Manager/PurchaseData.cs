using System;
using System.Collections.Generic;
#if UNITY_PURCHASING
using UnityEngine.Purchasing;
#endif
namespace Dev.Purchasing
{
	[Serializable]
	public struct PurchaseData
	{
		public List<IAPProduct> Products;
	}

	[Serializable]
	public struct IAPProduct
	{
		public string AndroidStoreId;
		public string iOSStoreId;
		public string Editorid;
#if UNITY_PURCHASING

		public ProductType Type;
#endif
	}

}

