using AutoMapper;
using IngresoSML2.Models;
using IngresoSML2.Data;

namespace Test_Experta
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<InvoicePostModel, Invoice>();
            CreateMap<Invoice, InvoiceDTO>();
            CreateMap<CustomerDTO, Customer>().ReverseMap();
            CreateMap<InvoiceItem, InvoiceItemDTO>();
        }
    }
}
