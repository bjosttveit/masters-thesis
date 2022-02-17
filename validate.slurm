#!/bin/sh
#SBATCH --partition=GPUQ
#SBATCH --account=ie-idi
#SBATCH --time=1:00:00
#SBATCH --nodes=1
#SBATCH --ntasks-per-node=1
#SBATCH --gres=gpu:V100:1
#SBATCH --cpus-per-task=24
#SBATCH --mem=16G
#SBATCH --job-name="validate"
#SBATCH --output=val.out
#SBATCH --mail-user=bjornar.osttveit@ntnu.no
#SBATCH --mail-type=NONE

mkdir -p tests

cd ${SLURM_SUBMIT_DIR}/yolov5

module purge
module load Python/3.8.6-GCCcore-10.2.0

pip3 install -r requirements.txt --user -q

#X: 36 13h
#L: 74 8h
#M: 106 5h
#S: 162 3h
#N: 312 2h

python3 val.py --data ../dataset/data.yaml --batch-size 32 --weights ../models/Baseline-${1}.pt --project ../tests --name Baseline-${1} --exist-ok --task=test --save-txt --save-conf --img 4064 --single-cls
