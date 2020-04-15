using LINQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LINQ
{
    class Program
    {
        static void Main()
        {
            //PrintAllProducts();
            //PrintAllCustomers();
            //Exercise1();
            //Exercise2();
            //Exercise3();
            //Exercise4();
            //Exercise5();
            //Exercise6();
            //Exercise7();
            //Exercise8();
            //Exercise9();
            //Exercise10(); 
            //Exercise11();
            //Exercise12();
            //Exercise13(); 
            //Exercise14();
            //Exercise15();
            //Exercise16();
            //Exercise17();
            //Exercise18();
            //Exercise19();
            //Exercise20();
            //Exercise21();
            //Exercise22();
            //Exercise23();
            //Exercise24(); 
            //Exercise25(); 
            //Exercise26();
            //Exercise27();
            //Exercise28(); 
            //Exercise29();
            //Exercise30();
            Exercise31(); 

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

        }

        #region "Sample Code"
        /// <summary>
        /// Sample, load and print all the product objects
        /// </summary>
        static void PrintAllProducts()
        {
            List<Product> products = DataLoader.LoadProducts();
            PrintProductInformation(products);
        }

        /// <summary>
        /// This will print a nicely formatted list of products
        /// </summary>
        /// <param name="products">The collection of products to print</param>
        static void PrintProductInformation(IEnumerable<Product> products)
        {
            string line = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6}";
            Console.WriteLine(line, "ID", "Product Name", "Category", "Unit", "Stock");
            Console.WriteLine("==============================================================================");

            foreach (var product in products)
            {
                Console.WriteLine(line, product.ProductID, product.ProductName, product.Category,
                    product.UnitPrice, product.UnitsInStock);
            }

        }

        /// <summary>
        /// Sample, load and print all the customer objects and their orders
        /// </summary>
        static void PrintAllCustomers()
        {
            var customers = DataLoader.LoadCustomers();
            PrintCustomerInformation(customers);
        }

        /// <summary>
        /// This will print a nicely formated list of customers
        /// </summary>
        /// <param name="customers">The collection of customer objects to print</param>
        static void PrintCustomerInformation(IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                Console.WriteLine("==============================================================================");
                Console.WriteLine(customer.CompanyName);
                Console.WriteLine(customer.Address);
                Console.WriteLine("{0}, {1} {2} {3}", customer.City, customer.Region, customer.PostalCode, customer.Country);
                Console.WriteLine("p:{0} f:{1}", customer.Phone, customer.Fax);
                Console.WriteLine();
                Console.WriteLine("\tOrders");
                foreach (var order in customer.Orders)
                {
                    Console.WriteLine("\t{0} {1:MM-dd-yyyy} {2,10:c}", order.OrderID, order.OrderDate, order.Total);
                }
                Console.WriteLine("==============================================================================");
                Console.WriteLine();
            }
        }
        #endregion


        /// <summary>
        /// Print all products that are out of stock.
        /// </summary>
        static void Exercise1()
        {

            var outOfStock = DataLoader.LoadProducts().Where(s => s.UnitsInStock == 0);

            PrintProductInformation(outOfStock);

        }

        /// <summary>
        /// Print all products that are in stock and cost more than 3.00 per unit.
        /// </summary>
        static void Exercise2()
        {
            var inStockGreaterThan3 = DataLoader.LoadProducts().Where(s => s.UnitsInStock > 0 && s.UnitPrice > 3);
            PrintProductInformation(inStockGreaterThan3);
        }

        /// <summary>
        /// Print all customer and their order information for the Washington (WA) region.
        /// </summary>
        static void Exercise3()
        {
            var customerByRegion = DataLoader.LoadCustomers().Where(s => s.Region == "WA");

            PrintCustomerInformation(customerByRegion);
        }

        /// <summary>
        /// Create and print an anonymous type with just the ProductName
        /// </summary>
        static void Exercise4()
        {
            var anonP = from p in DataLoader.LoadProducts()
                        select new { pName = p.ProductName };

            foreach (var prod in anonP)
            {
                Console.WriteLine(prod.pName);
            }
        }




        /// <summary>
        /// Create and print an anonymous type of all product information but increase the unit price by 25%
        /// </summary>
        static void Exercise5()
        {
            string line = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6}";
            Console.WriteLine(line, "ID", "Product Name", "Category", "Unit", "Stock");
            Console.WriteLine("==============================================================================");

            var product = from p in DataLoader.LoadProducts()
                          select new { p.ProductID, p.ProductName, p.Category, IncreasedPrice = p.UnitPrice * 1.25M, p.UnitsInStock };

            foreach (var withincrease in product)
            {
                Console.WriteLine(line, withincrease.ProductID,
                    withincrease.ProductName, withincrease.Category, withincrease.IncreasedPrice, withincrease.UnitsInStock);
            }

        }

        /// <summary>
        /// Create and print an anonymous type of only ProductName and Category with all the letters in upper case
        /// </summary>
        static void Exercise6()
        {
            string line = "{0,-40} {1}";
            Console.WriteLine(line, "Product Name", "Category");
            Console.WriteLine("==============================================================================");

            var anon = from p in DataLoader.LoadProducts()
                       select new { ProductNameUpper = p.ProductName.ToUpper(), CategoryUpper = p.Category.ToUpper() };
            foreach (var p in anon)
            {
                Console.WriteLine(line, p.ProductNameUpper, p.CategoryUpper);
            }

        }

        /// <summary>
        /// Create and print an anonymous type of all Product information with an extra bool property ReOrder which should 
        /// be set to true if the Units in Stock is less than 3
        /// 
        /// Hint: use a ternary expression
        /// </summary>
        static void Exercise7()
        {
            string line = "{0,-5} {1,-35} {2,-15} {3,7:c} {4,5} {5,3}";
            Console.WriteLine(line, "ID", "Product Name", "Category", "Unit", "Stock", "Reorder");
            Console.WriteLine("==============================================================================");


            var anon = from p in DataLoader.LoadProducts()
                       select new
                       {
                           p.ProductID,
                           p.ProductName,
                           p.Category,
                           p.UnitPrice,
                           p.UnitsInStock,
                           Reorder = p.UnitsInStock < 3 ? "Yes" : "No"
                       }; // true if less than 3, fralse greater than or equal to 3};

            foreach (var item in anon)
            {
                Console.WriteLine(line, item.ProductID, item.ProductName, item.Category, item.UnitPrice, item.UnitsInStock, item.Reorder);
            }



        }

        /// <summary>
        /// Create and print an anonymous type of all Product information with an extra decimal called 
        /// StockValue which should be the product of unit price and units in stock
        /// </summary>
        static void Exercise8()
        {
            string line = "{0,-5} {1,-35} {2,-15} {3,7:c} {4,5} {5,5:c}";
            Console.WriteLine(line, "ID", "Product Name", "Category", "Unit", "Stock", "Stock Value");
            Console.WriteLine("===================================================================================");


            var anon = from p in DataLoader.LoadProducts()
                       select new
                       {
                           p.ProductID,
                           p.ProductName,
                           p.Category,
                           p.UnitPrice,
                           p.UnitsInStock,
                           StockValue = (p.UnitsInStock * p.UnitPrice)
                       };

            foreach (var item in anon)
            {
                Console.WriteLine(line, item.ProductID, item.ProductName, item.Category, item.UnitPrice, item.UnitsInStock, item.StockValue);
            }


        }

        /// <summary>
        /// Print only the even numbers in NumbersA
        /// </summary>
        static void Exercise9()
        {
            var onlyEvens = DataLoader.NumbersA.Where(number => number % 2 == 0);

            foreach (var item in onlyEvens)
            {
                Console.WriteLine(item);
            }


        }

        /// <summary>
        /// Print only customers that have an order whos total is less than $500
        /// </summary>
        static void Exercise10()
        {
            //by customer, list of orders less than 500
            //list of customers with an order less than 500
            var customers = from c in DataLoader.LoadCustomers()
                            from o in c.Orders
                            where o.Total < 500
                            select c;


            PrintCustomerInformation(customers);


        }

        /// <summary>
        /// Print only the first 3 odd numbers from NumbersC
        /// </summary>
        static void Exercise11()
        {
            var onlyOdds = DataLoader.NumbersA.Where(number => number % 2 == 1).Take(3);

            foreach (var item in onlyOdds)
            {
                Console.WriteLine(item);
            }

        }

        /// <summary>
        /// Print the numbers from NumbersB except the first 3
        /// </summary>
        static void Exercise12()
        {
            var numbersNotFirst3 = DataLoader.NumbersB.Skip(3);



            foreach (var item in numbersNotFirst3)
            {
                Console.WriteLine(item);
            }

        }

        /// <summary>
        /// Print the Company Name and most recent order for each customer in Washington
        /// </summary>
        /// 
        //how do I get most recent order
        static void Exercise13()
        {

            var customersInWA = DataLoader.LoadCustomers().Where(c => c.Region == "WA");

            foreach (var customer in customersInWA)
            {
                Console.WriteLine(customer.CompanyName);

                var mostRecentOrder = customer.Orders.OrderByDescending(r => r.OrderDate).Take(1);

                foreach (var order in mostRecentOrder)
                {
                    Console.WriteLine("OrderID: {0} Order Date: {1} Order Total {2:c}", order.OrderID, order.OrderDate, order.Total);

                }

            }

        }




        /// <summary>
        /// Print all the numbers in NumbersC until a number is >= 6
        /// </summary>
        static void Exercise14()
        {
            var numbers = DataLoader.NumbersC;

            var filterdNumbers = numbers.TakeWhile(n => n <= 6);
                          

            foreach (var number in filterdNumbers)
            {
                Console.WriteLine(number);
            }

        }

        /// <summary>
        /// Print all the numbers in NumbersC that come after the first number divisible by 3
        /// </summary>
        static void Exercise15()
        {
            var numbers = DataLoader.NumbersC; //5,4,3,1,9,8,6,7,2,0

            var filteredNumbers = numbers.SkipWhile(n => n % 3 != 0).Skip(1); 
            int length = filteredNumbers.Count();
            var finalList = numbers.Skip(length);

            //Console.WriteLine(length);


            foreach (var x in filteredNumbers)
            {
                Console.WriteLine(x);
            }



        }

        /// <summary>
        /// Print the products alphabetically by name
        /// </summary>
        static void Exercise16()
        {
            var productsByName = DataLoader.LoadProducts().OrderBy(p => p.ProductName);

            PrintProductInformation(productsByName);

        }

        /// <summary>
        /// Print the products in descending order by units in stock
        /// </summary>
        static void Exercise17()
        {
            var descUnitStock = DataLoader.LoadProducts().OrderByDescending(d => d.UnitsInStock);
            PrintProductInformation(descUnitStock);
        }

        /// <summary>
        /// Print the list of products ordered first by category, then by unit price, from highest to lowest.
        /// </summary>
        static void Exercise18()
        {
            var orderedProducts = DataLoader.LoadProducts().OrderByDescending(o => o.UnitPrice).GroupBy(o => o.Category);



            foreach (var item in orderedProducts)
            {
                Console.WriteLine("Category: {0}", item.Key);
                Console.WriteLine("--------------------------------------------------------");

                foreach (var product in item)
                {
                    Console.WriteLine("Product Name: {0} Unit Price: {1:c}", product.ProductName, product.UnitPrice);
                }

            }
        }

        /// <summary>
        /// Print NumbersB in reverse order
        /// </summary>
        static void Exercise19()
        {
            var reverseB = DataLoader.NumbersB.Reverse().ToArray();

            foreach (var item in reverseB)
            {
                Console.Write(item);
            }

        }

        /// <summary>
        /// Group products by category, then print each category name and its products
        /// ex:
        /// 
        /// Beverages
        /// Tea
        /// Coffee
        /// 
        /// Sandwiches
        /// Turkey
        /// Ham
        /// 
        /// </summary>
        static void Exercise20()
        {
            var result = DataLoader.LoadProducts().GroupBy(r => r.Category);
            foreach (var item in result)
            {
                Console.WriteLine("Category: {0}", item.Key);
                Console.WriteLine("-------------------------------------------------");


                foreach (var p in item)
                {
                    Console.WriteLine(p.ProductName);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Print all Customers with their orders by Year then Month
        /// ex:
        /// 
        /// Joe's Diner
        /// 2015
        ///     1 -  $500.00
        ///     3 -  $750.00
        /// 2016
        ///     2 - $1000.00
        /// </summary>

        static void Exercise21()
        {
            var customers = DataLoader.LoadCustomers();
            foreach (var customer in customers)
            {
                Console.WriteLine(customer.CompanyName);
                Console.WriteLine("---------------------------------------------------------");
               
                //1. PRINT THE NAME OF THE COMPANY ONCE
                //group each customers list of orders by year
                var ordersByYear = from order in customer.Orders
                                        group order by order.OrderDate.Year;
                      
                //2. lIST ALL ORDERS BY YEAR
                foreach (var order in ordersByYear)
                {    
                    Console.WriteLine(order.Key); // this will print the year
                    var ordersByMonth = from order1 in order
                                        group order1 by order1.OrderDate.Month;

                //3. LIST ALL ORDERS BY MONTH AND SUM MONTHLY TOTAL
                    foreach (var o in ordersByMonth)
                    {
                        
                        var monthsum = o.Sum(x => x.Total); //sums each month total
                        
                        Console.WriteLine("{0}" + " - " + "{1:c}", o.Key, monthsum); // prints the month and month sum

                       Console.WriteLine();
                    }

                }

            }

        }


        /// <summary>
        /// Print the unique list of product categories
        /// </summary>
        static void Exercise22()
        {
            var products = DataLoader.LoadProducts();

            var groupByCategory =  from p in products
            group p by p.Category;

            foreach (var productCategory in groupByCategory)
            {
                Console.WriteLine(productCategory.Key);
            }
          
        }

        /// <summary>
        /// Write code to check to see if Product 789 exists
        /// </summary>
        static void Exercise23()
        {
            var products = DataLoader.LoadProducts();

            //for each product in this list of products
            // see if that prodict contains 789

            List<int> orderIDs = new List<int>();

            foreach (var p in products)
            {

                orderIDs.Add(p.ProductID);
            
            }

            var contains = orderIDs.Contains(789);
            Console.WriteLine(contains);

        
                                
        }

        /// <summary>
        /// Print a list of categories that have at least one product out of stock
        /// </summary>
        static void Exercise24()
        {
            var products = DataLoader.LoadProducts();
            var productsOutOfStock = from p in products
                                     where p.UnitsInStock == 0
                                     group p by p.Category;

            foreach (var item in productsOutOfStock)
            {
                Console.WriteLine(item.Key);

            }
        }

        /// <summary>
        /// Print a list of categories that have no products out of stock
        /// </summary>
        static void Exercise25()
        {
            var products = DataLoader.LoadProducts();
          
            var ProductCategoryGroups = from c in products
                                       group c by c.Category
                                       into p
                                       where p.All(o => o.UnitsInStock > 0)
                                       select p.Key;

            foreach (var item in ProductCategoryGroups)
            {
                Console.WriteLine(item);
            }


        }

        /// <summary>
        /// Count the number of odd numbers in NumbersA
        /// </summary>
        static void Exercise26()
        {
            var numbers = DataLoader.NumbersA;

            var oddNumbers = from n in numbers
                             where n % 2 == 1
                             select n;
            var count = oddNumbers.Count();
            Console.WriteLine(count);

        }

        /// <summary>
        /// Create and print an anonymous type containing CustomerId and the count of their orders
        /// 
        /// var
        /// </summary>
        static void Exercise27()
        {
            var customers = DataLoader.LoadCustomers();

            var result = from c in customers
                         select new
                         {
                             CustomerID = c.CustomerID,
                             orderCount = c.Orders.Count()

                        };

            foreach (var item in result)
            {
                Console.WriteLine("Customner ID: {0} Order Count: {1}", item.CustomerID, item.orderCount);

            }
        }

        /// <summary>
        /// Print a distinct list of product categories and the count of the products they contain
        /// </summary>
        static void Exercise28()
        {
            var products = DataLoader.LoadProducts();

            
            var groups = from p in products
                         group p by p.Category;
            
            foreach (var group in groups) 
            {
                Console.WriteLine("Product Category: {0} Number of Products: {1}", group.Key, group.Count()); 
     
            }


        }

        /// <summary>
        /// Print a distinct list of product categories and the total units in stock
        /// </summary>
        static void Exercise29()
        {
            var products = DataLoader.LoadProducts();

            //List of products by category

            var groups = from p in products
                         group p by p.Category
                         into g
                         select new { totalInStock = g.Sum(x => x.UnitsInStock),
                         inStockCategory = g.Key};

            foreach (var group in groups) //each group is a category (i.e meats)
            {
                Console.WriteLine("Category: {0} Total in stock: {1}", group.inStockCategory, group.totalInStock);

            }

        }

        /// <summary>
        /// Print a distinct list of product categories and the lowest priced product in that category
        /// </summary>
        static void Exercise30()
        {
            var products = DataLoader.LoadProducts();

            //List of products by category

            var categoryGroup = from product in products
                                group product by product.Category;

            foreach (var product in categoryGroup)
            {
                Console.WriteLine(product.Key);
                Console.WriteLine("-----------------------------------------------------");

                var lowestPriceInGroup = (from p in product
                                          orderby p.UnitPrice
                                          select p).Take(1);

                foreach (var item in lowestPriceInGroup)
                {
                    Console.WriteLine("{0}{1:c}",item.ProductName, item.UnitPrice);
                    Console.WriteLine();
                }      
                         
            }
        }

        /// <summary>
        /// Print the top 3 categories by the average unit price of their products
        /// </summary>
        static void Exercise31()
        {
            // 1 GROUP BY CATEGORY
            // 2 GET AVERAGE PRICE OF PRODUCTS
            // 3 TAKE top 3

            var products = DataLoader.LoadProducts();

            var aveProductPrice = from p in products
                                  group p by new
                                  {
                                      p.Category

                                  } into q
                                  select new
                                  {
                                      Average = q.Average(r => r.UnitPrice),
                                      q.Key.Category
                                  };
            var topThree = aveProductPrice.OrderByDescending(x => x.Average).Take(3);

            foreach (var item in topThree)
            {
                Console.WriteLine("Category: {0}  Average Unit Price: {1:c}", item.Category, item.Average);
            } 
        }
    }
}
