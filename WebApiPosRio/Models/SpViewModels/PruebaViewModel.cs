using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApiPosRio.Models.DB;

namespace WebApiPosRio.Models.SpViewModels
{
    public class PruebaViewModel
    {
        #region Properties  

        /// <summary>  
        /// Gets or sets Prueba ID property.  
        /// </summary>  
        [Required]
        [Display(Name = "numero")]
        public int numero { get; set; }

        /// <summary>  
        /// Gets or sets to products list whose price is greater than equal to 1000 property.  
        /// </summary>  
        [Required]
        [Display(Name = "texto")]
        public string texto { get; set; }

        /// <summary>  
        /// Gets or sets to products list whose price is greater than equal to 1000 property.  
        /// </summary>  
        [Display(Name = "Prueba with List")]
        public List<SpContext> listaPrueba { get; set; }

        #endregion
    }
}
