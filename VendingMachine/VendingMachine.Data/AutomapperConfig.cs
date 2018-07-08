using AutoMapper;
using System.Globalization;
using System.Linq;
using VendingMachine.Dto;
using VendingMachine.Model;

namespace VendingMachine.Data
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            ConfigurePropertyMappings();
        }

        private static void ConfigurePropertyMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Coin, CoinDto>()
                .ForMember(dto => dto.CoinTypeName, opt => opt.MapFrom(c => c.CoinType.Name))
                .ForMember(dto => dto.Value, opt => opt.MapFrom(c => c.CoinType.Value));

                cfg.CreateMap<ProductType, ProductTypeDto>()
                .ForMember(dto => dto.Description, opt => opt.MapFrom(p => $"{p.Name} {string.Format(CultureInfo.GetCultureInfo("es-ES"), "{0:C}", p.Price)}"));

                cfg.CreateMap<Machine, VendingMachineDto>()
                .ForMember(dto => dto.KnownCoins, opt => opt.Ignore())
                .ForMember(dto => dto.KnownProducts, opt => opt.Ignore())
                .ForMember(dto => dto.MoneyEjected, opt => opt.Ignore())
                .ForMember(dto => dto.Message, opt => opt.Ignore());
            });
        }

    }
}
