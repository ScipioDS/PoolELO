using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class PoolPlayer : IComparable<PoolPlayer>
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(18, 99,ErrorMessage ="Age must be between 18 and 99")]
        public int Age { get; set; }
        public int Elo {  get; set; }
        public string Img { get; set; }
        public string Description { get; set; }

        public int CompareTo(PoolPlayer other)
        {
            return other.Elo.CompareTo(Elo);
        }

        public void updatePlayerELO(bool winner, int otherELO)
        {
            //Expected score
            var E_B = 1 / (1 + Math.Pow(10, (otherELO - Elo) / 400.0));

            if (winner)
            {
                this.Elo = (int)Math.Round(Elo + 16 * (1 - E_B));
            }
            else
            {
                this.Elo = Elo + (Elo - (int)Math.Round(Elo + 16 * (1 - E_B)));
                /*
                var newELO = (int)Math.Round(playerELO + K * (0 - E_B));
                if (newELO == playerELO)
                {
                    return newELO-1;
                }
                else
                {
                    return newELO;
                }
                */
            }
        }
    }
}