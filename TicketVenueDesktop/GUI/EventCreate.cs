using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicketVenueDesktop.BusinessLogic;
using TicketVenueDesktop.Service;
using TicketVenueSystem.Model;

namespace TicketVenueDesktop.GUI
{
    public partial class EventCreate : Form
    {
        private EventOrganizer _eventOrganizer;
        private OrganizerLogic _oLogic;

        public EventCreate(String orgId)
        {
            initialize(orgId);
            InitializeComponent();


        }

        private async void initialize(string orgId)
        {
            _oLogic = new OrganizerLogic();
            _eventOrganizer = await _oLogic.getEventOrganizerById(orgId);
            await Console.Out.WriteLineAsync(_eventOrganizer.firstName);
        }
    }
}
