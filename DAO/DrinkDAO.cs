using _20T1020025QuanDrink.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020025QuanDrink.DAO
{
    public class DrinkDAO
    {
        private static DrinkDAO instance;
        public static DrinkDAO Instance
        {
            get { if (instance == null) instance = new DrinkDAO(); return DrinkDAO.instance; }
            private set { DrinkDAO.instance = value; }
        }
        private DrinkDAO() { }

        public List<Drink> GetDrinkByCategoryID(int id)
        {
            List<Drink> list = new List<Drink> ();

            string query = "select * from Drink where idCategory = " + id;

            DataTable data= DataProvider.Instance.ExecuteQuery (query);

            foreach (DataRow item in data.Rows)
            {
                Drink drink = new Drink(item);
                list.Add(drink);

            }
            return list;

        }
        public List<Drink> GetListDrink()
        {
            List<Drink> list = new List<Drink>();

            string query = "select * from Drink";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Drink drink = new Drink(item);
                list.Add(drink);
            }

            return list;
        }

        public List<Drink> SearchDrinkByName(string name)
        {

            List<Drink> list = new List<Drink>();

            string query = string.Format("SELECT * FROM dbo.Drink WHERE name = N'{0}' ", name);

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Drink drink = new Drink(item);
                list.Add(drink);
            }

            return list;
        }

        public bool InsertDrink(string name, int id, float price)
        {
            string query = string.Format("INSERT dbo.Drink ( name, idCategory, price )VALUES  ( N'{0}', {1}, {2})", name, id, price);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool UpdateDrink(int idDrink, string name, int id, float price)
        {
            string query = string.Format("UPDATE dbo.Drink SET name = N'{0}', idCategory = {1}, price = {2} WHERE id = {3}", name, id, price, idDrink);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool DeleteDrink(int idDrink)
        {
            BillInfoDAO.Instance.DeleteBillInfoByDrinkID(idDrink);

            string query = string.Format("Delete Drink where id = {0}", idDrink);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public List<Drink> SearchDrinkByNameApproximate(string name)
        {
            List<Drink> listDrink = new List<Drink>();
            string query = string.Format("SELECT * FROM dbo.Drink WHERE dbo.fuConvertToUnsign1(name) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", name);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Drink drink = new Drink(item);
                listDrink.Add(drink);
            }
            return listDrink;
        }

    }

}
