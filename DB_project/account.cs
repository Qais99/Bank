using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_project
{
    class account
    {
        private string u;
        decimal b;
        int phone;

        public account(string username, decimal b, int phone)
        {
            u = username;
            this.phone = phone;
            this.b = b;
        }
        ~account() { }
        public string name
        {
            set { u = value; }
            get { return u; }

        }
        public int Phone
        {
            set { phone = value; }
            get { return phone; }

        }


        public decimal balance
        {
            set { b = value; }
            get { return b; }
        }

    }

}
