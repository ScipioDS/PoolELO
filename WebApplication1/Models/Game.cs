using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Player1_id { get; set; }
        [Required]
        public int Player2_id { get; set; }
        [Required]
        public bool Winner {  get; set; }
        public string Player1_name { get; set; }
        public string Player2_name { get; set; }
        
    }
}