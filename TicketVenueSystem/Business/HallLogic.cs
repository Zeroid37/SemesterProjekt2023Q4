using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVenueSystem.DB;
using TicketVenueSystem.Model;

namespace TicketVenueSystem.Business
{
    public class HallLogic
    {
        private IConfiguration Configuration;
        private String? connectionString;

        public HallLogic(IConfiguration configuration) 
        {
            Configuration = configuration;
            connectionString = Configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Method to get a hall from it's hallnumber
        /// </summary>
        /// <param name="hallNo"></param>
        /// <returns>Hall object</returns>
        public Hall getHallFromHallNo(String hallNo)
        {
            HallDAO hdb = new HallDB(Configuration);
            Hall hall = hdb.getHallFromHallNo(hallNo);

            return hall;
        }
    }
}
