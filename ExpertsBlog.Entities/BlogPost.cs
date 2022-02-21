using System;
using System.Collections.Generic;

namespace ExpertsBlog.Entities
{
    /// <summary>
    /// Billet de blog 
    /// </summary>
    /// <remarks>
    /// Hérite de EntityBase
    /// </remarks>
    public class BlogPost : EntityBase
    {
        /// <summary>
        /// Image pour représenter le billet
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Auteur du billet de blog
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Titre du billet de blog
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Contenu du billet de blog
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Catégorie du billet de blog
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// Id de la catégorie du billet de blog
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Liste des mots-clé
        /// </summary>
        public HashSet<Tag> Tags { get; } = new HashSet<Tag>();

        /// <summary>
        /// Liste des adresses du lieu
        /// </summary>
        public HashSet<Address> Addresses { get; set; }
    }
}