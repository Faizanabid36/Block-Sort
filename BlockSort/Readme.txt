Block Merge Sort is a Hybrid Sort that has at least Two Merge Sorts and One Insertion Sort.
Ref. from Code: Buffer Means Number of Elements should be in Each Block. If Input Size of Array is 10, 4 Blocks will be created.1st three Blocks will Contain 3 Elements and Last Block will Contain 1. In Case of input 16, 4 blocks will be created and every Block will contain 4 elements.

Instructions to Run and Understand the Program Completely:
The User is Asked to Give the Size of Array and the elements of Array. The Input can only be Integer, not float, char, string or anything else.
User can Only See the Unsorted and Sorted Array on Console, but a lot is happening is behind the scenes. 
If User wants to see How the Array is Broken into Blocks, He Should Uncomment the Function “printBlocks()” on the Line Number 148.
The Blocks will then undergo Merge Sort and Elements of every Block will be sorted. In order to see the Blocks after applying merge sort in it, uncomment the function “printBlocksMergeSorted” on Line Number 155.
After applying merge Sort, Blocks will be merged and Insertion sort will be applied on new block. To See how it’s happening, uncomment the code from line Number 253 t	o 257.

Group Members:
Faizan Abid (17B-057-SE)
Hamzah Khalid (17B-027-SE)
Ibad Hashmi (17B-022-SE)