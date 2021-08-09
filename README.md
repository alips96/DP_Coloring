# DP_Coloring
A dynamic programming solution to make colored tiles symmetric.

The goal is to symmetry the left of the block with the right side with the minimum operations.
The costs are described as following: delete: 1, add: 1, replace: 1
For example, if the block is like this: 1123 the result should be 2323 with k = 2; k is the index of the point the block should be symmetries from.
Other examples: 11123, k = 1; result = 33; 34123, k = 3 result: 123123
As mentioned before, we should reach the result with minimum operations possible. For example, if the block is 12313 and k = 2, the operations used should be only 1 delete. Because if we take 2 from the block, the block would be symmetrized. Other solutions like replacing the first two items and deleting the last one would have 3, which is not acceptable.
We use dynamic programming to tackle this problem.
