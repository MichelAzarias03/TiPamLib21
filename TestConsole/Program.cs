using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiPamLib21;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Sql sql = new Sql(Sql.ChoiceOptions.MySQL, "host=localhost;user id=root;password=;database=TP_ADO_BD");
            Sql sql = new Sql(Sql.ChoiceOptions.SqlServer, @"data source=localhost\sqlexpress;initial catalog=TP_ADO_BD;integrated security=true");
            sql.Execute
            (
                "INSERT INTO Student (Matricule,FirstName,LastName) VALUES(@matricule,@firstName,@lastName)",
                new Sql.Paramater[]
                {
                    new Sql.Paramater("@matricule", "E789", Sql.DbType.Varchar),
                    new Sql.Paramater("@firstName", "Il est", Sql.DbType.Varchar),
                    new Sql.Paramater("@lastName", "L'heure", Sql.DbType.Varchar),
                }
            );
            Console.WriteLine("Save done !");
            Console.ReadKey();
        }
    }
}
