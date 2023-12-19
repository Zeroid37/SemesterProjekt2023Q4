using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVenueDesktop.Service;
using TicketVenueSystem.Model;

namespace TicketVenueDesktop.BusinessLogic
{
    public class HallLogic
    {
        private readonly IHallService _hallService;

        public HallLogic()
        {
            _hallService = new HallService();
        }
        /// <summary>
        /// Get halls by hall number
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Hall object</returns>
        public async Task<Hall> getHallByHallNo(String id)
        {
            Hall hall;

            try
            {
                hall = await _hallService.GetHallAndSeats(id);
            }

            catch
            {
                hall = null;
            }

            return hall;
        }
    }
}
