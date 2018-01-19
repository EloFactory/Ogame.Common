using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ogame.Common
{
    public enum Univers
    {
        Uni1,

    }

    public static class EXT
    {
        public static string Link(this Univers Uni)
        {
            switch (Uni)
            {
                case Univers.Uni1:
                    return "s1-fr.ogame.gameforge.com";
                default:
                    return "";
            }
        }
    }
}
