using CustomerManagement.Infrastructures;
using CustomerManagement.Models;
using CustomerManagement.Models.Customers;
using CustomerManagement.Services;
using CustomerManagement.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace CustomerManagement.ConsoleApp
{
    class Program
    {
        private static IApplicationService applicationService;
        private static ICustomerService customerService;
        static void Main(string[] args)
        {
            applicationService = new ApplicationService();
            customerService = new CustomerService();

            var application = applicationService.GetApplication();

            Console.WriteLine("Application Name: {0}", application.Name);
            RenderMenu(application.Menus);

            var showMenu = true;
            while (showMenu)
            {
                switch (Console.ReadLine())
                {
                    case "0":
                        RenderMenu(application.Menus);
                        showMenu = true;
                        break;
                    case "1":
                        RenderCustomer(customerService.GetCustomers());
                        showMenu = true;
                        break;
                    case "2":
                        RenderAddCustomer();
                        showMenu = true;
                        break;
                    case "3":
                        RenderDeleteCustomer();
                        showMenu = true;
                        break;
                    case "4":
                        showMenu = true;
                        break;
                    case "5":
                        Console.WriteLine("Good bye!!!");
                        showMenu = false;
                        break;
                    default:
                        RenderFillMenu();
                        showMenu = true;
                        break;
                }
            }
        }

        static void RenderMenu(List<Menu> menus)
        {
            Console.WriteLine("===== List Menu =====");
            foreach (var item in menus)
            {
                Console.WriteLine("Press {0} : {1}", item.Id, item.Name);
            }
            RenderFillMenu();
        }

        static void RenderCustomer(IEnumerable<Customer> customers)
        {
            Console.WriteLine("===== List Customer =====");
            foreach (var item in customers)
            {
                Console.WriteLine("{0} : {1} : {2}", item.Id, item.FirstName, item.LastName, item.BirthDate);
            }
            RenderFillMenu();
        }

        static void RenderAddCustomer()
        {
            var customer = new Customer();
            Console.WriteLine("===== Add new Customer =====");
            Console.Write("Enter Id: ");
            customer.Id = int.Parse(Console.ReadLine());

            Console.Write("Enter Firstname: ");
            customer.FirstName = Console.ReadLine();

            Console.Write("Enter Lastname: ");
            customer.LastName = Console.ReadLine();

            Console.Write("Enter Birthdate ex. 2020-05-31: ");
            var birthdate = Console.ReadLine();
            customer.BirthDate = birthdate.ToString().StringToDateTime();

            if(customerService.AddCustomer(customer))
            {
                Console.WriteLine("Customer was add.");
            }
            else
            {
                Console.WriteLine("Customer was not add.");
            }

            RenderFillMenu();
        }

        static void RenderDeleteCustomer()
        {
            Console.WriteLine("===== Delete Customer =====");
            Console.Write("Enter Id: ");
            var customerId = int.Parse(Console.ReadLine());

            if (customerService.DeleteCustomer(customerId))
            {
                Console.WriteLine("Customer was delete.");
            }
            else
            {
                Console.WriteLine("Customer was not delete.");
            }

            RenderFillMenu();
        }

        static void RenderFillMenu()
        {
            Console.Write("Enter your menu: ");
        }
    }
}
