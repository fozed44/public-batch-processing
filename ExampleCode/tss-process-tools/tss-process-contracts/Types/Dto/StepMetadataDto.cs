namespace TssProcessContracts.Types.Dto {
    public class StepMetadataDto {
        public long   StepMetadataId    { get; set; }
        public long   ProcessMetadataId { get; set; }
        public string Description       { get; set; }
        public int    Ordinal           { get; set; }
        public string InputTypename     { get; set; }
        public string OutputTypename    { get; set; }
    }
}