using _20T1020025QuanDrink.DAO;
using _20T1020025QuanDrink.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _20T1020025QuanDrink
{
    public partial class FormAdmin : Form
    {
        BindingSource drinkList = new BindingSource();
        BindingSource accountList = new BindingSource();
        BindingSource categoryList = new BindingSource();
        public FormAdmin()
        {
            InitializeComponent();
            Load();
        }

        #region methods
        List<Drink> SearchDrinkByName(string name)
        {
            List<Drink> listDrink = DrinkDAO.Instance.SearchDrinkByName(name);

            return listDrink;
        }
        void Load()
        {
            dtgvDrink.DataSource = drinkList;
            dtgvAccount.DataSource = accountList;
            LoadListDrink();
            LoadAccount();
            LoadListCategory();

            AddDrinkBinding();
            AddAccountBinding();
            AddCategoryBinding();
            LoadCategoryIntoCombobox(cbbDrinkCategory);

        }

        void AddAccountBinding()
        {
            txbUserName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "UserName", true, DataSourceUpdateMode.Never));
            txbDisplayName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            numericUpDown1.DataBindings.Add(new Binding("Value", dtgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));
        }

        void LoadAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccount();
        }

        void AddDrinkBinding()
        {
            txbDrinkName.DataBindings.Add(new Binding("Text", dtgvDrink.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txbDrinkID.DataBindings.Add(new Binding("Text", dtgvDrink.DataSource, "ID", true, DataSourceUpdateMode.Never));
            nmDrinkPrice.DataBindings.Add(new Binding("Value", dtgvDrink.DataSource, "Price", true, DataSourceUpdateMode.Never));
        }

        void LoadCategoryIntoCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }
        void LoadListDrink()
        {
           drinkList.DataSource = DrinkDAO.Instance.GetListDrink();
        }

        void AddAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.InsertAccount(userName, displayName, type))
            {
                MessageBox.Show("Thêm tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại");
            }

            LoadAccount();
        }

        void EditAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.UpdateAccount(userName, displayName, type))
            {
                MessageBox.Show("Cập nhật tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật tài khoản thất bại");
            }

            LoadAccount();
        }

        void DeleteAccount(string userName)
        {
            if (AccountDAO.Instance.DeleteAccount(userName))
            {
                MessageBox.Show("Xóa tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Xóa tài khoản thất bại");
            }

            LoadAccount();
        }

        void ResetPass(string userName)
        {
            if (AccountDAO.Instance.ResetPassword(userName))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công");
            }
            else
            {
                MessageBox.Show("Đặt lại mật khẩu thất bại");
            }
        }

        void AddCategoryBinding()
        {
            txbCategoryName.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "Name"));
            txbCategoryID.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "ID"));
        }
        void LoadListCategory()
        {
            dtgvCategory.DataSource = CategoryDAO.Instance.GetListCategory();

        }
        #endregion



        #region events
        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadListCategory();
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            int type = (int)numericUpDown1.Value;

            AddAccount(userName, displayName, type);
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;

            DeleteAccount(userName);
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            int type = (int)numericUpDown1.Value;

            EditAccount(userName, displayName, type);
        }


        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;

            ResetPass(userName);
        }

        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadAccount();
        }

        private void btnSearchDrink_Click(object sender, EventArgs e)
        {
            drinkList.DataSource = SearchDrinkByName(txbSearchDrinkName.Text);
        }


        private void btnShowDrink_Click(object sender, EventArgs e)
        {
            LoadListDrink();
        }
        #endregion

        private void txbDrinkID_TextChanged(object sender, EventArgs e)
{
    try
    {
        if (dtgvDrink.SelectedCells.Count > 0 && dtgvDrink.SelectedCells[0].OwningRow != null)
        {
            object categoryIDCellValue = dtgvDrink.SelectedCells[0].OwningRow.Cells["CategoryID"].Value;

            if (categoryIDCellValue != null)
            {
                int id = (int)categoryIDCellValue;

                Category category = CategoryDAO.Instance.GetCategoryByID(id);

                if (category != null)
                {
                    cbbDrinkCategory.SelectedItem = category;

                    int index = cbbDrinkCategory.FindStringExact(category.Name);
                    cbbDrinkCategory.SelectedIndex = index;
                }
            }
        }
    }
    catch (Exception ex)
    {
        // Xử lý exception hoặc hiển thị thông báo lỗi nếu cần thiết
        MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}


        private void btnAdđrink_Click(object sender, EventArgs e)
        {
            string name = txbDrinkName.Text;
            int categoryID = (cbbDrinkCategory.SelectedItem as Category).ID;
            float price = (float)nmDrinkPrice.Value;

            if (DrinkDAO.Instance.InsertDrink(name, categoryID, price))
            {
                MessageBox.Show("Thêm món thành công");
                LoadListDrink();
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm thức ăn");
            }
        }

        private void btnEditDrink_Click(object sender, EventArgs e)
        {
            string name = txbDrinkName.Text;
            int categoryID = (cbbDrinkCategory.SelectedItem as Category).ID;
            float price = (float)nmDrinkPrice.Value;
            int id = Convert.ToInt32(txbDrinkID.Text);

            if (DrinkDAO.Instance.UpdateDrink(id, name, categoryID, price))
            {
                MessageBox.Show("Sửa món thành công");
                LoadListDrink();
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa thức ăn");
            }
        }

        private void btnDeleteDrink_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbDrinkID.Text);

            if (DrinkDAO.Instance.DeleteDrink(id))
            {
                MessageBox.Show("Xóa món thành công");
                LoadListDrink();
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa thức ăn");
            }
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string categoryName = txbCategoryName.Text;
            if (CategoryDAO.Instance.InsertCategory(categoryName))
            {
                MessageBox.Show("Thêm danh mục thành công");
                LoadListCategory();
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm danh mục");
            }
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            // Sử dụng string cho categoryID, vì categoryID có thể là chuỗi hoặc số
            string categoryID = txbCategoryID.Text;
            string categoryName = txbCategoryName.Text;

            // Kiểm tra xem categoryID có giá trị không rỗng và có thể chuyển đổi thành số không
            if (!string.IsNullOrEmpty(categoryID) && int.TryParse(categoryID, out int categoryIDInt))
            {
                // Thực hiện cập nhật danh mục
                if (CategoryDAO.Instance.UpdateCategory(categoryID, categoryName))
                {
                    MessageBox.Show("Sửa danh mục thành công");
                    LoadListCategory();
                }
                else
                {
                    MessageBox.Show("Có lỗi khi sửa danh mục");
                }
            }
            else
            {
                MessageBox.Show("Category ID không hợp lệ");
            }
        }




        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int categoryID = Convert.ToInt32(txbCategoryID.Text);
            if (CategoryDAO.Instance.DeleteCategory(categoryID))
            {
                MessageBox.Show("Xóa danh mục thành công");
                LoadListCategory();
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa danh mục");
            }
        }

    }
}
