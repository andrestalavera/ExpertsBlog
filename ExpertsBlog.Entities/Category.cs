using System.Collections.Generic;

namespace ExpertsBlog.Entities
{
    /// <summary>
    /// Catégorie
    /// </summary>
    public class Category : EntityBase
    {
        /// <summary>
        /// Nom de la catégorie
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Billets de blog 
        /// </summary>
        public HashSet<BlogPost> BlogPosts { get; set; }
    }
}