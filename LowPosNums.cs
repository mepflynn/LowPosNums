This implementation uses the built in List<>.Sort() method (which in this case runs quicksort), and generally aims to minimize overhead wherever possible.using System.Collections.Generic;
using System.Linq;

public class Solution {

    static void Main() {

        int[] A = new int[] {1,2,3};

        int soln = solution(ref A);

        return;
    }

    static int solution(ref int[] A) {
        // Empty input case
        if (A.Length == 0) return 1;

        // Add each (nonnegative, nonzero) num to a List
        List<int> sortedNums = new List<int>();

        foreach(int num in A) {
            if (num < 1) continue;

            sortedNums.Add(num);
        }

        // Sort (via quicksort or heapsort, ideally O(nlogn))
        sortedNums.Sort();

        if (sortedNums.Count == 0) return 1;

        // Obtain the lowest not-appearing num
        // Num will start at 1 within the loop due to the increment
        int missingNum = 0;
        int i = 0;
        while (i < sortedNums.Count) {
            missingNum++;
            // Check if we found a blank spot, num that's not in the list
            if (missingNum != sortedNums[i]) {
                return missingNum;
            }

            // Skip forward through repeated occurrences of the same number in a row
            // Skip forward once by default, because we're already ON a num we just checked above
            do {
                i++;
                // Handling for the case where the list counts up sequentially all the way
                // The missing num is the next int one higher from the final (.Last()) element
                if (i == sortedNums.Count) return sortedNums.Last() + 1;
            } while (missingNum == sortedNums[i]);
        }

        

        return -1;
    }

}
