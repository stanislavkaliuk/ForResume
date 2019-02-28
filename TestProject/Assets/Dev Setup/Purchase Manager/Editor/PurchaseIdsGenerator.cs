using Dev.Utilities;

namespace Dev.Purchasing
{
    public static class PurchaseIdsGenerator
    {
        public static void GeneratePurchaseIds(PurchaseData data)
        {
            ScriptableObjectUtility.CreateAsset<PurchaseIds>(data);
        }
    }
}