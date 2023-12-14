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
using TicketVenueDesktop.GUI;
using TicketVenueSystem.Business;
using TicketVenueSystem.Model;

namespace TicketVenueDesktop
{
    public partial class StartPage : Form
    {
        private OrganizerLogic _oLogic;

        public StartPage()
        {
            _oLogic = new OrganizerLogic();
            populateList();
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void StartPage_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_Click(object sender, EventArgs e)
        {
            String selectedItem = listView1.SelectedItems[0].Text;
            EventCreate ec = new EventCreate(selectedItem);
            ec.Show();
            Visible = false;
        }

        private async void button1_Click(object sender, EventArgs e)
        {

        }

        private async void populateList()
        {
            List<EventOrganizer> eList;
            eList = await _oLogic.getAllEventOrganizers();
            ListViewItem lvitem;
            foreach (EventOrganizer eo in eList)
            {
                lvitem = new ListViewItem($"{eo.organizerId}");
                lvitem.SubItems.Add($"{eo.firstName} {eo.lastName}");
                listView1.Items.Add(lvitem);

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Console.WriteLine("Searched");
        }
    }
}
