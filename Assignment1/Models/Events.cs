//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Assignment1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Events
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a titile")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter a date time")]
        [DataType(DataType.DateTime)]
        public System.DateTime Start { get; set; }
        public string CustomerEmail { get; set; }
    }
}
