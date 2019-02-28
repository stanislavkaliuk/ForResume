using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct OurAdsData  {
	public List<GameAd> Ads;
	
}

[Serializable]
public struct GameAd
{
	public string AndroidLink;
	public string iOSLink;

	public Sprite AndroidAdImage;
	public Sprite iOSAdImage;
}
