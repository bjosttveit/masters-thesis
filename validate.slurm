#!/bin/sh
#SBATCH --partition=GPUQ
#SBATCH --account=ie-idi
#SBATCH --time=2:00:00
#SBATCH --nodes=1
#SBATCH --ntasks-per-node=1
#SBATCH --gres=gpu:V10032:1
#SBATCH --cpus-per-task=24
#SBATCH --mem=32G
#SBATCH --job-name="validate"
#SBATCH --output=val.out
#SBATCH --mail-user=bjornar.osttveit@ntnu.no
#SBATCH --mail-type=NONE

mkdir -p tests

cd ${SLURM_SUBMIT_DIR}/yolov5

module purge
module load Python/3.8.6-GCCcore-10.2.0

pip3 install -r requirements.txt --user -q

#X: 1
#L: 1
#M: 2
#S: 3
#N: 4

B="2"

python3 val.py --data ../dataset/data.yaml --batch-size $B --workers 12 --weights ../models/Baseline-m.pt --project ../tests --name Baseline-m --exist-ok --task=test --save-txt --save-conf --img 4064 --single-cls
python3 val.py --data ../dataset/data.yaml --batch-size $B --workers 12 --weights ../models/Mixed-m.pt --project ../tests --name Mixed-m --exist-ok --task=test --save-txt --save-conf --img 4064 --single-cls
