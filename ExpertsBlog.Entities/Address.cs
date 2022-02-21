namespace ExpertsBlog.Entities
{
    public class Address : EntityBase
    {
        public string Street { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public BlogPost BlogPost { get; set; }
        public int BlogPostId { get; set; }
    }
}