using External.Services.Movements.WebApi.Models;

namespace External.Services.Movements.WebApi.Test.Helpers
{
    public class DataSeeder
    {
        public readonly TestDbContext testDbContext;

        public DataSeeder(TestDbContext testDbContext)
        {
            this.testDbContext = testDbContext;
        }

        public void SeedAllData()
        {
            GenerateCustomers();
            GenerateProducts();
            GenerateProductsCustomers();
            testDbContext.SaveChanges();
        }


        public void GenerateCustomers()
        {
            if (!testDbContext.Customers.Any())
            {
                for (int i = 0; i < 2; i++)
                {
                    var newCustomers = new List<Customer>
                        {

                        new Customer
                        {
                            CustomerId = 1,
                            CustomerFirstName = "Pavlo",
                            CustomerLastName = "Prystaiko",
                            CustomerEmail = "pristayko.pavlo@gmail.com"
                        },
                        new Customer
                        {
                            CustomerId = 2,
                            CustomerFirstName = "Sylvian",
                            CustomerLastName = "Bril",
                            CustomerEmail = "sbril@brandnewday.nl>"
                        },
                        new Customer
                        {
                            CustomerId = 3,
                            CustomerFirstName = "John",
                            CustomerLastName = "Cena",
                            CustomerEmail = "john@cena.us>"
                        }
                    };
                    testDbContext.ChangeTracker.Clear();
                    testDbContext.Customers.AddRange(newCustomers);
                }
            }

        }
        public void GenerateProducts()
        {
            if (!testDbContext.Products.Any())
            {
                var ExternalAccount = "NL54FAKE0326806738";
                for (int i = 0; i < 5; i++)
                {
                    var newProducts = new List<Product>
                    {
                        new Product
                        {
                           ProductId=1,
                           ProductType="Internet booking",
                           ExternalAccount=ExternalAccount,
                        },
                        new Product
                        {
                           ProductId=2,
                           ProductType="Online shopping",
                           ExternalAccount=ExternalAccount,
                        },
                        new Product
                        {
                           ProductId=3,
                           ProductType="Gas",
                           ExternalAccount=ExternalAccount,
                        },
                        new Product
                        {
                           ProductId=4,
                           ProductType="Flowers",
                           ExternalAccount=ExternalAccount,
                        },
                        new Product
                        {
                           ProductId=5,
                           ProductType="Medicine",
                           ExternalAccount=ExternalAccount,
                        }
                    };
                    testDbContext.ChangeTracker.Clear();
                    testDbContext.Products.AddRange(newProducts);
                }
            }
        }
        public void GenerateProductsCustomers()
        {
            if (!testDbContext.ProductCustomer.Any())
            {
                for (int i = 0; i < 5; i++)
                {
                    var newProductCustomer = new List<ProductCustomer>
                    {
                        new ProductCustomer
                        {
                           ProductId=1,
                           CustomerId=2
                        },
                        new ProductCustomer
                        {
                           ProductId=1,
                           CustomerId=3
                        },
                        new ProductCustomer
                        {
                           ProductId=2,
                           CustomerId=2
                        },
                        new ProductCustomer
                        {
                           ProductId=3,
                           CustomerId=2
                        },
                        new ProductCustomer
                        {
                           ProductId=4,
                           CustomerId=4
                        }
                    };
                    testDbContext.ChangeTracker.Clear();
                    testDbContext.ProductCustomer.AddRange(newProductCustomer);
                }
            }
        }
    }
}
