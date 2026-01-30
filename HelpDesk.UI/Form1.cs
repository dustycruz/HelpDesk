using HelpDesk.BLL;
using HelpDesk.DAL;
using HelpDesk.Model;
using HelpDesk.DTO;

namespace HelpDesk.UI
{
    public partial class Form1 : Form
    {
        private readonly ITicketService _ticketService;
        private readonly ITicketCategoryRepository _ticketCategoryRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private DTO.Ticket _selectedTicket; // currently selected DTO ticket
        private bool _isDirty = false;
        private List<DTO.Ticket> _ticketList; // tracks current tickets in DataGridView







        public Form1(
            ITicketService ticketService,
            ITicketCategoryRepository ticketCategoryRepository,
            IEmployeeRepository employeeRepository)
        {
            InitializeComponent();
            _ticketService = ticketService;
            _ticketCategoryRepository = ticketCategoryRepository;
            _employeeRepository = employeeRepository;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDefaultValues();
            LoadTickets();
        }

        private void LoadDefaultValues()
        {
            cmbCategory.DataSource = _ticketCategoryRepository.GetAll();
            cmbCategory.DisplayMember = "Name";
            cmbCategory.ValueMember = "Id";

            cmbAssignedTo.DataSource = _employeeRepository.GetAll();
            cmbAssignedTo.DisplayMember = "FullName";
            cmbAssignedTo.ValueMember = "Id";

            cmbStatus.Items.AddRange(new string[] { "New", "In-Progress", "Resolved", "Closed" });
            cmbStatus.SelectedIndex = 0;
        }

        private void LoadTickets()
        {
            // Store DTOs in the in-memory list
            _ticketList = _ticketService.GetAll(null, null, null).ToList();

            dgTickets.AutoGenerateColumns = true;
            dgTickets.DataSource = null;      // reset binding
            dgTickets.DataSource = _ticketList; // bind to in-memory list
            dgTickets.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgTickets.ReadOnly = true;
            dgTickets.AllowUserToAddRows = false;
        }

        private void btnCreateTicket_Click(object sender, EventArgs e)
        {
            Model.Ticket ticket = new Model.Ticket()
            {
                IssueTitle = txtIssueTitle.Text,
                Description = txtDescription.Text,
                CategoryId = Convert.ToInt32(cmbCategory.SelectedValue),
                AssignedEmployeeId = Convert.ToInt32(cmbAssignedTo.SelectedValue),
                Status = cmbStatus.Text
            };

            var result = _ticketService.Add(ticket);

            if (!result.isOk)
                MessageBox.Show(result.message);

            if (result.isOk)
            {
                MessageBox.Show(result.message);
                LoadDefaultValues();
                LoadTickets();
                return;
            }
        }

        private void dgTickets_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = Convert.ToInt32(dgTickets.Rows[e.RowIndex].Cells[0].Value);

            // Find ticket in the in-memory list
            _selectedTicket = _ticketList.FirstOrDefault(t => t.Id == id);

            if (_selectedTicket == null)
            {
                MessageBox.Show("Ticket not found.");
                return;
            }

            // Populate controls
            txtIssueTitle.Text = _selectedTicket.IssueTitle;
            txtDescription.Text = _selectedTicket.Description;
            cmbCategory.Text = _selectedTicket.Category;
            cmbAssignedTo.Text = _selectedTicket.AssignedEmployee;
            cmbStatus.Text = _selectedTicket.Status;
            dateTimePicker1.Value = _selectedTicket.DateCreated;
            txtResolution.Text = _selectedTicket.ResolutionNotes ?? "";

            // Disable Update initially
            btnUpdateTicket.Enabled = false;
            _isDirty = false;

            // Capture original values
            string origTitle = txtIssueTitle.Text;
            string origDesc = txtDescription.Text;
            string origCategory = cmbCategory.Text;
            string origAssigned = cmbAssignedTo.Text;
            string origStatus = cmbStatus.Text;
            string origResolution = txtResolution.Text;
            DateTime origDate = dateTimePicker1.Value;

            // Local function to enable Update if changes happen
            void EnableIfChanged()
            {
                _isDirty =
                    txtIssueTitle.Text != origTitle ||
                    txtDescription.Text != origDesc ||
                    cmbCategory.Text != origCategory ||
                    cmbAssignedTo.Text != origAssigned ||
                    cmbStatus.Text != origStatus ||
                    txtResolution.Text != origResolution ||
                    dateTimePicker1.Value != origDate;

                btnUpdateTicket.Enabled = _isDirty;
            }

            // Attach change handlers
            txtIssueTitle.TextChanged += (_, __) => EnableIfChanged();
            txtDescription.TextChanged += (_, __) => EnableIfChanged();
            cmbCategory.SelectedIndexChanged += (_, __) => EnableIfChanged();
            cmbAssignedTo.SelectedIndexChanged += (_, __) => EnableIfChanged();
            cmbStatus.SelectedIndexChanged += (_, __) => EnableIfChanged();
            txtResolution.TextChanged += (_, __) => EnableIfChanged();
            dateTimePicker1.ValueChanged += (_, __) => EnableIfChanged();
        
        }


        private void btnUpdateTicket_Click(object sender, EventArgs e)
        {
            if (_selectedTicket == null)
            {
                MessageBox.Show("Please select a ticket first.");
                return;
            }

            // Update the selected ticket in-memory (DTO list)
            var ticket = _ticketList.First(t => t.Id == _selectedTicket.Id);

            ticket.IssueTitle = txtIssueTitle.Text;
            ticket.Description = txtDescription.Text;
            ticket.Category = cmbCategory.Text;
            ticket.AssignedEmployee = cmbAssignedTo.Text;
            ticket.Status = cmbStatus.Text;
            ticket.ResolutionNotes = txtResolution.Text;
            ticket.DateCreated = dateTimePicker1.Value;

            // Refresh DataGridView
            dgTickets.Refresh(); // redraw changes immediately

            btnUpdateTicket.Enabled = false;
            _isDirty = false;

            MessageBox.Show("Ticket updated successfully!");
        }
    
    }
}
