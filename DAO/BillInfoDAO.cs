using _20T1020025QuanDrink.DTO;
using System.Collections.Generic;
using System.Data;

namespace _20T1020025QuanDrink.DAO
{
    public class BillInfoDAO
    {
        private static BillInfoDAO instance;

        public static BillInfoDAO Instance
        {
            get { if (instance == null) instance = new BillInfoDAO(); return instance; }
            private set { BillInfoDAO.instance = value; }
        }

        private BillInfoDAO() { }



        public List<BillInfo> GetListBillInfo(int id)
        {
            List<BillInfo> listBillInfo = new List<BillInfo>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT  * FROM dbo.BillInfo WHERE idBill = @id", new object[] { id });

            foreach (DataRow item in data.Rows)
            {
                BillInfo info = new BillInfo(item);
                listBillInfo.Add(info);
            }

            return listBillInfo;
        }
        public void DeleteBillInfoByDrinkID(int id)
        {
            DataProvider.Instance.ExecuteQuery("delete dbo.BillInfo WHERE idDrink = " + id);
        }
        public void InsertBillInfo(int idBill, int idDrink, int count)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_InsertBillInfo @idBill , @idDrink , @count", new object[] { idBill, idDrink, count });
        }
        public bool CheckExitBillInfo(int idBill, int idDrink)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.BillInfo WHERE idBill = @idBill AND idDrink = @idDrink", new object[] { idBill, idDrink });
            return data.Rows.Count > 0;
        }

        public void UpdateDrinkCount(int idBill, int idDrink, int count)
        {
            DataProvider.Instance.ExecuteNonQuery("UPDATE dbo.BillInfo SET count = count + @count WHERE idBill = @idBill AND idDrink = @idDrink", new object[] { count, idBill, idDrink });
        }
    }
}
