using AutoMapper;
using SpiralWorksWalletBackendExam.Dtos.UserAccountDto;
using SpiralWorksWalletBackendExam.Helpers;
using SpiralWorksWalletBackendExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpiralWorksWalletBackendExam.AutoMapperProfiles
{
    public class UserAccountProfile:Profile
    {
        public UserAccountProfile()
        {
            CreateMap<UserAccountSaveDto, UserAccount>()
                .AfterMap((src, destination) =>
                {
                    destination.Password = AES.Encrypt(src.Password);
                    destination.LoginName = src.Username.Trim();
                });
        }
    }
}
