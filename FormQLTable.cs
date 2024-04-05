using _20T1020025QuanDrink.DAO;
using _20T1020025QuanDrink.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Menu = _20T1020025QuanDrink.DTO.Menu;

namespace _20T1020025QuanDrink
{
    public partial class FormQLTable : Form
    {
        public FormQLTable()
        {
            InitializeComponent();

            LoadTable();
            LoadCategory();
        }

        #region Method

        void LoadCategory()
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory();
            cbbCategory.DataSource = listCategory;
            cbbCategory.DisplayMember = "Name";
        }

        void LoadDrinkListByCategoryID(int id)
        {
            List<Drink>listDrink = DrinkDAO.Instance.GetDrinkByCategoryID(id);
            cbbDrink.DataSource = listDrink;
            cbbDrink.DisplayMember = "Name";
        }
        void LoadTable()
        {
            List<Table> tableList = TableDAO.Instance.LoadTableList();

            foreach (Table item in tableList)
            {
                Button btn = new Button() { Width = TableDAO.TableWidth, Height = TableDAO.TableHeight };
                btn.Text = item.Name + Environment.NewLine + item.Status;
                btn.Click += Btn_Click;
                btn.Tag = item;



                switch (item.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.GreenYellow;
                        break;
                    default: 
                        btn.BackColor = Color.BlueViolet; 
                        break;
                }
                flpTable.Controls.Add(btn);
            }

        }
        #endregion

        void ShowBill(int id)
        {
            lsvBill.Items.Clear();
            List<Menu> listBillInfo = MenuDAO.Instance.GetListMenuByTable(id);
            float totalPrice = 0;
            foreach (Menu item in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.DrinkName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;
                lsvBill.Items.Add(lsvItem);
            }
            CultureInfo culture = new CultureInfo("vi-VN");
            
            txbTotalPrice.Text = totalPrice.ToString("c", culture);
        }
        #region Events

        void Btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).ID;
            lsvBill.Tag = (sender as Button).Tag;
            ShowBill(tableID);
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAccountProfile f = new FormAccountProfile();
            f.ShowDialog();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAdmin f = new FormAdmin();
            f.ShowDialog(); 
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;

            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null) return;
            Category selected = cb.SelectedItem as Category;
            id = selected.ID;


            LoadDrinkListByCategoryID(id);
        }

        private void btnAddDrink_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;

            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);
            int drinkID = (cbbDrink.SelectedItem as Drink).ID;
            int count = (int)nmDrinkCount.Value;

            if (idBill == -1)
            {
                BillDAO.Instance.InsertBill(table.ID);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIDBill(), drinkID, count);
            }
            else
            {
                if (BillInfoDAO.Instance.CheckExitBillInfo(idBill, drinkID))
                {
                    BillInfoDAO.Instance.UpdateDrinkCount(idBill, drinkID, count);
                }
                else
                {
                    BillInfoDAO.Instance.InsertBillInfo(idBill, drinkID, count);
                }
            }

            ShowBill(table.ID);

            LoadTable();
        }


        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;

            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);

            if (idBill != -1)
            {
                if (MessageBox.Show("Bạn có chắc thanh toán hóa đơn cho bàn " + table.Name, "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(idBill);
                    ShowBill(table.ID);


                    LoadTable();
                }
            }
        }


        #endregion

        private void lsvBill_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
