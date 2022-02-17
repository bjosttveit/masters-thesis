import os
import wandb

api = wandb.Api()

runs = [
    {"artifact": "run_mxm1lu4o_model:v0", "model": "Baseline-n"},
    {"artifact": "run_14iz4jlm_model:v0", "model": "Baseline-s"},
    {"artifact": "run_j5iww4jw_model:v0", "model": "Baseline-m"},
    {"artifact": "run_n14agv9x_model:v0", "model": "Baseline-l"},
    {"artifact": "run_n969b61b_model:v0", "model": "Baseline-x"},
]

if not os.path.exists('models'):
    os.makedirs('models')

for run in runs:
    print(f"Downloading {run['model']}...")
    api.artifact(f"Masters/{run['artifact']}").download(root="./models")
    os.rename("models/best.pt", f"models/{run['model']}.pt")
