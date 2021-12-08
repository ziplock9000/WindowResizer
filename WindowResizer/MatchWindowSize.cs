namespace WindowResizer
{
    public enum MatchOrder
    {
        FullMatch = 1,
        PrefixMatch = 2,
        SuffixMatch = 3,
        WildcardMatch = 4,
    }

    public class MatchWindowSize
    {
        public WindowSize? FullMatch { get; set; }

        public WindowSize? PrefixMatch { get; set; }

        public WindowSize? SuffixMatch { get; set; }

        public WindowSize? WildcardMatch { get; set; }

        public bool NoMatch =>
            FullMatch == null
            && PrefixMatch == null
            && SuffixMatch == null
            && WildcardMatch == null;
    }
}
