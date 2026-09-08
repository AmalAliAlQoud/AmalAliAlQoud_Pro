namespace Bootcamp1_MVC_EF.Models
{
    public class Category
    {

        public int Id { get; set; }
        public string Name { get; set; }
       
        public string? ImageUrl { get; set; }
        public int ProductsCount {  get; set; }

        public Category(int id, string name,string imageUrl,int productCount)
        {
            Id= id;
            Name = name;
            ImageUrl = imageUrl;
            ProductsCount = productCount;
        }
    }
}
