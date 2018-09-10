using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;
//using FairyGUI;
using LitJson;

namespace ETHotfix
{
	public static class BPCommon
	{
		public static string nextSceneName;
		
		public static readonly StringBuilder stringBuilder = new StringBuilder();
		
		#region ============================== 加载程序文本 ==============================
		private static readonly Dictionary<string, string> textDic = new Dictionary<string, string>();
		
		// 读取配置文件，将文件信息保存到字典里    
		public static void LoadLocalText()
		{
			// var textAsset = BPLoadRes<TextAsset>("", "", "text_" + GetCurrentLanguageStr()) as TextAsset;
			var textAsset = BPLoadRes("", "", "text_" + GetCurrentLanguageStr()) as TextAsset;
			if (textAsset == null)
			{
				return;
			}
			var text = textAsset.text;    
			
			string[] lines = text.Split('\n');    
			foreach (string line in lines)    
			{    
				if (line == null)    
				{    
					continue;    
				}    
				string[] keyAndValue = line.Split('=');    
				textDic.Add(keyAndValue[0], keyAndValue[1]);    
			}     
		}
		
		public static string GetLocalTextByKey(string key)    
		{    
			if (textDic.ContainsKey(key) == false)    
			{    
				return "";    
			}    
			string value;    
			textDic.TryGetValue(key, out value);    
			return value;    
		}  
		#endregion

		public static string GetCurrentLanguageStr()
		{
//			return "en";
			
			var str = "en";
			switch (Application.systemLanguage)
			{
				case SystemLanguage.French:
					str = "fr";
					break;
				case SystemLanguage.English:
					str = "en";
					break;
				case SystemLanguage.Afrikaans:
					break;
				case SystemLanguage.Arabic:
					break;
				case SystemLanguage.Basque:
					break;
				case SystemLanguage.Belarusian:
					break;
				case SystemLanguage.Bulgarian:
					break;
				case SystemLanguage.Catalan:
					break;
				case SystemLanguage.Czech:
					break;
				case SystemLanguage.Danish:
					break;
				case SystemLanguage.Dutch:
					break;
				case SystemLanguage.Estonian:
					break;
				case SystemLanguage.Faroese:
					break;
				case SystemLanguage.Finnish:
					break;
				case SystemLanguage.German:
					str = "de";
					break;
				case SystemLanguage.Greek:
					break;
				case SystemLanguage.Hebrew:
					break;
//				case SystemLanguage.Hugarian:
//					break;
				case SystemLanguage.Icelandic:
					break;
				case SystemLanguage.Indonesian:
					break;
				case SystemLanguage.Italian:
					break;
				case SystemLanguage.Japanese:
					str = "jp";
					break;
				case SystemLanguage.Korean:
					str = "ko";
					break;
				case SystemLanguage.Latvian:
					break;
				case SystemLanguage.Lithuanian:
					break;
				case SystemLanguage.Norwegian:
					break;
				case SystemLanguage.Polish:
					break;
				case SystemLanguage.Portuguese:
					str = "pt";
					break;
				case SystemLanguage.Romanian:
					break;
				case SystemLanguage.Russian:
					str = "ru";
					break;
				case SystemLanguage.SerboCroatian:
					break;
				case SystemLanguage.Slovak:
					break;
				case SystemLanguage.Slovenian:
					break;
				case SystemLanguage.Spanish:
					str = "es";
					break;
				case SystemLanguage.Swedish:
					break;
				case SystemLanguage.Thai:
					str = "th";
					break;
				case SystemLanguage.Turkish:
					break;
				case SystemLanguage.Ukrainian:
					break;
				case SystemLanguage.Vietnamese:
					break;
				case SystemLanguage.Chinese:
				case SystemLanguage.ChineseSimplified:
					str = "cn";
					break;
				case SystemLanguage.ChineseTraditional:
					str = "tw";
					break;
				case SystemLanguage.Unknown:
					break;
//				default:
//					throw new ArgumentOutOfRangeException();
			}

			return str;
		}

		#region ============================== Json 辅助方法 ==============================
		public static int GetIntWithKey(JsonData jsonData, string keyStr, int defaultValue)
		{
			JsonData temp = null;
			try {
				temp = jsonData[keyStr];
			} catch (System.Exception e) {
				Log.Warning($"---- BPCommon GetIntWithKey 没有 {keyStr} key " + e);
//				Debug.LogWarning("---- BPCommon getIntWithKey " + e);
//				throw e;
			}
			
			return temp == null ? defaultValue : int.Parse(temp.ToString());
		}
		
