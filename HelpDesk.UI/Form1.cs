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
                Status = cmbStatus.Text,

                // ? THIS IS THE FIX
                ResolutionNotes = string.IsNullOrWhiteSpace(txtResolution.Text)
                                    ? null
                                    : txtResolution.Text
            };

            var result = _ticketService.Add(ticket);

            if (!result.isOk)
            {
                MessageBox.Show(result.message);
                return;
            }

            MessageBox.Show(result.message);
            LoadDefaultValues();
            LoadTickets();
        }





        private void dgTickets_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = Convert.ToInt32(dgTickets.Rows[e.RowIndex].Cells[0].Value);

            // ?? If clicking the same row again ? deselect
            if (_selectedTicket != null && _selectedTicket.Id == id)
            {
                dgTickets.ClearSelection();
                _selectedTicket = null;
                _isDirty = false;
                btnUpdateTicket.Enabled = false;

                // Clear controls
                txtIssueTitle.Clear();
                txtDescription.Clear();
                txtResolution.Clear();
                cmbStatus.SelectedIndex = 0;
                dateTimePicker1.Value = DateTime.Now;

                return;
            }

            // ?? Select new ticket
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

            // Attach handlers
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

            string status = cmbStatus.Text;

            // ?? RESOLVE RULES
            if (status == "Resolved" || status == "Closed")
            {
                if (string.IsNullOrWhiteSpace(cmbAssignedTo.Text))
                {
                    MessageBox.Show("Assigned employee is required to resolve a ticket.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtResolution.Text))
                {
                    MessageBox.Show("Resolution notes must not be empty.");
                    return;
                }

                DateTime resolvedDate = DateTime.Now;

                if (resolvedDate < dateTimePicker1.Value)
                {
                    MessageBox.Show("Date Resolved cannot be earlier than Date Created.");
                    return;
                }

                // ? Apply resolve data
                _selectedTicket.DateResolved = resolvedDate;
                _selectedTicket.ResolutionNotes = txtResolution.Text;
            }
            else
            {
                // ?? Not resolved ? clear resolution info
                _selectedTicket.DateResolved = null;
                _selectedTicket.ResolutionNotes = null;
            }

            // ?? Update in-memory DTO
            var ticket = _ticketList.First(t => t.Id == _selectedTicket.Id);

            ticket.IssueTitle = txtIssueTitle.Text;
            ticket.Description = txtDescription.Text;
            ticket.Category = cmbCategory.Text;
            ticket.AssignedEmployee = cmbAssignedTo.Text;
            ticket.Status = status;
            ticket.ResolutionNotes = _selectedTicket.ResolutionNotes;
            ticket.DateResolved = _selectedTicket.DateResolved;
            ticket.DateCreated = dateTimePicker1.Value;

            // ?? Refresh grid
            dgTickets.Refresh(); dgTickets.DataSource = null;
            dgTickets.DataSource = _ticketList;


            btnUpdateTicket.Enabled = false;
            _isDirty = false;

            MessageBox.Show("Ticket updated successfully.");
        }


        private void btnDeleleteTicket_Click(object sender, EventArgs e)
        {
            if (_selectedTicket == null)
            {
                MessageBox.Show("Please select a ticket to delete.");
                return;
            }

            // If confirmation checkbox is checked, ask for confirmation
            if (chkConfirmDelete.Checked)
            {
                var confirm = MessageBox.Show(
                    "Are you sure you want to delete this ticket?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes)
                    return;
            }

            // ?? Concurrency check: ensure ticket still exists
            var existingTicket = _ticketService
                .GetAll(null, null, null)
                .FirstOrDefault(t => t.Id == _selectedTicket.Id);

            if (existingTicket == null)
            {
                MessageBox.Show("This ticket was already deleted by another operation.");
                LoadTickets();
                _selectedTicket = null;
                return;
            }

            // ?? Delete from database
            var result = _ticketService.Delete(_selectedTicket.Id);

            if (!result.isOk)
            {
                MessageBox.Show(result.message);
                return;
            }

            // ?? Remove from in-memory list so DataGridView updates immediately
            _ticketList.RemoveAll(t => t.Id == _selectedTicket.Id);
            dgTickets.DataSource = null;
            dgTickets.DataSource = _ticketList;

            // ?? Reset UI
            dgTickets.ClearSelection();
            _selectedTicket = null;
            btnUpdateTicket.Enabled = false;

            txtIssueTitle.Clear();
            txtDescription.Clear();
            txtResolution.Clear();
            cmbStatus.SelectedIndex = 0;
            dateTimePicker1.Value = DateTime.Now;

            MessageBox.Show("Ticket deleted successfully.");

        }

        private void chkConfirmDelete_CheckedChanged(object sender, EventArgs e)
        {
            // Optional UX feedback (not required but helpful)
            if (chkConfirmDelete.Checked)
            {
                btnDeleleteTicket.Text = "Delete (Confirm)";
            }
            else
            {
                btnDeleleteTicket.Text = "Delete";
            }

        }
    }
}
