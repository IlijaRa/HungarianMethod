# Explanation of the method
The Hungarian method is a computational optimization technique that addresses the assignment problem in polynomial time and foreshadows following primal-dual alternatives. In 1955, Harold Kuhn used the term “Hungarian method” to honour two Hungarian mathematicians, Dénes Kőnig and Jenő Egerváry. Let’s go through the steps of the Hungarian method with the help of a solved example.

# Hungarian Method Steps
Check to see if the number of rows and columns are equal; if they are, the assignment problem is considered to be balanced. Then go to step 1. If it is not balanced, it should be balanced before the algorithm is applied.

<details>
  <summary>Step 1</summary>
  <p>In the given cost matrix, subtract the least cost element of each row from all the entries in that row. Make sure that each row has at least one zero.</p>
</details>
<details>
  <summary>Step 2</summary>
  <p>In the resultant cost matrix produced in step 1, subtract the least cost element in each column from all the components in that column, ensuring that each column contains at least one zero.</p>
</details>
<details>
  <summary>Step 3</summary>
  <p> Assign zeros
    *Analyse the rows one by one until you find a row with precisely one unmarked zero. Encircle this lonely unmarked zero and assign it a task. All other zeros in the  column of this circular zero should be crossed out because they will not be used in any future assignments. Continue in this manner until you’ve gone through all of the rows.
    *Examine the columns one by one until you find one with precisely one unmarked zero. Encircle this single unmarked zero and cross any other zero in its row to make an assignment to it. Continue until you’ve gone through all of the columns.
</p>
</details>
<details>
  <summary>Step 4</summary>
  <p>Perform the Optimal Test

    *The present assignment is optimal if each row and column has exactly one encircled zero.
    *The present assignment is not optimal if at least one row or column is missing an assignment (i.e., if at least one row or column is missing one encircled zero). Continue to step 5. Subtract the least cost element from all the entries in each column of the final cost matrix created in step 1 and ensure that each column has at least one zero.</p>
</details>
<details>
  <summary>Step 5</summary>
  <p>Draw the least number of straight lines to cover all of the zeros as follows:
    * Highlight the rows that aren’t assigned.
    * Label the columns with zeros in marked rows (if they haven’t already been marked).
    * Highlight the rows that have assignments in indicated columns (if they haven’t previously been marked).
    * Continue with (b) and (c) until no further marking is needed.
    * Simply draw the lines through all rows and columns that are not marked. If the number of these lines equals the order of the matrix, then the solution is optimal; otherwise, it is not.
  </p>
</details>
<details>
  <summary>Step 6</summary>
  <p>Find the lowest cost factor that is not covered by the straight lines. Subtract this least-cost component from all the uncovered elements and add it to all the elements that are at the intersection of these straight lines, but leave the rest of the elements alone.</p>
</details>
<details>
  <summary>Step 7</summary>
  <p>Continue with steps 1 – 6 until you’ve found the highest suitable assignment.</p>
</details>
