namespace FlowGrep.Options
{
    public enum RenderSource { Facts, Legacy }

    public sealed class RenderOptions
    {
        public RenderSource Source { get; set; } = RenderSource.Facts;
        public string OutputPath { get; set; } = "out";
    }
}
