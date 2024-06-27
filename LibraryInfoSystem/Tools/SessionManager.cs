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
        public static MongoHandler handler = new MongoHandler();

        public static DataBaseUser? CurrentUser { get; private set; }
        public static List<GameComponent>? ShoppingCart { get; private set; }
        public static List<DataBaseItem>? Items { get; private set; }
        public static List<DataBaseUser>? Users { get; private set; }
        public static List<DataBaseDuedate>? ItemsDue { get; private set; }
        public static List<DataBaseOverdue>? ItemsOverdue { get; private set; }


        public static void InitializeSession(DataBaseUser user)
        {
            CurrentUser = user;
            ShoppingCart = new List<GameComponent>();
            updateHandler();
        }
        public static void updateHandler()
        {
            MongoHandler.ConnectToDatabase(DataType.Games);
            MongoHandler.ConnectToDatabase(DataType.Users);
            MongoHandler.ConnectToDatabase(DataType.Duedate);
            MongoHandler.ConnectToDatabase(DataType.Overdue);

            Items = MongoHandler.items;
            Users = MongoHandler.users;
            ItemsDue = MongoHandler.duedate;
            ItemsOverdue = MongoHandler.overdue;
        }

        public static void UpdateSession(DataBaseUser user)
        {
            CurrentUser = user;
        }

        public static void ClearSession()
        {
            CurrentUser = null;
            ShoppingCart = null;
        }
    }
}
