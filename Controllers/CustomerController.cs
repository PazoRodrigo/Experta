using AutoMapper;
using IngresoSML2.Data;
using IngresoSML2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngresoSML2.Controllers
{
    [Route("/api/CUSTOMER")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public CustomerController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<CustomerDTO>>> Get()
        {
            var ListaEntidades = await _dbContext.Customers
                .Include(x => x.Invoices)
                .ThenInclude(x => x.Items)
                .ToListAsync();
            var ListaDTO = _mapper.Map<List<Customer>, List<CustomerDTO>>(ListaEntidades);
            return ListaDTO;
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CustomerDTO pm)
        {
            var objNuevo = _mapper.Map<CustomerDTO, Customer>(pm);
            _dbContext.Customers.Add(objNuevo);
            var valor = await _dbContext.SaveChangesAsync();
            if (valor == 0)
            {
                throw new Exception("No se pudo insertar el Cliente");
            }
            return Ok();
        }
    }
}