		public static string GetStringWithKey(JsonData jsonData, string keyStr, string defaultValue)
		{
			JsonData temp = null;
			try {
				temp = jsonData[keyStr];
			} catch (System.Exception e) {
				Log.Warning($"---- BPCommon GetStringWithKey 没有 {keyStr} key " + e);
//				Debug.LogWarning("---- BPCommon getStringWithKey " + e);
//				throw e;
			}
			
			return temp == null ? defaultValue : temp.ToString();
		}
		
		public static float GetFloatWithKey(JsonData jsonData, string keyStr, float defaultValue)
		{
			JsonData temp = null;
			try {
				temp = jsonData[keyStr];
			} catch (System.Exception e) {
				Log.Warning($"---- BPCommon GetFloatWithKey 没有 {keyStr} key " + e);
//				Debug.LogWarning("---- BPCommon getFloatWithKey " + e);
//				throw e;
			}
			
			return temp == null ? defaultValue : float.Parse(temp.ToString());
		}
		
		public static double GetDoubleWithKey(JsonData jsonData, string keyStr, double defaultValue)
		{
			JsonData temp = null;
			try {
				temp = jsonData[keyStr];
			} catch (System.Exception e) {
				Log.Warning($"---- BPCommon GetDoubleWithKey 没有 {keyStr} key " + e);
//				Debug.LogWarning("---- BPCommon getDoubleWithKey " + e);
//				throw e;
			}
			
			return temp == null ? defaultValue : double.Parse(temp.ToString());
		}
		
		public static bool GetBoolWithKey(JsonData jsonData, string keyStr, bool defaultValue)
		{
			JsonData temp = null;
			try {
				temp = jsonData[keyStr];
			} catch (System.Exception e) {
				Log.Warning($"---- BPCommon GetBoolWithKey 没有 {keyStr} key " + e);
//				Debug.LogWarning("---- BPCommon getBoolWithKey " + e);
//				throw e;
			}
			
			return temp == null ? defaultValue : bool.Parse(temp.ToString());
		}
		#endregion
		
		#region ============================== 根据 JsonData 创建游戏对象 ==============================
		public static T CreateBPModel<T>(JsonData jsonData, string typeName, bool isCheck = true)
		{
			Log.Debug("---- BPCommon CreateBPModel");
			// // 先判断表资源是否有这个 Id 的数据
			// if (isCheck)
			// {
			// 	var infoId = GetIntWithKey(jsonData, BPG_COMMON.KEY_INFO_ID, 0);
			// 	object info = null;
			// 	switch (typeName)
			// 	{
			// 		case "ETHotfix.BPItem":
			// 			info = BPInfoManager.GetItemInfoById(infoId);
			// 			break;
			// 		case "ETHotfix.BPMonster":
			// 			info = BPInfoManager.GetMonsterInfoById(infoId);
			// 			break;
			// 		case "ETHotfix.BPSkill":
			// 			info = BPInfoManager.GetSkillInfoById(infoId);
			// 			break;
			// 	}
				
			// 	if (info == null)
			// 	{
			// 		return default(T);
			// 	}
			// }

			var model = CreateBPModelFromJsonData<T>(jsonData, typeName);
			return model;
		}
		
