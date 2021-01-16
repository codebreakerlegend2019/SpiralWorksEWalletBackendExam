using AutoMapper;
using SpiralWorksWalletBackendExam.Dtos.TransactionDto;
using SpiralWorksWalletBackendExam.Enumerations;
using SpiralWorksWalletBackendExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SpiralWorksWalletBackendExam.AutoMapperProfiles
{
    public class TransactionProfile:Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, WithdrawTransactionReportDto>()
                .ForMember(x => x.DateTime, opt => opt.MapFrom(x => x.CreatedDate))
                .ForMember(x => x.AccountNumber, opt => opt.MapFrom(x => x.UserAccount.AccountNumber));


            CreateMap<Transaction, DepositTransactionReportDto>()
                .ForMember(x => x.DateTime, opt => opt.MapFrom(x => x.CreatedDate))
                .ForMember(x => x.AccountNumber, opt => opt.MapFrom(x => x.UserAccount.AccountNumber));

            CreateMap<Transaction, TransferFundTransactionReportDto>()
                .ForMember(x => x.DateTime, opt => opt.MapFrom(x => x.CreatedDate))
                .AfterMap((src, dest) =>
                {
                    if (src.SenderAccountNumber == src.UserAccount.AccountNumber)
                        dest.SenderAccountNumber = $"{src.SenderAccountNumber} - (You)";

                    if (src.RecipientAccountNumber == src.UserAccount.AccountNumber)
                        dest.RecipientAccountNumber = $"{src.RecipientAccountNumber} - (You)";
                });
            CreateMap<Transaction, MixedTransactionReportDto>()
            .ForMember(x => x.DateTime, opt => opt.MapFrom(x => x.CreatedDate))
            .ForMember(x => x.AccountNumber, opt => opt.MapFrom(x => x.UserAccount.AccountNumber))
            .AfterMap((src, dest) =>
            {
                if(src.TransactionType.Equals(TransactionType.DEPOSIT.ToString(),StringComparison.OrdinalIgnoreCase) 
                || src.TransactionType.Equals(TransactionType.WITHDRAW.ToString(),StringComparison.OrdinalIgnoreCase))
                {
                    dest.SenderAccountNumber = "N/A";
                    dest.RecipientAccountNumber = "N/A";
                }    
                if (src.SenderAccountNumber == src.UserAccount.AccountNumber)
                    dest.SenderAccountNumber = $"{src.SenderAccountNumber} - (You)";

                if (src.RecipientAccountNumber == src.UserAccount.AccountNumber)
                    dest.RecipientAccountNumber = $"{src.RecipientAccountNumber} - (You)";
            });
        }
    }
}
