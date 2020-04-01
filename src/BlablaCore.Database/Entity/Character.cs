using System.ComponentModel.DataAnnotations;

namespace BlablaCore.BlablaCore.Database.Entity
{
    public class Character
    {
        public long Bbl { get; set; }

        [Key]
        public long CharacterId { get; set; }

        public string ChatColor { get; set; }

        public bool Direction { get; set; }

        public short Grade { get; set; }

        public short Gender { get; set; }

        public short MapId { get; set; }

        public int MapX { get; set; }

        public int MapY { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; } = "";

        public short ServerId { get; set; }

        public int[] SkinColor { get; set; }

        public short SkinId { get; set; }

        public int Uid { get; set; }

        public long Xp { get; set; }
    }
}
