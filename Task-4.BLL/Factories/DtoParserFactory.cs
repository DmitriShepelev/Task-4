﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_4.BLL.Abstractions;
using Task_4.BLL.Abstractions.Factories;
using Task_4.BLL.DTOEntityParsers;

namespace Task_4.BLL.Factories
{
   public class DtoParserFactory : IDtoParserFactory<DataSourceDto>
    {
        public IDtoParser<DataSourceDto> CreateInstance()
        {
            return new DtoParser();
        }
    }
}