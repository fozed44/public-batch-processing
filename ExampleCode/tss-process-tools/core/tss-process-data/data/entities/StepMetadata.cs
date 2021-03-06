using AutoMapper;
using Tss.Process.Contracts.Types.Dto;

namespace Tss.Process.Data.Entities {
    public class StepMetadata {
        public long   StepMetadataId    { get; set; }
        public long   ProcessMetadataId { get; set; }
        public string Description       { get; set; }
        public int    Ordinal           { get; set; }
        public string InputTypename     { get; set; }
        public string OutputTypename    { get; set; }

        public ProcessMetadata ProcessMetadata { get; set; }

        private static Mapper _mapper 
            = new Mapper(
                new MapperConfiguration(c => c.CreateMap<StepMetadata, StepMetadataDto>())
            );

        public StepMetadataDto CreateDto()
            => _mapper.Map<StepMetadataDto>(this);
    }
}