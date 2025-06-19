import pathlib
import pandas as pd

UNITY_STREAMING_ASSETS_DIR = pathlib.Path('../hypatia-unity/Assets/StreamingAssets')

"""Read a csv file inside Unity Streaming Assets folder"""
def read_csv(filename):
    return pd.read_csv(UNITY_STREAMING_ASSETS_DIR / filename)
