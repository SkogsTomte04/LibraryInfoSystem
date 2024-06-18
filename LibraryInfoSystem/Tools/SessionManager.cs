using LibraryInfoSystem.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryInfoSystem.Tools
{
    class SessionManager
    {
        public static DataBaseUser? CurrentUser { get; private set; }
        public static List<GameComponent>? ShoppingCart { get; private set; }

        public static void InitializeSession(DataBaseUser user)
        {
            CurrentUser = user;
            ShoppingCart = new List<GameComponent>();
        }

        public static void ClearSession()
        {
            CurrentUser = null;
            ShoppingCart = null;
        }
    }
}
