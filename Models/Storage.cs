using System.Xml.Serialization;

namespace RFStorageModifier.Models
{
    public class Storage
    {
        [XmlAttribute]
        public ushort ItemId { get; set; }
        [XmlAttribute]
        public byte Width { get; set; }
        [XmlAttribute]
        public byte Height { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is not Storage other)
                return false;
            
            return ItemId == other.ItemId;
        }

        public override int GetHashCode()
        {
            return ItemId.GetHashCode();
        }
    }
}