		public static T CreateBPModelFromJsonData<T>(JsonData jsonData, string typeName)
		{
			Log.Debug("---- BPCommon CreateBPModelFromJsonData");
			Type type = Type.GetType(typeName);
			object instance = Activator.CreateInstance(type);
			MethodInfo mi = type.GetMethod("InitFromJsonData");
			if (mi != null)
			{
//				object[] param0 = new object[0];
//				object[] param1 = new object[1];
//				param1[0] = jsonData;
				mi.Invoke(instance, new object[]{jsonData});
			}
			return (T)instance;
		}
		
//        // 物品
//        public static BPItem CreateItemFromJsonData(JsonData jsonData)
//        {
//            Log.Debug("---- BPCommon CreateItemFromJsonData");
//            
//            var infoId = GetIntWithKey(jsonData, BPG_COMMON.KEY_INFO_ID, 0);
//            var info = BPInfoManager.GetItemInfoById(infoId);
//            if (info == null)
//            {
//                return null;
//            }
//
////            var item = new BPItem(info, false);
////            item.InitFromJsonData(jsonData);
////            return item;
//	        var item = CreateBPModelFromJsonData<BPItem>(jsonData, "ETHotfix.BPItem");
//	        return item;
//        }
//        
//        // 神兽
//        public static BPMonster CreateMonsterFromJsonData(JsonData jsonData)
//        {
//            Log.Debug("---- BPCommon CreateMonsterFromJsonData");
//            
//            var infoId = GetIntWithKey(jsonData, BPG_COMMON.KEY_INFO_ID, 0);
//            var info = BPInfoManager.GetMonsterInfoById(infoId);
//            if (info == null)
//            {
//                return null;
//            }
//
////            var monster = new BPMonster(info, false);
////            monster.InitFromJsonData(jsonData);
////            return monster;
//	        var monster = CreateBPModelFromJsonData<BPMonster>(jsonData, "ETHotfix.BPMonster");
//	        return monster;
//        }
//        
//        // 技能
//        public static BPSkill CreateSkillFromJsonData(JsonData jsonData)
//        {
//            Log.Debug("---- BPCommon CreateSkillFromJsonData");
//            
//            var infoId = GetIntWithKey(jsonData, BPG_COMMON.KEY_INFO_ID, 0);
//            var info = BPInfoManager.GetSkillInfoById(infoId);
//            if (info == null)
//            {
//                return null;
//            }
//
////            var skill = new BPSkill(info, false);
////            skill.InitFromJsonData(jsonData);
////            return skill;
//	        var skill = CreateBPModelFromJsonData<BPSkill>(jsonData, "ETHotfix.BPSkill");
//	        return skill;
//        }
        #endregion
        
        #region ============================== 根据 JsonData 创建游戏对象数组 ==============================
		public static List<T> CreateBPModelListFromJsonData<T>(JsonData jsonData, string typeName, string key, List<T> defaultList)
		{
			Log.Debug("---- BPCommon CreateBPModelListFromJsonData");
			JsonData keyJsonData = null;
			try {
				keyJsonData = jsonData[key];
			} catch (System.Exception e) {
				Log.Warning($"---- BPCommon CreateBPModelListFromJsonData 没有 {key} key " + e);
			}
			
			if (keyJsonData == null)
			{
				Log.Warning("---- BPCommon CreateBPModelListFromJsonData keyJsonData 空");
				return defaultList;
			}
            
			var newList = new List<T>();
			foreach (JsonData tempJsonData in keyJsonData)
			{
				var temp = CreateBPModel<T>(tempJsonData, typeName);
				if (temp != null)
				{
					newList.Add(temp);
				}
			}
			return newList;
		}
		
//        // 物品
//        public static List<BPBaseModel> CreateItemListFromJsonData(JsonData jsonData, string key, List<BPBaseModel> defaultList)
//        {
//            Log.Debug("---- BPCommon CreateItemListFromJsonData");
//
//            var keyJsonData = jsonData[key];
//            if (keyJsonData == null)
//            {
//                return defaultList;
//            }
//            
//            var newList = new List<BPBaseModel>();
//            foreach (JsonData tempJsonData in keyJsonData)
//            {
//                var temp = CreateItemFromJsonData(tempJsonData);
//                if (temp != null)
//                {
//                    newList.Add(temp);
//                }
//            }
//            return newList;
//        }
//        
//        // 神兽
//        public static List<BPMonster> CreateMonsterListFromJsonData(JsonData jsonData, string key, List<BPMonster> defaultList)
//        {
//            Log.Debug("---- BPCommon CreateMonsterListFromJsonData");
//
//            var keyJsonData = jsonData[key];
//            if (keyJsonData == null)
//            {
//                return defaultList;
//            }
//            
//            var newList = new List<BPMonster>();
//            foreach (JsonData tempJsonData in keyJsonData)
//            {
//                var temp = CreateMonsterFromJsonData(tempJsonData);
//                if (temp != null)
//                {
//                    temp.ShowLog("---- BPCommon CreateMonsterListFromJsonData ----");
//                    newList.Add(temp);
//                }
//            }
//            return newList;
//        }
//		
//		// 技能
//		public static List<BPBaseModel> CreateSkillListFromJsonData(JsonData jsonData, string key, List<BPBaseModel> defaultList)
//		{
//			Log.Debug("---- BPCommon CreateSkillListFromJsonData");
//
//			var keyJsonData = jsonData[key];
//			if (keyJsonData == null)
//			{
//				return defaultList;
//			}
//            
//			var newList = new List<BPBaseModel>();
//			foreach (JsonData tempJsonData in keyJsonData)
//			{
//				var temp = CreateSkillFromJsonData(tempJsonData);
//				if (temp != null)
//				{
//					temp.ShowLog("---- BPCommon CreateSkillListFromJsonData ----");
//					newList.Add(temp);
//				}
//			}
//			return newList;
//		}
        #endregion
		
