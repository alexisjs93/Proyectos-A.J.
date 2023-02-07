## 26.09.2021
## Propio

import os
from PIL import Image
import numpy as np


## Define flag size boundaries
finalX = 960
finalY = 640

## Initialize array
array = np.zeros((finalX,finalY,4))
directory = 'flags/'


## For each flag
for filename in os.listdir(directory):
    ## If a PNG image
    if filename.endswith(".png"):
        image = Image.open(directory + filename)
        ## Convert to RGBA 
        image = image.convert('RGBA')
        width, height = image.size
        
        ## For each pixel
        for x in range(width):
            for y in range(height):
                ## Update array values with the median RGBA values
                r, g, b, a = image.getpixel((x, y))
                array[x,y,0] = (array[x,y,0] + r) / 2
                array[x,y,1] = (array[x,y,1] + g) / 2
                array[x,y,2] = (array[x,y,2] + b) / 2
                if array[x,y,3] != 255 and a != 0:
                    array[x,y,3] = 255
                
                ## DISPLAY INFO
                ## print(str(x) + ', ' + str(y) + ' :: ' + str(r) + ',' + str(g) + ',' + str(b) + ',' + str(a))
                
                
      

    
        



## Create a new empty image
img = Image.new('RGBA', (finalX, finalY), (0,0,0,0))


for x in range (finalX):
    for y in range (finalY):
            ## Generate the median flag by inserting the matrix values into the empty image
            ## print(str(x) + ', ' + str(y) + ' :: ' + str(int(array[x,y,0])) + ' - ' + str(int(array[x,y,1])) + ' - ' + str(int(array[x,y,2])) + ' - ' + str(int(array[x,y,3])))
            img.putpixel((x, y), (int(array[x,y,0]), int(array[x,y,1]), int(array[x,y,2]), int(array[x,y,3])))

## Save result
img.save('resultmed.png')
