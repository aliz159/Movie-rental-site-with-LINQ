//using project_ASP.NET.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace project_ASP.NET.DAL
{
    public class CustomerTabelHelper
    {
        public static void InsertNewCustomer(string strCustName, int intAge, string strSubscription)
        {
            DataClasses3DataContext nw = new DataClasses3DataContext(DalUtils.GetConnectionString());
            Customer cust = new Customer();
            cust.Name = strCustName;
            cust.Age = intAge;
            cust.Subscription = strSubscription;
            nw.Customers.InsertOnSubmit(cust);
            nw.SubmitChanges();
        }

        public static List<Customer> GetCustomersList()
        {
            List<Customer> list = new List<Customer>();
            DataClasses3DataContext nw = new DataClasses3DataContext(DalUtils.GetConnectionString());

            var resultQuery = from cust in nw.Customers select cust;

            foreach (var item in resultQuery)
            {
                list.Add(new Customer
                {
                    Id = item.Id,
                    Name = item.Name,
                    Age = item.Age,
                    Subscription = item.Subscription
                });
            }
            return list;
        }

        //מחזיר רשימה עם שמות הלקוחות הקיימים בלבד
        public static List<string> GetCustomersNameOnly()
        {
            List<string> CustomerNames = new List<string>();

            foreach (var item in GetCustomersList())
            {
                CustomerNames.Add(item.Name);
            }
            return CustomerNames;
        }

        //בוזק אם שם הלקוח קיים או לא
        static public bool DoseCustomerExists(string Name)
        {
            bool exist = false;
            bool ExistInDB = GetCustomersList().Exists(x => (x.Name == Name));

            if (ExistInDB)
            {
                exist = true;
            }
            return exist;
        }
    }
}