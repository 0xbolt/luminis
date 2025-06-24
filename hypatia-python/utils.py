import os
import pathlib
import pandas as pd

UNITY_STREAMING_ASSETS_DIR = pathlib.Path('../hypatia-unity/Assets/StreamingAssets')

def read_csv(filename: str):
    """Read a csv file inside Unity Streaming Assets folder"""
    return pd.read_csv(UNITY_STREAMING_ASSETS_DIR / filename)

def save_csv(df: pd.DataFrame, filename: str, overwrite: bool = False):
    """Save a csv file inside Unity Streaming Assets folder"""
    path = UNITY_STREAMING_ASSETS_DIR / filename
    if os.path.exists(path) and not overwrite:
        raise Exception(f"{path} already exists")
    df.to_csv(UNITY_STREAMING_ASSETS_DIR / filename)
