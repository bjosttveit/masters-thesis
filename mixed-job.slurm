#!/bin/sh
#SBATCH --partition=GPUQ
#SBATCH --account=ie-idi
#SBATCH --time=16:00:00
#SBATCH --nodes=1
#SBATCH --ntasks-per-node=1
#SBATCH --gres=gpu:V10032:4
#SBATCH --cpus-per-task=32
#SBATCH --mem=64G
#SBATCH --job-name="sheep"
#SBATCH --output=yolo-mixed.out
#SBATCH --mail-user=bjornar.osttveit@ntnu.no
#SBATCH --mail-type=NONE


cd ${SLURM_SUBMIT_DIR}/yolov5

module purge
module load Python/3.8.6-GCCcore-10.2.0

pip3 install -r requirements.txt --user -q
pip3 install wandb --user -q

#X: 36 13h
#L: 74 8h
#M: 106 5h
#S: 162 3h
#N: 312 2h

python3 -m torch.distributed.launch --nproc_per_node=4 train.py --project Masters --name Mixed-M --single-cls --batch 300 --device 0,1,2,3 --workers 24 --epochs 1000 --data ../grid_dataset/mixed-data.yaml --weights yolov5m.pt --img 640
