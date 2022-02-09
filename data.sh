#!/bin/sh

# Uncomment when running on IDUN
module purge
module load Python/3.8.6-GCCcore-10.2.0

kaggle datasets download -d bjosttveit/yolo-sheep-colored-and-occluded
rm -rf dataset/images dataset/labels
unzip -o yolo-sheep-colored-and-occluded.zip -d dataset
rm yolo-sheep-colored-and-occluded.zip
