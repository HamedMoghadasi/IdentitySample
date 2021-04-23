namespace Authorization.Seeds
{
    public interface ISeed
    {
        double ExecutionOrder { get; }
        void Seed();
    }
}