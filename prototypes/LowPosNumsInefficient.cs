
namespace Codility_Practice {

// A cumbersome, inefficient, but functional solution to the problem
// Find lowest positive non-occuring int in an array

public class Soolution {
    // Skipping the OOP approach for now, for simplicity
        // static int Main() {

        // int[] A = {1,3,6,4,1,2};

        // int soln = solution(A);

        // Console.WriteLine("Lowest Nonoccuring Posnum:");
        // Console.WriteLine(soln);
        


        // return 1;
        // }


        // Traverse the array, searching for a match to num
        static bool contains(int[] arr, int num) {
            foreach(int i in arr) {
                if (num == i) return true;
            }
            return false;
        }

        static int solution(int[] A) {

            int[] onceNums = new int[] {};
            
            // Fill onceNums with one occurance for each positive int in A
            foreach(int num in A) {
                // Check whether this is a new, unique, positive int
                
                // Skip negatives and 0
                if (num < 1) continue;

                // Check this is a new/unique num
                if (!contains(onceNums,num)) {
                    // Add this unique num into onceNums
                    Array.Resize(ref onceNums, onceNums.Length + 1);
                    onceNums[onceNums.Length-1] = num;
                }
                
                // Else, do nothing, skip duplicate num
            }

            // Handle empty case, where all inputs were negative
            if (onceNums.Length == 0) return 1;

            // Sort onceNums
            Array.Sort(onceNums);

            // Step through, find the first numerical gap
            int j = 0;
            do {
                // Increment to next array element each loop
                j++;

                // Check that nums are counting up sequentially, i.e. 1,2,3,...
                if (j == onceNums[j-1]) continue;

                // The sequence has broken
                return j;
            } while (j < onceNums.Length);

            // If exec reaches here, the list was unbroken and counted up nicely
            // First not-appearing value is therefore (length of list) + 1
            return onceNums.Length + 1;
        }
    }
}
