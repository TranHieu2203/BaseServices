using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Custommer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        public string FirstName { set; get; }

        public string LastName { set; get; }

        public string Age { set; get; }

        public string Address { set; get; }

        public string Gender { set; get; }

    }
}