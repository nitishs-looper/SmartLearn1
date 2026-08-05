namespace SmartLearn1
{
    // Interface used for search-capable entities (Course, Student)
    public interface ISearchable
    {
        bool MatchesSearch(string keyword);
        string GetSearchSummary();
    }
}
