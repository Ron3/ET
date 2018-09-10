using System;
using ETModel;
using LitJson;


namespace ETHotfix
{
    public class BPCardInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }

        public int CardElementType { get; set; }      // 卡牌元素属性
        public int CardType { get; set; }             // 卡牌类型
        public int EnergyCost { get; set; }           // 能量消耗
        public int IconId { get; set; }               // 图标ID 
        public int PassiveIconId { get; set; }        // 被动图标ID (这张卡,作为被动技的时候专用)
        public int QualityGrade { get; set; }         // 品级
        //public BPSkillInfo {get; set;}              // 卡牌技能


        public BPCardInfo()
        {

        }
        

        /// <summary>
        /// 从json读取多国数据
        /// </summary>
        /// <param name="jsonData"></param>
        public void InitFromLanguageJsonData(JsonData jsonData)
        {
            Name = BPCommon.GetStringWithKey(jsonData, "Name", Name);
            Desc = BPCommon.GetStringWithKey(jsonData, "Desc", Desc);
        }
        

        /// <summary>
        /// 从json读取数据
        /// </summary>
        /// <param name="jsonData"></param>
        public void InitFromJsonData(JsonData jsonData)
        {
            Id = BPCommon.GetIntWithKey(jsonData, "ID", Id);
            CardElementType = BPCommon.GetElementTypeByString(BPCommon.GetStringWithKey(jsonData, "CardElementType", ""));
            CardType = BPCommon.GetCardTypeByString(BPCommon.GetStringWithKey(jsonData, "CardType", ""));
            EnergyCost = BPCommon.GetIntWithKey(jsonData, "EnergyCost", EnergyCost);
            IconId = BPCommon.GetIntWithKey(jsonData, "IconId", IconId);
            PassiveIconId = BPCommon.GetIntWithKey(jsonData, "PassiveIconId", PassiveIconId);
            QualityGrade = BPCommon.GetIntWithKey(jsonData, "QualityGrade", QualityGrade);

            
            // test
//            QualityGrade = BPG_COMMON.QUALITY_GRADE_BLUE;
//            CardElementType = BPG_COMMON.ELE_EARTH;
//            CardType = BPG_COMMON.CARD_TYPE_ATK;
            QualityGrade = RandomHelper.RandomNumber(BPG_COMMON.QUALITY_GRADE_WHITE, BPG_COMMON.QUALITY_GRADE_PURPLE + 1);
            CardElementType = RandomHelper.RandomNumber(BPG_COMMON.ELE_WIND, BPG_COMMON.ELE_ELEC + 1);
            CardType = RandomHelper.RandomNumber(BPG_COMMON.CARD_TYPE_ATK, BPG_COMMON.CARD_TYPE_ITEM + 1);
        }

        /// <summary>
        /// 显示Log
        /// </summary>
        /// <param name="tempStr"></param>
        public void ShowLog(string tempStr = "")
        {
            if (string.IsNullOrEmpty(tempStr) == true)
            {
                Log.Debug("---- BPCardInfo ShowLog ----" + ToString());
            }
            else
            {
                Log.Debug(tempStr + "---- BPCardInfo ShowLog ----" + ToString());
            }
        }


        /// <summary>
        /// 转成字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sb = BPCommon.stringBuilder;
            sb.Clear();
            sb.Append("---- BPCardInfo ToString ----");
            sb.Append(" Id = ");
            sb.Append(Id);
            sb.Append(" Name = ");
            sb.Append(Name);
            sb.Append(" Desc = ");
            sb.Append(Desc);

            return sb.ToString();
        }

        /// <summary>
        /// 获取技能icon的url
        /// </summary>
        /// <returns></returns>
        public string GetSkillIconUrlForFGUI()
        {
            return "ui://SanguoMonster/card_skill_1";
        }
    }
}