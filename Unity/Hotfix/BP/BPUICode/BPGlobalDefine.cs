using UnityEngine;

namespace ETHotfix
{
    /// <summary>
    /// 神兽的元素属性
    /// </summary>
    public static class BPG_COMMON
    {
	    public const float NEED_ROTATION = 55;	// 游戏 所有物体 x轴 需旋转的度数
	    
	    public const int FARIYGUI_SORT_WORLD_SPACE_PANELS_BY_Z_ORDER = 100;
	    public const float FARIYGUI_TO_3D_UI_SCALE = 0.01f;	// fariyGui 界面 在3D 空间显示 时，需缩放的比例
	    public const float FARIYGUI_TO_3D_UI_SCALE_HP_BAR_OFFSET_X = -0.94f; // fariyGui 血条 界面 在3D 空间显示 时，缩放后 x轴需要移动的距离
	    public const float FARIYGUI_TO_3D_UI_SCALE_HP_BAR_OFFSET_Y = 2.47f; // fariyGui 血条 界面 在3D 空间显示 时，缩放后 y轴需要移动的距离
	    public const float FARIYGUI_TO_3D_UI_SCALE_HP_BAR_OFFSET_Z = 0.4f; // fariyGui 血条 界面 在3D 空间显示 时，缩放后 旋转后 z轴需要移动的距离
	    
	    public const string UNITY_SORTING_LAYER_NAME_GROUND = "Ground";
	    public const string UNITY_SORTING_LAYER_NAME_DEFAULT = "Default";
	    public const string UNITY_SORTING_LAYER_NAME_OBJECT = "Object";
	    public const string UNITY_SORTING_LAYER_NAME_SKY = "Sky";
	    
	    
	    public const string SCENE_NAME_INIT = "InitScene";
	    public const string SCENE_NAME_MAP = "MapScene";
	    public const string SCENE_NAME_BATTLE = "BattleScene";


        // 出战卡组.一共有15张(玩家从固定卡组里选15章)
        public const int BATTLE_CARD_GROUP_NUM = 15;

	    
	    public const string SPINE_BATTLE_STATE_NAME_IDLE = "idle";
	    public const string SPINE_BATTLE_STATE_NAME_ATTACK = "attack";
	    public const string SPINE_BATTLE_STATE_NAME_RUN = "run";
	    public const string SPINE_BATTLE_STATE_NAME_WALK = "walk";
	    public const string SPINE_BATTLE_STATE_NAME_HEAD_TURN = "head-turn";
	    
	    // 水平组合的属性 Horizontal
	    public const int UI_ALIGNMENT_HORIZONTAL_TOP = 0;
	    public const int UI_ALIGNMENT_HORIZONTAL_CENTER = 1;
	    public const int UI_ALIGNMENT_HORIZONTAL_BOTTOM = 2;
	    
	    // 垂直组合的属性
	    public const int UI_ALIGNMENT_VERTICAL_LEFT = 3;
	    public const int UI_ALIGNMENT_VERTICAL_CENTER = 4;
	    public const int UI_ALIGNMENT_VERTICAL_RIGHT = 5;
	    
	    public static string BPSavePath
	    {
		    get
		    {
			    return Application.persistentDataPath;
		    }
	    }
        
	    public const string SaveDataFileName = "saveData.txt";

//	    //切记，你的二进制文件一定要放在StreamingAssets ！！！！！！  || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
//        public static readonly string fileURL =  
//	#if UNITY_EDITOR
//		Application.dataPath +"/StreamingAssets"+"/";
//	#elif UNITY_IPHONE
//		Application.dataPath +"/Raw"+"/";
//	#elif UNITY_ANDROID
//		"StreamingAssets" + Application.dataPath + "!/assets/"+"/;
//	#else  
//            string.Empty;  
//	#endif
	    
