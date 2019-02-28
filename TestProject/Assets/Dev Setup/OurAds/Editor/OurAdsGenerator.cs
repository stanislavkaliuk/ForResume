using Dev.Utilities;

namespace Dev.OurAdsModule
{
    public static class OurAdsGenerator
    {
        public static void GenerateOurAds(OurAdsData data)
        {
            ScriptableObjectUtility.CreateAsset<OurAdsSettings>(data);
        }
    }
}