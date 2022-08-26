
public class Sooolution {

    static bool checkForBlank(int i, int[] newBlanks) {
        // If a new blank is to be added here, return true
        foreach (int b in newBlanks) if (b == i) {
            return true;
        }
        return false;
    }

    static int[,] resizeArray(ref int[,] numSpaces, int[] newBlanks) {
        
        // Increase size by number of new blank spots
        int[,] newNumSpaces = new int[numSpaces.Length+newBlanks.Length,2];

        // Copy in values, skipping and adding a blank val when one is encountered
        // j is to save the place in the old array, numSpaces
        for (int i = 0, j = 0; i < newNumSpaces.Length; i++, j++) {
            if (checkForBlank(i,newBlanks)) {
                // Not using an entry this time, don't let j increment
                j--;
                // This row is now blank, to be assigned at a higher level
                continue;
            }

            // Not a blank spot. Insert the corresponding array entry
            newNumSpaces[i,0] = numSpaces[j,0];
            newNumSpaces[i,1] = numSpaces[j,1];
        }

        return newNumSpaces;
    }

 
    static void splitSpace(ref int[,] numSpaces, int spaceIndex, int newNum) {

    }

    static int solution(ref int[] A) {
        // Empty input case
        if (A.Length == 0) return 1;

        // Multidimensional (Nx2) array to represent the numSpaces
        // These are empty gaps of non-occurring numbers between nums that have appeared
        // The actual number boundaries are EXCLUSIVE of empty numberSpace
        // Numbers appearing on the bounds *have* been found in the array

        // Start with 1,1 as placeholder
        int[,] numSpaces = new int[1,2];

        // Manually populate the first pair

        int index = 0;

        // The first value in first
        while (index < A.Length) {
            // Skip all zero and negative nums
            if (A[index] < 1) {
                index++;
                continue;
            }

            numSpaces[0,0] = A[index]; 
            break;
        }

        // Find an appropriate second value, place it correctly against the first
        while (index < A.Length) { // Check against length to avoid OOB. //TODO:: weird edge case of all ones break my code?
            // Skip all zero and negative nums
            if (A[index] < 1) {
                index++;
                continue;
            }

            if (A[index] == numSpaces[0,0]) { // Number matches the one already present; skip it.
                index++;
                continue;
            } else if (A[index] < numSpaces[0,0]) { // Number is smaller, put it on left side of the pair
                int temp = numSpaces[0,0];
                numSpaces[0,0] = A[index];
                numSpaces[0,1] = temp;
            } else { // Number is larger, put it right side of the pair
                numSpaces[0,1] = A[index];
            }
        }

        // Starts on index to skip nums already looked at above
        for(int i = index; i < A.Length; i++) {
            // Skip all zero and negative nums
            if (A[i] < 1) continue; 

            bool reloop = false;

            // Fit this num into the numSpaces
            for(int j = 0; j < numSpaces.Length; j++) {
                if (A[i] > numSpaces[j,0] && A[i] < numSpaces[j,1]) { // Num fits in the middle of this space
                    // Break up the space into two, adding a new one to the array
                    int[] arr = {j};
                    numSpaces = resizeArray(ref numSpaces,arr);

                    // New space bounds are 

                    // Reloop for next element of A[]
                    reloop = true;
                    break;
                } else if (A[i] > numSpaces[j,1]) {    // Num is somewhere right of (bigger than) this space
                    // Continue on up the list
                    continue;
                } else if (A[i] < numSpaces[j,0]) {    // Num is somewhere left of (smaller than) this space
                    // Num is between the previous space and this one
                    // Create two new spaces to insert, for the two new empty regions

                    // Reloop for next element of A[]
                    reloop = true;
                    break;
                } else if (A[i] == numSpaces[j,0] || A[i] == numSpaces[j,1]){ // Num is one of the space bounds.
                    // Do nothing
                    continue;
                }

            }

            if (reloop) continue;

            // Execution reaches here, implies all spaces were searched, no spot found. The num is to the right (higher than the rest)
            // Push new space onto the end with bounds: {numSpaces[Length-1][1], A[i]}
        }

        return -1;
    }

}