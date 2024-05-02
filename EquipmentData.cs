


using Newtonsoft.Json;

namespace Sparta_Dungeon
{
    [System.Serializable]
    public class EquipmentData
    {
        [JsonProperty]
        public int Oid { get; set; }
        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public int Atk { get; set; }
        [JsonProperty]
        public int Def { get; set; }
        [JsonProperty]
        public string Description { get; set; }
        [JsonProperty]
        public int Price { get; set; }
        [JsonProperty]
        public bool IsEquipped { get; set; }
        [JsonProperty]
        public bool IsSelled { get; set; }

        public EquipmentData() { }

        public EquipmentData(int oid, string name, int atk, int def, string description, int price, bool isEquipped, bool isSelled)
        {
            Oid = oid;
            Name = name;
            Atk = atk;
            Def = def;
            Description = description;
            Price = price;
            IsEquipped = isEquipped;
            IsSelled = isSelled;
        }
    }
}