		#region ============================== 根据游戏对象获得 JsonData ==============================
		public static JsonData GetJsonDataByBPBaseModelList<T>(List<T> tempList)
		{
			Log.Debug("---- BPCommon GetJsonDataByBPBaseModelList");
			if (tempList == null || tempList.Count == 0)
			{
				return null;
			}
			
			var allJsonData = new JsonData();
			// foreach (var temp in tempList)
			// {
			// 	var baseModel = temp as BPBaseModel;
			// 	var jsonData = baseModel.GetJsonData();
			// 	allJsonData.Add(jsonData);
			// }

			return allJsonData;
		}

//		public static JsonData GetJsonDataByBPBaseModelList(List<BPBaseModel> tempList)
//		{
//			Log.Debug("---- BPCommon GetJsonDataByBPBaseModelList");
//			if (tempList == null || tempList.Count == 0)
//			{
//				return null;
//			}
//			
//			var allJsonData = new JsonData();
//			foreach (var temp in tempList)
//			{
//				var jsonData = temp.GetJsonData();
//				allJsonData.Add(jsonData);
//			}
//
//			return allJsonData;
//		}

		#endregion

		public static UnityEngine.Object BPLoadRes(string bundleName, string prefabName, string resourceName="")
		{
			return Resources.Load(resourceName);
		}
		
//         public static T BPLoadRes<T>(string bundleName, string prefabName, string resourceName="") where T : UnityEngine.Object
// 		{
// // #if RES_LOAD
// 			Log.Debug("BPCommon ==> BPLoadRes ");
// 			return Resources.Load<T>(resourceName);
//             //return null;
// // #else
// // 			return ETModel.Game.Scene.GetComponent<ETModel.ResourcesComponent>().GetAsset(bundleName, prefabName);
// // #endif
// 		}
		
		/// <summary>  
		/// 将文件读取到字符串中  
		/// </summary>  
		/// <param name="filePath">文件的绝对路径</param>  
		public static string FileToString(string filePath)  
		{  
			return FileToString(filePath, Encoding.Default);  
		}  
		
		/// <summary>  
		/// 将文件读取到字符串中  
		/// </summary>  
		/// <param name="filePath">文件的绝对路径</param>  
		/// <param name="encoding">字符编码</param>  
		public static string FileToString(string filePath, Encoding encoding)  
		{  
			Log.Debug("---- BPCommon FileToString filePath = " + filePath);
			//创建流读取器  
			var reader = new StreamReader(filePath, encoding);  
			try  
			{  
				//读取流  
				return reader.ReadToEnd();  
			}  
			catch  
			{  
				return string.Empty;  
			}  
			finally  
			{  
				//关闭流读取器  
				reader.Close();  
			}  
		}

		public static string LoadFile(string fileName)
		{
			Log.Debug("---- BPCommon LoadFile fileName = " + fileName);
			var filePath = Path.Combine(BPG_COMMON.BPSavePath, fileName);
			return FileToString(filePath);
		}

		public static void SaveFile(string fileName, string text)
		{
			Log.Debug("---- BPCommon SaveFile Application.dataPath = " + Application.dataPath);
			Log.Debug("---- BPCommon SaveFile Application.streamingAssetsPath = " + Application.streamingAssetsPath);
			Log.Debug("---- BPCommon SaveFile Application.persistentDataPath = " + Application.persistentDataPath);
			Log.Debug("---- BPCommon SaveFile Application.temporaryCachePath = " + Application.temporaryCachePath);

			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			
			var filePath = Path.Combine(BPG_COMMON.BPSavePath, fileName);
			
			// WriteAllText 创建一个新文件，在其中写入指定的字符串，然后关闭文件。如果目标文件已存在，则覆盖该文件。
			File.WriteAllText(filePath, text);
			
//			if (File.Exists(filePath))
//			{
//			}
//			// 文件流信息  
//			var fileInfo = new FileInfo(filePath); 
//			if(fileInfo.Exists)  
//			{  
//				File.Delete(filePath);
//			}  
//
//			//如果此文件不存在则创建  
//			var sw = fileInfo.CreateText(); 
//			
//			//以行的形式写入信息  
////			sw.WriteLine(text);  
//			sw.Write(text);  
//			//关闭流  
//			sw.Close();  
//			//销毁流  
//			sw.Dispose();  
		} 



