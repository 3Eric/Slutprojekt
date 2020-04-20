using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Chest
    {
        Rectangle chest;
        Rectangle mark;
        string type;
        public Chest(int X, int Y, int ww)
        {
            chest = new Rectangle(X, Y, ww / 20, ww / 26);
        }
    }
}
