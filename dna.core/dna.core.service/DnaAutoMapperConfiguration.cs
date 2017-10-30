using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.service
{
    public class DnaAutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.CreateMissingTypeMaps = true;
                x.AddProfile<DnaDomainToViewModelMapping>();
            });
        }
    }
}
