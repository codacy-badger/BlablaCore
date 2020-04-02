// ,-----.  ,--.        ,--.   ,--.         ,-----.                     
// |  |) /_ |  | ,--,--.|  |-. |  | ,--,--.'  .--./ ,---. ,--.--. ,---. 
// |  .-.  \|  |' ,-.  || .-. '|  |' ,-.  ||  |    | .-. ||  .--'| .-. :
// |  '--' /|  |\ '-'  || `-' ||  |\ '-'  |'  '--'\' '-' '|  |   \   --.
// `------' `--' `--`--' `---' `--' `--`--' `-----' `---' `--'    `----'
// 
// Copyright (C) 2020 - BlablaCore
// 
// BlablaCore is a free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.ComponentModel.DataAnnotations;

namespace BlablaCore.Database.Entity
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
