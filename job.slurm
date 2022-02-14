#!/bin/sh
#SBATCH --partition=GPUQ
#SBATCH --account=ie-idi
#SBATCH --time=36:00:00
#SBATCH --nodes=1
#SBATCH --ntasks-per-node=1
#SBATCH --gres=gpu:V100:2
#SBATCH --cpus-per-task=24
#SBATCH --mem=16G
#SBATCH --job-name="YOLO_SHEEP"
#SBATCH --output=yolo.out
#SBATCH --mail-user=bjornar.osttveit@ntnu.no
#SBATCH --mail-type=ALL


cd ${SLURM_SUBMIT_DIR}/yolov5

module purge
module load Python/3.8.6-GCCcore-10.2.0

pip3 install -r requirements.txt --user -q
pip3 install wandb --user -q

python3 -m torch.distributed.launch --nproc_per_node=2 train.py --project Masters --single-cls --batch 1 --device 0,1 --workers 16 --epochs 1000 --data ../dataset/data.yaml --weights yolov5l6.pt --img 4056
