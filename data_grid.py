import os

datasetLabelPath = "dataset/labels"
datasetImagePath = "dataset/images"
gridLabelPath = "grid_dataset/labels"
gridImagePath = "grid_dataset/images"

if not os.path.exists(gridLabelPath):
    os.makedirs(gridLabelPath)

if not os.path.exists(gridImagePath):
    os.makedirs(gridImagePath)

datasetLabels = list(filter(lambda x: x.endswith("txt"), os.listdir(datasetLabelPath)))

xFactor = 8
yFactor = 6
padding = 64

from PIL import Image
import numpy as np

count = len(datasetLabels)

for index, label in enumerate(datasetLabels):
    print(f"Image: {index + 1} / {count}", end="\r")

    fullLabelPath = f"{datasetLabelPath}/{label}"
    fullImagePath = f"{datasetImagePath}/{label[:-4]}.jpg"

    imageLabels = []
    with open(fullLabelPath) as l:
        while (line := l.readline()) != "":
            imageLabels.append(line)
    
    image = Image.open(fullImagePath)
    imageData = np.asarray(image)

    height, width, _ = imageData.shape

    for x in range(xFactor):
        for y in range(yFactor):
            xMin = max(0, x/xFactor - padding/width)
            xMax = min(1, (x+1)/xFactor + padding/width)
            yMin = max(0, y/yFactor - padding/height)
            yMax = min(1, (y+1)/yFactor + padding/height)

            gridCellLabels = []

            for line in imageLabels:
                c, xc, yc, w, h = line.split()
                xc = float(xc)
                yc = float(yc)
                w = float(w)
                h = float(h)

                # Adjust bounding boxes

                ixMin = max(0, (xc - w/2 - xMin) / (xMax - xMin))
                ixMax = min(1, (xc + w/2 - xMin) / (xMax - xMin))
                iyMin = max(0, (yc - h/2 - yMin) / (yMax - yMin))
                iyMax = min(1, (yc + h/2 - yMin) / (yMax - yMin))

                ixc = (ixMin + ixMax) / 2
                iyc = (iyMin + iyMax) / 2
                iw = ixMax - ixMin
                ih = iyMax - iyMin

                # Check if boxes intersect at least 2% of width and height
                if iw > 2e-2 and ih > 2e-2:
                    gridCellLabels.append(f"{c} {ixc:.6f} {iyc:.6f} {iw:.6f} {ih:.6f}\n")
            

            if len(gridCellLabels) > 0:
                # Write labels to file
                with open(f"{gridLabelPath}/{label[:-4]}-{x}-{y}.txt", "w+") as l:
                    l.writelines(gridCellLabels)
                
                # Write image to file
                gridCellXMin = int(xMin * width)
                gridCellXMax = int(xMax * width)
                gridCellYMin = int(yMin * height)
                gridCellYMax = int(yMax * height)

                gridCellImageData = imageData[gridCellYMin : gridCellYMax, gridCellXMin : gridCellXMax]
                gridCellImage = Image.fromarray(gridCellImageData)
                gridCellImage.save(f"{gridImagePath}/{label[:-4]}-{x}-{y}.jpg")
print()
