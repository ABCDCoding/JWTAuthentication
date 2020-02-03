using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PricingEngine.Models;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Primitives;

namespace FloatingPriceAPI.Controllers
{
    [Authorize]
    public class InstrumentPriceListController : ControllerBase
    {
        private readonly PricingEngineContext _context;
        public InstrumentPriceListController(PricingEngineContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("api/instrumentpricelists")]
        public async Task<ActionResult<IEnumerable<VWInstrumentPriceList>>> GetIndexPriceLists()
        {
            try
            {

                var token = Request.Headers["Authorization"];
                return await _context.VWIndexPriceLists.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}