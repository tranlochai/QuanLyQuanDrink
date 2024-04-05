﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20T1020025QuanDrink.DTO
{
    public class Drink
    {
        public Drink(int id, string name, int categoryID, float price ) 
        {
            this.iD=id;
            this.Name=name;
            this.CategoryID=categoryID;
            this.Price=price;
        }
        public Drink(DataRow row)
        {
            this.ID = (int)row["id"];
            this.Name = row["name"].ToString();
            this.CategoryID = (int)row["idcategory"];
            this.Price = (float)Convert.ToDouble(row["price"].ToString());
        }

        private float price;
        public float Price
        {
            get { return price; }
            set { price = value; }
        }


        private int categoryID;
        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }


        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

    private int iD;
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }

}
