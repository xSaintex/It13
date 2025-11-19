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
        private const string ArrowClosed = "›";
        private const string ArrowOpen = "⌄";
        private readonly Dictionary<Button, Panel> _submenus = new();

        private Button[] HeaderOrder => new[]
        {
            btnDashboard, btnInventory, btnProducts, btnOrders, btnSales,
            btnStockAdjustments, btnCustomers, btnDeliveries, btnReturns,
            btnRental, btnEmployees, btnUsers, btnReports, btnHelp
        };

        public Sidebar()
        {
            InitializeComponent();
            try { panelLogo.Dock = DockStyle.Top; } catch { }
            ConfigureButtons();
            this.Load += (s, e) => ReflowControls();
        }

        private void ConfigureButtons()
        {

            if (btnSales != null) btnSales.Visible = false;

            foreach (var btn in HeaderOrder)
            {
                if (btn == null) continue;

                string fieldName = GetFieldName(btn) ?? "";
                string label = fieldName switch
                {
                    "btnDashboard" => "Dashboard",
                    "btnInventory" => "Inventory",
                    "btnProducts" => "Products",
                    "btnOrders" => "Orders",
                    "btnSales" => "Sales",
                    "btnStockAdjustments" => "Stock Adjustments",
                    "btnCustomers" => "Client & Supplier",
                    "btnDeliveries" => "Deliveries",
                    "btnReturns" => "Returns",
                    "btnRental" => "Rental",
                    "btnEmployees" => "Employees",
                    "btnUsers" => "Users",
                    "btnReports" => "Reports",
                    "btnHelp" => "Help",
                    _ => ""
                };

                if (string.IsNullOrEmpty(label)) continue;

                bool hasDropdown = btn == btnProducts || 
                                   btn == btnOrders || 
                                   btn == btnCustomers || 
                                   btn == btnDeliveries || 
                                   btn == btnReturns || 
                                   btn == btnRental || 
                                   btn == btnUsers || 
                                   btn == btnReports; 

                // YOUR EXACT EMOJIS — LITERALLY IN THE CODE
                string icon = label switch
                {
                    "Dashboard" => "🏠",
                    "Inventory" => "📦",
                    "Products" => "🛒",
                    "Orders" => "📑",
                    "Stock Adjustments" => "🔧",
                    "Client & Supplier" => "👥",
                    "Deliveries" => "🚚",
                    "Returns" => "↩️",
                    "Rental" => "🔑",
                    "Employees" => "👷",
                    "Users" => "👤",
                    "Reports" => "📊",
                    "Help" => "❓",
                    _ => "Circle"
                };

                btn.Text = hasDropdown
                    ? $"  {icon}  {label}   {ArrowClosed}"
                    : $"  {icon}  {label}";

                btn.Tag = label;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.Padding = new Padding(22, 0, 0, 0);
                btn.Font = new Font("Segoe UI Emoji", 10.5F);
                btn.ForeColor = Color.FromArgb(60, 60, 60);
                btn.BackColor = Color.Transparent;
                btn.Cursor = Cursors.Hand;
                btn.Height = 52;
                btn.Dock = DockStyle.None;

                btn.MouseEnter += (s, e) => { if (btn != _activeButton) btn.BackColor = Color.FromArgb(240, 248, 255); };
                btn.MouseLeave += (s, e) => { if (btn != _activeButton) btn.BackColor = Color.Transparent; };

                if (hasDropdown)
                    btn.Click += Header_Click;
                else
                    btn.Click += Item_Click;
            }

            // Submenus with matching emojis
            CreateSubmenuIfMissing(btnProducts, new[]
            {
                "📄 Product List",
                "🏷️ Categories"
            });

            CreateSubmenuIfMissing(btnOrders, new[]
            {
                "🛒 Customer Order",
                "📦 Supplier Order",
                "📋 Order List"
            });

            CreateSubmenuIfMissing(btnCustomers, new[]
            {
                "🧑 Customer List",
                "🚚 Supplier List"
            });

            CreateSubmenuIfMissing(btnDeliveries, new[]
            {
                "🚛 Delivery List",
                "🚐 Delivery Vehicles"
            });

            CreateSubmenuIfMissing(btnReturns, new[]
            {
                "👤 Customer Returns",
                "📦 Supplier Returns",
                "📝 Returns List"
            });

            CreateSubmenuIfMissing(btnRental, new[]
            {
                "📝 Rental List",
                "➕ New Rental"
            });

            CreateSubmenuIfMissing(btnUsers, new[]
            {
                "👥 User List",
                "🛡️ User Admins"
            });

            CreateSubmenuIfMissing(btnReports, new[]
            {
                "📈 Sales Report",
                "📊 Inventory Report"
            });

            try { if (btnSuppliers != null) btnSuppliers.Visible = false; } catch { }
        }

        private string? GetFieldName(Button btn)
        {
            try
            {
                var fields = this.GetType().GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                foreach (var f in fields)
                    if (ReferenceEquals(f.GetValue(this), btn)) return f.Name;
            }
            catch { }
            return null;
        }

        private void CreateSubmenuIfMissing(Button header, string[] items)
        {
            if (header == null || _submenus.ContainsKey(header)) return;

            var pnl = new Panel
            {
                Visible = false,
                BackColor = Color.FromArgb(248, 252, 255)
            };

            foreach (var item in items)
            {
                var parts = item.Split(' ', 2);
                string icon = parts[0];
                string text = parts.Length > 1 ? parts[1] : item;

                var b = new Button
                {
                    Text = $"    {icon}  {text}",
                    Height = 44,
                    FlatStyle = FlatStyle.Flat,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(52, 0, 0, 0),
                    Font = new Font("Segoe UI Emoji", 10F),
                    ForeColor = Color.FromArgb(75, 75, 75),
                    BackColor = Color.Transparent,
                    Cursor = Cursors.Hand,
                    Tag = text.Trim()
                };
                b.FlatAppearance.BorderSize = 0;
                b.MouseEnter += (s, e) => b.BackColor = Color.FromArgb(220, 235, 255);
                b.MouseLeave += (s, e) => b.BackColor = Color.Transparent;
                b.Click += (s, e) =>
                {
                    SidebarItemClicked?.Invoke(this, new SidebarItemClickedEventArgs(text.Trim()));
                    ActivateButton(header);
                };
                pnl.Controls.Add(b);
            }

            _submenus[header] = pnl;
            panelSidebar.Controls.Add(pnl);
            ReflowControls();
        }

        private void ReflowControls()
        {
            if (panelSidebar == null) return;
            int y = panelLogo?.Height ?? 0;

            foreach (var header in HeaderOrder)
            {
                if (header == null || !header.Visible) continue;

                header.Left = 0;
                header.Top = y;
                header.Width = panelSidebar.ClientSize.Width;
                header.Height = 52;
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
                        c.Height = 44;
                        itemY += c.Height;
                    }
                    submenu.Height = submenu.Visible ? itemY : 0;
                    y += submenu.Height;
                }
            }

            panelSidebar.AutoScrollMinSize = new Size(0, Math.Max(y, panelSidebar.ClientSize.Height));
        }

        private void Header_Click(object? sender, EventArgs e)
        {
            if (sender is not Button header || !_submenus.TryGetValue(header, out var submenu)) return;

            bool willOpen = !submenu.Visible;
            foreach (var p in _submenus.Values) p.Visible = false;
            submenu.Visible = willOpen;

            header.Text = willOpen
                ? header.Text.Replace(ArrowClosed, ArrowOpen)
                : header.Text.Replace(ArrowOpen, ArrowClosed);

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
            if (_activeButton != null)
            {
                _activeButton.BackColor = Color.Transparent;
                _activeButton.ForeColor = Color.FromArgb(60, 60, 60);
                _activeButton.Font = new Font("Segoe UI Emoji", 10.5F, FontStyle.Regular);
            }
            _activeButton = btn;
            _activeButton.BackColor = Color.FromArgb(0, 119, 220);
            _activeButton.ForeColor = Color.White;
            _activeButton.Font = new Font("Segoe UI Emoji", 10.5F, FontStyle.Bold);
        }

        public event EventHandler<SidebarItemClickedEventArgs>? SidebarItemClicked;

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            ReflowControls();
        }
    }
}