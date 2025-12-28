
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagementSystem
{
    public static class UserSession
    {
        // Varsayılan olarak Admin (ID: 1) ile başlar
        public static int UserId { get; set; } = 1;   // KULLANICI KİMLİĞİ
        public static int RoleId { get; set; } = 1;
        public static string RoleName { get; set; } = "Admin";

        // Veritabanındaki RoleID karşılıkları
        public const int ADMIN_ID = 1;
        public const int EDITOR_ID = 2;
        public const int URETICI_ID = 3;
        public const int OKUYUCU_ID = 4;
    }
}
