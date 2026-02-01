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
        private DTO.Ticket _selectedTicket;
        private bool _isDirty = false;
        private List<DTO.Ticket> _ticketList;

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

            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new string[] { "New", "In-Progress", "Resolved", "Closed" });
            cmbStatus.SelectedIndex = 0;

            var categories = _ticketCategoryRepository.GetAll().ToList();
            categories.Insert(0, new TicketCategory { Id = 0, Name = "All" });

            cmbFilterCategory.DataSource = categories;
            cmbFilterCategory.DisplayMember = "Name";
            cmbFilterCategory.ValueMember = "Id";

            var statuses = _ticketService
                            .GetAll(null, null, null)
                            .Select(t => t.Status)
                            .Distinct()
                            .ToList();

            statuses.Insert(0, "All");

            cmbFilterStatus.DataSource = statuses;
        }

        private void LoadTickets()
        {
            _ticketList = _ticketService.GetAll(null, null, null).ToList();
            dgTickets.AutoGenerateColumns = true;
            dgTickets.DataSource = null;
            dgTickets.DataSource = _ticketList;
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
                ResolutionNotes = string.IsNullOrWhiteSpace(txtResolution.Text) ? null : txtResolution.Text
            };

            var result = _ticketService.Add(ticket);

            if (!result.isOk)
            {
                toolStripStatusLabel1.Text = result.message;
                return;
            }

            toolStripStatusLabel1.Text = result.message;
            LoadDefaultValues();
            LoadTickets();
        }

        private void dgTickets_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = Convert.ToInt32(dgTickets.Rows[e.RowIndex].Cells[0].Value);

            if (_selectedTicket != null && _selectedTicket.Id == id)
            {
                dgTickets.ClearSelection();
                _selectedTicket = null;
                _isDirty = false;
                btnUpdateTicket.Enabled = false;

                txtIssueTitle.Clear();
                txtDescription.Clear();
                txtResolution.Clear();
                cmbStatus.SelectedIndex = 0;
                dateTimePicker1.Value = DateTime.Now;

                return;
            }

            _selectedTicket = _ticketList.FirstOrDefault(t => t.Id == id);

            if (_selectedTicket == null)
            {
                toolStripStatusLabel1.Text = "Ticket not found.";
                return;
            }

            txtIssueTitle.Text = _selectedTicket.IssueTitle;
            txtDescription.Text = _selectedTicket.Description;
            cmbCategory.Text = _selectedTicket.Category;
            cmbAssignedTo.Text = _selectedTicket.AssignedEmployee;
            cmbStatus.Text = _selectedTicket.Status;
            dateTimePicker1.Value = _selectedTicket.DateCreated;
            txtResolution.Text = _selectedTicket.ResolutionNotes ?? "";

            btnUpdateTicket.Enabled = false;
            _isDirty = false;

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
                toolStripStatusLabel1.Text = "Please select a ticket first.";
                return;
            }

            var ticket = _ticketList.First(t => t.Id == _selectedTicket.Id);

            if (ticket == null)
            {
                toolStripStatusLabel1.Text = "Ticket not found in grid.";
                return;
            }

            string status = cmbStatus.Text;

            if (status == "Resolved" || status == "Closed")
            {
                if (string.IsNullOrWhiteSpace(cmbAssignedTo.Text))
                {
                    toolStripStatusLabel1.Text = "Assigned employee is required to resolve a ticket.";
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtResolution.Text))
                {
                    toolStripStatusLabel1.Text = "Resolution notes must not be empty.";
                    return;
                }

                DateTime resolvedDate = DateTime.Now;

                if (resolvedDate < dateTimePicker1.Value)
                {
                    toolStripStatusLabel1.Text = "Date Resolved cannot be earlier than Date Created.";
                    return;
                }

                ticket.DateResolved = resolvedDate;
                ticket.ResolutionNotes = txtResolution.Text;
            }
            else
            {
                ticket.DateResolved = null;
                ticket.ResolutionNotes = null;
            }

            ticket.IssueTitle = txtIssueTitle.Text;
            ticket.Description = txtDescription.Text;
            ticket.Category = cmbCategory.Text;
            ticket.AssignedEmployee = cmbAssignedTo.Text;
            ticket.Status = status;
            ticket.DateCreated = dateTimePicker1.Value;

            _selectedTicket = ticket;

            dgTickets.InvalidateRow(dgTickets.CurrentRow?.Index ?? 0);

            btnUpdateTicket.Enabled = false;
            _isDirty = false;

            toolStripStatusLabel1.Text = "Ticket updated successfully.";
        }

        private void btnDeleleteTicket_Click(object sender, EventArgs e)
        {
            if (_selectedTicket == null)
            {
                toolStripStatusLabel1.Text = "Please select a ticket to delete.";
                return;
            }

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

            var existingTicket = _ticketService
                .GetAll(null, null, null)
                .FirstOrDefault(t => t.Id == _selectedTicket.Id);

            if (existingTicket == null)
            {
                toolStripStatusLabel1.Text = "This ticket was already deleted by another operation.";
                LoadTickets();
                _selectedTicket = null;
                return;
            }

            var result = _ticketService.Delete(_selectedTicket.Id);

            if (!result.isOk)
            {
                toolStripStatusLabel1.Text = result.message;
                return;
            }

            _ticketList.RemoveAll(t => t.Id == _selectedTicket.Id);
            dgTickets.DataSource = null;
            dgTickets.DataSource = _ticketList;

            dgTickets.ClearSelection();
            _selectedTicket = null;
            btnUpdateTicket.Enabled = false;

            txtIssueTitle.Clear();
            txtDescription.Clear();
            txtResolution.Clear();
            cmbStatus.SelectedIndex = 0;
            dateTimePicker1.Value = DateTime.Now;

            toolStripStatusLabel1.Text = "Ticket deleted successfully.";
        }

        private void chkConfirmDelete_CheckedChanged(object sender, EventArgs e)
        {
            btnDeleleteTicket.Text = chkConfirmDelete.Checked ? "Delete (Confirm)" : "Delete";
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            if (_ticketList == null || _ticketList.Count == 0)
            {
                toolStripStatusLabel1.Text = "There are no tickets to clear.";
                return;
            }

            var confirm = MessageBox.Show(
                "Delete ALL tickets?",
                "Confirm Clear All",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            foreach (var t in _ticketList.ToList())
            {
                var result = _ticketService.Delete(t.Id);
                if (!result.isOk)
                {
                    toolStripStatusLabel1.Text = $"Failed to delete ticket {t.Id}: {result.message}";
                    return;
                }
            }

            LoadTickets();

            toolStripStatusLabel1.Text = "All tickets deleted successfully.";
        }

        private void cmbFilterCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbFilterStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnApplyFilter_Click(object sender, EventArgs e)
        {
            string? status = cmbFilterStatus.Text == "All" ? null : cmbFilterStatus.Text;

            int? categoryId = null;
            if (cmbFilterCategory.Text != "All" && cmbFilterCategory.SelectedValue != null)
                categoryId = Convert.ToInt32(cmbFilterCategory.SelectedValue);

            _ticketList = _ticketService.GetAll(status, categoryId, null);

            dgTickets.DataSource = null;
            dgTickets.DataSource = _ticketList;
        }

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            cmbFilterCategory.SelectedIndex = 0;
            cmbFilterStatus.SelectedIndex = 0;
            LoadTickets();
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
