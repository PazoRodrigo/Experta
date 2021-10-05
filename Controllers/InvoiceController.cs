using IngresoSML2.Data;
using IngresoSML2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace IngresoSML2.Controllers
{
    [Route("/api/invoice")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public InvoiceController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<InvoiceDTO>>> Get()
        {
            var ListaEntidades = await _dbContext.Invoices
                .Include(x => x.Customer)
                .Include(x => x.Items)
                .ToListAsync();
            var ListaDTO = _mapper.Map<List<Invoice>, List<InvoiceDTO>>(ListaEntidades);
            return ListaDTO;
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<List<InvoiceDTO>>> GetById(int id)
        {
            var ListaEntidades = await _dbContext.Invoices.Where(x => x.CustomerId == id)
                .Include(x => x.Customer)
                .Include(x => x.Items)
                .ToListAsync();
            var ListaDTO = _mapper.Map<List<Invoice>, List<InvoiceDTO>>(ListaEntidades);
            return ListaDTO;
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] InvoicePostModel pm)
        {
            var objCustomer = _dbContext.Customers.Where(x => x.CustomerId == pm.CustomerId).FirstOrDefault();
            if (objCustomer == null) {
                throw new Exception("El Cliente no existe");
            }
            var objNuevo = _mapper.Map<InvoicePostModel, Invoice>(pm);
            _dbContext.Invoices.Add(objNuevo);
            var valorFactura = await _dbContext.SaveChangesAsync();
            if (valorFactura == 0)
            {
                throw new Exception("No se pudo insertar la Factura");
            }
            foreach (var item in pm.Codes)
            {
                InvoiceItem InvI = new InvoiceItem();
                InvI.InvoiceId = objNuevo.Id;
                InvI.ProductCode = item;
                _dbContext.InvoiceItems.Add(InvI);
            }
            var valorItemsFactura = await _dbContext.SaveChangesAsync();
            if (valorItemsFactura == 0)
            {
                throw new Exception("No se pudieron insertar los Items de la Factura");
            }
            return Ok();
        }

    }
}