        // 元素属性 全/风/火/水/土/雷/无
        public const int ELE_ALL = 0;
        public const int ELE_WIND = 1;
        public const int ELE_FIRE = 2;
        public const int ELE_WATER = 3;
        public const int ELE_EARTH = 4;             
        public const int ELE_ELEC = 5;
        public const int ELE_NONE = 6;

        // 卡牌品级	白/绿/蓝/紫
        public const int QUALITY_GRADE_WHITE = 1;
        public const int QUALITY_GRADE_GREEN = 2;
        public const int QUALITY_GRADE_BLUE = 3;
        public const int QUALITY_GRADE_PURPLE = 4;

	    // 卡牌类型  攻击/防御/辅助/物品
        public const int CARD_TYPE_ATK = 1;
        public const int CARD_TYPE_DEF = 2;
        public const int CARD_TYPE_ASSIST = 3;
        public const int CARD_TYPE_ITEM = 4;

        // 通用的下标
        public const int Index_0 = 0;
        public const int Index_1 = 1;
        public const int Index_2 = 2;
        public const int Index_3 = 3;
        public const int Index_4 = 4;
        public const int Index_5 = 5;
        public const int Index_6 = 6;
        public const int Index_7 = 7;
        public const int Index_8 = 8;
        public const int Index_9 = 9;
        public const int Index_10 = 10;

        // 队伍的标记
        public const int TEAM_INDEX_0 = 0;
        public const int TEAM_INDEX_1 = 1;
        public const int TEAM_INDEX_2 = 2;
        public const int TEAM_INDEX_DEBUG = 10;
        public const int TEAM_TOTAL_NUM = 3;            // 一共就三队

        public static int[] TEAM_INDEX_ARRAY = {BPG_COMMON.TEAM_INDEX_0, BPG_COMMON.TEAM_INDEX_1, BPG_COMMON.TEAM_INDEX_2};

        
        
        public const string KEY_INFO_ID = "KEY_INFO_ID";
//        public const string KEY_INFO_ID = "Id";
    }

    
    /// <summary>
    /// ui相关的所有定义 
    /// 注意写好注释
    /// </summary>
    public static class BPG_UI
    {   
        // 所有FairyGUI的panel名字都定义在这里.
        public const string LEADER_PANEL_NAME = "Ron_Leader_Panel";
        public const string EDIT_TEAM_PANEL_NAME = "Ron_EditTeam_Panel";
        public const string CHOOSE_MONSTER_PANEL_NAME = "Ron_Choose_Monster_Panel";
        public const string MONSTER_DETAIL_PANEL_NAME = "Ron_Monster_Detail_Panel";
        public const string MONSTER_CARD_SETTING_PANEL_NAME = "Ron_Setting_Card_Detail_Panel";
	    public const string EVENT_TEST_UI = "EVENT_TEST_UI";
        public const string BATTLE_UI = "BattleUI";
        public const string LOADING_UI = "LoadingUI";
        public const string TEST_MAIN_UI = "TestMainUI";
        public const string TITLE_UI = "TitleUI";
        
        

        // 主要用于这2个地方 Ron_Choose_Monster_Panel, BPController_ChooseMonsterPanel
        public const string BUTTON_ALL = "Button_All";              // 所有按钮
        public const string BUTTON_WATER = "Button_Water";          // 水
        public const string BUTTON_EARTH = "Button_Earth";          // 土
        public const string BUTTON_WIND = "Button_Wind";            // 风
        public const string BUTTON_FIRE = "Button_Fire";            // 火
        public const string BUTTON_ELEC = "Button_Elec";            // 电


        // 队伍编号按钮. 主要用于 Ron_EditTeam_Panel, BPController_EditTeam_Panel
        public const string TEAM_BUTTON_1 = "Team_button_1";
        public const string TEAM_BUTTON_2 = "Team_button_2";
        public const string TEAM_BUTTON_3 = "Team_button_3";
        public const string TEAM_BUTTON_DEBUG = "Team_button_4";
    }

}