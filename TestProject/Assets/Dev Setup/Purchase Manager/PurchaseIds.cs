using System.Collections.Generic;
using Dev.Utilities;
using UnityEngine;
#if UNITY_PURCHASING
using UnityEngine.Purchasing;
#endif
namespace Dev.Purchasing
{
	public class PurchaseIds : ExtendedScriptableObject
	{
		[HideInInspector]
		public string[] EditorIds;
		[HideInInspector]
		public string[] AndroidIds;
		[HideInInspector]
		public string[] IosIds;
#if UNITY_PURCHASING
		[HideInInspector]
		public ProductType[] ProductTypes;
#endif

		public override void ParseData(object data)
		{
			PurchaseData purchaseData = (PurchaseData) data;
			var editorids = new List<string>();
			var andrids = new List<string>();
			var iosids = new List<string>();
#if UNITY_PURCHASING
			var producttypes = new List<ProductType>();
#endif
			foreach (var product in purchaseData.Products)
			{
				editorids.Add(product.Editorid);
				andrids.Add(product.AndroidStoreId);
				iosids.Add(product.iOSStoreId);
				#if UNITY_PURCHASING
				producttypes.Add(product.Type);
#endif
			}
			EditorIds = editorids.ToArray();
			AndroidIds = andrids.ToArray();
			IosIds = iosids.ToArray();
#if UNITY_PURCHASING
			ProductTypes = producttypes.ToArray();
#endif
		}
	}
}
