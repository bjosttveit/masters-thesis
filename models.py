import os
import wandb

api = wandb.Api()

runs = [
    {"artifact": "run_3bdx3ghg_model:v0", "model": "Baseline-m"},
    {"artifact": "run_3fh447fq_model:v0", "model": "Mixed-m"},
]

if not os.path.exists('models'):
    os.makedirs('models')

for run in runs:
    print(f"Downloading {run['model']}...")
    api.artifact(f"Masters/{run['artifact']}").download(root="./models")
    os.rename("models/best.pt", f"models/{run['model']}.pt")
