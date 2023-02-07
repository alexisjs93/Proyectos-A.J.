## 12.10.2021
## Propio
## https://en.wikipedia.org/wiki/Sierpi%C5%84ski_triangle

import numpy as np
import math
import random



## Functions for the distance between two points and for the midpoint between two points
def distance(pt1, pt2):
    dist = math.sqrt((pt1[0] - pt2[0])**2 + (pt1[1] - pt2[1])**2)
    return dist
    
def midpoint(pt1, pt2):
    mid = [round((pt1[0]+pt2[0])/2), round((pt1[1]+pt2[1])/2)]
    return mid



## Declare variables: RGB values of the output, the number of iterations, the canvas X size
R = 0
G = 100
B = 100
numOfIterations = 1500000
sizeX = 5000 ## WIDTH

## Calculate the canvas Y size
sizeY = round(math.sqrt(3) / 2 * (sizeX+2))  ## HEIGHT
defaultValue = 0

## Vertices array
vertices = []
## Append the 3 vertices
vertices.append([0, sizeY-1])
vertices.append([sizeX-1, sizeY-1])
vertices.append([round((sizeX-1) / 2), 0])
currentPoint = vertices[-1]

## Initialize board
board = np.full((sizeX, sizeY), defaultValue)



## Add the vertices to the board array (for later printing)    
for vertice in vertices:
    board[vertice[0], vertice[1]] = 1
    
## Display board info    
print(board)


## For the amount of iterations:
for i in range(numOfIterations):
    ## Select a random vertex
    vertexChoice = random.choice(vertices)
    ## The current point becomes the midpoint between the current point and the selected Vertex
    currentPoint = midpoint(currentPoint, vertexChoice)
    ## Include the current point in the board array
    board[currentPoint[0], currentPoint[1]] = 1



## Import PIL Image
from PIL import Image
## Increase max image pixels
Image.MAX_IMAGE_PIXELS = 8281351135
## Initialize black image
img = Image.new('RGB', (sizeX , sizeY), color = 'black')
## For each value in the board
for i in range(len(board)):
    for j in range(len(board[0])):
        if board[i][j] == 1:
            ## If the array position (i,j) has been visited then color it with the RGB values
            img.putpixel( (i, j), (R,G,B) )





## Scale up the image by sizeMult factor
sizeMult = 10
img = img.resize((len(board) * sizeMult , len(board[0])  * sizeMult),resample=Image.NEAREST)

## Save Image
img.save(str(sizeX)+"_"+str(sizeY)+"_"+str(numOfIterations)+"_("+str(R)+","+str(G)+","+str(B)+")"+'.png')





