using EfCoreTutorial.Database;
using EfCoreTutorial.Entity.SchoolModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

Console.WriteLine("Hello, Wazi!");

//#########################--Function Definitions--#########################//

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

        foreach(Grade grade in gradeList)
        {
            Console.WriteLine($"Grade ID:{grade.GradeId}, Grade: {grade.GradeName}");
        }

    }
}

void UpdateMultipleEntries()
{
    using(var db = new SchoolContext()) 
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
    using(var db =new SchoolContext())
    {
        string lastName = "3";
        
        List<Student> students = db.Students.FromSql($"select * from Students where LastName = {lastName}").ToList();

        if(students.Count == 0)
        {
            Console.WriteLine("No student found");
        }
        else
        {
            Console.WriteLine($"Student Name: { students[0].FirstName + " " + students[0].LastName }");
        }
    }
}

void ExecuteStoredProcedures()
{
    try
    {
        //Execute Stored Procedures
        using(var db = new SchoolContext())
        {
            DateTime date = DateTime.Now;
            var rowsAffected = db.Database.ExecuteSql($"UpdateGradesInBulk {date}");
        }
    }
    catch(Exception ex)
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


