		// /// <summary>
		// /// 从FairyGUI的view里面.递归遍历得到对应名字的child(只会遇到第一个就返回)
		// /// </summary>
		// /// <param name="comp"></param>
		// /// <param name="childName"></param>
		// /// <returns></returns>
		// public static GObject GetChildFromComp(GComponent comp, string childName)
		// {
		// 	GObject[] childArray = comp.GetChildren();
		// 	return BPCommon.GetChildFromComp(childArray, childName);
		// }

		// /// <summary>
		// /// 从FairyGUI的view里面.递归遍历得到对应名字的child(只会遇到第一个就返回)
		// /// </summary>
		// /// <param name="childArray"></param>
		// /// <param name="childName"></param>
		// /// <returns></returns>
		// public static GObject GetChildFromComp(GObject[] childArray, string childName)
		// {
		// 	if(childArray == null){
		// 		return null;
		// 	}
			
		// 	foreach(GObject obj in childArray)
		// 	{
		// 		if(obj.name == childName) {
		// 			return obj;
		// 		}
		// 		else if(obj.GetType() == typeof(GComponent)){
		// 			// Log.Debug("递归遍历====> " + obj.name);
		// 			GComponent comp = obj as GComponent;
		// 			GObject resultObj = GetChildFromComp(comp.GetChildren(), childName);
		// 			if(resultObj != null) {
		// 				return resultObj;
		// 			}
		// 		}
		// 	}
			
		// 	return null;
		// }


		// /// <summary>
		// /// 获取神兽图像的图片名字
		// /// </summary>
		// /// <param name="monsterId"></param>
		// /// <returns></returns>
		// public static string GetMonsterHeadIconNameByMonsterId(int monsterId)
		// {
        //     BPMonsterInfo monstserInfo = BPInfoManager.GetMonsterInfoById(monsterId);
		// 	return BPCommon.GetMonsterHeadIconNameByIconId(monstserInfo.HeadIconId);
		// }

		// public static string GetMonsterHeadIconNameByIconId(int iconId)
		// {
		// 	return "pet_portrait_" + Convert.ToString(iconId);
		// }

		// public static string GetCardBgNameByQualityGrade(int gualityGrade)
		// {
		// 	return "card_bg_" + Convert.ToString(gualityGrade);
		// }

		// public static string GetCardIconNameByIconId(int iconId)
		// {
		// 	return "card_skill_" + Convert.ToString(iconId);
		// }

		// public static string GetCardElementNameByElementType(int elementType)
		// {
		// 	return "element_" + Convert.ToString(elementType);
		// }


		// /// <summary>
		// /// 获取FairyGUI的图片路径.让GLoader可以加载.     ui://包名/图片名
		// /// </summary>
		// /// <param name="imageName"></param>
		// /// <param name="packageName"></param>
		// /// <returns></returns>
		// public static string GetFariyGUIPath(string imageName, string packageName="SanguoMonster")
		// {
		// 	return "ui://" + packageName + "/" + imageName;
		// }

