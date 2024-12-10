﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.ViewModel
{
	public class CaoViewModel 
	{
		public Guid DogId { get; set; }
		public string Name { get; set; }
		public string Breed { get; set; }
		public int Age { get; set; }
		public string Description { get; set; }
		public string Gender { get; set; }
		public string Size { get; set; }
		public string UniqueCharacteristics { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string CaminhoFoto { get; set; }	
		public List<PhotoViewModel> Photos { get; set; }
	}
}
