public class Program
{
    public static void Main(string[] args)
    {
        int[] numbers = Enumerable.Range(1,10).ToArray();
        int target = 7;
        int indexOftarget = numbers.IndexOfTarget(target);
        Console.WriteLine($"index of {target} is {indexOftarget}");
    }

}
public static class ArrayExtensions
{
    public static int IndexOfTarget(this int[] array, in int target)
    {
        int index = -1;
        if (array.Length == 0) return index;

        int left = 0, right = array.Length - 1;
        while(left <= right)
        {
            int middle = (left + right) / 2;

            if(target == array[middle])
            {
                index = middle;
                return index;
            }

            if(target > array[middle])
            {
                left = middle + 1;
            }
            else
            {
                right = middle - 1;
            }
        }
        return index;
    }
}




