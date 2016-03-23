public abstract class ASingleton<TypeThatWillBeSingletoned> where TypeThatWillBeSingletoned : class, new()
{
    private static TypeThatWillBeSingletoned instance = null;

    private ASingleton() { }

    public static TypeThatWillBeSingletoned Instance
    {
        get
        {
            if (instance == null)
                instance = new TypeThatWillBeSingletoned();

            return instance;
        }
    }
}