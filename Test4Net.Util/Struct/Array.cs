namespace Test4Net.Util.Struct;

public static class ArrayUtil
{
    /// <summary>
    /// Adds extra element to the beginning of the array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="array"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    public static T[] AppendFirst<T>(this T[] array, T item) => 
        new List<T>(array).Prepend(item).ToArray();
}