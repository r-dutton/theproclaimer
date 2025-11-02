using GraphKit.Facts;
using GraphKit.Analysis.Passes;

namespace GraphKit.Outputs.Facts
{
    public static class FactsPipeline
    {
        public static FactBag Finalize(FactWriter facts)
        {
            var bag = facts.ToBag();
            bag = DeduplicatePass.Run(bag);
            bag = SanityPass.Run(bag);
            bag = HttpLinkerPass.Run(bag);
            bag = MessageLinkerPass.Run(bag);
            bag = DeduplicatePass.Run(bag); // once more after linkers
            return bag;
        }
    }
}
