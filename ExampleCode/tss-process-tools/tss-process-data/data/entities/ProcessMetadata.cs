using System;
using System.Collections.Generic;
using AutoMapper;
using Tss.Process.Contracts.Types.Dto;

namespace Tss.Process.Data.Entities {
    public class ProcessMetadata {
        public long     ProcessMetadataId     { get; set; }
        public long     StepServiceInfoId     { get; set; }
        public string   Name                  { get; set; }
        public string   Description           { get; set; }
        public bool     AllowMultiple         { get; set; }
        public bool     AllowMultiplePerCycle { get; set; }
        public DateTime Created               { get; set; }
        public string   Version               { get; set; } 

        public List<StepMetadata> Steps           { get; set; }
        public StepServiceInfo    StepServiceInfo { get; set; }

        private static Mapper _mapper 
            = new Mapper(
                new MapperConfiguration(c => c.CreateMap<ProcessMetadata, ProcessMetadataDto>())
            );

        public ProcessMetadataDto CreateDto()
            => _mapper.Map<ProcessMetadataDto>(this);
    }
}