		// /// <summary>
		// /// 根据monsterId获取对应的FairyGUI对应的uiPath.
		// /// </summary>
		// /// <param name="name"></param>
		// /// <param name="packageName"></param>
		// /// <returns></returns>
		// public static string GetFariyGUIPathByMonsterId(int monsterId)
		// {
		// 	string imageName = BPCommon.GetMonsterHeadIconNameByMonsterId(monsterId);
		// 	return BPCommon.GetFariyGUIPath(imageName);
		// 	// return "";
		// }
		
// 		/// <summary>
// 		/// 设置 FairyGUI.GObject 中心点坐标
// 		/// </summary>
// 		/// <param name="gObject"></param>
// 		/// <param name="posX"></param>
// 		/// <param name="posY"></param>
// 		/// <returns></returns>
// 		public static void SetFariyGUIGObjectCenterXY(FairyGUI.GObject gObject, float posX, float posY)
// 		{
// //			Log.Debug("----BPCommon SetFariyGUIGObjectCenterXY");
// 			if (gObject == null)
// 			{
// 				return;
// 			}
			
// 			gObject.SetXY(posX - gObject.width / 2, posY - gObject.height / 2);
// //			Log.Debug($"----BPCommon SetFariyGUIGObjectCenterXY centerXY x = {posX - gObject.width / 2}, y = {posY - gObject.height / 2}");
// 		}
		
// 		/// <summary>
// 		/// 获得 FairyGUI 将世界空间的坐标转换为屏幕坐标
// 		/// </summary>
// 		/// <param name="camera"></param>
// 		/// <param name="position"></param>
// 		/// <returns></returns>
// 		public static Vector2 GetFariyGUIWorldToScreenPosition(Camera camera, Vector3 position)
// 		{
// //			Log.Debug("----BPCommon GetFariyGUIWorldToScreenPosition position = " + position);
// 			if (camera == null || position == null)
// 			{
// 				return Vector2.zero;
// 			}
			
// //			var screenPos = Camera.main.WorldToScreenPoint(position); 
// 			var screenPos = camera.WorldToScreenPoint(position); 
// 			//原点位置转换
// 			screenPos.y = Screen.height - screenPos.y; 
// 			var pos = GRoot.inst.GlobalToLocal(screenPos);
// //			Log.Debug($"----BPController_Battle_Panel GetFariyGUIWorldToScreenPosition screenPos == {screenPos}, pos = {pos}");
// 			return pos;
// 		}

// 		/// <summary>
// 		/// 创建一个 组合多个组件 的 大组件
// 		/// </summary>
// 		/// <param name="spacing"></param>
// 		/// <param name="alignment"></param>
// 		/// <param name="gObjectList"></param>
// 		/// <returns></returns>
// 		public static GComponent CreateGComponentWithSpacing(float spacing, int alignment, List<GObject>gObjectList)
// 		{
// 			Log.Debug($"----BPCommon CreateGComponentWithSpacing spacing = {spacing}");
// 			if (gObjectList == null || gObjectList.Count == 0)
// 			{
// 				return null;
// 			}

// 			if (alignment == BPG_COMMON.UI_ALIGNMENT_HORIZONTAL_TOP ||
// 			    alignment == BPG_COMMON.UI_ALIGNMENT_HORIZONTAL_CENTER ||
// 			    alignment == BPG_COMMON.UI_ALIGNMENT_HORIZONTAL_BOTTOM)
// 			{
// 				return CreateGComponentHorizontallyWithSpacing(spacing, alignment, gObjectList);
// 			}

// 			return CreateGComponentVerticalWithSpacing(spacing, alignment, gObjectList);
// 		}

// 		/// <summary>
// 		/// 创建一个水平 组合多个组件 的 大组件
// 		/// </summary>
// 		/// <param name="spacing"></param>
// 		/// <param name="alignment"></param>
// 		/// <param name="gObjectList"></param>
// 		/// <returns></returns>
// 		public static GComponent CreateGComponentHorizontallyWithSpacing(float spacing, int alignment, List<GObject>gObjectList)
// 		{
// 			Log.Debug($"----BPCommon CreateGComponentHorizontallyWithSpacing spacing = {spacing}");
// 			if (gObjectList == null || gObjectList.Count == 0)
// 			{
// 				return null;
// 			}

// 			// 算出宽高
// 			float width = 0;
// 			float height = 0;
// 			int len = gObjectList.Count;
// 			GObject tempGObject;
// 			for (int i = 0; i < len; i++)
// 			{
// 				tempGObject = gObjectList[i];
// 				width += tempGObject.actualWidth;
// 				width += spacing;
// 				height = Math.Max(height, tempGObject.actualHeight);
// 			}
			
// 			// 减去最后的间隔
// 			width -= spacing;
			
// 			var allGComponent = new GComponent();
// 			allGComponent.SetSize(width, height);
// 			// 设置组件点击不穿透。
// 			allGComponent.opaque = true;
			
// //			var gGraph = new GGraph();
// //			gGraph.DrawRect(width, height, 0, Color.red, Color.green);
// //			gGraph.alpha = 0.5f;
// //			allGComponent.AddChild(gGraph);
			
// 			float offsetX = 0;
// 			for (int i = 0; i < len; i++)
// 			{
// 				tempGObject = gObjectList[i];
// 				allGComponent.AddChild(tempGObject);
				
// 				float offsetY = 0;
// 				switch (alignment)
// 				{
// 					case BPG_COMMON.UI_ALIGNMENT_HORIZONTAL_TOP:
// 						offsetY = 0;
// 						break;

// 					case BPG_COMMON.UI_ALIGNMENT_HORIZONTAL_CENTER:
// 						offsetY = (height - tempGObject.actualHeight)/2;
// 						break;

// 					case BPG_COMMON.UI_ALIGNMENT_HORIZONTAL_BOTTOM:
// 						offsetY = height - tempGObject.actualHeight;
// 						break;

// 					default:
// 						offsetY = height/2;
// 						break;
// 				}

// 				tempGObject.SetXY(offsetX, offsetY);
				
// 				offsetX += tempGObject.actualWidth;
// 				offsetX += spacing;
// 			}

// 			return allGComponent;
// 		}

// 		/// <summary>
// 		/// 创建一个垂直 组合多个组件 的 大组件
// 		/// </summary>
// 		/// <param name="spacing"></param>
// 		/// <param name="alignment"></param>
// 		/// <param name="gObjectList"></param>
// 		/// <returns></returns>
// 		public static GComponent CreateGComponentVerticalWithSpacing(float spacing, int alignment, List<GObject>gObjectList)
// 		{
// 			Log.Debug($"----BPCommon CreateGComponentVerticalWithSpacing spacing = {spacing}");
// 			if (gObjectList == null || gObjectList.Count == 0)
// 			{
// 				return null;
// 			}

// 			// 算出宽高
// 			float width = 0;
// 			float height = 0;
// 			int len = gObjectList.Count;
// 			GObject tempGObject;
// 			for (int i = 0; i < len; i++)
// 			{
// 				tempGObject = gObjectList[i];
// 				height += tempGObject.actualHeight;
// 				height += spacing;
// 				width = Math.Max(width, tempGObject.actualWidth);
// 			}
			
// 			// 减去最后的间隔
// 			height -= spacing;
			
// 			var allGComponent = new GComponent();
// 			allGComponent.SetSize(width, height);
// 			// 设置组件点击不穿透。
// 			allGComponent.opaque = true;
			
// //			var gGraph = new GGraph();
// //			gGraph.DrawRect(width, height, 0, Color.red, Color.blue);
// //			gGraph.alpha = 0.5f;
// //			allGComponent.AddChild(gGraph);
			
// 			float offsetY = 0;
// 			for (int i = 0; i < len; i++)
// 			{
// 				tempGObject = gObjectList[i];
// 				allGComponent.AddChild(tempGObject);
				
// 				float offsetX = 0;
// 				switch (alignment)
// 				{
// 					case BPG_COMMON.UI_ALIGNMENT_VERTICAL_LEFT:
// 						offsetX = 0;
// 						break;

// 					case BPG_COMMON.UI_ALIGNMENT_VERTICAL_CENTER:
// 						offsetX = (width - tempGObject.actualWidth)/2;
// 						break;

// 					case BPG_COMMON.UI_ALIGNMENT_VERTICAL_RIGHT:
// 						offsetX = width - tempGObject.actualWidth;
// 						break;

// 					default:
// 						offsetX = width/2;
// 						break;
// 				}

// 				tempGObject.SetXY(offsetX, offsetY);
				
// 				offsetY += tempGObject.actualHeight;
// 				offsetY += spacing;
// 			}

// 			return allGComponent;
// 		}

