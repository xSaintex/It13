using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace IT13
{
    public class SidebarItemClickedEventArgs : EventArgs
    {
        public string Section { get; }
        public SidebarItemClickedEventArgs(string section) => Section = section;
    }

    public partial class Sidebar : UserControl
    {
        private Button? _activeButton;
        private const string ArrowClosed = "▸";
        private const string ArrowOpen = "▾";

        // submenu panels keyed by header Button instance
        private readonly Dictionary<Button, Panel> _submenus = new();

        // header order — must match the fields declared in the Designer (your file)
        private Button[] HeaderOrder => new[]
        {
            btnDashboard, btnInventory, btnProducts, btnOrders, btnSales,
            btnStockAdjustments, btnCustomers, btnDeliveries, btnReturns,
            btnRental, btnEmployees, btnUsers, btnReports, btnHelp
        };

        public Sidebar()
        {
            InitializeComponent();

            // ensure panelLogo is docked to top (designer already does, but be safe)
            try { panelLogo.Dock = DockStyle.Top; } catch { }

            ConfigureButtons();

            // Reflow after designer has fully added child controls
            this.Load += (s, e) => ReflowControls();
        }

        private void ConfigureButtons()
        {
            // labels for each header using field names
            var labels = new Dictionary<string, string>
            {
                { nameof(btnDashboard), "Dashboard" },
                { nameof(btnInventory), "Inventory" },
                { nameof(btnProducts), "Products" },
                { nameof(btnOrders), "Orders" },
                { nameof(btnSales), "Sales" },
                { nameof(btnStockAdjustments), "Stock Adjustments" },
                { nameof(btnCustomers), "Client & Supplier" },
                { nameof(btnDeliveries), "Deliveries" },
                { nameof(btnReturns), "Returns" },
                { nameof(btnRental), "Rental" },
                { nameof(btnEmployees), "Employees" },
                { nameof(btnUsers), "Users" },
                { nameof(btnReports), "Reports" },
                { nameof(btnHelp), "Help" }
            };

            foreach (var header in HeaderOrder)
            {
                if (header == null) continue;

                var fieldName = GetFieldName(header) ?? ""; // fallback used only for clarity
                string label = labels.ContainsKey(fieldName) ? labels[fieldName] : header.Text ?? fieldName;

                bool hasDropdown = header == btnProducts || header == btnOrders || header == btnCustomers || header == btnDeliveries;

                header.Tag = label;
                header.Text = label + (hasDropdown ? $"  {ArrowClosed}" : "");

                header.FlatStyle = FlatStyle.Flat;
                header.FlatAppearance.BorderSize = 0;
                header.TextAlign = ContentAlignment.MiddleLeft;
                header.Padding = new Padding(20, 0, 0, 0);
                header.Font = new Font("Segoe UI", 10F);
                header.ForeColor = Color.FromArgb(80, 80, 80);
                header.BackColor = Color.Transparent;
                header.Cursor = Cursors.Hand;
                header.Height = 48;

                // Important — remove Dock so manual positioning works reliably
                header.Dock = DockStyle.None;

                header.MouseEnter += (s, e) => { if (header != _activeButton) header.BackColor = Color.FromArgb(245, 247, 250); };
                header.MouseLeave += (s, e) => { if (header != _activeButton) header.BackColor = Color.Transparent; };

                if (hasDropdown) header.Click += Header_Click;
                else header.Click += Item_Click;
            }

            // create submenus (only once)
            CreateSubmenuIfMissing(btnProducts, new[] { "Product List", "Product Categories" });
            CreateSubmenuIfMissing(btnOrders, new[] { "Customer Order", "Supplier Order", "Order List" });
            CreateSubmenuIfMissing(btnCustomers, new[] { "Customer List", "Supplier List" });
            CreateSubmenuIfMissing(btnDeliveries, new[] { "Delivery List", "Delivery Vehicles" });

            // hide legacy suppliers button if present
            try { if (btnSuppliers != null) btnSuppliers.Visible = false; } catch { }
        }

        // Try to return the private field name (like "btnDashboard") for a Button instance.
        // Not strictly required — used only for mapping to the label dictionary.
        private string? GetFieldName(Button btn)
        {
            try
            {
                var fields = this.GetType().GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                foreach (var f in fields)
                {
                    if (ReferenceEquals(f.GetValue(this), btn)) return f.Name;
                }
            }
            catch { }
            return null;
        }

        private void CreateSubmenuIfMissing(Button header, string[] items)
        {
            if (header == null) return;
            if (_submenus.ContainsKey(header)) return;

            var pnl = new Panel
            {
                Visible = false,
                BackColor = Color.FromArgb(248, 250, 252),
                Tag = "sub_" + (GetFieldName(header) ?? header.Text ?? Guid.NewGuid().ToString())
            };

            // create items (regular Buttons)
            foreach (var item in items)
            {
                var b = new Button
                {
                    Text = item,
                    Height = 40,
                    FlatStyle = FlatStyle.Flat,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(48, 0, 0, 0),
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = Color.FromArgb(100, 100, 100),
                    BackColor = Color.Transparent,
                    Cursor = Cursors.Hand,
                    Tag = item
                };

                b.FlatAppearance.BorderSize = 0;
                b.MouseEnter += (s, e) => b.BackColor = Color.FromArgb(230, 240, 255);
                b.MouseLeave += (s, e) => b.BackColor = Color.Transparent;
                b.Click += (s, e) =>
                {
                    SidebarItemClicked?.Invoke(this, new SidebarItemClickedEventArgs(item));
                    ActivateButton(header);
                };

                pnl.Controls.Add(b);
            }

            _submenus[header] = pnl;
            panelSidebar.Controls.Add(pnl); // add to visual tree
            ReflowControls();
        }

        // Position headers and their attached submenu panel directly below them
        private void ReflowControls()
        {
            if (panelSidebar == null) return;

            // start after the logo area at the top
            int y = (panelLogo != null && panelLogo.Visible) ? panelLogo.Height : 0;

            // Place each header then its submenu (if any)
            foreach (var header in HeaderOrder)
            {
                if (header == null || !header.Visible) continue;

                header.Left = 0;
                header.Top = y;
                header.Width = panelSidebar.ClientSize.Width;
                header.Height = 48;
                header.Dock = DockStyle.None; // ensure manual

                y += header.Height;

                if (_submenus.TryGetValue(header, out var submenu))
                {
                    submenu.Left = 0;
                    submenu.Top = y;
                    submenu.Width = panelSidebar.ClientSize.Width;

                    int itemY = 0;
                    foreach (Control c in submenu.Controls)
                    {
                        c.Left = 0;
                        c.Top = itemY;
                        c.Width = submenu.ClientSize.Width;
                        c.Height = 40;
                        itemY += c.Height;
                    }

                    submenu.Height = submenu.Visible ? itemY : 0;
                    y += submenu.Height;
                }
            }

            // allow scrolling if needed
            panelSidebar.AutoScrollMinSize = new Size(0, Math.Max(y, panelSidebar.ClientSize.Height));
        }

        private void Header_Click(object? sender, EventArgs e)
        {
            if (sender is not Button header) return;
            if (!_submenus.TryGetValue(header, out var submenu)) return;

            bool willOpen = !submenu.Visible;

            // close all submenus
            foreach (var kv in _submenus) kv.Value.Visible = false;

            // open only the selected one (if willOpen)
            submenu.Visible = willOpen;

            // update arrows on headers
            foreach (var kv in _submenus)
            {
                var h = kv.Key;
                var p = kv.Value;
                if (h == null) continue;
                if (p.Visible)
                {
                    if (!h.Text.Contains(ArrowOpen)) h.Text = h.Text.Replace(ArrowClosed, ArrowOpen);
                }
                else
                {
                    if (!h.Text.Contains(ArrowClosed)) h.Text = h.Text.Replace(ArrowOpen, ArrowClosed);
                }
            }

            if (willOpen) ActivateButton(header);

            ReflowControls();

            if (willOpen) SidebarItemClicked?.Invoke(this, new SidebarItemClickedEventArgs(header.Tag?.ToString() ?? ""));
        }

        private void Item_Click(object? sender, EventArgs e)
        {
            if (sender is not Button btn) return;
            ActivateButton(btn);
            SidebarItemClicked?.Invoke(this, new SidebarItemClickedEventArgs(btn.Tag?.ToString() ?? ""));
        }

        private void ActivateButton(Button btn)
        {
            if (btn == null) return;

            if (_activeButton != null)
            {
                _activeButton.BackColor = Color.Transparent;
                _activeButton.ForeColor = Color.FromArgb(80, 80, 80);
                _activeButton.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            }

            _activeButton = btn;
            _activeButton.BackColor = Color.FromArgb(230, 240, 255);
            _activeButton.ForeColor = Color.FromArgb(0, 89, 179);
            _activeButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        }

        public event EventHandler<SidebarItemClickedEventArgs>? SidebarItemClicked;

        private void PanelLogo_Click(object? sender, EventArgs e)
        {
            SidebarItemClicked?.Invoke(this, new SidebarItemClickedEventArgs("Dashboard"));
            if (btnDashboard != null) ActivateButton(btnDashboard);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            ReflowControls();
        }
    }
}
