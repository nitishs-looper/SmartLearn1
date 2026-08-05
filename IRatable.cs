namespace SmartLearn1
{
    // Interface to allow entities (like Course) to be rated by students
    public interface IRatable
    {
        void AddRating(int stars, string review);
        double GetAverageRating();
        int GetTotalRatings();
    }
}
