using Newtonsoft.Json;

namespace ExpertsBlog.Entities
{
    /// <summary>
    /// Adresse d'un lieu dont on parle dans un billet
    /// </summary>
    public class Address : EntityBase
    {
        /// <summary>
        /// Nom de la rue et numéro du lieu-dit
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Code postal
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// Ville
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Numéro de téléphone
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Billet associé à cette adresse
        /// </summary>
        [JsonIgnore]
        public BlogPost BlogPost { get; set; }

        /// <summary>
        /// Id du billet, clé étrangère
        /// </summary>
        public int BlogPostId { get; set; }


        /// <summary>
        /// Latitude
        /// </summary>
        public double Latitude { get; set; }


        /// <summary>
        /// Longitude
        /// </summary>
        public double Longitude { get; set; }
    }
}