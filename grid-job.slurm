#!/bin/sh
#SBATCH --partition=GPUQ
#SBATCH --account=ie-idi
#SBATCH --time=12:00:00
#SBATCH --nodes=1
#SBATCH --ntasks-per-node=1
#SBATCH --gres=gpu:V100:2
#SBATCH --cpus-per-task=24
#SBATCH --mem=16G
#SBATCH --job-name="sheep"
#SBATCH --output=yolo.out
#SBATCH --mail-user=bjornar.osttveit@ntnu.no
#SBATCH --mail-type=NONE


cd ${SLURM_SUBMIT_DIR}/yolov5

module purge
module load Python/3.8.6-GCCcore-10.2.0

pip3 install -r requirements.txt --user -q
pip3 install wandb --user -q

#X: 36 12h
#L: 48 8h
#M: 80 5h
#S: 128 3h
#N: 256 2h

python3 -m torch.distributed.launch --nproc_per_node=2 train.py --project Masters --name Grid-X --single-cls --batch 36 --device 0,1 --workers 16 --epochs 300 --data ../grid_dataset/data.yaml --weights yolov5x.pt --img 640
