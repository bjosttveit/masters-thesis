mkdir -p tests

cd yolov5

python3 val.py --data ../dataset/data.yaml --batch-size 8 --weights ../models/Baseline-${1}.pt --project ../tests --name Baseline-${1} --exist-ok --task=test --save-txt --save-conf --img 4056 --single-cls

