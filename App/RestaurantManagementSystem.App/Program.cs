using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantManagementSystem.App
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string fullName = "Alex Waiter";
            string roleName = "Waiter";
            int roleId = 1; // Replace with actual RoleID for waiter from your DB
            int employeeId = 1; // Replace with actual EmployeeID for Alex Waiter from your DB
            Application.Run(new WelcomeForm(fullName, roleName, roleId, employeeId));
            //Application.Run(new Form1());
        }
    }
}