﻿using ConexaoCaninaApp.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.Interfaces
{
	public interface IAlbumService
	{
		Task CriarAlbum(AlbumDto albumDto);
	}
}