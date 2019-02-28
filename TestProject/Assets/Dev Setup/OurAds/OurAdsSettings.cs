using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dev.Utilities;
namespace Dev.OurAdsModule
{
public class OurAdsSettings : ExtendedScriptableObject  {

	public int GameCount;
	public string[] AndroidLinks;
	public string[] iOSLinks;
	public Sprite[] AndroidSprites;
	public Sprite[] iOSSprites;

	public override void ParseData(object data)
	{
		OurAdsData ourData = (OurAdsData) data;
		int games = ourData.Ads.Count;
		GameCount = games;
		AndroidLinks = new string[games];
		iOSLinks = new string[games];
		AndroidSprites = new Sprite[games];
		iOSSprites = new Sprite[games];
		for(int i = 0; i < games; i++)
		{
			AndroidLinks[i] = ourData.Ads[i].AndroidLink;
			iOSLinks[i] = ourData.Ads[i].iOSLink;
			AndroidSprites[i] = ourData.Ads[i].AndroidAdImage;
			iOSSprites[i] = ourData.Ads[i].iOSAdImage;
		}
	}
}
}