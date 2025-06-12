using System.ComponentModel.DataAnnotations;

namespace LoginApi.Models
{
    public class Login
    {
        public int Id { get; set; }

        public int User_id { get; set; }

        public int Extension { get; set; }

        public int TipoMov { get; set; } // 1:login, 0:logout

        public DateTime Fecha { get; set; }
    }
}
