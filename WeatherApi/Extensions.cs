public static class Extensions
{
    public static bool IsNull<T>(this T instance) => instance == null;
    public static bool IsNullOrEmpty(this string instance) => string.IsNullOrEmpty(instance);
    public static bool IsNullOrWhiteSpace(this string instance) => string.IsNullOrWhiteSpace(instance);
    public static void MustNotBeNull<T>(this T instance)
    {
        if (instance.IsNull())
        {
            throw new System.NullReferenceException();
        }
    }
    public static void MustNotBeNullOrEmpty(this string instance)
    {
        if (instance.IsNullOrEmpty())
        {
            throw new System.ArgumentException();
        }
    }
    public static void MustNotBeNullOrWhiteSpace(this string instance)
    {
        if (instance.IsNullOrEmpty())
        {
            throw new System.ArgumentException();
        }
    }
}