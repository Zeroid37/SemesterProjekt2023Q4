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
        private EventOrganizer _eventOrganizer { get; set; }
        private OrganizerLogic _oLogic;
        private VenueEventLogic _veLogic;
        private HallLogic _hallLogic;

        public EventCreate(String orgId)
        {
            _veLogic = new VenueEventLogic();
            _hallLogic = new HallLogic();
            initialize(orgId);
            InitializeComponent();
        }

        private async void initialize(string orgId)
        {
            _oLogic = new OrganizerLogic();
            _eventOrganizer = await _oLogic.getEventOrganizerById(orgId);
            label1.Text = _eventOrganizer.email;
            label5.Text = _eventOrganizer.firstName;
            label7.Text = _eventOrganizer.lastName;
            label11.Text = _eventOrganizer.address.street + " " + _eventOrganizer.address.houseNo + " - " + _eventOrganizer.address.city;
            label14.Text = _eventOrganizer.phoneNo;
            label16.Text = _eventOrganizer.dateOfBirth.ToString();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void button_createEvent(object sender, EventArgs e)
        {
            //DELETE THIS IN FINAL PRODUCT
            Random rnd = new Random();
            string id = rnd.Next().ToString();

            String eventName = eventNameTxt.Text;
            DateTime startDate = dateTimePickerStart.Value;
            DateTime endDate = dateTimePickerEnd.Value;
            String hallNo = hallNumberTxt.Text;
            double price = double.Parse(priceTicketTxt.Text);

            Hall hall = await _hallLogic.getHallByHallNo(hallNo);

            VenueEvent ve = new VenueEvent(id, price, eventName, startDate, endDate, hall, _eventOrganizer);

            Boolean ok = await _veLogic.createVenueEvent(ve);

            await Console.Out.WriteLineAsync(ok.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StartPage sp = new StartPage();
            sp.Show();
            Visible = false;
        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void eventNameTxt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
