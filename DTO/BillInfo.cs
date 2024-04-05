﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020025QuanDrink.DTO
{
    public class BillInfo
    {

        public BillInfo(int id, int billID, int drinkID, int count)
        {
            this.ID = id;
            this.BillID = billID;
            this.DrinkID = drinkID;
            this.Count = count;
        }

        public BillInfo(DataRow row)
        {
            this.ID = (int)row["id"];
            this.BillID = (int)row["idbill"];
            this.DrinkID = (int)row["iddrink"];
            this.Count = (int)row["count"];

        }


        private int count;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }



        private int drinkID;

        public int DrinkID
        {
            get { return drinkID; }
            set { drinkID = value; }
        }

        private int billID;

        public int BillID
        {
            get { return billID; }
            set { billID = value; }
        }



        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }
}
