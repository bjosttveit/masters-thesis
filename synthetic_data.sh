#!/bin/sh

# Uncomment when running on IDUN
module purge
module load Python/3.8.6-GCCcore-10.2.0

kaggle datasets download -d bjosttveit/yolo-sheep-synthetic
rm -rf grid_dataset/synthetic
unzip -o yolo-sheep-synthetic.zip -d grid_dataset
rm yolo-sheep-synthetic.zip
