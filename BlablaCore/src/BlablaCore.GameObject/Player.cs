using System;
using System.Collections.Generic;
using System.Text;

namespace BlablaCore.src.BlablaCore.GameObject
{
    public class Player
    {
        public string name { get; set; }
        public int grade { get; set; }
        public int xp { get; set; }
        public int skin { get; set; }
        public int sexe { get; set; }
        public int uid { get; set; }
        public int pid { get; set; }
        public int lastMapId { get; set; }
        public int time { get; set; }
        public bool console { get; set; }
        public bool logged { get; set; }
        public int moderatorColor { get; set; }
        public bool isModeratorColorEnabled { get; set; }

    }
}
