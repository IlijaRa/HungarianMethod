# -*- coding: utf-8 -*-
"""
@author: Savic Nikola, Rakocevic Ilija
"""
import numpy as np
import sys
np.set_printoptions(suppress = True, precision = 3)

#C = np.array([6, 14, 13, 0, 0])
#A = np.array([[0.5, 2, 1, 1, 0],[1, 2, 4, 0, 1]])
#b = np.array([24, 60])

C = np.array([2, 1.5, 0, 0])
A = np.array([[6, 3, 1, 0], [75, 100, 0, 1]])
b = np.array([1200, 25000])
base = [2,3]



n = A.shape[1]
m = A.shape[0]

#postavljanje tabele
matrix = np.zeros((m + 1, m + 3))
matrix[1:, 0] = base

B = A[:, base]

try:
    Binv = np.linalg.inv(B)
except:
    print("Invalid Base choice")
    sys.exit()
    
matrix[1:m+1, 1:m+1] = Binv

matrix[1:, -2] = b

# W = Cb * Binv
matrix[0, 1:-2] = np.matmul(C[base[0]: base[1]+1], Binv)

#Cb * (Binv  * b)
b_ = np.matmul(Binv, b)

if min(b_) < 0:
    print("Invalid Base choice")
    sys.exit()

matrix[0, -2] = np.matmul(C[base[0]: base[1]+1], b_)

def simplexStep(matrix, base, n, isMax):

    #racunanje nove bazne promenljive
    indexCheck = np.arange(n)
    indexCheck = np.delete(indexCheck, base)
    Cx = np.delete(C, base)
    Ax = np.delete(A, base, axis=1)
    x = []
    
    for i in np.arange(Cx.shape[0]):
        x.append(np.matmul(matrix[0, 1:-2], Ax[:, i]) - Cx[i])
    
        
    minEl = min(x)
    
    if minEl > 0 & isMax:
        print("Optimalna vrednost je " + str(matrix[0, -2]))
        i = 1
        for num in matrix[1:, 0]:
            print("Za promenljivu " + str(int(num + 1)) + " optimalna vrednost je " + str(matrix[i, -2]))
            i += 1
        print("Ukoliko postoje druge promenljive, njihova optimalna vrednost je 0")
        return
 
    
    minElIndex = x.index(minEl)
    newBaseIndex = indexCheck[minElIndex]
    
    
    #postavljanje pivot kolone
    matrix[0, -1] = minEl
    matrix[1:, -1] = np.matmul(matrix[1:, 1:-2], A[:, newBaseIndex])
    

    #racunanje pivot reda
    col1 = matrix[1:, -2]
    col2 = matrix[1:, -1]
    res = np.zeros(col1.shape[0])
    np.divide(col1, col2, res)
    
    minEl = min(res)
    pivotRowIndex = 0
    it = 0
    for num in res:
        if num == minEl:
            pivotRowIndex = it
        else:
            it += 1
    
    #mora +1 jer je prvi red namenjen za W i Cb * b(nadvuceno)
    pivotRowIndex += 1
    pivotElement = matrix[pivotRowIndex, -1]
    
    #sledeca tabela
    outMatrix = np.zeros((m + 1, m + 3))
    
    outMatrix[1:, 0] = matrix[1:, 0]
    outMatrix[pivotRowIndex, 0] = newBaseIndex
    
    
    for i in np.arange(outMatrix.shape[0]):
        for j in np.arange(start=1, stop=outMatrix.shape[1]-1, step=1):
            outMatrix[i, j] = matrix[i, j] - (matrix[pivotRowIndex, j] * matrix[i, -1] / pivotElement)
            
    
    for j in np.arange(start=1, stop=outMatrix.shape[1]-1, step=1):
        outMatrix[pivotRowIndex, j] = matrix[pivotRowIndex, j] / pivotElement
    
    base = outMatrix[1:, 0]
    
    newBase = base.tolist()
    newBase = [round(el) for el in newBase]
    
    simplexStep(outMatrix, newBase, n, isMax)
        

simplexStep(matrix, base, n, True)