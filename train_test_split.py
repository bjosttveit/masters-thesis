import sys
sys.path.insert(0, './yolov5')

from utils.datasets import *

autosplit('dataset/images', (0.7, 0.15, 0.15), seed=8030)