		// /// <summary>
		// /// 创建卡牌组件
		// /// </summary>
		// /// <param name="cardInfo"></param>
		// /// <returns></returns>
		// public static GComponent CreateCardComponentByCardInfo(BPCardInfo cardInfo)
		// {
		// 	if (cardInfo == null)
		// 	{
		// 		return null;
		// 	}
			
		// 	var cardUI = UIPackage.CreateObject(BPControllerComponent.packageName, "CardUI").asCom;
			
		// 	var itemImage = cardUI.GetChild("itemImage").asImage;
		// 	var defImage = cardUI.GetChild("defImage").asImage;
		// 	var atkImage = cardUI.GetChild("atkImage").asImage;
		// 	var skillImage = cardUI.GetChild("skillImage").asImage;
		// 	var elementBG = cardUI.GetChild("elementBG").asImage;

		// 	itemImage.visible = false;
		// 	defImage.visible = false;
		// 	atkImage.visible = false;
		// 	skillImage.visible = false;
		// 	elementBG.visible = false;
			
		// 	// 背景图
		// 	var bg_Loader = cardUI.GetChild("bg_Loader").asLoader;
		// 	bg_Loader.url = GetFariyGUIPath(GetCardBgNameByQualityGrade(cardInfo.QualityGrade));

		// 	// 卡图标
		// 	var skill_Loader = cardUI.GetChild("skill_Loader").asLoader;
		// 	skill_Loader.url = GetFariyGUIPath(GetCardIconNameByIconId(cardInfo.IconId));

