# DP_Coloring

A dynamic programming solution to make colored tiles symmetric.

---

## Problem Description

The objective is to **symmetrize** a block of colored tiles by making the left side mirror the right side with the **minimum number of operations**.  
The operations and their costs are as follows:
- **Delete**: Cost = 1
- **Add**: Cost = 1
- **Replace**: Cost = 1

---

## Examples

### Example 1
Input: `1123`  
Symmetry point (`k`): `2`  
Result: `2323`  

---

### Example 2
Input: `11123`  
Symmetry point (`k`): `1`  
Result: `33`  

---

### Example 3
Input: `34123`  
Symmetry point (`k`): `3`  
Result: `123123`  

---

## Key Insights

To achieve the goal with **minimum operations**, we carefully evaluate the choices:
1. **Delete** items.
2. **Add** items.
3. **Replace** items.  

For instance:  
- Input: `12313`  
- Symmetry point (`k`): `2`  
- Result: `123`.  

Here, the optimal solution is to **delete** one item (`3` at the end), costing only `1`.  
Other solutions, such as replacing the first two items and deleting the last one, would incur a cost of `3`, which is not acceptable.

---

## Approach

We use **dynamic programming** to efficiently solve the problem. The DP algorithm evaluates all possible operations and selects the one with the **minimum cost** to achieve symmetry.

---

