using System;
using UnityEngine;
using UnityEngine.Analytics;
#if UNITY_PURCHASING
using UnityEngine.Purchasing;
#endif
namespace Dev.Purchasing
{
	public class PurchaseManager : MonoBehaviour
#if UNITY_PURCHASING
, IStoreListener 
#endif
	{
		public static PurchaseManager Instance;
		public event EventHandler OnPurchase;
		public PurchaseIds PurchaseIds;
		#if UNITY_PURCHASING
		private static IStoreController _storeController;

		private static IExtensionProvider _extensionProvider;

		private string[] _editorIds;
		private string[] _googlePlayIds;
		private string[] _appstoreIds;
		private ProductType[] _productTypes;

		private void Awake()
		{
			if (Instance != null)
				return;
			Instance = this;
			DontDestroyOnLoad(gameObject);
			_editorIds = PurchaseIds.EditorIds;
			_appstoreIds = PurchaseIds.IosIds;
			_googlePlayIds = PurchaseIds.AndroidIds;
			_productTypes = PurchaseIds.ProductTypes;
		}

		private bool IsInitialized()
		{
			return _storeController != null && _extensionProvider != null;
		}

		private void Start()
		{
			Init();
		}

		private void Init()
		{
			if (IsInitialized())
				return;
			var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

			for (int i = 0; i < _editorIds.Length; i++)
			{
				builder.AddProduct(_editorIds[i], _productTypes[i], new IDs
				{
					{_googlePlayIds[i], GooglePlay.Name},
					{_appstoreIds[i], AppleAppStore.Name}
				});
			}

			UnityPurchasing.Initialize(this, builder);
		}


		public void BuyProduct(string id)
		{
			if (!IsInitialized())
			{
				Debug.LogError("Unity IAP is not initialized");
				return;
			}

			Product product = _storeController.products.WithID(id);
			if (product != null && product.availableToPurchase)
			{
				_storeController.InitiatePurchase(product);
			}
			else
			{
				Debug.LogError("Failed to purchase. Product is missing or not available");
			}
		}

		public void OnInitializeFailed(InitializationFailureReason error)
		{
			Debug.LogError("Unity IAP failed to Initialize");
		}

		public void RestorePurchases()
		{
			if (!IsInitialized())
			{
				Debug.Log("RestorePurchases FAIL. Not initialized.");
				return;
			}

			#if UNITY_IOS
				Debug.Log("RestorePurchases started ...");

				var apple = _extensionProvider.GetExtension<IAppleExtensions>();
				apple.RestoreTransactions((result) =>
				{
					int i = _storeController.products.WithID("removeads").hasReceipt ? 1 : 0;
					PlayerPrefs.SetInt("NO_ADS",i);
				});
			#endif
		}

		public string GetLocalPrice(int id)
		{
			return _storeController.products.WithID(_editorIds[id]).metadata.localizedPriceString;
		}


		public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
		{
			bool equal = false;
			foreach (var editorId in _editorIds)
			{
				if (String.Equals(e.purchasedProduct.definition.id, editorId, StringComparison.Ordinal))
				{
					equal = true;
					break;
				}
			}
			if (equal && e.purchasedProduct.definition.id == "removeads" && OnPurchase == null)
			{
				PlayerPrefs.SetInt("NO_ADS",1);
				return PurchaseProcessingResult.Complete;				
			}
			if (equal)
			{
				AnalyticsEvent.IAPTransaction("Buy coins", (float) e.purchasedProduct.metadata.localizedPrice,
					e.purchasedProduct.definition.id, e.purchasedProduct.definition.type.ToString(), null,
					e.purchasedProduct.transactionID);
				OnPurchase?.Invoke(this, null);
			}
			else
			{
				Debug.LogError("Product: " + e.purchasedProduct.definition.id + " is not recognized");
			}
			return PurchaseProcessingResult.Complete;
		}

		public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
		{
			Debug.LogError("Fail to purchase product: " + i.definition.id + ". Cause: " + p);
		}

		public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
		{
			Debug.Log("Unity IAP initialized");
			_storeController = controller;
			_extensionProvider = extensions;
		}
#endif
	}
}