		// 	// 能量消耗
		// 	var ap_TextField = cardUI.GetChild("ap_TextField").asTextField;
		// 	ap_TextField.text = Convert.ToString(cardInfo.EnergyCost);
			
		// 	// 元素
		// 	var element_Loader = cardUI.GetChild("element_Loader").asLoader;
		// 	element_Loader.url = GetFariyGUIPath(GetCardElementNameByElementType(cardInfo.CardElementType));
			
		// 	// 名字
		// 	var name_TextField = cardUI.GetChild("name_TextField").asTextField;
		// 	name_TextField.text = cardInfo.Name;
			
		// 	// 卡类型
		// 	switch (cardInfo.CardType)
		// 	{
		// 		case BPG_COMMON.CARD_TYPE_ATK:
		// 			atkImage.visible = true;
		// 			elementBG.visible = true;
		// 			break;
				
		// 		case BPG_COMMON.CARD_TYPE_DEF:
		// 			defImage.visible = true;
		// 			elementBG.visible = true;
		// 			break;
				
		// 		case BPG_COMMON.CARD_TYPE_ASSIST:
		// 			skillImage.visible = true;
		// 			elementBG.visible = true;
		// 			break;
				
		// 		case BPG_COMMON.CARD_TYPE_ITEM:
		// 			itemImage.visible = true;
		// 			break;
				
		// 	}
			
		// 	return cardUI;
		// }

		/// <summary>
		/// 将字符串类型的枚举转成int
		/// </summary>
		/// <param name="strElement"></param>
		/// <returns></returns>
		public static int GetElementTypeByString(string strElement)
		{
			if(strElement == "风"){
				return BPG_COMMON.ELE_WIND;
			}
			else if(strElement == "火"){
				return BPG_COMMON.ELE_FIRE;
			}
			else if(strElement == "水"){
				return BPG_COMMON.ELE_WATER;
			}
			else if(strElement == "土"){
				return BPG_COMMON.ELE_EARTH;
			}
			else if(strElement == "雷"){
				return BPG_COMMON.ELE_ELEC;
			}
			
			return BPG_COMMON.ELE_NONE;
		}

		/// <summary>
		/// 将字符串类型的枚举转成int
		/// </summary>
		/// <param name="strElement"></param>
		/// <returns></returns>
		public static int GetCardTypeByString(string strElement)
		{
			if(strElement == "攻击"){
				return BPG_COMMON.CARD_TYPE_ATK;
			}
			else if(strElement == "防御"){
				return BPG_COMMON.CARD_TYPE_DEF;
			}
			else if(strElement == "物品"){
				return BPG_COMMON.CARD_TYPE_ITEM;
			}
			else if(strElement == "辅助"){
				return BPG_COMMON.CARD_TYPE_ASSIST;
			}
			
			return BPG_COMMON.CARD_TYPE_ITEM;
		}


		/// <summary>
		/// 将"1,1;1,1;2,2"转成[1,1][1,1][2,2]这样的数组返回
		/// </summary>
		/// <returns></returns>	

        public static List<Dictionary<int, int>> splitStringToIntArrayBySemicolon(string tmpString)
		{
			if(string.IsNullOrEmpty(tmpString) == true){
                return null;
			}

			// 去除最后的空格
            tmpString = tmpString.Trim();

            // 切割字符串,并且去除最后的,;
            int realSize = 0;
            string[] tmpArray = tmpString.Split(new char[2]{',', ';'});
            for (int index = tmpArray.Length - 1; index >= 0; --index)
            {
                string lastChar = tmpArray[index];
                if(lastChar == ";" || lastChar == ","){
                    continue;
                }
                else
                {
                    realSize = index;
                    break;
                }
            }
            
			// 得到结果
            List<Dictionary<int, int>> resultList = new List<Dictionary<int, int>>();
            for(int index = 0; index < realSize; index += 2)
			{		
                // Log.Debug("realSize ==> " + realSize + "index ==> " + index + "Error  CardId===> " + tmpArray[index] + " num ==> " + tmpArray[index+1]);
				int key = int.Parse(tmpArray[index]);
				int val = int.Parse(tmpArray[index+1]);
				
                Dictionary<int, int> dic = new Dictionary<int, int>();
                dic.Add(key, val);
                resultList.Add(dic);
			}
			
            return resultList;
		}
	}
}