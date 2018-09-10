using System;
using ETModel;
using UnityEngine;
using LitJson;
using System.Collections.Generic;

namespace ETHotfix
{
	public static class Init
	{
		public static void Start()
		{
			try
			{
				Game.Scene.ModelScene = ETModel.Game.Scene;

				// 注册热更层回调
				ETModel.Game.Hotfix.Update = () => { Update(); };
				ETModel.Game.Hotfix.LateUpdate = () => { LateUpdate(); };
				ETModel.Game.Hotfix.OnApplicationQuit = () => { OnApplicationQuit(); };
				
				Game.Scene.AddComponent<UIComponent>();
				Game.Scene.AddComponent<OpcodeTypeComponent>();
				Game.Scene.AddComponent<MessageDispatherComponent>();

				// 加载热更配置
				ETModel.Game.Scene.GetComponent<ResourcesComponent>().LoadBundle("config.unity3d");
				Game.Scene.AddComponent<ConfigComponent>();
				ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle("config.unity3d");

				UnitConfig unitConfig = (UnitConfig)Game.Scene.GetComponent<ConfigComponent>().Get(typeof(UnitConfig), 1001);
				Log.Debug($"config {JsonHelper.ToJson(unitConfig)}");

				// 测试代码
				Init.LoadGameSettingInfo();

				Game.EventSystem.Run(EventIdType.InitSceneStart);
			}
			catch (Exception e)
			{
				Log.Error(e);
			}
		}

		public static void Update()
		{
			try
			{
				Game.EventSystem.Update();
			}
			catch (Exception e)
			{
				Log.Error(e);
			}
		}

		public static void LateUpdate()
		{
			try
			{
				Game.EventSystem.LateUpdate();
			}
			catch (Exception e)
			{
				Log.Error(e);
			}
		}

		public static void OnApplicationQuit()
		{
			Game.Close();
		}

		public static void LoadGameSettingInfo()
		{
			Log.Debug("---- BPInfoManager LoadCardInfo ----------------------------");  
            // var textAsset = BPCommon.BPLoadRes<TextAsset>("", "", "card") as TextAsset; 
			var textAsset = BPCommon.BPLoadRes("", "", "card") as TextAsset; 
            if (textAsset == null || string.IsNullOrEmpty(textAsset.text))
            {
                Log.Debug("---- BPInfoManager LoadCardInfo ----------- textAsset == null");  
                return;
            }
			
			Log.Debug("textAsset ==> " + textAsset.text);
            
            // 多语言
            JsonData languageJsonData = null;
            // var languageTextAsset = BPCommon.BPLoadRes<TextAsset>("", "", "card_" + BPCommon.GetCurrentLanguageStr()) as TextAsset;
			var languageTextAsset = BPCommon.BPLoadRes("", "", "card_" + BPCommon.GetCurrentLanguageStr()) as TextAsset;
            if (languageTextAsset != null && !string.IsNullOrEmpty(languageTextAsset.text))
            {
                languageJsonData = JsonMapper.ToObject(languageTextAsset.text);
            }
            
            Dictionary<int, BPCardInfo> cardInfoDic = new Dictionary<int, BPCardInfo>();
            var allJsonData = JsonMapper.ToObject(textAsset.text);
            for (var i = 0; i < allJsonData.Count; i++)
            {
                var jsonData = allJsonData[i];
                var tmpCardInfo = new BPCardInfo();
                tmpCardInfo.InitFromJsonData(jsonData);
                if (languageJsonData != null && i < languageJsonData.Count)
                {
                    tmpCardInfo.InitFromLanguageJsonData(languageJsonData[i]);
                }
                tmpCardInfo.ShowLog();
                cardInfoDic.Add(tmpCardInfo.Id, tmpCardInfo);
            }
            
            allJsonData.Clear();
		}
	}
}