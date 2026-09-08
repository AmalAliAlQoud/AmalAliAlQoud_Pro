namespace Bootcamp1_MVC_EF.Models
{
    public class Customers
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public string Mail { get; set; }

        public Customers()
        {
        }

        public Customers(int id,string name,int age,string mail)
        {
            Id = id;
            Name = name;
            Age = age;
            Mail = mail;
        }

    }
}
