using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Model
{
	public class ImageDataModel
	{
        // Have one column that will be the key for the Image table
        [Key]
        public string Address { get; set; }
    }
}

