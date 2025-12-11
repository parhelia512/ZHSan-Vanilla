using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace GameObjects
{
    [DataContract]
    public class TreasureCreationSettingList
    {

        // 公开的字典属性，在反序列化后构建
        [DataMember]
        public Dictionary<int, TreasureCreationSetting> TreasureCreationSettingDictionary  = new Dictionary<int, TreasureCreationSetting>();

        //[DataMember]
        //public bool IsNumber { get; set; }

        //[DataMember]
        //public string PropertyName { get; set; }

        //[DataMember]
        //public bool SmallToBig { get; set; }

        public bool AddTreasureCreationKind(TreasureCreationSetting TreasureCreationKind)
        {
            if (this.TreasureCreationSettingDictionary.ContainsKey(TreasureCreationKind.ID))
            {
                return false;
            }
            this.TreasureCreationSettingDictionary.Add(TreasureCreationKind.ID, TreasureCreationKind);
            return true;
        }

        public void Clear()
        {
            this.TreasureCreationSettingDictionary.Clear();
        }

        public TreasureCreationSetting GetTreasureCreationKind(int TreasureCreationID)
        {
            TreasureCreationSetting kind = null;
            this.TreasureCreationSettingDictionary.TryGetValue(TreasureCreationID, out kind);
            return kind;
        }

        public GameObjectList GetTreasureCreationSettingList()
        {
            GameObjectList list = new GameObjectList();
            foreach (TreasureCreationSetting kind in this.TreasureCreationSettingDictionary.Values)
            {
                list.Add(kind);
            }
            return list;
        }

        public void LoadFromString(TreasureCreationSettingList SettingList, string IDs)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = IDs.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            TreasureCreationSetting kind = null;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (SettingList.TreasureCreationSettingDictionary.TryGetValue(int.Parse(strArray[i]), out kind))
                {
                    this.AddTreasureCreationKind(kind);
                }
            }
        }

        public bool RemoveTreasureCreationKind(int id)
        {
            if (!this.TreasureCreationSettingDictionary.ContainsKey(id))
            {
                return false;
            }
            this.TreasureCreationSettingDictionary.Remove(id);
            return true;
        }

        public string SaveToString()
        {
            string str = "";
            foreach (TreasureCreationSetting kind in this.TreasureCreationSettingDictionary.Values)
            {
                str = str + kind.ID.ToString() + " ";
            }
            return str;
        }

        public int Count
        {
            get
            {
                return this.TreasureCreationSettingDictionary.Count;
            }
        }


    }
}