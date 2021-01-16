using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpiralWorksWalletBackendExam.Dtos.AuthDto
{
    public class LoginResultReadDto
    {
        public string LoggedInUser { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
