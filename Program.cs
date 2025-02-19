using EfCoreTutorial.Database;
using EfCoreTutorial.Entity.ECommerceModels;
using EfCoreTutorial.Entity.SchoolModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

Console.WriteLine("Hello, Wazi!");

//#########################--Function Definitions--#########################//

#region SchoolContext
//######### School Context #########//
async void CreateDatabaseAndAddEntries()
{
    using (var db = new SchoolContext())
    {
        try
        {
            //create db if not exist
            //db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            //Add entries to the table
            Grade g1 = new Grade() { GradeName = "A" };
            Grade g2 = new Grade() { GradeName = "B" };

            Student s1 = new Student() { FirstName = "Student", LastName = "#1", Grade = g1 };
            Student s2 = new Student() { FirstName = "Student", LastName = "#2", Grade = g2 };

            //save the new information to the database if it does not exist
            try
            {
                int studentCount = db.Students.Count();
                int gradeCount = db.Grades.Count();

                if (studentCount == 0 && gradeCount == 0)
                {
                    db.Grades.Add(g1);
                    db.Grades.Add(g2);

                    db.Students.Add(s1);
                    db.Students.Add(s2);

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to execute Task. Reason:");
            Console.WriteLine(ex.ToString());
        }
    }
}

void DisplayStates(IEnumerable<EntityEntry> entries)
{
    foreach (var entry in entries)
    {
        Console.WriteLine($"Entity: {entry.Entity.GetType().Name},State: {entry.State.ToString()}");
    }
}

void EntityStateExample()
{
    using (var db = new SchoolContext())
    {
        var student = db.Students.FirstOrDefault();
        DisplayStates(db.ChangeTracker.Entries());
    }
}

void QueryingResults_v1()
{
    using (var db = new SchoolContext())
    {
        //Option 1
        List<Student> students = db.Students.Include(s => s.Grade).ToList();

        //Options 2
        List<Student> students2 = db.Students.Include("Grade").ToList();

        //Fetch and display the results
        if (students.Count > 0)
        {
            foreach (Student student in students)
            {
                Console.WriteLine($"Student Name: {student.FirstName + " " + student.LastName}, Grade: {student.Grade.GradeName}");
            }
        }
        else
        {
            Console.WriteLine("No data found");
        }

        if (students2.Count > 0)
        {
            foreach (Student student in students2)
            {
                Console.WriteLine($"Student Name: {student.FirstName + " " + student.LastName}, Grade: {student.Grade.GradeName}");
            }
        }
        else
        {
            Console.WriteLine("No data found");
        }
    }
}

void QueryingResults_v2()
{
    using (var db = new SchoolContext())
    {
        //Option 1
        var student = db.Students.Where(s => s.StudentId > 0)
                        .Select(s => new
                        {
                            Student = s,
                            Grade = s.Grade,
                        })
                        .ToList();

        //Fetch and display the results
        Console.WriteLine($"Student Name: {student.ToList()[0].Student.FirstName + " " + student.ToList()[0].Student.LastName}, Grade: {student.ToList()[0].Grade.GradeName}");
    }
}

void InsertingDisconnectedEntries()
{
    using (var db = new SchoolContext())
    {
        Student s = db.Students.Where(s => s.LastName == "#3").Include(g => g.Grade).FirstOrDefault();

        if (s == null)
        {
            Grade g = db.Grades.Where(g => g.GradeName == "A").First();

            s = new Student() { FirstName = "Student", LastName = "#3", Grade = g };

            db.Add<Student>(s);

            db.SaveChanges();

            Student s1 = db.Students.Where(s => s.LastName == "#3").Include("Grade").FirstOrDefault();

            Console.WriteLine($"Name: {s1.FirstName + " " + s1.LastName} Grade: {s1.Grade.GradeName}");
        }

        Console.WriteLine($"Name: {s.FirstName + " " + s.LastName} Grade: {s.Grade.GradeName}");
    }
}

void InsertingMultipleRecords()
{
    using (var db = new SchoolContext())
    {
        List<Grade> gradeList = db.Grades.Where(g => g.GradeName == "C" || g.GradeName == "D" || g.GradeName == "E").ToList();

        if (gradeList == null || gradeList.Count == 0)
        {
            List<Grade> listToAdd = new List<Grade>()
            {
                new Grade() { GradeName = "C" },
                new Grade() { GradeName = "D" },
                new Grade() { GradeName = "E" }
            };

            db.AddRange(listToAdd);

            db.SaveChanges();
        }

        gradeList = db.Grades.ToList();

        foreach (Grade grade in gradeList)
        {
            Console.WriteLine($"Grade ID:{grade.GradeId}, Grade: {grade.GradeName}");
        }

    }
}

void UpdateMultipleEntries()
{
    using (var db = new SchoolContext())
    {
        List<Student> students = db.Students.ToList();

        Console.WriteLine("Before Update:");

        foreach (Student student in students)
        {
            Console.WriteLine($"Student ID: {student.StudentId}, Student Name: {student.FirstName} {student.LastName}");
        }

        foreach (Student student in students)
        {
            student.FirstName = "Student#";
            student.LastName = student.LastName.Replace("#", "");
        }

        db.UpdateRange(students); db.SaveChanges();

        Console.WriteLine("After Update:");

        students = db.Students.ToList();

        foreach (Student student in students)
        {
            Console.WriteLine($"Student ID: {student.StudentId}, Student Name: {student.FirstName} {student.LastName}");
        }
    }
}

void ExecuteRawSql()
{
    using (var db = new SchoolContext())
    {
        string lastName = "3";

        List<Student> students = db.Students.FromSql($"select * from Students where LastName = {lastName}").ToList();

        if (students.Count == 0)
        {
            Console.WriteLine("No student found");
        }
        else
        {
            Console.WriteLine($"Student Name: {students[0].FirstName + " " + students[0].LastName}");
        }
    }
}

void ExecuteStoredProcedures()
{
    try
    {
        //Execute Stored Procedures
        using (var db = new SchoolContext())
        {
            DateTime date = DateTime.Now;
            var rowsAffected = db.Database.ExecuteSql($"UpdateGradesInBulk {date}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error Message -v");
        Console.WriteLine(ex.Message);
    }
}

//#########################--Main Thread Of Tasks--#########################//
//Console.WriteLine("Version 0:");
//CreateDatabaseAndAddEntries();

////EntityStateExample();
//Console.WriteLine("Version 1:");
//QueryingResults_v1();

//Console.WriteLine("Version 2:");
//QueryingResults_v2();

//Console.WriteLine("Version 3:");
//InsertingDisconnectedEntries();

//Console.WriteLine("Version 4:");
//InsertingMultipleRecords();

//Console.WriteLine("Version 5");
//UpdateMultipleEntries();

//Console.WriteLine("Version 6");
//ExecuteRawSql();

//Console.WriteLine("Version 7");
//ExecuteStoredProcedures();

#endregion


#region ECommerce Platform
///Function Definitions
void InsertFirstUser()
{
    string username = "Wazi";
    string email = "w@gmail.com";
    string password = "12345678";
    string mobileNo = "01735152470";
    int createdBy = 1;
    DateTime createdDate = DateTime.Now;
    bool isDeleted = false;

    using (var db = new EcommerceContext())
    {
        try
        {
            User u = db.Users.First();

            if (u == null)
            {
                db.Users.FromSql($"Insert into Users(Username, Password, Email, MobileNo, CreatedBy, CreatedDate, IsDeleted) values({username},{password},{email},{mobileNo},{createdBy},{createdDate},{isDeleted})\r\n");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to insert user.");
        }
    }
}
void PopulateProductCategories()
{
    int userId = 1;

    using (var db = new EcommerceContext())
    {
        try
        {
            List<ProductCategory> productCategories = db.ProductCategories.ToList();

            if (productCategories.Count == 0)
            {
                User u = db.Users.Where(u => u.Id == userId).FirstOrDefault();

                Console.WriteLine("No data found");
                string categoryDataString = "Electronics, Clothing, Home & Kitchen, Beauty & Personal Care, Health & Wellness, Toys & Games, Sports & Outdoors, Automotive, Office Supplies, Books, Music & Instruments, Pet Supplies, Grocery & Gourmet Food, Baby Products, Jewelry & Accessories, Shoes & Footwear, Garden & Outdoor, Tools & Home Improvement, Fitness & Exercise, Arts & Crafts, Video Games, Furniture, Watches, Industrial & Scientific, Travel Accessories, Smart Home Devices, Party Supplies, Cameras & Photography, Collectibles, Subscription Boxes";
                string[] catList = categoryDataString.Split(',');
                List<ProductCategory> categories = new List<ProductCategory>();

                foreach (string cat in catList)
                {
                    categories.Add(new ProductCategory()
                    {
                        CategoryName = cat,
                        IsDeleted = false,
                        CreatedBy = userId,
                        CreatedDate = DateTime.Now,
                        CreatedByUser = null
                    });
                }

                try
                {
                    db.ProductCategories.AddRange(categories);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to insert product categories");
                }
            }
            else
            {
                for (int i = 0; i < productCategories.Count; i++)
                {
                    Console.WriteLine($"Cat ID: {i + 1} Product Category: {productCategories[i].CategoryName}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to fetch product categories");
        }
    }
}

void PopulateSellers()
{
    string sellerInfo = @"ABC Traders, 123 Main St, 9876543210, abc@example.com, 4.5 :  
                            XYZ Enterprises, 456 Elm St, 8765432109, xyz@example.com, 4.2 :  
                            Global Mart, 789 Oak St, 7654321098, global@example.com, 4.8 :  
                            Sunrise Retail, 101 Maple Ave, 6543210987, sunrise@example.com, 4.3 :  
                            Elite Supplies, 202 Pine Rd, 5432109876, elite@example.com, 4.7 :  
                            Best Deals Inc., 303 Cedar Ln, 4321098765, bestdeals@example.com, 4.4 :  
                            SuperMart, 404 Birch Blvd, 3210987654, supermart@example.com, 4.6 :  
                            FastShop, 505 Spruce Dr, 2109876543, fastshop@example.com, 4.1 :  
                            Prime Goods, 606 Redwood Ct, 1098765432, primegoods@example.com, 4.9 :  
                            QuickBuy, 707 Aspen Way, 1987654321, quickbuy@example.com, 4.0 :  
                            MegaMart, 808 Willow Rd, 2876543210, megamart@example.com, 4.2 :  
                            ValueMart, 909 Chestnut Pl, 3765432109, valuemart@example.com, 4.5 :  
                            Tech Haven, 111 Oakwood St, 4654321098, techhaven@example.com, 4.7 :  
                            Urban Bazaar, 222 River Ave, 5543210987, urbanbazaar@example.com, 4.3 :  
                            SmartChoice, 333 Hilltop Blvd, 6432109876, smartchoice@example.com, 4.6 :  
                            ProDeals, 444 Sunset Dr, 7321098765, prodeals@example.com, 4.4 :  
                            Trendy Hub, 555 Sunrise Ct, 8210987654, trendyhub@example.com, 4.2 :  
                            EasyShop, 666 Lakeside Ln, 9109876543, easyshop@example.com, 4.5 :  
                            FreshMart, 777 Mountain Rd, 1098765432, freshmart@example.com, 4.8 :  
                            Gadget World, 888 Meadow Ave, 2187654321, gadgetworld@example.com, 4.7 :  
                            Everyday Needs, 999 Brook St, 3276543210, everyday@example.com, 4.3 :  
                            Smart Buys, 112 Greenway Ln, 4365432109, smartbuys@example.com, 4.6 :  
                            Quick Cart, 223 Valley Rd, 5454321098, quickcart@example.com, 4.4 :  
                            Direct Deals, 334 Summit Ave, 6543210987, directdeals@example.com, 4.2 :  
                            Global Traders, 445 Highland Ct, 7632109876, globaltraders@example.com, 4.7 :  
                            BestChoice, 556 Evergreen Dr, 8721098765, bestchoice@example.com, 4.1 :  
                            Speedy Mart, 667 Maple Grove, 9810987654, speedymart@example.com, 4.5 :  
                            Elite Emporium, 778 Sycamore St, 1909876543, eliteemporium@example.com, 4.6 :  
                            Bargain Hub, 889 Redwood Ave, 2998765432, bargainhub@example.com, 4.3 :  
                            Home Essentials, 990 Walnut Ln, 3087654321, homeessentials@example.com, 4.8  
                            ";

    List<Seller> sellers = new List<Seller>();
    string[] seller_string = sellerInfo.Split(":");

    foreach (string s1 in seller_string)
    {
        string[] s = s1.Split(",");

        Seller seller = new Seller()
        {
            CompanyName = s[0].Trim(),
            Address = s[1].Trim(),
            MobileNo = s[2].Trim(),
            Email = s[3].Trim(),
            Rating = Convert.ToDecimal(s[4].Trim()),
            CreatedBy = 1,
            CreatedDate = DateTime.Now,
            IsDeleted = false
        };

        sellers.Add(seller);
    }

    try
    {
        using (var db = new EcommerceContext())
        {
            db.Sellers.AddRange(sellers);
            db.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Failed to insert sellers");
    }
}

void PopulateProducts()
{
    string product_string = @"Smartphone, 1, 4.5, $699, High-performance smartphone with AMOLED display, ABC Traders, Electronics :  
                            Laptop, 1, 4.7, $1199, Powerful laptop with SSD storage and fast processor, XYZ Enterprises, Electronics :  
                            Wireless Earbuds, 1, 4.3, $149, Noise-canceling wireless earbuds with long battery life, Global Mart, Electronics :  
                            Gaming Console, 1, 4.6, $499, Next-gen gaming console with high-resolution graphics, Sunrise Retail, Electronics :  
                            Smartwatch, 1, 4.4, $199, Fitness tracking smartwatch with heart rate monitor, Elite Supplies, Electronics :  
                            Men’s T-Shirt, 1, 4.2, $19, Comfortable cotton t-shirt in multiple colors, Best Deals Inc., Clothing :  
                            Women’s Jeans, 1, 4.5, $49, Stretchable high-waist jeans with a modern fit, SuperMart, Clothing :  
                            Running Shoes, 1, 4.6, $89, Lightweight running shoes with breathable mesh, FastShop, Clothing :  
                            Winter Jacket, 1, 4.7, $129, Insulated winter jacket for extreme cold, Prime Goods, Clothing :  
                            Leather Wallet, 1, 4.3, $39, Genuine leather wallet with RFID protection, QuickBuy, Accessories :  
                            Microwave Oven, 1, 4.4, $299, High-power microwave with grill function, MegaMart, Home & Kitchen :  
                            Air Fryer, 1, 4.6, $199, Oil-free cooking air fryer with multiple presets, ValueMart, Home & Kitchen :  
                            Vacuum Cleaner, 1, 4.3, $159, High-suction vacuum cleaner with HEPA filter, Tech Haven, Home & Kitchen :  
                            Blender, 1, 4.5, $79, Multi-speed blender with stainless steel blades, Urban Bazaar, Home & Kitchen :  
                            Hair Dryer, 1, 4.2, $49, Fast-drying hair dryer with ionic technology, SmartChoice, Beauty & Personal Care :  
                            Shampoo, 1, 4.3, $19, Herbal shampoo with natural extracts, ProDeals, Beauty & Personal Care :  
                            Electric Toothbrush, 1, 4.5, $99, Rechargeable toothbrush with multiple modes, Trendy Hub, Beauty & Personal Care :  
                            Face Cream, 1, 4.6, $29, Hydrating face cream with SPF protection, EasyShop, Beauty & Personal Care :  
                            Dumbbells, 1, 4.7, $79, Adjustable dumbbells for strength training, FreshMart, Fitness & Exercise :  
                            Yoga Mat, 1, 4.5, $39, Non-slip yoga mat with carrying strap, Gadget World, Fitness & Exercise :  
                            Basketball, 1, 4.4, $29, Official size and weight basketball, Everyday Needs, Sports & Outdoors :  
                            Camping Tent, 1, 4.6, $199, Waterproof tent for 4 people, Smart Buys, Sports & Outdoors :  
                            Car Vacuum Cleaner, 1, 4.3, $59, Portable car vacuum cleaner with attachments, Quick Cart, Automotive :  
                            LED Headlights, 1, 4.7, $99, Super bright LED headlights for cars, Direct Deals, Automotive :  
                            Office Chair, 1, 4.5, $149, Ergonomic office chair with lumbar support, Global Traders, Office Supplies :  
                            Writing Desk, 1, 4.4, $199, Wooden writing desk with storage drawers, BestChoice, Office Supplies :  
                            Fiction Novel, 1, 4.6, $19, Best-selling fiction novel by top author, Speedy Mart, Books :  
                            Cookbook, 1, 4.3, $29, Cookbook with 100+ delicious recipes, Elite Emporium, Books :  
                            Acoustic Guitar, 1, 4.5, $199, Wooden acoustic guitar for beginners, Bargain Hub, Music & Instruments :  
                            Keyboard Piano, 1, 4.6, $249, 61-key digital piano with built-in speakers, Home Essentials, Music & Instruments :  
                            Dog Food, 1, 4.4, $39, Nutrient-rich dry dog food for all breeds, ABC Traders, Pet Supplies :  
                            Cat Litter, 1, 4.5, $19, Odor-control cat litter with clumping formula, XYZ Enterprises, Pet Supplies :  
                            Organic Coffee, 1, 4.6, $15, Freshly ground organic coffee beans, Global Mart, Grocery & Gourmet Food :  
                            Protein Powder, 1, 4.7, $49, Whey protein powder for muscle recovery, Sunrise Retail, Grocery & Gourmet Food :  
                            Baby Stroller, 1, 4.8, $299, Lightweight and foldable baby stroller, Elite Supplies, Baby Products :  
                            Diapers, 1, 4.5, $39, Ultra-absorbent baby diapers, Best Deals Inc., Baby Products :  
                            Silver Necklace, 1, 4.3, $99, Sterling silver necklace with pendant, SuperMart, Jewelry & Accessories :  
                            Luxury Watch, 1, 4.7, $499, Elegant stainless steel luxury watch, FastShop, Watches :  
                            Artificial Grass Mat, 1, 4.4, $79, Realistic artificial grass mat for outdoors, Prime Goods, Garden & Outdoor :  
                            Power Drill, 1, 4.6, $129, Cordless power drill with multiple speeds, QuickBuy, Tools & Home Improvement :  
                            Foam Roller, 1, 4.5, $29, High-density foam roller for muscle relief, MegaMart, Fitness & Exercise :  
                            Watercolor Paint Set, 1, 4.3, $39, Professional watercolor paint set, ValueMart, Arts & Crafts :  
                            Gaming Mouse, 1, 4.7, $79, High-precision gaming mouse with RGB lighting, Tech Haven, Video Games :  
                            Smart Light Bulbs, 1, 4.5, $49, WiFi-enabled smart light bulbs, Urban Bazaar, Smart Home Devices :  
                            Disposable Party Plates, 1, 4.3, $19, Biodegradable party plates, SmartChoice, Party Supplies :  
                            DSLR Camera, 1, 4.7, $699, High-resolution DSLR camera, ProDeals, Cameras & Photography :  
                            Vintage Collectible Coins, 1, 4.8, $199, Rare collectible coin set, Trendy Hub, Collectibles :  
                            Wine Subscription Box, 1, 4.6, $79, Monthly curated wine selection, EasyShop, Subscription Boxes :  
                            Electric Kettle, 1, 4.4, $39, Fast-boiling electric kettle, FreshMart, Home & Kitchen :  
                            Bluetooth Speaker, 1, 4.5, $99, Waterproof portable Bluetooth speaker, Gadget World, Electronics :
                            ";

    string product_string_2 = @"Car Vacuum Cleaner, 1, 4.3, $59, Portable car vacuum cleaner with attachments, Quick Cart, Automotive :  
                            LED Headlights, 1, 4.7, $99, Super bright LED headlights for cars, Direct Deals, Automotive :  
                            Office Chair, 1, 4.5, $149, Ergonomic office chair with lumbar support, Global Traders, Office Supplies :  
                            Writing Desk, 1, 4.4, $199, Wooden writing desk with storage drawers, BestChoice, Office Supplies :  
                            Fiction Novel, 1, 4.6, $19, Best-selling fiction novel by top author, Speedy Mart, Books :  
                            Cookbook, 1, 4.3, $29, Cookbook with 100+ delicious recipes, Elite Emporium, Books :  
                            Acoustic Guitar, 1, 4.5, $199, Wooden acoustic guitar for beginners, Bargain Hub, Music & Instruments :  
                            Keyboard Piano, 1, 4.6, $249, 61-key digital piano with built-in speakers, Home Essentials, Music & Instruments :  
                            Dog Food, 1, 4.4, $39, Nutrient-rich dry dog food for all breeds, ABC Traders, Pet Supplies :  
                            Cat Litter, 1, 4.5, $19, Odor-control cat litter with clumping formula, XYZ Enterprises, Pet Supplies :  
                            Organic Coffee, 1, 4.6, $15, Freshly ground organic coffee beans, Global Mart, Grocery & Gourmet Food :  
                            Protein Powder, 1, 4.7, $49, Whey protein powder for muscle recovery, Sunrise Retail, Grocery & Gourmet Food :  
                            Baby Stroller, 1, 4.8, $299, Lightweight and foldable baby stroller, Elite Supplies, Baby Products :  
                            Diapers, 1, 4.5, $39, Ultra-absorbent baby diapers, Best Deals Inc., Baby Products :  
                            Silver Necklace, 1, 4.3, $99, Sterling silver necklace with pendant, SuperMart, Jewelry & Accessories :  
                            Luxury Watch, 1, 4.7, $499, Elegant stainless steel luxury watch, FastShop, Watches :  
                            Artificial Grass Mat, 1, 4.4, $79, Realistic artificial grass mat for outdoors, Prime Goods, Garden & Outdoor :  
                            Power Drill, 1, 4.6, $129, Cordless power drill with multiple speeds, QuickBuy, Tools & Home Improvement :  
                            Foam Roller, 1, 4.5, $29, High-density foam roller for muscle relief, MegaMart, Fitness & Exercise :  
                            Watercolor Paint Set, 1, 4.3, $39, Professional watercolor paint set, ValueMart, Arts & Crafts :  
                            Gaming Mouse, 1, 4.7, $79, High-precision gaming mouse with RGB lighting, Tech Haven, Video Games :  
                            Smart Light Bulbs, 1, 4.5, $49, WiFi-enabled smart light bulbs, Urban Bazaar, Smart Home Devices :  
                            Disposable Party Plates, 1, 4.3, $19, Biodegradable party plates, SmartChoice, Party Supplies :  
                            DSLR Camera, 1, 4.7, $699, High-resolution DSLR camera, ProDeals, Cameras & Photography :  
                            Vintage Collectible Coins, 1, 4.8, $199, Rare collectible coin set, Trendy Hub, Collectibles :  
                            Wine Subscription Box, 1, 4.6, $79, Monthly curated wine selection, EasyShop, Subscription Boxes :  
                            Electric Kettle, 1, 4.4, $39, Fast-boiling electric kettle, FreshMart, Home & Kitchen :  
                            Bluetooth Speaker, 1, 4.5, $99, Waterproof portable Bluetooth speaker, Gadget World, Electronics :  
                            Standing Desk, 1, 4.6, $249, Height-adjustable standing desk, Everyday Needs, Office Supplies :  
                            Camping Stove, 1, 4.4, $89, Portable propane camping stove, Smart Buys, Sports & Outdoors :  
                            Wireless Keyboard, 1, 4.5, $79, Ergonomic wireless keyboard with backlight, Quick Cart, Office Supplies :  
                            Action Camera, 1, 4.7, $299, Waterproof action camera with 4K video, Direct Deals, Cameras & Photography :  
                            Board Game, 1, 4.6, $49, Fun and engaging board game for families, Global Traders, Toys & Games :  
                            Car Phone Mount, 1, 4.4, $29, Adjustable phone mount for cars, BestChoice, Automotive :  
                            Resistance Bands, 1, 4.5, $25, Set of resistance bands for workouts, Speedy Mart, Fitness & Exercise :  
                            Noise-Canceling Headphones, 1, 4.7, $299, Over-ear noise-canceling headphones, Elite Emporium, Electronics :  
                            Tactical Flashlight, 1, 4.6, $49, High-lumen tactical flashlight, Bargain Hub, Tools & Home Improvement :  
                            Smart Doorbell, 1, 4.5, $129, Video-enabled smart doorbell, Home Essentials, Smart Home Devices :  
                            Fishing Rod, 1, 4.4, $89, Lightweight and durable fishing rod, ABC Traders, Sports & Outdoors :  
                            Projector, 1, 4.6, $399, HD projector with WiFi support, XYZ Enterprises, Electronics :  
                            Baking Set, 1, 4.5, $79, Complete baking set with utensils, Global Mart, Home & Kitchen :  
                            Wireless Charging Pad, 1, 4.3, $49, Fast-charging wireless pad, Sunrise Retail, Electronics :  
                            Robot Vacuum, 1, 4.7, $299, Self-cleaning robot vacuum with mapping, Elite Supplies, Home & Kitchen :  
                            Smart Thermostat, 1, 4.5, $199, Energy-efficient smart thermostat, Best Deals Inc., Smart Home Devices :  
                            Luggage Set, 1, 4.6, $249, 3-piece lightweight luggage set, SuperMart, Travel Accessories :  
                            Garden Hose, 1, 4.4, $39, Expandable and durable garden hose, FastShop, Garden & Outdoor :  
                            Mechanical Keyboard, 1, 4.7, $129, RGB backlit mechanical keyboard, Prime Goods, Electronics :  
                            Travel Pillow, 1, 4.5, $29, Memory foam travel pillow, QuickBuy, Travel Accessories :  
                            Electric Bike, 1, 4.8, $1299, High-speed electric bike with long battery life, MegaMart, Sports & Outdoors  
                            ";

    string[] productInfo = product_string.Split(":");
    string[] productInfo2 = product_string_2.Split(":");

    string[] sellerNameArray = new string[productInfo.Length];

    List<Product> products = new List<Product>();
    List<string> failedProducts = new List<string>();

    #region Parse the string into Product lists
    try
    {
        foreach (string p1 in productInfo)
        {
            string[] p = p1.Split(",");
            
            if (p.Length == 7)
            {
                try
                {
                    Product product = new Product()
                    {
                        Name = p[0].Trim(),
                        DefaultQuantity = Convert.ToInt32(p[1].Trim()),
                        Rating = Convert.ToDecimal(p[2].Trim()),
                        Price = Convert.ToDecimal(p[3].Trim().Replace("$", "")),
                        Description = p[4].Trim(),
                        SellerCompanyName = p[5].Trim(),
                        ProductCategoryName = p[6].Trim(),
                    };

                    products.Add(product);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                failedProducts.Add(p1);
            }
        }
    }
    catch (Exception e)
    {
        Console.WriteLine("Failed to parse product string #1.");
    }

    try
    {
        foreach (string p1 in productInfo2)
        {
            string[] p = p1.Split(",");

            if (p.Length == 7)
            {
                try
                {
                    Product product = new Product()
                    {
                        Name = p[0].Trim(),
                        DefaultQuantity = Convert.ToInt32(p[1].Trim()),
                        Rating = Convert.ToDecimal(p[2].Trim()),
                        Price = Convert.ToDecimal(p[3].Trim().Replace("$", "")),
                        Description = p[4].Trim(),
                        SellerCompanyName = p[5].Trim(),
                        ProductCategoryName = p[6].Trim(),
                    };

                    products.Add(product);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                failedProducts.Add(p1);
            }
        }
    }
    catch (Exception e)
    {
        Console.WriteLine("Failed to parse product string #2.");
    }
    #endregion

    using (var db = new EcommerceContext())
    {
        try
        {
            foreach (Product p in products)
            {
                Seller seller = db.Sellers.Where(s => s.CompanyName == p.SellerCompanyName).Select(x => new Seller() { Id = x.Id }).FirstOrDefault();
                ProductCategory category = db.ProductCategories.Where(s => s.CategoryName == p.ProductCategoryName).Select(x => new ProductCategory() { Id = x.Id }).FirstOrDefault();

                p.SellerId = seller != null ? seller.Id : 0;
                p.ProductCategoryId = category != null ? category.Id : 0;
            }
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
///Main Execution Thread
//InsertFirstUser();
//PopulateProductCategories();
//PopulateSellers();
PopulateProducts();















#endregion








